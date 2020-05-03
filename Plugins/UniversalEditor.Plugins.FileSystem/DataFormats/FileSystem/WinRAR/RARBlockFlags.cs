//
//  RARHeaderFlagsV5.cs
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
namespace UniversalEditor.DataFormats.FileSystem.WinRAR
{
	[Flags()]
	public enum RARBlockFlags
	{
		None = 0x0000,
		/// <summary>
		/// Extra area is present in the end of header.
		/// </summary>
		ExtraAreaPresent = 0x0001,
		/// <summary>
		/// Data area is present in the end of header.
		/// </summary>
		DataAreaPresent = 0x0002,
		/// <summary>
		/// Blocks with unknown type and this flag must be skipped when updating an archive.
		/// </summary>
		SkipUnknownBlocks = 0x0004,
		/// <summary>
		/// Data area is continuing from previous volume.
		/// </summary>
		ContinuedFromPrevious = 0x0008,
		/// <summary>
		/// Data area is continuing in next volume.
		/// </summary>
		ContinuedInNext = 0x0010,
		/// <summary>
		/// Block depends on preceding file block.
		/// </summary>
		DependentBlock = 0x0020,
		/// <summary>
		/// Preserve a child block if host block is modified.
		/// </summary>
		PreserveChild = 0x0040
	}
}
