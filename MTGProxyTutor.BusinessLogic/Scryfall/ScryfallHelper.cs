using AutoMapper;
using MTGProxyTutor.Contracts.Exceptions;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Scryfall;
using System;
using System.Net;

namespace MTGProxyTutor.BusinessLogic.Scryfall
{
    public class ScryfallFetcher : ICardDataFetcher
    {
        private const string BASE_URL = "https://api.scryfall.com";
        private const string CARD_BY_NAME_URL = BASE_URL + "/cards/named?fuzzy={0}";
        private IWebApiConsumer _webApiConsumer;
        private ILogger _logger;
        private IMapper _mapper;

        public ScryfallFetcher(IWebApiConsumer webApiConsumer, ILogger logger, IMapper mapper)
        {
            _webApiConsumer = webApiConsumer;
            _logger = logger;
            _mapper = mapper;
        }

        public Card GetCardByName(string name)
        {
            string correctedName = sanitize(name);
            var cardDetails = _webApiConsumer.Get<ScryfallCard>(string.Format(CARD_BY_NAME_URL, correctedName));
            if (cardDetails != null)
                return _mapper.Map<Card>(cardDetails);
            return null;
        }

        public CardImage GetCardImageByUrl(string url)
        {
            var binary = _webApiConsumer.GetBinary(url);
            if (binary != null)
                return new CardImage(binary);
            return null;
        }

        private string sanitize(string name)
        {
            return name.Replace(" ", "+");
        }
    }
}
