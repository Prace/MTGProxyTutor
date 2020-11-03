using AutoMapper;
using MTGProxyTutor.BusinessLogic;
using MTGProxyTutor.BusinessLogic.Http;
using MTGProxyTutor.BusinessLogic.Loggers;
using MTGProxyTutor.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            container.RegisterType<IWebApiConsumer, WebApiConsumer>(new InjectionConstructor(container.Resolve<HttpClient>(), container.Resolve<ILogger>()));
            container.RegisterType<ICardDataFetcher, ScryfallFetcher>(new InjectionConstructor(container.Resolve<IWebApiConsumer>(), container.Resolve<ILogger>()));

            return container;
        }
    }
}
