using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.SevenZip
{
	public struct SevenZipPackInfo
	{
		public ulong packPos;
		public ulong packStreamCount;
		public ulong[] packSizes;
		public uint[] packStreamDigests;
	}
}
