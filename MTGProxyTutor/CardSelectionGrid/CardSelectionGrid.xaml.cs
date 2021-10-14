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
                if (CardSelectionDataGrid.ItemsSource is IEnumerable<CardWrapper> cards)
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
                                CardImage image = await VM.GetCardImageByUrlAsync(ci);
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
