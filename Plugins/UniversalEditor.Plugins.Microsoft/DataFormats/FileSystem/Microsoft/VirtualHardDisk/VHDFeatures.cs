//
//  VHDFeatures.cs - indicates the special features enabled for the virtual hard disk
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.VirtualHardDisk
{
	/// <summary>
	/// Indicates the special features enabled for the virtual hard disk.
	/// </summary>
	public enum VHDFeatures
	{
		/// <summary>
		/// The hard disk image has no special features enabled in it.
		/// </summary>
		None = 0,
		/// <summary>
		/// This bit is set if the current disk is a temporary disk. A temporary disk designation indicates to an application that this disk is a candidate for deletion on shutdown.
		/// </summary>
		Temporary = 1,
		/// <summary>
		/// This bit must always be set to 1.
		/// </summary>
		Reserved = 2
	}
}
