using Microsoft.Win32;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MTGProxyTutor
{
    public partial class MainWindow : Window
	{
        private readonly MainWindowViewModel _vm;
        private List<ParsedCard> _parsedCards;

		public MainWindow()
		{
			_vm = ViewModelLocator.GetViewModel<MainWindowViewModel>();
			DataContext = _vm;
			InitializeComponent();
			SubscribeToChildrenEvents();
		}

        public async void ParseCards(object sender, RoutedEventArgs e)
		{
			_vm.ParseCardsBtnEnabled = false;

			_parsedCards = CardList.GetParsedCards().ToList();
			await FillCardGrid();

			_vm.ParseCardsBtnEnabled = true;
		}

		private async Task FillCardGrid()
		{
			var failedFetch = new List<ParsedCard>();
			var updatedCards = new List<string>();

			foreach (var pc in _parsedCards)
			{
				try
				{
					var cardWrapper = await _vm.GetCard(pc);
					AddOrUpdateCard(cardWrapper);
					updatedCards.Add(cardWrapper.Card.CardName);
				}
				catch
				{
					failedFetch.Add(pc);
				}
			}

			var cardsToRemove = CardSelection.VM.Cards.Where(c => !updatedCards.Contains(c.Card.CardName));

			foreach (var rc in cardsToRemove.ToList())
            {
				CardSelection.VM.RemoveCard(rc);
            }

			NotifyFailedFetchedCards(failedFetch);
		}

		private async void ExportToPDF(object sender, RoutedEventArgs e)
		{
			_vm.ExportBtnEnabled = false;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                DefaultExt = ".pdf",
                Filter = "PDF documents (.pdf)|*.pdf"
            };
            saveFileDialog.ShowDialog();

			if(saveFileDialog.FileName != "")
				await CardSelection.ExportToPDF(saveFileDialog.FileName);

			_vm.ExportBtnEnabled = true;
		}

		private void NotifyFailedFetchedCards(List<ParsedCard> failedFetch)
        {
			if (failedFetch.Any())
            {
				var failedParseMessage = $"Could not fetch the following card(s):\n\n{string.Join("\n", failedFetch.Select(f => f.CardName))}";
				MessageBox.Show(failedParseMessage, "Failed Cards", MessageBoxButton.OK, MessageBoxImage.Information);
			}
		}

		private void SubscribeToChildrenEvents()
		{
			CardSelection.SelectedCardsChanged += ToggleExportBtn;
		}

        private void ToggleExportBtn()
        {
			_vm.ExportBtnEnabled = CardSelection.VM.Cards.Any(c => c.IsSelected);
		}

		private void AddOrUpdateCard(CardWrapperViewModel cardWrapper)
        {
			var match = CardSelection.VM.Cards.FirstOrDefault(c => cardWrapper.Card.CardName == c.Card.CardName);

			if (match != null)
			{
				match.Quantity = cardWrapper.Quantity;
			}
			else
			{
				CardSelection.VM.Cards.Add(cardWrapper);
			}
		}
	}
}
