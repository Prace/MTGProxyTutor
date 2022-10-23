using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace MTGProxyTutor.Contracts.Models.App
{
	public abstract class Card
	{
		public event Action OnSelectedPrintChanged;

		public string CardName { get; set; }
		public string CardId { get; set; }
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
				OnSelectedPrintChanged?.Invoke();
			}
		}
	}
}
