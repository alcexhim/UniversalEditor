//
//  MachOCpuSubType.cs - specifies the CPU sub-type for an Apple Mach-O executable
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
	/// Specifies the CPU sub-type for an Apple Mach-O executable.
	/// </summary>
	public enum MachOCpuSubType
	{
		// VAX subtypes (these do *not* necessary conform to the actual cpu ID assigned by DEC
		// available via the SID register,.
		VAX_780 = 1,
		VAX_785 = 2,
		VAX_750 = 3,
		VAX_730 = 4,
		UVAX_I = 5,
		UVAX_II = 6,
		VAX_8200 = 7,
		VAX_8500 = 8,
		VAX_8600 = 9,
		VAX_8650 = 10,
		VAX_8800 = 11,
		UVAX_III = 12,

		// Alpha subtypes (these do *not* necessary conform to the actual cpu ID assigned by DEC
		// available via the SID register,.
		AlphaADU = 1,
		DEC_4000 = 2,
		DEC_7000 = 3,
		DEC_3000_500 = 4,
		DEC_3000_400 = 5,
		DEC_10000 = 6,
		DEC_3000_300 = 7,
		DEC_2000_300 = 8,
		DEC_2100_A500 = 9,
		DEC_AXPVME_64 = 10,
		/* #define DEC_MORGAN = 11, */
		/* cancelled */
		DEC_AXPPCI_33 = 11,
		DEC_AVANTI = 13,
		DEC_MUSTANG = 14,
		DEC_800_5 = 15,
		DEC_21000_800 = 16,
		DEC_1000 = 17,
		DEC_21000_900 = 18,
		EB66 = 19,
		EB64P = 20,
		ALPHABOOK = 21,
		DEC_4100 = 22,
		DEC_EV45_PBP = 23,
		DEC_2100A_A500 = 24,
		EB164 = 26,
		DEC_1000A = 27,
		DEC_ALPHAVME_224 = 28,
		DEC_550 = 30,
		DEC_EV56_PBP = 32,
		DEC_ALPHAVME_320 = 33,
		DEC_6600 = 34,
		ALPHA_WILDFIRE = 35,
		DMCC_EV6 = 37,
		/*
		 *	ROMP subtypes.
		 */

		RT_PC = 1,
		RT_APC = 2,
		RT_135 = 3,

		/*
		 *	68020 subtypes.
		 */

		SUN3_50 = 1,
		SUN3_160 = 2,
		SUN3_260 = 3,
		SUN3_110 = 4,
		SUN3_60 = 5,

		HP_320 = 6,
		/* 16.67 Mhz HP 300 series, custom MMU [HP 320] */
		HP_330 = 7,
		/* 16.67 Mhz HP 300 series, MC68851 MMU [HP 318,319,330,349] */
		HP_350 = 8,
		/* 25.00 Mhz HP 300 series, custom MMU [HP 350] */
		APOLLO_3000 = 9,
		APOLLO_4000 = 10,

		/*
		 *	32032/32332/32532 subtypes.
		 */

		MMAX_DPC = 1,	/* 032 CPU */
		SQT = 2,
		MMAX_APC_FPU = 3,	/* 32081 FPU */
		MMAX_APC_FPA = 4,	/* Weitek FPA */
		MMAX_XPC_FPU = 5,	/* 532 +'381 FPU */
		MMAX_XPC_FPA = 6,	/* 532 +580+WTL3164 */
		// MMAX_RES1		/* Reserved */
		// MMAX_RES2		/* Reserved */

		/*
		 *	80386 subtypes.
		 */

		AT386 = 1,
		EXL = 2,
		SQT86 = 3,

		/*
		 *	Mips subtypes.
		 */

		MIPS_R2300 = 1,
		MIPS_R2600 = 2,
		MIPS_R2800 = 3,
		MIPS_R2000a = 4,
		/* dgd -- addition for 3max support */
		MIPS_R3000a = 5,

		/*
		 * 	MC68030 subtypes
		 */

		/// <summary>
		/// NeXt thinks MC68030 is 6 rather than 9
		/// </summary>
		NeXT = 1,
		HP_340 = 2,
		/* 16.67 Mhz HP 300 series [HP 332,340] */
		HP_360 = 3,
		/* 25.00 Mhz HP 300 series [HP 360] */
		HP_370 = 4,
		/* 33.33 Mhz HP 300 series [HP 370] */
		APOLLO_2500 = 5,
		APOLLO_3500 = 6,
		APOLLO_4500 = 7,


		/*
		 *	PA_RISC subtypes  Hewlett-Packard HP-PA family of
		 *	risc processors 800 series workstations.
		 *	Port done by Hewlett-Packard
		 */

		PA_RISC_840 = 0x004,
		PA_RISC_825 = 0x008,
		PA_RISC_835 = 0x00a,
		PA_RISC_850 = 0x00c,
		PA_RISC_855 = 0x081,
		PA_RISC_810 = 0x100,
		PA_RISC_815 = 0x103,
		PA_RISC_710 = 0x300,
		PA_RISC_720 = 0x200,
		PA_RISC_730 = 0x202,
		PA_RISC_750 = 0x201,

		/* 
		 * 	Acorn subtypes - Acorn Risc Machine port done by
		 *		Olivetti System Software Laboratory
		 */

		ARM_A500_ARCH = 1,
		ARM_A500 = 2,
		ARM_A440 = 3,
		ARM_M4 = 4,
		ARM_A680 = 5,

		/*
		 *	MC88000 subtypes - Encore doing port.
		 */

		MMAX_JPC = 1,

		/*
		 *	Sun4 subtypes - port done at CMU
		 */

		Sun4_All = 0,
		Sun4_260 = 1,
		Sun4_110 = 2,

		Sparc_All = 0,

		// PowerPC subtypes
		PowerPC_All = 0,
		PowerPC_601 = 1,
		PowerPC_602 = 2,
		PowerPC_603 = 3,
		PowerPC_603e = 4,
		PowerPC_603ev = 5,
		PowerPC_604 = 6,
		PowerPC_604e = 7,
		PowerPC_620 = 8,
		PowerPC_750 = 9,
		PowerPC_7400 = 10,
		PowerPC_7450 = 11,
		PowerPC_970 = 100,

		// VEO subtypes
		// Note: the VEO_ALL will likely change over time to be defined as one of the specific
		// subtypes.
		VEO_1 = 1,
		VEO_2 = 2,
		VEO_3 = 3,
		VEO_4 = 4,
		VEO_ALL = VEO_2
	}
}
