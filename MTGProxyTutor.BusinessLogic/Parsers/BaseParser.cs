using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MTGProxyTutor.BusinessLogic.Parsers
{
	public abstract class BaseParser
	{
		protected Regex lineWithQtyParseRegex = new Regex(@"\s*(\d+)\s*[xX]?\s+(.+)");

		protected ParsedCard ParseSingleLine(string line)
		{
			var lineWithQtyMatch = lineWithQtyParseRegex.Match(line);

			if (lineWithQtyMatch.Success)
				return new ParsedCard(Int32.Parse(lineWithQtyMatch.Groups[1].Value), lineWithQtyMatch.Groups[2].Value);
			else
				return new ParsedCard(1, line.Trim());
		}
	}
}
