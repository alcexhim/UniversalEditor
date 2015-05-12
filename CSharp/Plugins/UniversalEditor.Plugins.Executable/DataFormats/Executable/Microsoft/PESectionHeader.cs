using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	public struct PESectionHeader
	{
		public const uint HeaderSize = 40;

		public string name;
		public uint virtualSize;
		public uint virtualAddress;
		public uint rawDataSize;
		public uint rawDataPtr;
		public uint unknown1;
		public uint unknown2;
		public uint unknown3;
		public PESectionCharacteristics characteristics;
	}
}
