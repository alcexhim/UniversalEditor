//
//  TapeArchiveRecordType.cs - indicates the type of record in a TAR archive
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

namespace UniversalEditor.DataFormats.FileSystem.TapeArchive
{
	/// <summary>
	/// Indicates the type of record in a TAR archive.
	/// </summary>
	public enum TapeArchiveRecordType : int
	{
		Normal = 0,
		HardLink = 1,
		SymbolicLink = 2,
		CharacterSpecial = 3,
		BlockSpecial = 4,
		Directory = 5,
		NamedPipe = 6,
		ContiguousFile = 7,
		GlobalExtendedHeader = (int)'g',
		NextFileExtendedHeader = (int)'x',
		VendorSpecificExtensionA = (int)'A',
		VendorSpecificExtensionZ = (int)'Z'
	}
}
