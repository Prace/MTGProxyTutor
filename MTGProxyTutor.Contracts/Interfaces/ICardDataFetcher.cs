using MTGProxyTutor.Contracts.Models;
using MTGProxyTutor.Contracts.Models.Scryfall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		ScryfallCard GetCardByName(string name);
	}
}
