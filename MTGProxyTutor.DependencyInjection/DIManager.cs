using AutoMapper;
using MTGProxyTutor.BusinessLogic.Http;
using MTGProxyTutor.BusinessLogic.Loggers;
using MTGProxyTutor.BusinessLogic.Parsers;
using MTGProxyTutor.BusinessLogic.PDF;
using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Scryfall.Logic;
using MTGProxyTutor.ViewModels;
using System.Net.Http;
using Unity;
using Unity.Lifetime;

namespace MTGProxyTutor.DependencyInjection
{
    public class DIManager
    {
        public readonly IUnityContainer Container;

        public DIManager()
        {
            Container = InitializeContainer();
        }

        private static IUnityContainer InitializeContainer()
        {
            var container = new UnityContainer();

            #region Common

            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager());
            container.RegisterType<ILogger, SimpleLogger>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<IMapper>(Mapper.Configuration, new ContainerControlledLifetimeManager());
            container.RegisterType<IPDFManager, PDFManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IMultiLineStringParser, MultiLineStringParser>();
            container.RegisterType<IWebApiConsumer, WebApiConsumer>();
            container.RegisterType<ICardDataFetcher, ScryfallFetcher>();

            #endregion

            #region ViewModels

            container.RegisterType<MainWindowViewModel>();
            container.RegisterType<CardListBoxViewModel>();
            container.RegisterType<CardSelectionGridViewModel>();

            #endregion

            return container;
        }
    }
}
