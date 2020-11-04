namespace MTGProxyTutor.Contracts.Models.App
{
	public class CardWrapper
	{
		public CardWrapper(Card card, int quantity)
		{
			Card = card;
			Quantity = quantity;
		}

		public Card Card { get; set; }
		public int Quantity { get; set; }
	}
}
