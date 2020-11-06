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
	public partial class Form1 : Form
	{
		private readonly ICardDataFetcher _cardDataFetcher;
		private readonly MultiLineStringParser _parser;
		private readonly ILogger _logger;
		private List<CardWrapper> _cards;

		public Form1()
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
						// Alert User 
					}

					List<ParsedCard> failedCardFetches = new List<ParsedCard>();

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
							failedCardFetches.Add(pc);
						}
					}

					if (failedCardFetches.Any())
					{
						// Alert user
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
			try
			{
				this.exportToPDFBtn.Enabled = false;

				foreach (var c in _cards)
				{
					c.Image = await _cardDataFetcher.GetCardImageByUrlAsync(c.Card.ImageUrl);
				}

				PDFHelper.SavePDF(_cards, "prova.pdf");
			}
			catch (Exception ex)
			{
				_logger.Info($"Get cards images error: {ex.Message}");
			}

			this.exportToPDFBtn.Enabled = true;
		}
	}
}
