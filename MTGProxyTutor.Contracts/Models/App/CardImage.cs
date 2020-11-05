using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTGProxyTutor.Contracts.Models.App
{
	public class CardImage
	{
		private byte[] _binary;

		public CardImage(byte[] binary)
		{
			_binary = binary;
		}

		public MemoryStream GetStream()
		{
			return new MemoryStream(_binary);
		}

		public byte[] GetBinary()
		{
			return _binary;
		}

		public string GetBase64()
		{
			return Convert.ToBase64String(_binary);
		}
	}
}
