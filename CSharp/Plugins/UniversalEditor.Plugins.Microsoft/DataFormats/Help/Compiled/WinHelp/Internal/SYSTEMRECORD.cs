using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct SYSTEMRECORD
	{
		public int SavePos;
		public int Remaining;
		public SystemRecordType RecordType;
		public ushort DataSize;
		public byte[] Data;
	}
}
