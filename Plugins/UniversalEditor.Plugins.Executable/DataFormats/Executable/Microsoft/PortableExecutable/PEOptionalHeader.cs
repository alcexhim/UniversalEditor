//
//  PEOptionalHeader.cs - describes the optional header for a Portable Executable file
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
	/// Describes the optional header for a Portable Executable file.
	/// </summary>
	public struct PEOptionalHeader
	{
		public bool enabled;
		public ushort magic;
		public ushort unknown1;
		public uint unknown2;
		public uint unknown3;
		public uint unknown4;
		public uint entryPointAddr;
		public uint unknown5;
		public uint unknown6;
		public uint imageBase;
		public uint sectionAlignment;
		public uint fileAlignment;
		public uint unknown7;
		public uint unknown8;
		public ushort majorSubsystemVersion; // 4 = NT 4 or later
		public ushort unknown9;
		public uint unknown10;
		public uint imageSize;
		public uint headerSize;
		public uint unknown11;
		public ushort subsystem;
		public ushort unknown12;
		public uint unknown13;
		public uint unknown14;
		public uint unknown15;
		public uint unknown16;
		public uint unknown17;
		public uint rvaCount;

		public uint[] rvas1;
		public uint[] rvas2;
	}
}
