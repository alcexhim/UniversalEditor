
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public class XCOFF32AuxilaryHeader : XCOFFAuxilaryHeaderBase
	{
		private uint mvarTextSize = 0;
		public uint TextSize { get { return mvarTextSize; } set { mvarTextSize = value; } }
		
		private uint mvarInitializedDataSize = 0;
		public uint InitializedDataSize { get { return mvarInitializedDataSize; } set { mvarInitializedDataSize = value; } }
		
		private uint mvarUninitializedDataSize = 0;
		public uint UninitializedDataSize { get { return mvarUninitializedDataSize; } set { mvarUninitializedDataSize = value; } }
		
		private uint mvarEntryPointDescriptor = 0;
		public uint EntryPointDescriptor { get { return mvarEntryPointDescriptor; } set { mvarEntryPointDescriptor = value; } }
		
		private uint mvarBaseAddressText = 0;
		public uint BaseAddressText { get { return mvarBaseAddressText; } set { mvarBaseAddressText = value; } }
		
		private uint mvarBaseAddressData = 0;
		public uint BaseAddressData { get { return mvarBaseAddressData; } set { mvarBaseAddressData = value; } }
		
		private uint mvarTOCAnchorAddress = 0;
		public uint TOCAnchorAddress { get { return mvarTOCAnchorAddress; } set { mvarTOCAnchorAddress = value; } }
		
		private ulong mvarReserved2 = 0;
		public ulong Reserved2 { get { return mvarReserved2; } set { mvarReserved2 = value; } }
	}
}
