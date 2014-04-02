using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
    /// <summary>
    /// CHKDSK flags, bits 7-2 always cleared
    /// </summary>
	public enum FATCheckDiskFlags
	{
		None = 0x00,
		/// <summary>
		/// Volume is "dirty" and was not properly unmounted before shutdown, run CHKDSK on next boot.
		/// </summary>
		Dirty = 0x01,
		/// <summary>
		/// Disk I/O errors encountered, possible bad sectors, run surface scan on next boot.
		/// </summary>
		Error = 0x02
	}
}
