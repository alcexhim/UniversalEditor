//
//  MachOSectionFlags.cs - indicates the type of data stored in a section of an Apple Mach-O executable
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

using System;

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Indicates the type of data stored in a section of an Apple Mach-O executable.
	/// </summary>
	[Flags()]
	public enum MachOSectionFlags
	{
		/// <summary>
		/// zero-filled on demand
		/// </summary>
		ZeroFill = 0x01,
		/// <summary>
		/// section has only literal C strings
		/// </summary>
		LiteralCStrings = 0x02,
		/// <summary>
		/// section has only 4-byte literals
		/// </summary>
		Literal4Bytes = 0x03,
		/// <summary>
		/// section has only 8-byte literals
		/// </summary>
		Literal8Bytes = 0x04,
		/// <summary>
		/// section has only pointers to literals
		/// </summary>
		LiteralPointers = 0x05
	}
}
