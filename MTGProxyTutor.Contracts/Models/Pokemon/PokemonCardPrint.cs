using MTGProxyTutor.Contracts.Models.App;

namespace MTGProxyTutor.Contracts.Models.Pokemon
{
    public class PokemonCardPrint : CardPrint
    {
        public string SpecificCardName { get; set; }

        public override string CompleteInfo
        {
            get
            {
                return $"{SpecificCardName} ({SetName}) ";
            }
        }
    }
}
