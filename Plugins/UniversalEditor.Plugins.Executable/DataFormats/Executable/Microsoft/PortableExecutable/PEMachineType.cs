//
//  PEMachineType.cs - indicates the required machine type for a Portable Executable file
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
	/// Indicates the required machine type for a Portable Executable file.
	/// </summary>
	public enum PEMachineType : ushort
	{
		/// <summary>
		/// The contents of this field are assumed to be applicable to any machine type
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// Matsushita AM33
		/// </summary>
		AM33 = 0x1d3,
		/// <summary>
		/// x64
		/// </summary>
		AMD64 = 0x8664,
		/// <summary>
		/// ARM little endian
		/// </summary>
		ARM = 0x1c0,
		/// <summary>
		/// ARMv7 (or higher) Thumb mode only
		/// </summary>
		ARMNT = 0x1c4,
		/// <summary>
		/// ARMv8 in 64-bit mode
		/// </summary>
		ARM64 = 0xaa64,
		/// <summary>
		/// EFI byte code
		/// </summary>
		EBC = 0xebc,
		/// <summary>
		/// Intel 386 or later processors and compatible processors
		/// </summary>
		Intel386 = 0x14c,
		/// <summary>
		/// Intel Itanium processor family
		/// </summary>
		Itanium64 = 0x200,
		/// <summary>
		/// Mitsubishi M32R little endian
		/// </summary>
		M32R = 0x9041,
		/// <summary>
		/// MIPS16
		/// </summary>
		MIPS16 = 0x266,
		/// <summary>
		/// MIPS with FPU
		/// </summary>
		MIPSFloatingPoint = 0x366,
		/// <summary>
		/// MIPS16 with FPU
		/// </summary>
		MIPS16FloatingPoint = 0x466,
		/// <summary>
		/// Power PC little endian
		/// </summary>
		PowerPC = 0x1f0,
		/// <summary>
		/// Power PC with floating point support
		/// </summary>
		PowerPCFloatingPoint = 0x1f1,
		/// <summary>
		/// MIPS little endian
		/// </summary>
		R4000 = 0x166,
		/// <summary>
		/// Hitachi SH3
		/// </summary>
		HitachiSH3 = 0x1a2,
		/// <summary>
		/// Hitachi SH3 DSP
		/// </summary>
		HitachiSH3DSP = 0x1a3,
		/// <summary>
		/// Hitachi SH4
		/// </summary>
		HitachiSH4 = 0x1a6,
		/// <summary>
		/// Hitachi SH5
		/// </summary>
		HitachiSH5 = 0x1a8,
		/// <summary>
		/// ARM or Thumb ("interworking")
		/// </summary>
		Thumb = 0x1c2,
		/// <summary>
		/// MIPS little-endian WCE v2
		/// </summary>
		MIPSWCEv2 = 0x169
	}
}
