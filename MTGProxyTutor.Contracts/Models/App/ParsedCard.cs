using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class ParsedCard
	{
		public ParsedCard(int quantity, string cardName)
		{
			Quantity = quantity;
			CardName = cardName;
		}

		public int Quantity { get; set; }
		public string CardName { get; set; }
	}
}
