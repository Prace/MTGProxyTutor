using MTGProxyTutor.Contracts.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MTGProxyTutor.BusinessLogic.Http
{
	public class WebApiConsumer : IWebApiConsumer
	{
		private static HttpClient _client;
		private ILogger _logger;

		public WebApiConsumer(HttpClient client, ILogger logger)
		{
			_client = client;
			_logger = logger;
		}

		public async Task<T> GetAsync<T>(string url)
		{
			try
			{
				var response = await _client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				var body = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<T>(body);
			}
			catch (Exception ex)
			{
				_logger.Error($"GET Error: {ex.Message}");
				throw;
			}
		}

		public T Get<T>(string url)
		{
			return GetAsync<T>(url).Result;
		}
	}
}
