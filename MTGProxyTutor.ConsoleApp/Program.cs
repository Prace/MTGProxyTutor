using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.DependencyInjection;
using System;
using Unity;
using System.Linq;
using System.Threading.Tasks;
using MTGProxyTutor.Contracts.Models.App;

namespace MTGProxyTutor.ConsoleApp
{
    class Program
	{
		static void Main(string[] args)
		{
			// Sandbox Console App - Simple Example

			var diManager = new DIManager();
			var fetcher = diManager.Container.Resolve<ICardDataFetcher>("Magic");
			var card = fetcher.GetCardByNameAsync("eternal witness").Result;
			Task.Delay(200).Wait();
			CardImage cardImg = fetcher.GetCardImageByUrlAsync(card.SelectedPrint.ImageUrls.First()).Result;
			Console.ReadKey();
		}
	}
}
