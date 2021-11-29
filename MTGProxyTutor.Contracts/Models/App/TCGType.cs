using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.App
{
    public enum TCGType
    {
        [Description("Magic the Gathering")]
        MAGIC,
        [Description("Pokémon TCG")]
        POKEMON
    }
}
