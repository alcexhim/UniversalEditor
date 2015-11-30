using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// This is a different order than FF_ROMAN, FF_SWISS, etc. of Windows!
	/// </summary>
	public enum FontFamily : byte
	{
		Modern = 0x01,
		Roman = 0x02,
		Swiss = 0x03,
		Tech = 0x03,
		Nil = 0x03,
		Script = 0x04,
		Decor = 0x05
	}
}
