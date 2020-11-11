using MTGProxyTutor.BusinessLogic.Parsers;
using MTGProxyTutor.BusinessLogic.PDF;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Unity;

namespace MTGProxyTutor
{
	public partial class Main : Form
	{
		private readonly ICardDataFetcher _cardDataFetcher;
		private readonly MultiLineStringParser _parser;
		private readonly ILogger _logger;
		private List<CardWrapper> _cards;

		public Main()
		{
			InitializeComponent();
			_cardDataFetcher = DIManager.Container.Resolve<ICardDataFetcher>();
			_parser = DIManager.Container.Resolve<MultiLineStringParser>();
			_logger = DIManager.Container.Resolve<ILogger>();
			_cards = new List<CardWrapper>();

		}

		private async void searchCardsBtn_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(this.cardList.Text))
				{
					this.searchCardsBtn.Enabled = false;
					this.listView1.Items.Clear();
					this._cards.Clear();

					List<string> failed;
					var parsedCards = _parser.Parse(this.cardList.Text, out failed).ToList();

					if (failed.Any())
					{
						var failedParseAlertForm = new FailedAlert(failed
							.Select(f => new Tuple<string, Exception>(f, new Exception("Failed parse"))));
						failedParseAlertForm.ShowDialog();
					}

					List<Tuple<string, Exception>> failedCardFetches = new List<Tuple<string, Exception>>();

					this.progressBar1.Maximum = parsedCards.Count;
					this.progressBar1.Step = 1;
					this.progressBar1.Value = 0;

					foreach (var pc in parsedCards)
					{
						try
						{
							var cardData = await _cardDataFetcher.GetCardByNameAsync(pc.CardName);
							var cardWrapper = new CardWrapper
							{
								Card = cardData,
								Quantity = pc.Quantity
							};
							string[] row = { cardWrapper.Card.CardName, cardWrapper.Quantity.ToString(), cardWrapper.Card.Rarity };
							this.listView1.Items.Add(new ListViewItem(row));
							_cards.Add(cardWrapper);
						}
						catch (Exception ex)
						{
							failedCardFetches.Add(new Tuple<string, Exception>(pc.CardName, ex));
						}
						finally
						{
							this.progressBar1.PerformStep();
						}
					}


					if (failedCardFetches.Any())
					{
						var failedCardsAlertForm = new FailedAlert(failedCardFetches);
						failedCardsAlertForm.ShowDialog();
					}
				}
			}
			catch(Exception ex)
			{
				_logger.Info($"Get cards button click error: {ex.Message}");
				this._cards.Clear();
			}

			this.searchCardsBtn.Enabled = true;
		}

		private async void exportToPDFBtn_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();
			saveFileDialog1.Filter = "PDF|*.pdf";
			saveFileDialog1.Title = "Save PDF";
			saveFileDialog1.ShowDialog();

			if (saveFileDialog1.FileName != "")
			{
				try
				{
					this.exportToPDFBtn.Enabled = false;

					foreach (var c in _cards)
					{
						c.Image = await _cardDataFetcher.GetCardImageByUrlAsync(c.Card.ImageUrl);
					}

					PDFHelper.SavePDF(_cards, saveFileDialog1.FileName);
				}
				catch (Exception ex)
				{
					_logger.Info($"Get cards images error: {ex.Message}");
				}
			}

			this.exportToPDFBtn.Enabled = true;
		}
	}
}
