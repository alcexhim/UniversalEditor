//
//  WMFMapMode.cs
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
	/// 	<para>
	/// 		The MapMode Enumeration defines how logical units are mapped to physical units; that is,
	/// 		assuming that the origins in both the logical and physical coordinate systems are at the same point on
	/// 		the drawing surface, what is the physical coordinate(x',y') that corresponds to logical coordinate
	/// 		(x, y).
	/// 	</para>
	/// 	<para>
	/// 		For example, suppose the mapping mode is <see cref="Text" />. Given the following definition of that
	/// 		mapping mode, and an origin(0,0) at the top left corner of the drawing surface, logical coordinate
	/// 		(4,5) would map to physical coordinate(4,5) in pixels.
	/// 	</para>
	/// 	<para>
	/// 		Now suppose the mapping mode is <see cref="LoEnglish" />, with the same origin as the previous example.
	/// 		Given the following definition of that mapping mode, logical coordinate(4,-5) would map to physical
	/// 		coordinate(0.04,0.05) in inches.
	/// 	</para>
	/// </summary>
	public enum WMFMapMode
	{
		/// <summary>
		/// Each logical unit is mapped to one device pixel. Positive x is to the right; positive y is
		/// down.
		/// </summary>
		Text = 0x0001,
		/// <summary>
		/// Each logical unit is mapped to 0.1 millimeter. Positive x is to the right; positive y is
		/// up.
		/// </summary>
		LoMetric = 0x0002,
		/// <summary>
		/// Each logical unit is mapped to 0.01 millimeter. Positive x is to the right; positive y is
		/// up.
		/// </summary>
		HiMetric = 0x0003,
		/// <summary>
		/// Each logical unit is mapped to 0.01 inch. Positive x is to the right; positive y is up.
		/// </summary>
		LoEnglish = 0x0004,
		/// <summary>
		/// Each logical unit is mapped to 0.001 inch. Positive x is to the right; positive y is
		/// up.
		/// </summary>
		HiEnglish = 0x0005,
		/// <summary>
		/// Each logical unit is mapped to one twentieth (1/20) of a point. In printing, a point is
		/// 1/72 of an inch; therefore, 1/20 of a point is 1/1440 of an inch. This unit is also known as a
		/// "twip". Positive x is to the right; positive y is up.
		/// </summary>
		Twips = 0x0006,
		/// <summary>
		/// 	<para>
		/// 		Logical units are mapped to arbitrary device units with equally scaled axes; that is,
		/// 		one unit along the x-axis is equal to one unit along the y-axis. The <see cref="WMFRecordType.SetWindowExtents" />
		/// 		(section 2.3.5.30) and <see cref="WMFRecordType.SetViewportExtents" /> (section 2.3.5.28) records specify the units
		/// 		and the orientation of the axes.
		/// 	</para>
		/// 	<para>
		/// 		The processing application SHOULD make adjustments as necessary to ensure the x and y units
		/// 		remain the same size. For example, when the window extent is set, the viewport SHOULD be
		/// 		adjusted to keep the units isotropic.
		/// 	</para>
		/// </summary>
		Isotropic = 0x0007,
		/// <summary>
		/// Logical units are mapped to arbitrary units with arbitrarily scaled axes.
		/// </summary>
		Anisotropic = 0x0008
	}
}
