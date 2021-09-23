using MTGProxyTutor.Contracts.Models.App;
using System.Collections.Generic;

namespace MTGProxyTutor.Contracts.Interfaces
{
    public interface IPDFManager
    {
        void CreatePDF(IEnumerable<CardWrapper> cardWrappers, string filename);
    }
}