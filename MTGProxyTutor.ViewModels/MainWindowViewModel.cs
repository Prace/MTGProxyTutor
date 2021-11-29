using MTGProxyTutor.Contracts.Interfaces;
using MTGProxyTutor.Contracts.Models.App;
using System.Threading.Tasks;

namespace MTGProxyTutor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel()
        {
        }

        private TCGType selectedTCGType = TCGType.MAGIC;
        public TCGType SelectedTCGType
        {
            get {  return selectedTCGType; }
            set 
            {  
                selectedTCGType = value;
                OnPropertyChanged(nameof(SelectedTCGType));
            }
        }

        private bool parseCardsBtnEnabled = true;
        public bool ParseCardsBtnEnabled
        {
            get {  return parseCardsBtnEnabled; }
            set
            {
                parseCardsBtnEnabled = value;
                OnPropertyChanged(nameof(ParseCardsBtnEnabled));
            }
        }

        private bool exportBtnEnabled = false;
        public bool ExportBtnEnabled
        {
            get { return exportBtnEnabled; }
            set
            {
                exportBtnEnabled = value;
                OnPropertyChanged(nameof(ExportBtnEnabled));
            }
        }
    }
}
