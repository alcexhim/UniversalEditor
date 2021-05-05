//
//  SMCExtendedHeader.cs - provides extended header metadata information for a Nintendo SNES game dump file in SMC format
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

using System;

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// Provides extended header metadata information for a Nintendo SNES game dump file in SMC format.
	/// </summary>
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
