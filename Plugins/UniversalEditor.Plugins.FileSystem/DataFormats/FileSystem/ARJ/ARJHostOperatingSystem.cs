using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ARJ
{
	public enum ARJHostOperatingSystem : byte
	{
		DOS = 0,
		PrimOS = 1,
		Unix = 2,
		Amiga = 3,
		MacOS = 4,
		OS2 = 5,
		AppleGS = 6,
		AtariST = 7,
		NeXT = 8,
		VaxVMS = 9,
		Windows = 11
	}
}
