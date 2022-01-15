//
//  WMFPalette.cs
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
using MBS.Framework.Drawing;

namespace UniversalEditor.DataFormats.Multimedia.VectorImage.Microsoft.WindowsMetafile.Internal
{
	internal class WMFPalette : WMFObject
	{
		public WMFPaletteEntry[] Entries;

		internal class WMFPaletteEntry
		{
			public Color Color { get; set; } = Color.Empty;
			public WMFPaletteEntryFlags Flags { get; set; } = WMFPaletteEntryFlags.None;
		}

		/// <summary>
		/// A 16-bit unsigned integer that defines the offset into the Palette Object when used
		/// with the META_SETPALENTRIES and META_ANIMATEPALETTE record types. When used with
		/// META_CREATEPALETTE record type, it MUST be 0x0300.
		/// </summary>
		/// <value>The offset into the Palette Object.</value>
		public ushort Start { get; set; } = 0x0000;
	}
}
