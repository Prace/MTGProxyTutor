using MTGProxyTutor.Contracts.Models.App;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface ICardDataFetcher
	{
		Card GetCardByName(string name);
		CardImage GetCardImageByUrl(string url);
	}
}
