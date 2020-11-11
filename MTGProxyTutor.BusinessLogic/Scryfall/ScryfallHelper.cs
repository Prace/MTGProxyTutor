using AutoMapper;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.Contracts.Models.Scryfall;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        public async Task<Card> GetCardByNameAsync(string name)
        {
            string correctedName = sanitize(name);
            var cardDetails = await _webApiConsumer.GetAsync<ScryfallCard>(string.Format(CARD_BY_NAME_URL, correctedName));
            await Task.Delay(100); // Wait at least 100ms between searches on Scryfall APIs
            if (cardDetails != null)
                return _mapper.Map<Card>(cardDetails);
            return null;
        }

        public async Task<CardImage> GetCardImageByUrlAsync(string url)
        {
            var binary = await _webApiConsumer.GetBinaryAsync(url);
            await Task.Delay(100); // Wait at least 100ms between searches on Scryfall APIs
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
    }
}
