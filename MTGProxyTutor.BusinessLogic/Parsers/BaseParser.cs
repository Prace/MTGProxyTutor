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
		protected Regex lineParseRegex = new Regex(@"\s*(\d+)\s*[xX]?\s+(.+)");

		protected ParsedCard ParseSingleLine(string line)
		{
			var match = lineParseRegex.Match(line);
			if (match.Success)
				return new ParsedCard(Int32.Parse(match.Groups[1].Value), match.Groups[2].Value);
			else
				return null;
		}
	}
}
