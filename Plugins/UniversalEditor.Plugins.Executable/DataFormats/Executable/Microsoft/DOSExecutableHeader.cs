//
//  DOSExecutableHeader.cs - describes the header of an MS-DOS executable
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	/// <summary>
	/// Describes the header of an MS-DOS executable.
	/// </summary>
	public class DOSExecutableHeader
	{
		private bool mvarEnabled = true;
		/// <summary>
		/// Determines whether to write the 16-bit DOS executable header in the Microsoft executable file.
		/// </summary>
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }
		
		
		private ushort mvarLastBlockLength = 0;
		/// <summary>
		/// The number of bytes in the last block of the program that are actually used. If this value is zero, that means the entire last block is used (i.e. the effective value
		/// is 512).
		/// </summary>
		public ushort LastBlockLength { get { return mvarLastBlockLength; } set { mvarLastBlockLength = value; } }
		
		private ushort mvarNumBlocksInEXE = 0;
		/// <summary>
		/// Number of blocks in the file that are part of the EXE file. If <see cref="LastBlockLength" /> is non-zero, only that much of the last block is used.
		/// </summary>
		public ushort NumBlocksInEXE { get { return mvarNumBlocksInEXE; } set { mvarNumBlocksInEXE = value; } }
		
		private ushort mvarNumRelocEntriesAfterHeader = 0;
		/// <summary>
		/// Number of relocation entries stored after the header. May be zero.
		/// </summary>
		public ushort NumRelocEntriesAfterHeader { get { return mvarNumRelocEntriesAfterHeader; } set { mvarNumRelocEntriesAfterHeader = value; } }
		
		private ushort mvarNumParagraphsInHeader = 0;
		/// <summary>
		/// Number of paragraphs in the header. The program's data begins just after the header, and this field can be used to calculate the appropriate file offset. The header
		/// includes the relocation entries. Note that some OSs and/or programs may fail if the header is not a multiple of 512 bytes.
		/// </summary>
		public ushort NumParagraphsInHeader { get { return mvarNumParagraphsInHeader; } set { mvarNumParagraphsInHeader = value; } }
		
		private ushort mvarNumParagraphsAdditionalMemory = 0;
		/// <summary>
		/// Number of paragraphs of additional memory that the program will need. This is the equivalent of the BSS size in a Unix program. The program can't be loaded if there
		/// isn't at least this much memory available to it.
		/// </summary>
		public ushort NumParagraphsAdditionalMemory { get { return mvarNumParagraphsAdditionalMemory; } set { mvarNumParagraphsAdditionalMemory = value; } }
		
		private ushort mvarNumMaxParagraphsAdditionalMemory = 0;
		/// <summary>
		/// Maximum number of paragraphs of additional memory. Normally, the OS reserves all the remaining conventional memory for your program, but you can limit it with this
		/// field.
		/// </summary>
		public ushort NumMaxParagraphsAdditionalMemory { get { return mvarNumMaxParagraphsAdditionalMemory; } set { mvarNumMaxParagraphsAdditionalMemory = value; } }
		
		private ushort mvarRelativeStackSegmentValue = 0;
		/// <summary>
		/// Relative value of the stack segment. This value is added to the segment the program was loaded at, and the result is used to initialize the SS register.
		/// </summary>
		public ushort RelativeStackSegmentValue { get { return mvarRelativeStackSegmentValue; } set { mvarRelativeStackSegmentValue = value; } }
		
		private ushort mvarInitialValueRegisterSP = 0;
		/// <summary>
		/// Initial value of the SP register.
		/// </summary>
		public ushort InitialValueRegisterSP { get { return mvarInitialValueRegisterSP; } set { mvarInitialValueRegisterSP = value; } }
		
		private ushort mvarWordChecksum = 0;
		/// <summary>
		/// Word checksum. If set properly, the 16-bit sum of all words in the file should be zero. Usually, this isn't filled in.
		/// </summary>
		public ushort WordChecksum { get { return mvarWordChecksum; } set { mvarWordChecksum = value; } }
		
		private ushort mvarInitialValueRegisterIP = 0;
		/// <summary>
		/// Initial value of the IP register.
		/// </summary>
		public ushort InitialValueRegisterIP { get { return mvarInitialValueRegisterIP; } set { mvarInitialValueRegisterIP = value; } }
		
		private ushort mvarInitialValueRegisterCS = 0;
		/// <summary>
		/// Initial value of the CS register, relative to the segment the program was loaded at.
		/// </summary>
		public ushort InitialValueRegisterCS { get { return mvarInitialValueRegisterCS; } set { mvarInitialValueRegisterCS = value; } }
		
		private ushort mvarFirstRelocationItemOffset = 0;
		/// <summary>
		/// Offset of the first relocation item in the file.
		/// </summary>
		public ushort FirstRelocationItemOffset { get { return mvarFirstRelocationItemOffset; } set { mvarFirstRelocationItemOffset = value; } }
		
		private ushort mvarOverlayNumber = 0;
		/// <summary>
		/// Overlay number. Normally zero, meaning that it's the main program.
		/// </summary>
		public ushort OverlayNumber { get { return mvarOverlayNumber; } set { mvarOverlayNumber = value; } }

		private uint mvarNewEXEHeaderOffset = 0;
		/// <summary>
		/// File address of new exe header
		/// </summary>
		public uint NewEXEHeaderOffset { get { return mvarNewEXEHeaderOffset; } set { mvarNewEXEHeaderOffset = value; } }
		
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
