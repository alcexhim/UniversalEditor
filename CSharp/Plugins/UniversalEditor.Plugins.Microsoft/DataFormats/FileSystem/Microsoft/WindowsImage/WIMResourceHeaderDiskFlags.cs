using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	[Flags()]
	public enum WIMResourceHeaderDiskFlags
	{
		None = 0x00,
		Free = 0x01,
		Metadata = 0x02,
		Compressed = 0x04,
		Spanned = 0x08
	}
}
