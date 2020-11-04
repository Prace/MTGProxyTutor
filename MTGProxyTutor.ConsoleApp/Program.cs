using MTGProxyTutor.BusinessLogic.Parsers;
using MTGProxyTutor.BusinessLogic.PDF;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using System.Collections.Generic;
using Unity;

namespace MTGProxyTutor.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var cardFetcher = DIManager.Container.Resolve<ICardDataFetcher>();

			var fileparser = new FileParser();
			var cards = fileparser.Parse(@"..\listaCarte.txt");
			var cardList = new List<CardWrapper>();

			foreach(var cc in cards)
			{
				var card = cardFetcher.GetCardByName(cc.CardName);
				cardList.Add(new CardWrapper(card, cc.Quantity));
			}

			PDFHelper.SavePDF(cardList, "prova.pdf");

			var a = 1;
		}
	}
}
