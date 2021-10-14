using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MTGProxyTutor.ViewModels
{
    public class CardSelectionGridViewModel : BaseViewModel
    {
        private readonly ICardDataFetcher _cardDataFetcher;
        private readonly IPDFManager _pdfManager;

        public CardSelectionGridViewModel(ICardDataFetcher cardDataFetcher, IPDFManager pdfManager)
        {
            _cardDataFetcher = cardDataFetcher;
            _pdfManager = pdfManager;
        }

        private ObservableCollection<CardWrapper> cards = new ObservableCollection<CardWrapper>();
        public ObservableCollection<CardWrapper> Cards
        {
            get
            {
                return this.cards;
            }
            set
            {
                this.cards = value;
                this.OnPropertyChanged(nameof(Cards));
            }
        }

        public async Task<CardImage> GetCardImageByUrlAsync(string cardImageUrl)
        {
            return await _cardDataFetcher.GetCardImageByUrlAsync(cardImageUrl);
        }

        public void CreatePDF(IEnumerable<CardWrapper> cards, string filePath)
        {
            _pdfManager.CreatePDF(cards, filePath);
        }
    }
}
