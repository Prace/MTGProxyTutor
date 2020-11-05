using MTGProxyTutor.Contracts.Interfaces;

namespace MTGProxyTutor.BusinessLogic.Loggers
{
	public class FakeLogger : ILogger
	{
		public void Debug(string text)
		{
			// Does nothing
		}

		public void Error(string text)
		{
			// Does nothing
		}

		public void Info(string text)
		{
			// Does nothing
		}

		public void Warning(string text)
		{
			// Does nothing
		}
	}
}
