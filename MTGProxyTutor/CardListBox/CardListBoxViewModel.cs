using MTGProxyTutor.ViewModel;
using MTGProxyTutor.Contracts.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor
{
    public class CardListBoxViewModel : BaseViewModel
    {
        public string pastedCardList = "";
        public string PastedCardList
        {
            get { return pastedCardList; }
            set
            {
                pastedCardList = value;
                this.OnPropertyChanged(nameof(pastedCardList));
            }
        }
    }
}
