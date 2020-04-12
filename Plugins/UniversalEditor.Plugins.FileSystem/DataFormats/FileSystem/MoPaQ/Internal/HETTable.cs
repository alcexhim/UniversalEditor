//
//  HETTable.cs - internal structure representing a HET table in a MoPaQ archive
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

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
	public partial class MPQDataFormat
	{
		/// <summary>
		/// Internal structure representing a HET table in a MoPaQ archive.
		/// </summary>
		private struct HETTable
		{
			public uint dwVersion;
			public uint dwDataSize;
			public uint dwTableSize;
			public uint dwMaxFileCount;
			public uint dwHashTableSize;
			public uint dwHashEntrySize;
			public uint dwTotalIndexSize;
			public uint dwIndexSizeExtra;
			public uint dwIndexSize;
			public uint dwBlockTableSize;

			public static HETTable Read(IO.Reader br)
			{
				// The structure of the HET table, as stored in the MPQ, is the following:

				// Common header, for both HET and BET tables
				string HET = br.ReadFixedLengthString(4); // 'HET\x1A'
				if (HET != "HET\x1A") throw new InvalidDataFormatException("Expected HET signature, \"HET\\x1A\" not found");

				HETTable het = new HETTable();
				het.dwVersion = br.ReadUInt32();                       // Version. Seems to be always 1
				het.dwDataSize = br.ReadUInt32();                      // Size of the contained table
				het.dwTableSize = br.ReadUInt32();                     // Size of the entire hash table, including the header (in bytes)
				het.dwMaxFileCount = br.ReadUInt32();                  // Maximum number of files in the MPQ
				het.dwHashTableSize = br.ReadUInt32();                 // Size of the hash table (in bytes)
				het.dwHashEntrySize = br.ReadUInt32();                 // Effective size of the hash entry (in bits)
				het.dwTotalIndexSize = br.ReadUInt32();                // Total size of file index (in bits)
				het.dwIndexSizeExtra = br.ReadUInt32();                // Extra bits in the file index
				het.dwIndexSize = br.ReadUInt32();                     // Effective size of the file index (in bits)
				het.dwBlockTableSize = br.ReadUInt32();                // Size of the block index subtable (in bytes)
				return het;
			}
		}
	}
}
