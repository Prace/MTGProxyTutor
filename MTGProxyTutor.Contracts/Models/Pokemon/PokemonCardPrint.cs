using MTGProxyTutor.Contracts.Models.App;

namespace MTGProxyTutor.Contracts.Models.Pokemon
{
    public class PokemonCardPrint : CardPrint
    {
        public string SpecificCardName { get; set; }
        public bool IsHolo { get; set; }

        public string CompleteInfo
        {
            get
            {
                var info = $"{SpecificCardName} ({SetName}) ";
                return IsHolo ? $"{info} Holo" : info;
            }
        }
    }
}
