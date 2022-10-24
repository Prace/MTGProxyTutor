namespace MTGProxyTutor.Contracts.Models.App
{
	public class ParsedCard
	{
		public ParsedCard(int quantity, string cardName)
		{
			Quantity = quantity;
			CardName = cardName;
		}

        public ParsedCard(int quantity, string set, string number)
        {
            Quantity = quantity;
			Set = set;
			Number = number;
			IsSetAndNumberFormat = true;
        }

        public int Quantity { get; set; }
		public string CardName { get; set; }
		public string Set { get; set; }
		public string Number { get; set; }
        public bool IsSetAndNumberFormat { get; internal set; }
    }
}
