using MTGProxyTutor.BusinessLogic;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace MTGProxyTutor.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var cardFetcher = DIManager.Container.Resolve<ICardDataFetcher>();
			var card = cardFetcher.GetCardByName("austere com");
			Console.ReadKey();
		}
	}
}
