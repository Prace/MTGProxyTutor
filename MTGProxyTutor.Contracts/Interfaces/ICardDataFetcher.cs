using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		Task<Card> GetCardByNameAsync(string cardName);
		Task<CardImage> GetCardImageByUrlAsync(string url);
	}
}
