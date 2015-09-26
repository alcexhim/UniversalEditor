using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Nintendo.Optical
{
	[Flags()]
	public enum NintendoOpticalDiscSystemType
	{
		Unknown = 0,
		GameCube = 1,
		Wii = 2,
	}
}
