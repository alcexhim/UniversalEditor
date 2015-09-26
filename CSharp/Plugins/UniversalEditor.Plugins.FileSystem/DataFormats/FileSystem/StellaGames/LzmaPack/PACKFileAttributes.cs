using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.StellaGames.LzmaPack
{
	[Flags()]
	public enum PACKFileAttributes : ushort
	{
		None = 0,
		Compressed = 1
	}
}
