//
//  WMFPolyfillMode.cs - specifies the method used for filling a polygon
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
	/// Specifies the method used for filling a polygon.
	/// </summary>
	public enum WMFPolyfillMode
	{
		/// <summary>
		/// Selects alternate mode (fills the area between odd-numbered and even-numbered
		/// polygon sides on each scan line).
		/// </summary>
		Alternate = 0x0001,
		/// <summary>
		/// Selects winding mode (fills any region with a nonzero winding value).
		/// </summary>
		Winding = 0x0002
	}
}
