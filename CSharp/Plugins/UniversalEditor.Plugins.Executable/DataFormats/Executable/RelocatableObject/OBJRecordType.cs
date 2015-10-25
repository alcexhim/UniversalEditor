using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.RelocatableObject
{
	public enum OBJRecordType : byte
	{
		Comment = 0x88,
		
		ExternalReference = 0x8C,
		
		ExternalSymbols0x90 = 0x90,
		ExternalSymbols0x91 = 0x91,
		
		Segment0x98 = 0x98,
		Segment0x99 = 0x99,
		
		SegmentGroup = 0x9A,
		
		Relocation0x9C = 0x9C,
		Relocation0x9D = 0x9D,
		
		CodeDataText0xA0 = 0xA0,
		CodeDataText0xA1 = 0xA1,
		
		CommonDataUninitialized = 0xB0,
		
		CommonDataInitialized0xC2 = 0xC2,
		CommonDataInitialized0xC3 = 0xC3,

		ModuleEnd0x8A = 0x8A,
		ModuleEnd0x8B = 0x8B
	}
}
