//
//  MicrosoftRegistryKeyNodeFlags.cs - indicates the attributes on a registry key node in a Microsoft registry file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	/// <summary>
	/// Indicates the attributes on a registry key node in a Microsoft registry file.
	/// </summary>
	[Flags()]
	public enum MicrosoftRegistryKeyNodeFlags : short
	{
		/// <summary>
		/// Is volatile (not used, a key node on a disk isn't expected to have this flag set)
		/// </summary>
		Volatile = 0x0001,
		/// <summary>
		/// Is the mount point of another hive (a key node on a disk isn't expected to have this flag set)
		/// </summary>
		HiveExit = 0x0002,
		/// <summary>
		/// Is the root key for this hive
		/// </summary>
		HiveEntry = 0x0004,
		/// <summary>
		/// This key can't be deleted
		/// </summary>
		NoDelete = 0x0008,
		/// <summary>
		/// This key is a symlink (a target key is specified as a UTF-16LE string
		/// (REG_LINK) in a value named "SymbolicLinkValue", example:
		/// \REGISTRY\MACHINE\SOFTWARE\Classes\Wow6432Node)
		/// </summary>
		SymbolicLink = 0x0010,
		/// <summary>
		/// Key name is an ASCII string, possibly an extended ASCII string (otherwise it
		/// is a UTF-16LE string)
		/// </summary>
		CompatibleName = 0x0020,
		/// <summary>
		/// Is a predefined handle (a handle is stored in the Number of key values field)
		/// </summary>
		PredefinedHandle = 0x0040,
		/// <summary>
		/// This key was virtualized at least once
		/// </summary>
		/// <remarks>Since Windows Vista</remarks>
		VirtualSource = 0x0080,
		/// <summary>
		/// Is virtual
		/// </summary>
		/// <remarks>Since Windows Vista</remarks>
		VirtualTarget = 0x0100,
		/// <summary>
		/// Is a part of a virtual store path
		/// </summary>
		/// <remarks>Since Windows Vista</remarks>
		VirtualStore = 0x0200
	}
}
