//
//  SMCSaveRAMSize.cs - indicates the size of the save RAM in an SMC dump file
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
	/// Indicates the size of the save RAM in an SMC dump file.
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
