//
//  CFFILE.cs - internal structure representing a Microsoft Cabinet file entry
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

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.Cabinet.Internal
{
	/// <summary>
	/// Internal structure representing a Microsoft Cabinet file entry.
	/// </summary>
	internal struct CFFILE
    {
        /// <summary>
        /// uncompressed size of this file in bytes
        /// </summary>
        public uint decompressedSize;
        /// <summary>
        /// uncompressed offset of this file in the folder
        /// </summary>
        public uint offset;
        /// <summary>
        /// index into the CFFOLDER area
        /// </summary>
        public ushort folderIndex;
        /// <summary>
        /// date stamp for this file
        /// </summary>
        public ushort date;
        /// <summary>
        /// time stamp for this file
        /// </summary>
        public ushort time;
        /// <summary>
        /// attribute flags for this file
        /// </summary>
        public ushort attribs;
        /// <summary>
        /// name of this file
        /// </summary>
        public string name;
    }
}
