using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MTGProxyTutor.BusinessLogic.Parsers
{
	public class FileParser : BaseParser
	{
		public IEnumerable<ParsedCard> Parse(string filePath)
		{
			if (File.Exists(filePath))
			{
				var lines = File.ReadAllLines(filePath).ToList();
				return lines.Select(l => ParseSingleLine(l)).Where(p => p != null);
			}

			return new List<ParsedCard>();
		}
	}
}
