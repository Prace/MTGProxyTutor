using Microsoft.Win32;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
					var cardWrapper = await GetCard(pc);
					AddOrUpdateCard(cardWrapper);
					updatedCards.Add(cardWrapper.Card.CardId);
				}
				catch
				{
					failedFetch.Add(pc);
				}
			}

			var cardsToRemove = CardSelection.VM.Cards.Where(c => !updatedCards.Contains(c.Card.CardId));

			foreach (var rc in cardsToRemove.ToList())
            {
				CardSelection.VM.RemoveCard(rc);
            }

			NotifyFailedFetchedCards(failedFetch);
			UpdateTotalInfo();
		}

		private async void ExportToPDF(object sender, RoutedEventArgs e)
		{
            try
            {
				_vm.ExportBtnEnabled = false;

				SaveFileDialog saveFileDialog = new SaveFileDialog
				{
					DefaultExt = ".pdf",
					Filter = "PDF documents (.pdf)|*.pdf"
				};
				saveFileDialog.ShowDialog();

				if (saveFileDialog.FileName != "")
					await CardSelection.ExportToPDF(saveFileDialog.FileName);

			}
            catch (Exception ex)
            {
				var message = $"An error has occurred exporting PDF file: {ex.Message}";
				MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
            finally
            {
				_vm.ExportBtnEnabled = true;
			}
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
			CardSelection.SelectedCardsChanged += UpdateTotalInfo;
			TCGSelection.SelectionChanged += UpdateCardFecthingStrategy;
		}
        private void UpdateCardFecthingStrategy(object sender, SelectionChangedEventArgs e)
        {
			CardDataFetcherLocator.CurrentGame = _vm.SelectedTCGType;
			ClearData();
		}

		private void AddOrUpdateCard(CardWrapperViewModel cardWrapper)
        {
			var match = CardSelection.VM.Cards.FirstOrDefault(c => cardWrapper.Card.CardId == c.Card.CardId);

			if (match != null)
			{
				match.Quantity = cardWrapper.Quantity;
			}
			else
			{
				CardSelection.VM.Cards.Add(cardWrapper);
			}
		}

		private async Task<Card> GetCardByNameAsync(string cardName)
		{
			return await CardDataFetcherLocator.Instance.GetCardByNameAsync(cardName);
		}

        private async Task<Card> GetCardBySetAndNumberAsync(string set, string number)
        {
            return await CardDataFetcherLocator.Instance.GetCardBySetAndNumber(set, number);
        }

        private async Task<CardWrapperViewModel> GetCard(ParsedCard parsedCard)
		{
			await Task.Delay(200);
			Card cardData;
			if(parsedCard.IsSetAndNumberFormat)
				cardData = await GetCardBySetAndNumberAsync(parsedCard.Set,	parsedCard.Number);
			else
                cardData = await GetCardByNameAsync(parsedCard.CardName);

            var cardWrapper = new CardWrapperViewModel(cardData, parsedCard.Quantity);
			return cardWrapper;
		}

		private void ToggleExportBtn()
		{
			_vm.ExportBtnEnabled = CardSelection.VM.Cards.Any(c => c.IsSelected);
		}

		private void UpdateTotalInfo()
		{
			if (CardSelection.VM.Cards != null)
            {
				_vm.TotalCardsToPrint = CardSelection.VM.Cards.Where(c => c.IsSelected).Sum(c => c.NumCardImages * c.Quantity);
			}
		}

		private void ClearData()
        {
			CardSelection.VM.Cards.Clear();
			ToggleExportBtn();
			UpdateTotalInfo();
		}
	}
}
