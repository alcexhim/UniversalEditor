using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	public enum SMCExtendedHeaderFlags : byte
	{
		/// <summary>
		/// Save-RAM size is 32 kilobytes.
		/// </summary>
		SaveRam32K = 0x00,
		/// <summary>
		/// Save-RAM size is 8 kilobytes.
		/// </summary>
		SaveRam8K = 0x04,
		/// <summary>
		/// Save-RAM size is 2 kilobytes.
		/// </summary>
		SaveRam2K = 0x08,
		/// <summary>
		/// Save-RAM size is 0 kilobytes.
		/// </summary>
		SaveRamNone = 0x0C,
		/// <summary>
		/// Use HiROM.
		/// </summary>
		HiRomEnabled = 0x30,
		/// <summary>
		/// This is a split file but not the last image.
		/// </summary>
		SplitFile = 0x40,
		/// <summary>
		/// Jump to $8000 instead of the address in the reset vector.
		/// </summary>
		ResetVectorAddressOverride = 0x80
	}
}
