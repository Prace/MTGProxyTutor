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

			var fetcher = DIManager.Container.Resolve<ICardDataFetcher>();
			var card = fetcher.GetCardByNameAsync("delver of secrets").Result;
			Task.Delay(200).Wait();
			//CardImage cardImg = fetcher.GetCardImageByUrlAsync(card.ImageUrls.First()).Result;
			Console.ReadKey();
		}
	}
}
