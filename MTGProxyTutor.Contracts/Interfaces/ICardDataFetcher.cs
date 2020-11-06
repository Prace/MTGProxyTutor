using MTGProxyTutor.Contracts.Models.App;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		Task<Card> GetCardByNameAsync(string name);
		Task<CardImage> GetCardImageByUrlAsync(string url);
	}
}
