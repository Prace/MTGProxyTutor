using System.Collections.Generic;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class CardWrapper
	{
		public bool IsSelected { get; set; } = true;
		public Card Card { get; set; }
		public int Quantity { get; set; }
		public List<CardImage> Images { get; set; } = new List<CardImage>();
	}
}
