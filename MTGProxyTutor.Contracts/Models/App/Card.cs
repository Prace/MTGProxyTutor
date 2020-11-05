using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class Card
	{
		public string CardName { get; set; }
		public string ManaCost { get; set; }
		public string Type { get; set; }
		public string Text { get; set; }
		public string Rarity { get; set; }
		public string Power { get; set; }
		public string Toughness { get; set; }
		public string ImageUrl { get; set; }
	}
}
