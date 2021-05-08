using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia.FileSystem.Microsoft.Merlin
{
	[Flags()]
	public enum TEXTextureFlags : ushort
	{
		None = 0,
		HasTransparency = 1
	}
}
