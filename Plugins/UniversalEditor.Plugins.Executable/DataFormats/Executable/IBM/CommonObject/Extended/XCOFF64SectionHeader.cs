//
//  XCOFF64SectionHeader.cs - describes a header of a section in a 64-bit Extended Common Object File Format executable
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
	/// Describes a header of a section in a 64-bit Extended Common Object File Format executable.
	/// </summary>
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
