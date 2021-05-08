//
//  OBJRecordType.cs - the type of record in an intermediate OBJ file
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

namespace UniversalEditor.DataFormats.Executable.RelocatableObject
{
	/// <summary>
	/// The type of record in an intermediate OBJ file.
	/// </summary>
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
