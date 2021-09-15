using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.DependencyInjection;
using System;
using Unity;

namespace MTGProxyTutor.ConsoleApp
{
    class Program
	{
		static void Main(string[] args)
		{
			// Sandbox Console App - Simple Example

			var fetcher = DIManager.Container.Resolve<ICardDataFetcher>();
			var card = fetcher.GetCardByNameAsync("command tower").Result;
			Console.ReadKey();
		}
	}
}
