//
//  SevenZipBlockType.cs - indicates the type of block in a 7-Zip archive
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

namespace UniversalEditor.DataFormats.FileSystem.SevenZip
{
	/// <summary>
	/// Indicates the type of block in a 7-Zip archive.
	/// </summary>
	public enum SevenZipBlockType : long
	{
		End = 0x00,
		Header = 0x01,
		ArchiveProperties = 0x02,

		AdditionalStreamsInfo = 0x03,
		MainStreamsInfo = 0x04,
		FilesInfo = 0x05,

		PackInfo = 0x06,
		UnpackInfo = 0x07,
		SubStreamsInfo = 0x08,

		Size = 0x09,
		CRC = 0x0A,

		Folder = 0x0B,

		CodersUnpackSize = 0x0C,
		NumUnpackStream = 0x0D,

		EmptyStream = 0x0E,
		EmptyFile = 0x0F,
		Anti = 0x10,

		Name = 0x11,
		CTime = 0x12,
		ATime = 0x13,
		MTime = 0x14,
		WinAttributes = 0x15,
		Comment = 0x16,

		EncodedHeader = 0x17,

		StartPos = 0x18,
		Dummy = 0x19
	}
}
