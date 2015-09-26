using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	public class SMCExtendedHeader
	{
		private bool mvarEnabled = false;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private int mvarFileSize = 0;
		public int FileSize { get { return mvarFileSize; } set { mvarFileSize = value; } }

		private bool mvarHiRomEnabled = false;
		/// <summary>
		/// Determines whether Hi-ROM is enabled.
		/// </summary>
		public bool HiRomEnabled { get { return mvarHiRomEnabled; } set { mvarHiRomEnabled = value; } }

		private SMCSaveRAMSize mvarSaveRAMSize = SMCSaveRAMSize.SaveRAM32K;
		/// <summary>
		/// Amount of space to allocate for save data.
		/// </summary>
		/// <remarks>
		/// Some headers set the save-RAM size to 32 kilobytes instead of the actual save-RAM
		/// size. For example, Super Mario World uses 2 kilobytes, but the clean headered ROM of
		/// SMW has byte $00 (meaning LoROM with 32 kilobytes of save-RAM) at offset 2.
		/// </remarks>
		public SMCSaveRAMSize SaveRAMSize { get { return mvarSaveRAMSize; } set { mvarSaveRAMSize = value; } }

		private bool mvarSplitFile = false;
		/// <summary>
		/// True if this is a split file, but not the last image.
		/// </summary>
		public bool SplitFile { get { return mvarSplitFile; } set { mvarSplitFile = value; } }

		private int mvarResetVectorOverride = -1;
		/// <summary>
		/// If non-negative, determines the address to jump to in place of the reset vector.
		/// </summary>
		public int ResetVectorOverride { get { return mvarResetVectorOverride; } set { mvarResetVectorOverride = value; } }

		private short mvarDSP1Settings = 0;
		public short DSP1Settings { get { return mvarDSP1Settings; } set { mvarDSP1Settings = value; } }

		private string mvarCreator = String.Empty;
		public string Creator
		{
			get { return mvarCreator; } 
			set 
			{
				if (value.Length > 8) throw new ArgumentOutOfRangeException("Value must be less than or equal to 8 characters in length");
				mvarCreator = value; 
			}
		}
	}
}
