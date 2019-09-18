
using System;

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	public class XCOFF64AuxilaryHeader
		: XCOFFAuxilaryHeaderBase
	{
		private ulong mvarTextSize = 0;
		public ulong TextSize { get { return mvarTextSize; } set { mvarTextSize = value; } }
		
		private ulong mvarInitializedDataSize = 0;
		public ulong InitializedDataSize { get { return mvarInitializedDataSize; } set { mvarInitializedDataSize = value; } }
		
		private ulong mvarUninitializedDataSize = 0;
		public ulong UninitializedDataSize { get { return mvarUninitializedDataSize; } set { mvarUninitializedDataSize = value; } }
		
		private ulong mvarEntryPointDescriptor = 0;
		public ulong EntryPointDescriptor { get { return mvarEntryPointDescriptor; } set { mvarEntryPointDescriptor = value; } }
		
		private ulong mvarBaseAddressText = 0;
		public ulong BaseAddressText { get { return mvarBaseAddressText; } set { mvarBaseAddressText = value; } }
		
		private ulong mvarBaseAddressData = 0;
		public ulong BaseAddressData { get { return mvarBaseAddressData; } set { mvarBaseAddressData = value; } }
		
		private ulong mvarTOCAnchorAddress = 0;
		public ulong TOCAnchorAddress { get { return mvarTOCAnchorAddress; } set { mvarTOCAnchorAddress = value; } }
		
		private uint mvarReserved2 = 0;
		public uint Reserved2 { get { return mvarReserved2; } set { mvarReserved2 = value; } }
		
		private byte[] mvarReserved3 = new byte[116];
		public byte[] Reserved3 { get { return mvarReserved3; } set { mvarReserved3 = value; } }
	}
}
