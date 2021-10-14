﻿using Microsoft.Win32;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.ViewModels;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MTGProxyTutor
{
    public partial class MainWindow : Window
	{
        private readonly MainWindowViewModel _vm;
        private List<ParsedCard> _parsedCards;
		private const int _apiCallWaitingTimeMs = 100;

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
			EmptyCardSelectionGrid();
			await FillCardGrid();

			_vm.ParseCardsBtnEnabled = true;
		}

		private void EmptyCardSelectionGrid()
		{
			this.CardSelection.VM.Cards.Clear();
		}

		private async Task FillCardGrid()
		{
			var failedFetch = new List<ParsedCard>();

			foreach (var pc in _parsedCards)
			{
				try
				{
					var cardWrapper = await GetCard(pc);
					CardSelection.VM.Cards.Add(cardWrapper);
				}
				catch
				{
					failedFetch.Add(pc);
				}
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

		private async Task<CardWrapper> GetCard(ParsedCard parsedCard)
        {
			await Task.Delay(_apiCallWaitingTimeMs);
			var cardData = await _vm.GetCardByNameAsync(parsedCard.CardName);
			var cardWrapper = new CardWrapper
			{
				Card = cardData,
				Quantity = parsedCard.Quantity
			};
			return cardWrapper;
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
	}
}
