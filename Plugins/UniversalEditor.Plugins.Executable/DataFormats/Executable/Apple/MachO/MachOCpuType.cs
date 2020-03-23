using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	public enum MachOCpuType
	{
		Any = -1,
		VAX = 1,
		ROMP = 2,
		MC68020 = 3,
		NS32032 = 4,
		NS32332 = 5,
		NS32532 = 6,
		X86 = 7,
		MIPS = 8,
		MC68030 = 9,
		MC68040 = 10,
		HPPA = 11,
		ARM = 12,
		MC88000 = 13,
		SPARC = 14,
		I860 = 15,
		ALPHA = 16,
		POWERPC = 18,
		X86_64 = 0x01000007
	}
}
