//
//  WMFFloodFillMode.cs
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
	public enum WMFFloodFillMode
	{
		/// <summary>
		/// The fill area is bounded by the color specified by the Color member. This style
		/// is identical to the filling performed by the <see cref="WMFRecordType.FloodFill" /> Record.
		/// </summary>
		Border = 0x0000,
		/// <summary>
		/// The fill area is bounded by the color that is specified by the Color member.
		/// Filling continues outward in all directions as long as the color is encountered. This style is useful
		/// for filling areas with multicolored boundaries.
		/// </summary>
		Surface = 0x0001
	}
}
