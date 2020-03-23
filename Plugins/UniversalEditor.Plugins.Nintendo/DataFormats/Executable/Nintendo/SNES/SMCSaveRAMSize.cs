using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// Amount of space to allocate for save data.
	/// </summary>
	/// <remarks>
	/// Some headers set the save-RAM size to 32 kilobytes instead of the actual save-RAM
	/// size. For example, Super Mario World uses 2 kilobytes, but the clean headered ROM of
	/// SMW has byte $00 (meaning LoROM with 32 kilobytes of save-RAM) at offset 2.
	/// </remarks>
	public enum SMCSaveRAMSize
	{
		SaveRAMNone = 0,
		SaveRAM2K = 2,
		SaveRAM8K = 8,
		SaveRAM32K = 32
	}
}
