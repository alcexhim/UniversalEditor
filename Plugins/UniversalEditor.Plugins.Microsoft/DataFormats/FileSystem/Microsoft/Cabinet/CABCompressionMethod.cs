using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet
{
	public enum CABCompressionMethod : ushort
	{
		None = 0,
		MSZIP = 1,
		LZX = 5379,
		Quantum
	}
}
