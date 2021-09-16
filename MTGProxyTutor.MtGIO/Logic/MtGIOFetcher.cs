using AutoMapper;
using MtgApiManager.Lib.Service;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Linq;
using System.Threading.Tasks;

namespace MTGProxyTutor.MtGIO.Logic
{
    public class MtGIOFetcher : ICardDataFetcher
    {
        private IMtgServiceProvider _serviceProvider;
        private IWebApiConsumer _webApiConsumer;
        private IMapper _mapper;
        private ILogger _logger;

        public MtGIOFetcher(IMtgServiceProvider serviceProvider, IWebApiConsumer webApiConsumer, IMapper mapper, ILogger logger)
        {
            _serviceProvider = serviceProvider;
            _webApiConsumer = webApiConsumer;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Card> GetCardByNameAsync(string name)
        {
            ICardService service = _serviceProvider.GetCardService();
            var cards = await service.Where(x => x.Name, name).AllAsync();
            var cardDetails = cards?.Value?.FirstOrDefault();
            if (cardDetails != null)
                return _mapper.Map<Card>(cardDetails);
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
    }
}
