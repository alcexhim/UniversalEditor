//
//  WMFColorUage.cs
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
	public enum WMFColorUsage : ushort
	{
		/// <summary>
		/// The color table contains RGB values specified by RGBQuad Objects.
		/// </summary>
		RGBColors = 0x0000,
		/// <summary>
		/// The color table contains 16-bit indices into the current logical palette in the
		/// playback device context.
		/// </summary>
		PaletteColors = 0x0001,
		/// <summary>
		/// No color table exists. The pixels in the DIB are indices into the current logical
		/// palette in the playback device context.
		/// </summary>
		PaletteIndices = 0x0002
	}
}
