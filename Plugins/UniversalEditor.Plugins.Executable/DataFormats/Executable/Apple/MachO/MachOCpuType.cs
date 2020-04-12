//
//  MachOCpuType.cs - indicates the CPU type for an Apple Mach-O executable
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

namespace UniversalEditor.DataFormats.Executable.Apple.MachO
{
	/// <summary>
	/// Indicates the CPU type for an Apple Mach-O executable.
	/// </summary>
	public enum MachOCpuType
	{
		Any = -1,
		VAX = 1,
		ROMP = 2,
		MC68020 = 3,
		NS32032 = 4,
		NS32332 = 5,
		NS32532 = 6,
		X86 = 7,
		MIPS = 8,
		MC68030 = 9,
		MC68040 = 10,
		HPPA = 11,
		ARM = 12,
		MC88000 = 13,
		SPARC = 14,
		I860 = 15,
		ALPHA = 16,
		POWERPC = 18,
		X86_64 = 0x01000007
	}
}
