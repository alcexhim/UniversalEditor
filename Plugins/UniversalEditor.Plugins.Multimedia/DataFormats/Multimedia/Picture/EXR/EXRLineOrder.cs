//
//  EXRLineOrder.cs
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
namespace UniversalEditor.DataFormats.Multimedia.Picture.EXR
{
	public enum EXRLineOrder
	{
		/// <summary>
		/// Indicates that scan lines are stored in order of increasing Y
		/// coordinates.
		/// </summary>
		IncreasingY = 0,
		/// <summary>
		/// Indicates that scan lines are stored in order of decreasing Y
		/// coordinates.
		/// </summary>
		DecreasingY = 1,
		/// <summary>
		/// Indicates that scan lines are stored randomly. The proper order of
		/// scan lines can be found by reading the offset table, in which scan
		/// line offsets are stored in order of increasing Y coordinates.
		/// </summary>
		RandomY = 2
	}
}
