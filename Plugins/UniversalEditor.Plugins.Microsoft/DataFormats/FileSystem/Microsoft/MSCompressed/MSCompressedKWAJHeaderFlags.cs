//
//  MSCompressedKWAJHeaderFlags.cs - indicates attributes for a MSCompressed KWAJ archive
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.MSCompressed
{
	/// <summary>
	/// Indicates attributes for a MSCompressed KWAJ archive.
	/// </summary>
	[Flags()]
    public enum MSCompressedKWAJHeaderFlags
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0x00000000,
        /// <summary>
        /// 4 bytes: decompressed length of file
        /// </summary>
        HasDecompressedLength = 0x00000001,
        /// <summary>
        /// 2 bytes: unknown purpose
        /// </summary>
        UnknownBit1 = 0x00000002,
        /// <summary>
        /// 2 bytes: length of data, followed by that many bytes of (unknown purpose) data
        /// </summary>
        HasExtraData = 0x00000004,
        /// <summary>
        /// 1-9 bytes: null-terminated string with max length 8: file name
        /// </summary>
        HasFileName = 0x00000008,
        /// <summary>
        /// 1-4 bytes: null-terminated string with max length 3: file extension
        /// </summary>
        HasFileExtension = 0x00000010,
        /// <summary>
        /// 2 bytes: length of data, followed by that many bytes of (arbitrary text) data
        /// </summary>
        HasExtraText = 0x00000011
    }
}
