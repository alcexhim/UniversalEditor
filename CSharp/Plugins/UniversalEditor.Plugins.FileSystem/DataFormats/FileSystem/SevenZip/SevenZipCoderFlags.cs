using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.SevenZip
{
	[Flags()]
	public enum SevenZipCoderFlags : byte
	{
		IsComplex = 0x01,
		HasAttributes = 0x02,
		Reserved = 0x04,
		AlternativeMethods = 0x08
	}
}
