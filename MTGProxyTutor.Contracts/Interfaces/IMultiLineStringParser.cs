using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutor.Contracts.Interfaces
{
    public interface IMultiLineStringParser
    {
        IEnumerable<ParsedCard> Parse(string input, out List<string> failedParse);
    }
}