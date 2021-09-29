using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.App
{
    public class CardPrint
    {
        public string SetName {  get; set; }
        public List<string> ImageUrls { get; set; }
    }
}
