using MTGProxyTutor.Contracts.Models.App;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Linq;
using MTGProxyTutor.ViewModels;
using System.Collections.ObjectModel;

namespace MTGProxyTutor
{
    public partial class CardSelectionGrid : UserControl
    {
        public readonly CardSelectionGridViewModel VM;
        public event Action SelectedCardsChanged;
        private const int _apiCallWaitingTimeMs = 100;

        public CardSelectionGrid()
        {
            VM = ViewModelLocator.GetViewModel<CardSelectionGridViewModel>();
            DataContext = VM;
            InitializeComponent();
        }

        public async Task ExportToPDF(string filePath)
        {
            try
            {
                if (CardSelectionDataGrid.ItemsSource is IEnumerable<CardWrapperViewModel> cards)
                {
                    IEnumerable<CardWrapperViewModel> selectedCards = cards.Where(x => x.IsSelected);

                    if (selectedCards.Any())
                    {
                        foreach (CardWrapperViewModel c in selectedCards)
                        {
                            if (c.Images != null)
                                c.Images.Clear();
                            else
                                c.Images = new List<CardImage>();

                            foreach (string ci in c.Card.SelectedPrint.ImageUrls)
                            {
                                await Task.Delay(_apiCallWaitingTimeMs);
                                CardImage image = await CardDataFetcherLocator.Instance.GetCardImageByUrlAsync(ci);
                                c.Images.Add(image);
                            }
                        }

                        if (selectedCards.Any())
                        {
                            VM.CreatePDF(selectedCards, filePath);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        private void CardSelectionCheckChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            SelectedCardsChanged?.Invoke();
        }
    }
}
