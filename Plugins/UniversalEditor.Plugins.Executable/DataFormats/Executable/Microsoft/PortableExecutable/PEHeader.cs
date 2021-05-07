//
//  PEHeader.cs - describes the header of a Portable Executable file
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

namespace UniversalEditor.DataFormats.Executable.Microsoft.PortableExecutable
{
	/// <summary>
	/// Describes the header of a Portable Executable file.
	/// </summary>
	public struct PEHeader
	{
		public bool enabled;
		public string signature;
		public PEMachineType machine;
		public short sectionCount;
		public short timestamp;
		public short symbolTableOffset;
		public short symbolCount;
		public short unknown4;
		public short unknown5;
		public short unknown6;
		public short sizeOfOptionalHeader; // relative offset to sectiontable
		public PECharacteristics characteristics;
	}
}
