//
//  WMFBrushStyle.cs
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
	public enum WMFBrushStyle : ushort
	{
		/// <summary>
		/// A brush that paints a single, constant color, either solid or dithered.
		/// </summary>
		Solid = 0x0000,
		/// <summary>
		/// A brush that does nothing. Using a BS_NULL brush in a graphics operation MUST have the
		/// same effect as using no brush at all.
		/// </summary>
		Null = 0x0001,
		/// <summary>
		/// A brush that paints a predefined simple pattern, or "hatch", onto a solid background.
		/// </summary>
		Hatched = 0x0002,
		/// <summary>
		/// A brush that paints a pattern defined by a bitmap, which can be a Bitmap16 Object
		/// or a DeviceIndependentBitmap Object.
		/// </summary>
		Pattern = 0x0003,
		/// <summary>
		/// Not supported.
		/// </summary>
		Indexed = 0x0004,
		/// <summary>
		/// A pattern brush specified by a DIB.
		/// </summary>
		DIBPattern = 0x0005,
		/// <summary>
		/// A pattern brush specified by a DIB.
		/// </summary>
		DIBPatternPT = 0x0006,
		/// <summary>
		/// Not supported.
		/// </summary>
		Pattern8x8 = 0x0007,
		/// <summary>
		/// Not supported.
		/// </summary>
		DIBPattern8x8 = 0x0008,
		/// <summary>
		/// Not supported.
		/// </summary>
		MonoPattern = 0x0009
	}
}
