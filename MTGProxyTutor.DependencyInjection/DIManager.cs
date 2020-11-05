using AutoMapper;
using MTGProxyTutor.BusinessLogic.Http;
using MTGProxyTutor.BusinessLogic.Loggers;
using MTGProxyTutor.BusinessLogic.Scryfall;
using MTGProxyTutor.Contracts.Interfaces;
using System.Net.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace MTGProxyTutor.DependencyInjection
{
    public class DIManager
    {
        private static IUnityContainer _container = null;

        private DIManager()
        { }

        public static IUnityContainer Container
        {
            get
            {
                if (_container is null)
                    _container = InitializeContainer();
                return _container;
            }
        }

        private static IUnityContainer InitializeContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogger, SimpleLogger>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IMapper>(Mapper.Configuration, new ContainerControlledLifetimeManager());
            container.RegisterType<IWebApiConsumer, WebApiConsumer>(
                new InjectionConstructor(container.Resolve<HttpClient>(), container.Resolve<ILogger>()));
            container.RegisterType<ICardDataFetcher, ScryfallFetcher>(
                new InjectionConstructor(container.Resolve<IWebApiConsumer>(), container.Resolve<ILogger>(), container.Resolve<IMapper>()));

            return container;
        }
    }
}
