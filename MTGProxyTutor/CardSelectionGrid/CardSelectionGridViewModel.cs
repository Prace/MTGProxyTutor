using MTGProxyTutor.ViewModel;
using MTGProxyTutor.Contracts.Models.App;
using System.Collections.ObjectModel;

namespace MTGProxyTutor
{
    public class CardSelectionGridViewModel : BaseViewModel
    {
        private ObservableCollection<CardWrapper> cards = new ObservableCollection<CardWrapper>();
        public ObservableCollection<CardWrapper> Cards
        {
            get
            {
                return this.cards;
            }
            set
            {
                this.cards = value;
                this.OnPropertyChanged(nameof(cards));
            }
        }
    }
}
