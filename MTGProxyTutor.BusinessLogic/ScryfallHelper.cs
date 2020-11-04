using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Scryfall;

namespace MTGProxyTutor.BusinessLogic
{
    public class ScryfallFetcher : ICardDataFetcher
    {
        private const string BASE_URL = "https://api.scryfall.com";
        private const string CARD_BY_NAME_URL = BASE_URL + "/cards/named?fuzzy={0}";
        private IWebApiConsumer _webApiConsumer;
        private ILogger _logger;

        public ScryfallFetcher(IWebApiConsumer webApiConsumer, ILogger logger)
        {
            _webApiConsumer = webApiConsumer;
            _logger = logger;
        }

        public Card GetCardByName(string name)
        {
            var correctedName = name.Replace(" ", "+");
            var cardDetails = _webApiConsumer.Get<ScryfallCard>(string.Format(CARD_BY_NAME_URL, correctedName));
            if (cardDetails != null)
            {
                var cardImage = _webApiConsumer.GetBinary(cardDetails.Image_uris.Normal);
                return new Card { Image = cardImage };
            }
            return null;
        }
    }
}
