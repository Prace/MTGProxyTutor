using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Threading.Tasks;

namespace MTGProxyTutor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICardDataFetcher _cardDataFetcher;
        private const int _apiCallWaitingTimeMs = 100;


        public MainWindowViewModel(ICardDataFetcher cardDataFetcher)
        {
            _cardDataFetcher = cardDataFetcher;
        }

        
        private bool parseCardsBtnEnabled = true;
        public bool ParseCardsBtnEnabled
        {
            get {  return parseCardsBtnEnabled; }
            set
            {
                parseCardsBtnEnabled = value;
                OnPropertyChanged(nameof(ParseCardsBtnEnabled));
            }
        }

        private bool exportBtnEnabled = false;
        public bool ExportBtnEnabled
        {
            get { return exportBtnEnabled; }
            set
            {
                exportBtnEnabled = value;
                OnPropertyChanged(nameof(ExportBtnEnabled));
            }
        }

        public async Task<Card> GetCardByNameAsync(string cardName)
        {
            return await _cardDataFetcher.GetCardByNameAsync(cardName);
        }

        public async Task<CardWrapperViewModel> GetCard(ParsedCard parsedCard)
        {
            await Task.Delay(_apiCallWaitingTimeMs);
            var cardData = await GetCardByNameAsync(parsedCard.CardName);
            var cardWrapper = new CardWrapperViewModel(cardData, parsedCard.Quantity);
            return cardWrapper;
        }
    }
}
