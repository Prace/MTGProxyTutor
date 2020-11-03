using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Interfaces
{
	public interface IWebApiConsumer
	{
		T Get<T>(string url);
		Task<T> GetAsync<T>(string url);
	}
}