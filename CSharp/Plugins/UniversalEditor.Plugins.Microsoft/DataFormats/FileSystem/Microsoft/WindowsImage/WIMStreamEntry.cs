using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public struct WIMStreamEntry
	{
		public ulong liLength;
		public ulong Unused1;
		public byte[/*20*/] bHash;
		public ushort wStreamNameLength;
		public string StreamName;
	}
}
