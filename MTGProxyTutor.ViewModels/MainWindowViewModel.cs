using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Threading.Tasks;

namespace MTGProxyTutor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICardDataFetcher _cardDataFetcher;

        public MainWindowViewModel(ICardDataFetcher cardDataFetcher)
        {
            _cardDataFetcher = cardDataFetcher;
        }

        public async Task<Card> GetCardByNameAsync(string cardName)
        {
            return await _cardDataFetcher.GetCardByNameAsync(cardName);
        }
    }
}
