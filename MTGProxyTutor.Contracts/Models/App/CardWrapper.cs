using System.Collections.Generic;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class CardWrapper
	{
		public Card Card { get; set; }
		public int Quantity { get; set; }
		public List<CardImage> Images { get; set; } = new List<CardImage>();
	}
}
