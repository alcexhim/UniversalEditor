//
//  SMCExtendedHeaderFlags.cs - indicates attributes of a Nintendo SNES game dump file extended header
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

namespace UniversalEditor.DataFormats.Executable.Nintendo.SNES
{
	/// <summary>
	/// Indicates attributes of a Nintendo SNES game dump file extended header.
	/// </summary>
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
