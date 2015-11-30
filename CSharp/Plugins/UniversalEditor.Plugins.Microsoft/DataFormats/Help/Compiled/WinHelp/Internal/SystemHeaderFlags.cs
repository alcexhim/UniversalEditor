using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	[Flags()]
	public enum SystemHeaderFlags
	{
		None = 0,
		CompressedLZ77TopicBlockSize4k = 4,
		CompressedLZ77TopicBlockSize2k = 8
	}
}
