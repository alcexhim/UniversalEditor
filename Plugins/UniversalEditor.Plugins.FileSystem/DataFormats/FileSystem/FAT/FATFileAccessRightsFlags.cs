//
//  FATFileAccessRightsFlags.cs - indicates access rights attributes for a file in a FAT filesystem
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

namespace UniversalEditor.DataFormats.FileSystem.FAT
{
	/// <summary>
	/// Indicates access rights attributes for a file in a FAT filesystem.
	/// </summary>
	[Flags()]
	public enum FATFileAccessRightsFlags : short
	{
		None = 0x0000,
		/// <summary>
		/// Owner delete/rename/attribute change requires permission
		/// </summary>
		OwnerMetaPermissionRequired = 0x0001,
		/// <summary>
		/// Owner execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		OwnerExecutePermissionRequired = 0x0002,
		/// <summary>
		/// Owner write/modify requires permission
		/// </summary>
		OwnerWritePermissionRequired = 0x0004,
		/// <summary>
		/// Owner read/copy requires permission
		/// </summary>
		OwnerReadPermissionRequired = 0x0008,
		/// <summary>
		/// Group delete/rename/attribute change requires permission
		/// </summary>
		GroupMetaPermissionRequired = 0x0010,
		/// <summary>
		/// Group execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		GroupExecutePermissionRequired = 0x0020,
		/// <summary>
		/// Group write/modify requires permission
		/// </summary>
		GroupWritePermissionRequired = 0x0040,
		/// <summary>
		/// Group read/copy requires permission
		/// </summary>
		GroupReadPermissionRequired = 0x0080,
		/// <summary>
		/// World delete/rename/attribute change requires permission
		/// </summary>
		WorldMetaPermissionRequired = 0x0100,
		/// <summary>
		/// World execute requires permission (FlexOS, 4680 OS, 4690 OS only)
		/// </summary>
		WorldExecutePermissionRequired = 0x0200,
		/// <summary>
		/// World write/modify requires permission
		/// </summary>
		WorldWritePermissionRequired = 0x0400,
		/// <summary>
		/// World read/copy requires permission
		/// </summary>
		WorldReadPermissionRequired = 0x0800,
	}
}
