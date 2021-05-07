//
//  MPQHeader.cs - internal structure representing the header of a MoPaQ archive
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
		/// Internal structure representing the header of a MoPaQ archive.
		/// </summary>
		private struct MPQHeader
		{
			/// <summary>
			/// Size of the archive header.
			/// </summary>
			public uint dwHeaderSize;
			/// <summary>
			/// Size of MPQ archive. This field is deprecated in the Burning Crusade MoPaQ format, and the
			/// size of the archive is calculated as the size from the beginning of the archive to the end
			/// of the hash table, block table, or extended block table (whichever is largest).
			/// </summary>
			public uint dwArchiveSize;
			public MPQFormatVersion wFormatVersion;
			/// <summary>
			/// Power of two exponent specifying the number of 512-byte disk sectors in each logical sector
			/// in the archive. The size of each logical sector in the archive is 512 * 2^wBlockSize.
			/// </summary>
			public ushort wBlockSize;
			/// <summary>
			/// Offset to the beginning of the hash table, relative to the beginning of the archive.
			/// </summary>
			public uint dwHashTablePos;
			/// <summary>
			/// Offset to the beginning of the block table, relative to the beginning of the archive.
			/// </summary>
			public uint dwBlockTablePos;
			/// <summary>
			/// Number of entries in the hash table. Must be a power of two, and must be less than 2^16 for
			/// the original MoPaQ format, or less than 2^20 for the Burning Crusade format.
			/// </summary>
			public uint dwHashTableEntryCount;
			/// <summary>
			/// Number of entries in the block table
			/// </summary>
			public uint dwBlockTableEntryCount;

			#region Header V2
			/// <summary>
			/// Offset to the beginning of array of 16-bit high parts of file offsets.
			/// </summary>
			public ulong HiBlockTablePos64;
			/// <summary>
			/// High 16 bits of the hash table offset for large archives.
			/// </summary>
			public ushort wHashTablePosHi;
			/// <summary>
			/// High 16 bits of the block table offset for large archives.
			/// </summary>
			public ushort wBlockTablePosHi;
			#endregion

			#region Header V3
			/// <summary>
			/// 64-bit version of the archive size
			/// </summary>
			public ulong ArchiveSize64;
			/// <summary>
			/// 64-bit position of the BET table
			/// </summary>
			public ulong BetTablePos64;
			/// <summary>
			/// 64-bit position of the HET table
			/// </summary>
			public ulong HetTablePos64;
			#endregion

			#region Header V4
			/// <summary>
			/// Compressed size of the hash table
			/// </summary>
			ulong HashTableSize64;
			/// <summary>
			/// Compressed size of the block table
			/// </summary>
			ulong BlockTableSize64;
			/// <summary>
			/// Compressed size of the hi-block table
			/// </summary>
			ulong HiBlockTableSize64;
			/// <summary>
			/// Compressed size of the HET block
			/// </summary>
			ulong HetTableSize64;
			/// <summary>
			/// Compressed size of the BET block
			/// </summary>
			ulong BetTableSize64;
			/// <summary>
			/// Size of raw data chunk to calculate MD5.
			/// </summary>
			uint dwRawChunkSize;
			#endregion

			public static MPQHeader Read(IO.Reader br)
			{
				MPQHeader hdr = new MPQHeader();
				hdr.dwHeaderSize = br.ReadUInt32();
				hdr.dwArchiveSize = br.ReadUInt32();
				hdr.wFormatVersion = (MPQFormatVersion)br.ReadUInt16();
				hdr.wBlockSize = br.ReadUInt16();
				hdr.dwHashTablePos = br.ReadUInt32();
				hdr.dwBlockTablePos = br.ReadUInt32();
				hdr.dwHashTableEntryCount = br.ReadUInt32();
				hdr.dwBlockTableEntryCount = br.ReadUInt32();

				#region Header V2
				if ((ushort)hdr.wFormatVersion >= 1)
				{
					hdr.HiBlockTablePos64 = br.ReadUInt64();
					hdr.wHashTablePosHi = br.ReadUInt16();
					hdr.wBlockTablePosHi = br.ReadUInt16();
				}
				#endregion
				#region Header V3
				if ((ushort)hdr.wFormatVersion >= 2)
				{
					hdr.ArchiveSize64 = br.ReadUInt64();
					hdr.BetTablePos64 = br.ReadUInt64();
					hdr.HetTablePos64 = br.ReadUInt64();
				}
				#endregion
				#region Header V4
				if ((ushort)hdr.wFormatVersion >= 3)
				{
					hdr.HashTableSize64 = br.ReadUInt64();
					hdr.BlockTableSize64 = br.ReadUInt64();
					hdr.HiBlockTableSize64 = br.ReadUInt64();
					hdr.HetTableSize64 = br.ReadUInt64();
					hdr.BetTableSize64 = br.ReadUInt64();
					hdr.dwRawChunkSize = br.ReadUInt32();

					// MD5 of each data chunk follows the raw file data.

					// Array of MD5's
					/*
					byte[] MD5_BlockTable[MD5_DIGEST_SIZE];      // MD5 of the block table before decryption
					byte[] MD5_HashTable[MD5_DIGEST_SIZE];       // MD5 of the hash table before decryption
					byte[] MD5_HiBlockTable[MD5_DIGEST_SIZE];    // MD5 of the hi-block table
					byte[] MD5_BetTable[MD5_DIGEST_SIZE];        // MD5 of the BET table before decryption
					byte[] MD5_HetTable[MD5_DIGEST_SIZE];        // MD5 of the HET table before decryption
					byte[] MD5_MpqHeader[MD5_DIGEST_SIZE];       // MD5 of the MPQ header from signature to (including) MD5_HetTable
					*/
				}
				#endregion
				return hdr;
			}
		}
	}
}
