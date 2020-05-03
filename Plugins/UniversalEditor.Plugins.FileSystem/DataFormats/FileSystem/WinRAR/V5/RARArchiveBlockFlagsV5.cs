//
//  RARArchiveHeaderFlagsV5.cs
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

namespace UniversalEditor.DataFormats.FileSystem.WinRAR.V5
{
	[Flags()]
	public enum RARArchiveBlockFlagsV5
	{
		None = 0x0000,
		/// <summary>
		/// Volume. Archive is a part of multivolume set.
		/// </summary>
		Volume = 0x0001,
		/// <summary>
		/// Volume number field is present. This flag is present in all volumes except first.
		/// </summary>
		VolumeNumberFieldPresent = 0x0002,
		/// <summary>
		/// Solid archive.
		/// </summary>
		Solid = 0x0004,
		/// <summary>
		/// Recovery record is present.
		/// </summary>
		RecoveryRecordPresent = 0x0008,
		/// <summary>
		/// Locked archive.
		/// </summary>
		Locked = 0x0010
	}
}
