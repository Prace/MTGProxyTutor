using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using Unity;
using System.Linq;

namespace MTGProxyTutor
{
    public partial class CardSelectionGrid : UserControl
    {
        public CardSelectionGridViewModel CardSelectionGridVM;
        private ICardDataFetcher _cardDataFetcher;
        private IPDFManager _pdfManager;
        private const int _apiCallWaitingTimeMs = 100;

        public CardSelectionGrid()
        {
            this.CardSelectionGridVM = new CardSelectionGridViewModel();
            this.DataContext = CardSelectionGridVM;
            _cardDataFetcher = DIManager.Container.Resolve<ICardDataFetcher>();
            _pdfManager = DIManager.Container.Resolve<IPDFManager>();
            InitializeComponent();
        }

        public async Task ExportToPDF(string filePath)
        {
            try
            {
                IEnumerable<CardWrapper> cards = CardSelectionDataGrid.ItemsSource as IEnumerable<CardWrapper>;

                if (cards != null)
                {
                    IEnumerable<CardWrapper> selectedCards = cards.Where(x => x.IsSelected);

                    if (selectedCards.Any())
                    {
                        foreach (CardWrapper c in selectedCards)
                        {
                            c.Images.Clear();

                            foreach (string ci in c.Card.SelectedPrint.ImageUrls)
                            {
                                await Task.Delay(_apiCallWaitingTimeMs);
                                CardImage image = await _cardDataFetcher.GetCardImageByUrlAsync(ci);
                                c.Images.Add(image);
                            }
                        }

                        _pdfManager.CreatePDF(cards, filePath);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
