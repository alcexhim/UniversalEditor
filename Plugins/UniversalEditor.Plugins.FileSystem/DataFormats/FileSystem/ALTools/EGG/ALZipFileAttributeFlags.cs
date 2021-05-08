//
//  ALZipFileAttributeFlags.cs - indicates attributes for a file in an ALZip archive
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

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG
{
	/// <summary>
	/// Indicates attributes for a file in an ALZip archive.
	/// </summary>
	[Flags()]
	public enum ALZipFileAttributeFlags
	{
		None = 0x00,
		ReadOnly = 0x01,
		Hidden = 0x02,
		System = 0x04,
		SymbolicLink = 0x08,
		Undefined10 = 0x10,
		Undefined20 = 0x20,
		Undefined40 = 0x40,
		Directory = 0x80
	}
}
