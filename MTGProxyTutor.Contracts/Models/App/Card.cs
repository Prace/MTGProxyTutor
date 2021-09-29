using System.Collections.Generic;
using System.Linq;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class Card
	{
		public string CardName { get; set; }
		public string ManaCost { get; set; }
		public string Type { get; set; }
		public string Text { get; set; }
		public string Power { get; set; }
		public string Toughness { get; set; }
		public List<CardPrint> Printings { get; set; }

		private CardPrint selectedPrint;
		public CardPrint SelectedPrint
		{
			get
			{
				return selectedPrint ?? Printings.FirstOrDefault();
			}
			set
			{
				selectedPrint = value;
			}
		}
	}
}
