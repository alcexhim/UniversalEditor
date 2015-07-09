using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public class WIMResourceHeaderDisk
	{
		public ulong ullSize;
		public WIMResourceHeaderDiskFlags flags;
		public ulong liOffset;
	}
}
