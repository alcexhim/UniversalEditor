using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.WindowsImage
{
	public struct WIMDirectoryEntry
	{
		public ulong liLength;
		public uint dwAttributes;
		public int dwSecurityId;
		public ulong liSubdirOffset;
		public ulong liUnused1;
		public ulong liUnused2;
		public ulong liCreationTime;
		public ulong liLastAccessTime;
		public ulong liLastWriteTime;
		public byte[/*20*/] bHash;
		public uint dwReparseTag;
		public uint dwReparseReserved;
		public ulong liHardLink;
		public ushort wShortNameLength;
		public string FileName;
		public WIMStreamEntry[] wStreams;
	}
}
