﻿using AutoMapper;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Pokemon;
using MTGProxyTutor.DataGathering.PokemonTCG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTGProxyTutor.DataGathering.PokemonTCG.Logic
{
    public class PokemonTCGFetcher : ICardDataFetcher
    {
        private const string SEARCH_BY_NAME_URL = "https://api.pokemontcg.io/v2/cards?q=name:{0}";
        private IWebApiConsumer _webApiConsumer;
        private ILogger _logger;
        private IMapper _mapper;

        public PokemonTCGFetcher(IWebApiConsumer webApiConsumer, ILogger logger, IMapper mapper)
        {
            _webApiConsumer = webApiConsumer;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Card> GetCardByNameAsync(string cardName)
        {
            PokemonTCGSearchResult cardsDetails = await getPokemonTCGCardByName(cardName);

            if (cardsDetails != null)
            {
                return mapResultDataToCard(cardsDetails);
            }

            return null;
        }

        public Task<Card> GetCardBySetAndNumber(string set, string number) =>
            throw new NotImplementedException();

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
            string result = Regex.Replace(trimmed, @"\s+", "*");
            return result;
        }

        private Task<PokemonTCGSearchResult> getPokemonTCGCardByName(string cardName)
        {
            string correctedName = sanitize(cardName);
            string finalUrl = string.Format(SEARCH_BY_NAME_URL, correctedName);
            return _webApiConsumer.GetAsync<PokemonTCGSearchResult>(finalUrl);
        }

        private Card mapResultDataToCard(PokemonTCGSearchResult resultData)
        {
            var card = new PokemonCard();
            var firstPrint = resultData.data.First();
            card.CardName = firstPrint.name;
            card.CardId = firstPrint.id;
            card.Printings = resultData.data.Select(c =>
            {
                var printing = new PokemonCardPrint();
                printing.SetName = c.set.name;
                printing.SpecificCardName = c.name;
                printing.Rarity = c.rarity;
                printing.ImageUrls = new List<string> { c.images.large };
                return printing as CardPrint;
            }).ToList();

            return card;
        }
    }
}
