using Microsoft.Win32;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;

namespace MTGProxyTutor
{
    public partial class MainWindow : Window
	{
		private ICardDataFetcher _cardDataFetcher;
		private List<ParsedCard> _parsedCards;
		private const int _apiCallWaitingTimeMs = 100;

		public MainWindow()
		{
			_cardDataFetcher = DIManager.Container.Resolve<ICardDataFetcher>();
			InitializeComponent();
		}

		public async void ParseCards(object sender, RoutedEventArgs e)
		{
			this.ParseCardsBtn.IsEnabled = false;

			_parsedCards = this.CardList.GetParsedCards().ToList();
			EmptyCardSelectionGrid();
			await FillCardGrid();

			this.ParseCardsBtn.IsEnabled = true;
		}

		private void EmptyCardSelectionGrid()
		{
			this.CardSelection.CardSelectionGridVM.Cards.Clear();
		}

		private async Task FillCardGrid()
		{
			var failedFetch = new List<ParsedCard>();

			foreach (var pc in _parsedCards)
			{
				try
				{
					var cardWrapper = await GetCard(pc);
					this.CardSelection.CardSelectionGridVM.Cards.Add(cardWrapper);
				}
				catch (Exception ex)
				{
					failedFetch.Add(pc);
				}
			}

			NotifyFailedFetchedCards(failedFetch);
		}

		private async void ExportToPDF(object sender, RoutedEventArgs e)
		{
			this.ExportToPDFBtn.IsEnabled = false;

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = ".pdf";
			saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf";
			saveFileDialog.ShowDialog();

			if(saveFileDialog.FileName != "")
				await this.CardSelection.ExportToPDF(saveFileDialog.FileName);

			this.ExportToPDFBtn.IsEnabled = true;
		}

		private async Task<CardWrapper> GetCard(ParsedCard parsedCard)
        {
			await Task.Delay(_apiCallWaitingTimeMs);
			var cardData = await _cardDataFetcher.GetCardByNameAsync(parsedCard.CardName);
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
    }
}
