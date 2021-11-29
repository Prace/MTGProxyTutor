using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using MTGProxyTutor.DependencyInjection;
using Unity;

namespace MTGProxyTutor
{
    internal static class CardDataFetcherLocator
    {
        private static readonly DIManager _DIManager = new DIManager();
        public static TCGType CurrentGame = TCGType.MAGIC;

        public static ICardDataFetcher Instance
        {
            get
            {
                try
                {
                    ICardDataFetcher currentFetcher;

                    switch (CurrentGame)
                    {
                        case TCGType.MAGIC:
                            currentFetcher = _DIManager.Container.Resolve<ICardDataFetcher>("Magic");
                            break;
                        case TCGType.POKEMON:
                            currentFetcher = _DIManager.Container.Resolve<ICardDataFetcher>("Pokemon");
                            break;
                        default:
                            currentFetcher = _DIManager.Container.Resolve<ICardDataFetcher>("Magic");
                            break;
                    }

                    return currentFetcher;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
