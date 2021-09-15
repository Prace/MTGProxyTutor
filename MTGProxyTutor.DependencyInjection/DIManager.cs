using AutoMapper;
using MTGProxyTutor.BusinessLogic.Http;
using MTGProxyTutor.BusinessLogic.Loggers;
using MTGProxyTutor.BusinessLogic.Parsers;
using MTGProxyTutor.BusinessLogic.Scryfall;
using MTGProxyTutor.Contracts.Interfaces;
using System.Net.Http;
using Unity;
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
            container.RegisterType<MultiLineStringParser>();
            container.RegisterType<IWebApiConsumer, WebApiConsumer>();
            container.RegisterType<ICardDataFetcher, ScryfallFetcher>();

            return container;
        }
    }
}
