//
//  MicrosoftRegistryReservedFlags.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
	[Flags()]
	public enum MicrosoftRegistryReservedFlags
	{
		/// <summary>
		/// The Kernel Transaction Manager (KTM) locked the hive (there are pending or anticipated transactions).
		/// </summary>
		Locked = 0x00000001,
		/// <summary>
		/// The hive has been defragmented (all its pages are dirty therefore) and it is being written to a disk
		/// (Windows 8 and Windows Server 2012 only, this flag is used to speed up hive recovery by reading a
		/// transaction log file instead of a primary file); this hive supports the layered keys feature (starting from
		/// Insider Preview builds of Windows 10 "Redstone 1")
		/// </summary>
		DefragmentedHiveOrLayeredKeys = 0x00000002,
	}
}
