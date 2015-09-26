using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Valve.GCF
{
	public enum GCFBlockEntryFlags
	{
		None = 0x00000000,
		/// <summary>
		/// The item is a file.
		/// </summary>
		File = 0x00004000,
		/// <summary>
		/// The item is encrypted.
		/// </summary>
		Encrypted = 0x00000100,
		/// <summary>
		/// Backup the item before overwriting it.
		/// </summary>
		BackupLocal = 0x00000040,
		/// <summary>
		/// The item is to be copied to the disk.
		/// </summary>
		CopyLocal = 0x0000000a,
		/// <summary>
		/// Don't overwrite the item if copying it to the disk and the item
		/// already exists.
		/// </summary>
		CopyLocalNoOverwrite = 0x00000001
	}
}
