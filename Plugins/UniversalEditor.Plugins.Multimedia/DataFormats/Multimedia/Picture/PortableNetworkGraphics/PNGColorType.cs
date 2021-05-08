//
//  PNGColorType.cs - indicates the color type of a PNG image file
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

namespace UniversalEditor.DataFormats.Multimedia.Picture.PortableNetworkGraphics
{
	/// <summary>
	/// Indicates the color type of a PNG image file. Describes the interpolation of the image data.
	/// </summary>
	[Flags()]
	public enum PNGColorType
	{
		None = 0,
		/// <summary>
		/// Each pixel is referenced by an index into the palette table.
		/// </summary>
		Palette = 1,
		/// <summary>
		/// Each pixel is an R, G, B triple.
		/// </summary>
		Color = 2,
		/// <summary>
		/// Each pixel is followed by an alpha sample.
		/// </summary>
		AlphaChannel = 4
	}
}
