using AutoMapper;
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
        private readonly IMapper _mapper;

        public CardSelectionGridViewModel(ICardDataFetcher cardDataFetcher, IPDFManager pdfManager, IMapper mapper)
        {
            _cardDataFetcher = cardDataFetcher;
            _pdfManager = pdfManager;
            _mapper = mapper;
        }

        private ObservableCollection<CardWrapperViewModel> cards = new ObservableCollection<CardWrapperViewModel>();
        public ObservableCollection<CardWrapperViewModel> Cards
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

        public void CreatePDF(IEnumerable<CardWrapperViewModel> cards, string filePath)
        {
            _pdfManager.CreatePDF(cards.Select(c => _mapper.Map<CardWrapper>(c)), filePath);
        }

        public void RemoveCard(string cardName)
        {
            var toRemove = this.Cards.Where(c => c.Card.CardName == cardName);
            foreach (var c in toRemove)
            {
                this.Cards.Remove(c);
            }
        }

        public void RemoveCard(CardWrapperViewModel card)
        {
            this.Cards.Remove(card);
        }
    }
}
