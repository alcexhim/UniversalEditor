using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	public enum MachOMagic : uint
	{
		MachOBigEndian = 0xFEEDFACE,
		MachOLittleEndian = 0xCEFAEDFE,
		FatBinaryBigEndian = 0xCAFEBABE,
		FatBinaryLittleEndian = 0xBEBAFECA,
		MachO64BigEndian = 0xFEEDFACF,
		MachO64LittleEndian = 0xCFFAEDFE
	}
}
