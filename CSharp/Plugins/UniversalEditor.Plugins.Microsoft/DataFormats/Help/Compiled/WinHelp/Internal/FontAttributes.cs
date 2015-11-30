using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	[Flags()]
	public enum FontAttributes : byte
	{
		None = 0x00,
		Bold = 0x01,
		Italic = 0x02,
		Underline = 0x04,
		Strikeout = 0x08,
		DoubleUnderline = 0x10,
		SmallCaps = 0x20
	}
}
