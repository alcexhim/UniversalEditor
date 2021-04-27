//
//  ACOColorSpace.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Palette.Adobe
{
	public enum ACOColorSpace : ushort
	{
		/// <summary>
		/// The first three values in the color data are red, green, and blue.
		/// They are full unsigned 16-bit values as in Apple's RGBColor data
		/// structure. Pure red = 65535, 0, 0.
		/// </summary>
		RGB = 0,
		/// <summary>
		/// The first three values in the color data are hue, saturation, and
		/// brightness. They are full unsigned 16-bit values as in Apple's
		/// HSVColor data structure. Pure red = 0, 65535, 65535.
		/// </summary>
		HSB = 1,
		/// <summary>
		/// The four values in the color data are cyan, magenta, yellow, and
		/// black. They are full unsigned 16-bit values. 0 = 100% ink.
		/// </summary>
		CMYK = 2,
		/// <summary>
		/// Pantone matching system (custom colorspace)
		/// </summary>
		Pantone = 3,
		/// <summary>
		/// Focoltone colour system (custom colorspace)
		/// </summary>
		Focoltone = 4,
		/// <summary>
		/// Trumatch color (custom colorspace)
		/// </summary>
		Trumatch = 5,
		/// <summary>
		/// Toyo 88 colorfinder 1050 (custom colorspace)
		/// </summary>
		Toyo88 = 6,
		/// <summary>
		/// The first three values in the color data are lightness,
		/// a-chrominance, and b-chrominance.
		/// </summary>
		LAB = 7,
		/// <summary>
		/// The first value in the color data is the gray value, from 0...10000.
		/// </summary>
		Grayscale = 8,
		/// <summary>
		/// HKS colors (custom colorspace)
		/// </summary>
		HKS = 10
	}
}
