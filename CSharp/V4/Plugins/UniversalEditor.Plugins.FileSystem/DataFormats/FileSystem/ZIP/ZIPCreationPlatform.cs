using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ZIP
{
	public enum ZIPCreationPlatform
	{
		/// <summary>
		/// MS-DOS and OS/2 (FAT / VFAT / FAT32 file systems)
		/// </summary>
		MSDOS = 0,
		Amiga = 1,
		OpenVMS = 2,
		Unix = 3,
		VMCMS = 4,
		AtariST = 5,
		/// <summary>
		/// OS/2 H.P.F.S.
		/// </summary>
		HPFS = 6,
		Macintosh = 7,
		/// <summary>
		/// Z-System
		/// </summary>
		ZSystem = 8,
		/// <summary>
		/// CP/M
		/// </summary>
		CPM = 9,
		/// <summary>
		/// Windows NTFS
		/// </summary>
		WindowsNTFS = 10,
		/// <summary>
		/// MVS (OS/390 - Z/OS)
		/// </summary>
		MVS = 11,
		VSE = 12,
		AcornRISC = 13,
		VFAT = 14,
		AlternateMVS = 15,
		BeOS = 16,
		Tandem = 17,
		/// <summary>
		/// OS/400
		/// </summary>
		OS400 = 18,
		/// <summary>
		/// Mac OS X (Darwin)
		/// </summary>
		Darwin = 19
	}
}
