//
//  HFSVolumeAttributes.cs - indicates volume attributes for an HFS filesystem
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS
{
	/// <summary>
	/// Indicates volume attributes for an HFS filesystem.
	/// </summary>
	[Flags()]
	public enum HFSVolumeAttributes : short
	{
		/// <summary>
		/// Set if the volume is locked by hardware
		/// </summary>
		HardwareLocked = 0x0040,
		/// <summary>
		/// Set if the volume was successfully unmounted
		/// </summary>
		SuccessfullyUnmounted = 0x0080,
		/// <summary>
		/// Set if the volume has had its bad blocks spared
		/// </summary>
		BadBlocksSpared = 0x0100,
		/// <summary>
		/// Set if the volume is locked by software
		/// </summary>
		SoftwareLocked = 0x0400
	}
}
