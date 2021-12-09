//
//  WMFDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile
{
	/// <summary>
	/// The PaletteEntryFlag Enumeration specifies how the palette entry is used.
	/// </summary>
	[Flags()]
	public enum WMFPaletteEntryFlags
	{
		None = 0x00,
		/// <summary>
		/// Specifies that the logical palette entry be used for palette animation. This value
		/// prevents other windows from matching colors to the palette entry because the color frequently
		/// changes. If an unused system-palette entry is available, the color is placed in that entry.
		/// Otherwise, the color is not available for animation.
		/// </summary>
		Reserved = 0x01,
		/// <summary>
		/// Specifies that the low-order word of the logical palette entry designates a hardware
		/// palette index. This value allows the application to show the contents of the display device palette.
		/// </summary>
		Explicit = 0x02,
		/// <summary>
		/// Specifies that the color be placed in an unused entry in the system palette
		/// instead of being matched to an existing color in the system palette. If there are no unused entries
		/// in the system palette, the color is matched normally. Once this color is in the system palette,
		/// colors in other logical palettes can be matched to this color.
		/// </summary>
		NoCollapse = 0x04
	}
}
