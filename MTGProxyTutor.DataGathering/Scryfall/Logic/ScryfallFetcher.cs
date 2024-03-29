﻿using AutoMapper;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;
using MTGProxyTutor.Contracts.Models.Magic;
using MTGProxyTutor.DataGathering.Scryfall.Models;
using System.Dynamic;

namespace MTGProxyTutor.DataGathering.Scryfall.Logic
{
    public class ScryfallFetcher : ICardDataFetcher
    {
        private const string BASE_URL = "https://api.scryfall.com";
        private const string CARD_BY_NAME_URL = BASE_URL + "/cards/named?fuzzy={0}";
        private const string CARD_BY_SET_AND_NUMBER_URL = BASE_URL + "/cards/{0}/{1}";
        private const int CALL_WAIT_TIME_MS = 100;
        private IWebApiConsumer _webApiConsumer;
        private ILogger _logger;
        private IMapper _mapper;

        public ScryfallFetcher(IWebApiConsumer webApiConsumer, ILogger logger, IMapper mapper)
        {
            _webApiConsumer = webApiConsumer;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Card> GetCardBySetAndNumber(string set, string number)
        {
            ScryfallCard cardDetails = await getScryfallCardBySetAndNumber(set, number);
            if (cardDetails == null)
                return null;

            var card = _mapper.Map<MagicCard>(cardDetails);
            await Task.Delay(CALL_WAIT_TIME_MS);
            var cardPrint = _mapper.Map<MagicCardPrint>(cardDetails) as CardPrint;
            card.Printings = new System.Collections.Generic.List<CardPrint>() { cardPrint };
            return card;
        }

        public async Task<Card> GetCardByNameAsync(string cardName)
        {
            ScryfallCard cardDetails = await getScryfallCardByName(cardName);

            if (cardDetails != null)
            {
                var card = _mapper.Map<MagicCard>(cardDetails);
                await Task.Delay(CALL_WAIT_TIME_MS);
                var printings = await _webApiConsumer.GetAsync<ScryfallCardPrintings>(cardDetails.Prints_search_uri);
                card.Printings = printings.Data.Select(print => _mapper.Map<MagicCardPrint>(print) as CardPrint).ToList();
                return card;
            }

            return null;
        }

        public async Task<CardImage> GetCardImageByUrlAsync(string url)
        {
            if (url == null)
                return null;
            
            var binary = await _webApiConsumer.GetBinaryAsync(url);
            if (binary != null)
                return new CardImage(binary);
            return null;
        }

        private string sanitize(string name)
        {
            var trimmed = name.Trim();
            string result = Regex.Replace(trimmed, @"\s+", "+");
            return result;
        }

        private Task<ScryfallCard> getScryfallCardByName(string cardName)
        {
            string correctedName = sanitize(cardName);
            return _webApiConsumer.GetAsync<ScryfallCard>(string.Format(CARD_BY_NAME_URL, correctedName));
        }

        private Task<ScryfallCard> getScryfallCardBySetAndNumber(string set, string number)
        {
            string correctedSet = sanitize(set);
            string correctedNumber = sanitize(number);
            return _webApiConsumer.GetAsync<ScryfallCard>(string.Format(CARD_BY_SET_AND_NUMBER_URL, correctedSet, correctedNumber));
        }
    }
}
