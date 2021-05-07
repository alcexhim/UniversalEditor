//
//  VHDHardDiskType.cs - indicates the type of virtual hard disk represented by the VHD file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.VirtualHardDisk
{
	/// <summary>
	/// Indicates the type of virtual hard disk represented by the VHD file.
	/// </summary>
	public enum VHDHardDiskType
	{
		None = 0,
		Reserved1 = 1,
		Fixed = 2,
		Dynamic = 3,
		Differencing = 4,
		Reserved5 = 5,
		Reserved6 = 6
	}
}
