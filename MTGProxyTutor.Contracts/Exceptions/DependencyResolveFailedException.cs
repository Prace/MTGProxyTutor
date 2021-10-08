using System;

namespace MTGProxyTutor.Contracts.Exceptions
{
    public class DependencyResolveFailedException : Exception
	{
		public DependencyResolveFailedException(string message)
			: base(message)
		{

		}

		public DependencyResolveFailedException(string message, Exception innerException)
			: base(message, innerException)
		{

		}
	}
}
