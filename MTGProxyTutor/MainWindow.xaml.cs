using Microsoft.Win32;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;

namespace MTGProxyTutor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private ICardDataFetcher _cardDataFetcher;
		private List<ParsedCard> _parsedCards;
		private const int _apiCallWaitingTimeMs = 100;


		public MainWindow()
		{
			InitializeComponent();
			_cardDataFetcher = DIManager.Container.Resolve<ICardDataFetcher>();
		}

		public async void ParseCards(object sender, RoutedEventArgs e)
		{
			_parsedCards = this.CardList.GetParsedCards().ToList();
			EmptyCardSelectionGrid();
			await FillCardGrid();
		}

		private void EmptyCardSelectionGrid()
		{
			this.CardSelection.CardSelectionGridVM.Cards.Clear();
		}

		private async Task FillCardGrid()
		{
			foreach (var pc in _parsedCards)
			{
				try
				{
					await Task.Delay(_apiCallWaitingTimeMs);
					var cardData = await _cardDataFetcher.GetCardByNameAsync(pc.CardName);
					var cardWrapper = new CardWrapper
					{
						Card = cardData,
						Quantity = pc.Quantity
					};

					this.CardSelection.CardSelectionGridVM.Cards.Add(cardWrapper);
				}
				catch (Exception ex)
				{
				}
				finally
				{
				}
			}
		}

		private void ExportToPDF(object sender, RoutedEventArgs e)
		{
			this.ExportToPDFBtn.IsEnabled = false;

			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.DefaultExt = ".pdf";
			saveFileDialog.Filter = "PDF documents (.pdf)|*.pdf";
			saveFileDialog.ShowDialog();

			if(saveFileDialog.FileName != "")
				this.CardSelection.ExportToPDF(saveFileDialog.FileName);

			this.ExportToPDFBtn.IsEnabled = true;
		}
	}
}
