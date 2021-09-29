using MTGProxyTutor.ViewModel;
using MTGProxyTutor.Contracts.Models.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MTGProxyTutor.BusinessLogic.Parsers;

namespace MTGProxyTutor
{
    public partial class CardListBox : UserControl
    {
        private MultiLineStringParser _parser;
        public CardListBoxViewModel CardListBoxVM;

        public CardListBox()
        {
            _parser = new MultiLineStringParser();
            CardListBoxVM = new CardListBoxViewModel();
            DataContext = CardListBoxVM;
            InitializeComponent();
        }

        public IEnumerable<ParsedCard> GetParsedCards()
        {
            var parsedCards = _parser.Parse(CardListBoxVM.PastedCardList, out List<string> failed);
            if (failed.Any())
            {
                var failedParseMessage = $"Could not parse the following card(s):\n\n{string.Join("\n", failed)}";
                MessageBox.Show(failedParseMessage, "Failed Cards", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return parsedCards;
        }
    }
}
