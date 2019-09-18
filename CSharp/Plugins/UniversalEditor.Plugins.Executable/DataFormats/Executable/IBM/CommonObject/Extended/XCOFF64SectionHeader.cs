
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public class XCOFF64SectionHeader : XCOFFSectionHeader
	{	
		private ulong mvarPhysicalAddress = 0;
		public ulong PhysicalAddress { get { return mvarPhysicalAddress; } set { mvarPhysicalAddress = value; } }
		
		private ulong mvarVirtualAddress = 0;
		public ulong VirtualAddress { get { return mvarVirtualAddress; } set { mvarVirtualAddress = value; } }
		
		private ulong mvarSectionSize = 0;
		public ulong SectionSize { get { return mvarSectionSize; } set { mvarSectionSize = value; } }
		
		private ulong mvarOffsetRawData = 0;
		public ulong OffsetRawData { get { return mvarOffsetRawData; } set { mvarOffsetRawData = value; } }
		
		private ulong mvarOffsetRelocationEntries = 0;
		public ulong OffsetRelocationEntries { get { return mvarOffsetRelocationEntries; } set { mvarOffsetRelocationEntries = value; } }
		
		private ulong mvarOffsetLineNumberEntries = 0;
		public ulong OffsetLineNumberEntries { get { return mvarOffsetLineNumberEntries; } set { mvarOffsetLineNumberEntries = value; } }
		
		private uint mvarCountRelocationEntries = 0;
		public uint CountRelocationEntries { get { return mvarCountRelocationEntries; } set { mvarCountRelocationEntries = value; } }
		
		private uint mvarCountLineNumberEntries = 0;
		public uint CountLineNumberEntries { get { return mvarCountLineNumberEntries; } set { mvarCountLineNumberEntries = value; } }
		
		private XCOFFSectionType64 mvarSectionType = default(XCOFFSectionType64);
		public XCOFFSectionType64 SectionType { get { return mvarSectionType; } set { mvarSectionType = value; } }
	}
}
