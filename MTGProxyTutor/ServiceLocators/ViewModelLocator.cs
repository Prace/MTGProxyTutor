using MTGProxyTutor.Contracts.Exceptions;
using MTGProxyTutor.DependencyInjection;
using MTGProxyTutor.ViewModels;
using Unity;

namespace MTGProxyTutor
{
    internal static class ViewModelLocator
    {
        private static readonly DIManager _DIManager = new DIManager();

        public static T GetViewModel<T>() where T : BaseViewModel
        {
            try
            {
                var vmInstance = _DIManager.Container.Resolve<T>();
                if (vmInstance == null)
                    throw new DependencyResolveFailedException($"Could not resolve deependency for ViewModel type {typeof(T).Name}");
                return vmInstance;
            }
            catch
            {
                throw;
            }
        }
    }
}
