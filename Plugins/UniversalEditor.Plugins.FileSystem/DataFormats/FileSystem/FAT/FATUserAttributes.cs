using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	[Flags()]
	public enum FATUserAttributes : byte
	{
		/// <summary>
		/// Modify default open rules (user attribute F1)
		/// </summary>
		ModifyDefaultOpenRules =					0x80,
		/// <summary>
		/// Partial close default (user attribute F2)
		/// </summary>
		PartialCloseDefault = 0x40,
		/// <summary>
		/// Ignore close checksum error (user attribute F3)
		/// </summary>
		IgnoreCloseChecksumError = 0x20,
		/// <summary>
		/// Disable checksums (user attribute F4)
		/// </summary>
		DisableChecksums = 0x10,
		Reserved = 0x08,
		DeleteRequiresPassword = 0x04,
		WriteRequiresPassword = 0x02,
		ReadRequiresPassword = 0x01
	}
}
