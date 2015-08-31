using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	public enum FATMediaDescriptor : byte
	{
		/// <summary>
		/// The value of the media descriptor is unknown to this implementation of FAT.
		/// </summary>
		Unknown = 0,
		/// <summary>
		/// 3.5" Double Sided, 80 tracks per side, 18 or 36 sectors per track (1.44MB
		/// or 2.88MB). 5.25" Double Sided, 80 tracks per side, 15 sectors per track
		/// (1.2MB). Used also for other media types.
		/// </summary>
		MediaDescriptor0 = 0xF0,
		/// <summary>
		/// Fixed disk (i.e. Hard disk).
		/// </summary>
		FixedDisk = 0xF8,
		/// <summary>
		/// 3.5" Double sided, 80 tracks per side, 9 sectors per track (720K). 5.25" Double sided, 80 tracks per side, 15 sectors per track (1.2MB)
		/// </summary>
		MediaDescriptor2 = 0xF9,
		/// <summary>
		/// 5.25" Single sided, 80 tracks per side, 8 sectors per track (320K)
		/// </summary>
		MediaDescriptor3 = 0xFA,
		/// <summary>
		/// 3.5" Double sided, 80 tracks per side, 8 sectors per track (640K)
		/// </summary>
		MediaDescriptor4 = 0xFB,
		/// <summary>
		/// 5.25" Single sided, 40 tracks per side, 9 sectors per track (180K)
		/// </summary>
		MediaDescriptor5 = 0xFC,
		/// <summary>
		/// 5.25" Double sided, 40 tracks per side, 9 sectors per track (360K). Also used for 8".
		/// </summary>
		MediaDescriptor6 = 0xFD,
		/// <summary>
		/// 5.25" Single sided, 40 tracks per side, 8 sectors per track (160K). Also used for 8".
		/// </summary>
		MediaDescriptor7 = 0xFE,
		/// <summary>
		/// 5.25" Double sided, 40 tracks per side, 8 sectors per track (320K)
		/// </summary>
		MediaDescriptor8 = 0xFF
	}
}
