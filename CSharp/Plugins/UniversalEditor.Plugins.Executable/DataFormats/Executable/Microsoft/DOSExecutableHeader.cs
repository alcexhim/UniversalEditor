using System;

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	/// <summary>
	/// Description of DOSExecutableHeader.
	/// </summary>
	public class DOSExecutableHeader
	{
		private bool mvarEnabled = true;
		/// <summary>
		/// Determines whether to write the 16-bit DOS executable header in the Microsoft executable file.
		/// </summary>
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }
		
		
		private ushort mvarLastBlockLength = 0;
		public ushort LastBlockLength { get { return mvarLastBlockLength; } set { mvarLastBlockLength = value; } }
		
		private ushort mvarNumBlocksInEXE = 0;
		public ushort NumBlocksInEXE { get { return mvarNumBlocksInEXE; } set { mvarNumBlocksInEXE = value; } }
		
		private ushort mvarNumRelocEntriesAfterHeader = 0;
		public ushort NumRelocEntriesAfterHeader { get { return mvarNumRelocEntriesAfterHeader; } set { mvarNumRelocEntriesAfterHeader = value; } }
		
		private ushort mvarNumParagraphsInHeader = 0;
		public ushort NumParagraphsInHeader { get { return mvarNumParagraphsInHeader; } set { mvarNumParagraphsInHeader = value; } }
		
		private ushort mvarNumParagraphsAdditionalMemory = 0;
		public ushort NumParagraphsAdditionalMemory { get { return mvarNumParagraphsAdditionalMemory; } set { mvarNumParagraphsAdditionalMemory = value; } }
		
		private ushort mvarNumMaxParagraphsAdditionalMemory = 0;
		public ushort NumMaxParagraphsAdditionalMemory { get { return mvarNumMaxParagraphsAdditionalMemory; } set { mvarNumMaxParagraphsAdditionalMemory = value; } }
		
		private ushort mvarRelativeStackSegmentValue = 0;
		public ushort RelativeStackSegmentValue { get { return mvarRelativeStackSegmentValue; } set { mvarRelativeStackSegmentValue = value; } }
		
		private ushort mvarInitialValueRegisterSP = 0;
		public ushort InitialValueRegisterSP { get { return mvarInitialValueRegisterSP; } set { mvarInitialValueRegisterSP = value; } }
		
		private ushort mvarWordChecksum = 0;
		public ushort WordChecksum { get { return mvarWordChecksum; } set { mvarWordChecksum = value; } }
		
		private ushort mvarInitialValueRegisterIP = 0;
		public ushort InitialValueRegisterIP { get { return mvarInitialValueRegisterIP; } set { mvarInitialValueRegisterIP = value; } }
		
		private ushort mvarInitialValueRegisterCS = 0;
		public ushort InitialValueRegisterCS { get { return mvarInitialValueRegisterCS; } set { mvarInitialValueRegisterCS = value; } }
		
		private ushort mvarFirstRelocationItemOffset = 0;
		public ushort FirstRelocationItemOffset { get { return mvarFirstRelocationItemOffset; } set { mvarFirstRelocationItemOffset = value; } }
		
		private ushort mvarOverlayNumber = 0;
		public ushort OverlayNumber { get { return mvarOverlayNumber; } set { mvarOverlayNumber = value; } }
		
		public long EXEDataStart
		{
			get
			{
				return (mvarNumParagraphsInHeader * 16L);
			}
		}
		public long ExtraDataStart
		{
			get
			{
				long tmp = mvarNumBlocksInEXE * 512L;
				if (mvarLastBlockLength > 0)
				{
					tmp -= (512 - mvarLastBlockLength);
				}
				return tmp;
			}
		}
	}
}
