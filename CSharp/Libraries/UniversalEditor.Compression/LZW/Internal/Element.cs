using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.Compression.LZW.Internal
{
	internal struct Element
	{
		public int prefix;
		public int suffix;
		public Element(int _prefix, int _suffix)
		{
			this.prefix = _prefix;
			this.suffix = _suffix;
		}
	}
}
