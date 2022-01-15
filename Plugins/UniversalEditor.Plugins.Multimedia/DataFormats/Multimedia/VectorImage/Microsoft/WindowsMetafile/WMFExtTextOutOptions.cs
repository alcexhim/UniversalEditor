//
//  WMFExtTextOutOptions.cs
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
	public enum WMFExtTextOutOptions
	{
		/// <summary>
		/// Indicates that the background color that is defined in the playback
		/// device context SHOULD be used to fill the rectangle.
		/// </summary>
		Opaque = 0x0002,
		/// <summary>
		/// Indicates that the text SHOULD be clipped to the rectangle.
		/// </summary>
		Clipped = 0x0004,
		/// <summary>
		/// Indicates that the string to be output SHOULD NOT require further
		/// processing with respect to the placement of the characters, and an array
		/// of character placement values SHOULD be provided. This character
		/// placement process is useful for fonts in which diacritical characters affect
		/// character spacing.
		/// </summary>
		GlyphIndex = 0x0010,
		/// <summary>
		/// Indicates that the text MUST be laid out in right-to-left reading order,
		/// instead of the default left-to-right order. This SHOULD be applied only
		/// when the font that is defined in the playback device context is either
		/// Hebrew or Arabic.
		/// </summary>
		RTLReading = 0x0080,
		/// <summary>
		/// Indicates that to display numbers, digits appropriate to the locale
		/// SHOULD be used.
		/// </summary>
		NumericsLocal = 0x0400,
		/// <summary>
		/// Indicates that to display numbers, European digits SHOULD be
		/// used.
		/// </summary>
		NumericsLatin = 0x0800,
		/// <summary>
		/// Indicates that both horizontal and vertical character displacement values
		/// SHOULD be provided.
		/// </summary>
		PDY = 0x2000
	}
}
