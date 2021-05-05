//
//  XCOFF32SectionHeader.cs - describes a header of a section in a 32-bit Extended Common Object File Format executable
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

namespace UniversalEditor.DataFormats.Executable.IBM.CommonObject.Extended
{
	/// <summary>
	/// Describes a header of a section in a 32-bit Extended Common Object File Format executable.
	/// </summary>
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
