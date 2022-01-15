//
//  WMFPenStyle.cs
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
	[Flags()]
	public enum WMFPenStyle
	{
		/// <summary>
		/// The pen is cosmetic.
		/// </summary>
		Cosmetic = 0x0000,
		/// <summary>
		/// Line end caps are round.
		/// </summary>
		EndcapRound = 0x0000,
		/// <summary>
		/// Line joins are round.
		/// </summary>
		JoinRound = 0x0000,
		/// <summary>
		/// The pen is solid.
		/// </summary>
		Solid = 0x0000,

		/// <summary>
		/// The pen is dashed.
		/// </summary>
		Dash = 0x0001,
		/// <summary>
		/// The pen is dotted.
		/// </summary>
		Dot = 0x0002,
		/// <summary>
		/// The pen has alternating dashes and dots.
		/// </summary>
		DashDot = 0x0003,
		/// <summary>
		/// The pen has dashes and double dots.
		/// </summary>
		DashDotDot = 0x0004,
		/// <summary>
		/// The pen is invisible.
		/// </summary>
		Null = 0x0005,
		/// <summary>
		/// The pen is solid. When this pen is used in any drawing record that takes a
		/// bounding rectangle, the dimensions of the figure are shrunk so that it fits entirely in the bounding
		/// rectangle, taking into account the width of the pen.
		/// </summary>
		InsideFrame = 0x0006,
		/// <summary>
		/// The pen uses a styling array supplied by the user.
		/// </summary>
		UserStyle = 0x0007,
		/// <summary>
		/// The pen sets every other pixel (this style is applicable only for cosmetic pens).
		/// </summary>
		Alternate = 0x0008,
		/// <summary>
		/// Line end caps are square.
		/// </summary>
		EndcapSquare = 0x0100,
		/// <summary>
		/// Line end caps are flat.
		/// </summary>
		EndcapFlat = 0x0200,
		/// <summary>
		/// Line joins are beveled.
		/// </summary>
		JoinBevel = 0x1000,
		/// <summary>
		/// Line joins are mitered when they are within the current limit set by the
		/// SETMITERLIMIT Record (section 2.3.6.42). A join is beveled when it would exceed the limit.
		/// </summary>
		JoinMiter = 0x2000
	}
}
