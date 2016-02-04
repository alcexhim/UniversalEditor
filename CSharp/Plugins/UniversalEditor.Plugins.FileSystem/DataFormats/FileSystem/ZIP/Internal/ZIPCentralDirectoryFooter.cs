using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZIP.Internal
{
	internal struct ZIPCentralDirectoryFooter
	{
		public uint unknown1;
		public ushort unknown2;
		public ushort unknown3;
		public uint centralDirectoryLength;
		public uint centralDirectoryOffset;
		public ushort unknown4;
	}
}
