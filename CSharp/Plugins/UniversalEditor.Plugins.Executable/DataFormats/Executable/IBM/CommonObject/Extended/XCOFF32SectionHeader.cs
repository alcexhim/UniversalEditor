
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public class XCOFF32SectionHeader : XCOFFSectionHeader
	{
		private uint mvarPhysicalAddress = 0;
		public uint PhysicalAddress { get { return mvarPhysicalAddress; } set { mvarPhysicalAddress = value; } }
		
		private uint mvarVirtualAddress = 0;
		public uint VirtualAddress { get { return mvarVirtualAddress; } set { mvarVirtualAddress = value; } }
		
		private uint mvarSectionSize = 0;
		public uint SectionSize { get { return mvarSectionSize; } set { mvarSectionSize = value; } }
		
		private uint mvarOffsetRawData = 0;
		public uint OffsetRawData { get { return mvarOffsetRawData; } set { mvarOffsetRawData = value; } }
		
		private uint mvarOffsetRelocationEntries = 0;
		public uint OffsetRelocationEntries { get { return mvarOffsetRelocationEntries; } set { mvarOffsetRelocationEntries = value; } }
		
		private uint mvarOffsetLineNumberEntries = 0;
		public uint OffsetLineNumberEntries { get { return mvarOffsetLineNumberEntries; } set { mvarOffsetLineNumberEntries = value; } }
		
		private ushort mvarCountRelocationEntries = 0;
		public ushort CountRelocationEntries { get { return mvarCountRelocationEntries; } set { mvarCountRelocationEntries = value; } }
		
		private ushort mvarCountLineNumberEntries = 0;
		public ushort CountLineNumberEntries { get { return mvarCountLineNumberEntries; } set { mvarCountLineNumberEntries = value; } }
		
		private XCOFFSectionType32 mvarSectionType = default(XCOFFSectionType32);
		public XCOFFSectionType32 SectionType { get { return mvarSectionType; } set { mvarSectionType = value; } }
	}
}
