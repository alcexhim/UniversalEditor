//
//  RARHeaderType.cs - indicates the type of header in a RAR file
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

namespace UniversalEditor.DataFormats.FileSystem.WinRAR.V4
{
	/// <summary>
	/// Indicates the type of header in a RAR file.
	/// </summary>
	public enum RARBlockTypeV4 : byte
	{
		Marker = 0x72,
		Archive = 0x73,
		File = 0x74,
		OldComment = 0x75,
		OldAuthenticity = 0x76,
		OldSubblock = 0x77,
		OldRecoveryRecord = 0x78,
		OldAuthenticity2 = 0x79,
		Subblock = 0x7A
	}
}
