//
//  CHDFlags.cs - indicates attributes for the CHD archive
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

namespace UniversalEditor.DataFormats.FileSystem.CHD
{
	/// <summary>
	/// Indicates attributes for the CHD archive.
	/// </summary>
	[Flags()]
	public enum CHDFlags
	{
		None = 0x00000000,
		/// <summary>
		/// Set if this drive has a parent.
		/// </summary>
		HasParent = 0x00000001,
		/// <summary>
		/// Set if this drive allows writes.
		/// </summary>
		AllowWrite = 0x00000002
	}
}
