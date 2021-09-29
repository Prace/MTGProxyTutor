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
                var cards = CardSelectionDataGrid.ItemsSource as IEnumerable<CardWrapper>;

                if (cards != null)
                {
                    var selectedCards = cards.Where(x => x.IsSelected);

                    if (selectedCards.Any())
                    {
                        foreach (CardWrapper c in cards.Where(x => x.IsSelected))
                        {
                            c.Images.Clear();

                            foreach (var ci in c.Card.SelectedPrint.ImageUrls)
                            {
                                await Task.Delay(_apiCallWaitingTimeMs);
                                var image = await _cardDataFetcher.GetCardImageByUrlAsync(ci);
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
