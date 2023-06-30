//
//  PartitionType.cs
//
//  Author:
//       beckermj <>
//
//  Copyright (c) 2023 ${CopyrightHolder}
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
namespace UniversalEditor.ObjectModels.PartitionedFileSystem
{
	public enum PartitionType
	{
		None = 0,
		/// <summary>
		/// FAT12 as primary partition in first physical 32 MB of disk or as
		/// logical drive anywhere on disk (else use 06h instead)
		/// </summary>
		FAT12,
		/// <summary>
		/// XENIX root
		/// </summary>
		XenixRoot,
		/// <summary>
		/// XENIX usr
		/// </summary>
		XenixUsr,

		IFS_HPFS_NTFS_exFAT_QNX,

		FAT32LBA
	}
}
