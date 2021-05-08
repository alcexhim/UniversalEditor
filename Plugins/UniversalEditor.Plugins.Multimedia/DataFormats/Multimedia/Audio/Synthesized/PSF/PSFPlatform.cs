//
//  PSFPlatform.cs - indicates the platform and format for the PSF file
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

namespace UniversalEditor.DataFormats.Multimedia.Audio.Synthesized.PSF
{
	/// <summary>
	/// Indicates the platform and format for the PSF file.
	/// </summary>
	public enum PSFPlatform : byte
	{
		/// <summary>
		/// Playstation (PSF1)
		/// </summary>
		Playstation = 0x01,
		/// <summary>
		/// Playstation 2 (PSF2)
		/// </summary>
		Playstation2 = 0x02,
		/// <summary>
		/// Saturn (SSF) (format subject to change)
		/// </summary>
		Saturn = 0x11,
		/// <summary>
		/// Dreamcast (DSF) (format subject to change)
		/// </summary>
		Dreamcast = 0x12,
		/// <summary>
		/// Sega Genesis (format to be announced)
		/// </summary>
		Genesis = 0x13,
		/// <summary>
		/// Nintendo 64 (USF)
		/// </summary>
		Nintendo64 = 0x21,
		/// <summary>
		/// GameBoy Advance (GSF)
		/// </summary>
		GameBoyAdvance = 0x22,
		/// <summary>
		/// Super NES (SNSF)
		/// </summary>
		SuperNES = 0x23,
		/// <summary>
		/// Capcom QSound (QSF)
		/// </summary>
		CapcomQSound = 0x41
	}
}
