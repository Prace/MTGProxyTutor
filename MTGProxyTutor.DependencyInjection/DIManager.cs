using AutoMapper;
using MtgApiManager.Lib.Service;
using MTGProxyTutor.BusinessLogic.Http;
using MTGProxyTutor.BusinessLogic.Loggers;
using MTGProxyTutor.BusinessLogic.Parsers;
using MTGProxyTutor.BusinessLogic.PDF;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.MtGIO.Logic;
using MTGProxyTutor.Scryfall.Logic;
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
            container.RegisterType<IPDFManager, PDFManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<MultiLineStringParser>();
            container.RegisterType<IWebApiConsumer, WebApiConsumer>();
            container.RegisterType<IMtgServiceProvider, MtgServiceProvider>(); // MtGIO
            container.RegisterType<ICardDataFetcher, ScryfallFetcher>();

            return container;
        }
    }
}
