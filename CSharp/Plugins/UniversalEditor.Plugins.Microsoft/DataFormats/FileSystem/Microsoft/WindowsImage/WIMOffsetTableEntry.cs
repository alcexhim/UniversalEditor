using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public class WIMOffsetTableEntry : WIMResourceHeaderDiskShort
	{
		public ushort usPartNumber;
		public uint dwRefCount;
		public byte[/*20*/] bHash;
	}
}
