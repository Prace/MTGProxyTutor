using MTGProxyTutor.Contracts.Models.App;
using System;
using System.Text.RegularExpressions;

namespace MTGProxyTutor.BusinessLogic.Parsers
{
    public abstract class BaseParser
	{
		protected Regex lineWithQtyParseRegex = new Regex(@"\s*(\d+)\s*[xX]?\s+(.+)");

		protected ParsedCard ParseSingleLine(string line)
		{
			var lineWithQtyMatch = lineWithQtyParseRegex.Match(line);
            int qty = 1;
            string cardData;
            ParsedCard parsedCard = null;
            if (lineWithQtyMatch.Success)
            {
                qty = Int32.Parse(lineWithQtyMatch.Groups[1].Value);
                cardData = lineWithQtyMatch.Groups[2].Value;
            }
            else if (!string.IsNullOrWhiteSpace(line))
                cardData = line.Trim();
            else
                return null;

            if (cardData.StartsWith("#"))
            {
                //format : "#set:number"
                try
                {
                    var searchElements = cardData.Split(new char[] { '#', ':' });
                    var set = searchElements[1];
                    var number = searchElements[2];
                    parsedCard = new ParsedCard(qty, set, number);
                }
                catch { };
            }
            else
                parsedCard = new ParsedCard(qty, cardData);
            return parsedCard;
        }
	}
}
