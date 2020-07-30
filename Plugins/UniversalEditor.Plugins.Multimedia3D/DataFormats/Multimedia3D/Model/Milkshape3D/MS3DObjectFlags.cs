//
//  MS3DVertexFlags.cs - indicates flags for a vertex in a MilkShape 3D model file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Milkshape3D
{
	/// <summary>
	/// Indicates flags for a vertex in a MilkShape 3D model file.
	/// </summary>
	[Flags()]
	public enum MS3DObjectFlags : byte
	{
		None = 0,
		Selected = 1,
		Hidden = 2,
		Selected2 = 4
	}
}
