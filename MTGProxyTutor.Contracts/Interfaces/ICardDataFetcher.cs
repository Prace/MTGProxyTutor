using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		Task<Card> GetCardByNameAsync(string cardName);
        Task<Card> GetCardBySetAndNumber(string set, string number);
        Task<CardImage> GetCardImageByUrlAsync(string url);
    }
}
