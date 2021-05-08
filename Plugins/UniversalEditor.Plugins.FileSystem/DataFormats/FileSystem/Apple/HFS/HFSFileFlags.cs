//
//  HFSFileFlags.cs - indicates attributes for files in an HFS filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	/// <summary>
	/// Indicates attributes for files in an HFS filesystem.
	/// </summary>
	[Flags()]
	public enum HFSFileFlags : short
	{
		None = 0x00,
		/// <summary>
		/// If set, this file is locked and cannot be written to.
		/// </summary>
		Locked = 0x01,
		/// <summary>
		/// If set, a file thread record exists for this file.
		/// </summary>
		ThreadRecordExists = 0x02,
		/// <summary>
		/// If set, the file record is used.
		/// </summary>
		Used = 0x40
	}
}
