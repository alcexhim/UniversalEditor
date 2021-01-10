//
//  DSKDataFormat.cs - provides a DataFormat for manipulating disk images in Texas Instruments DSK format
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TexasInstruments.DSK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating disk images in Texas Instruments DSK format.
	/// </summary>
	public class DSKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the name of the volume. The volume name must have at least one non-space character, and can be any combination of ten ASCII characters except for space, '.', or NULL.
		/// </summary>
		/// <value>The name of the volume.</value>
		[CustomOptionText("_Volume name")]
		public string VolumeName { get; set; } = null;
		/// <summary>
		/// Gets or sets the total number of allocation units on the volume.
		/// </summary>
		/// <value>The total number of allocation units on the volume.</value>
		public short TotalAllocationUnits { get; set; } = 360;
		/// <summary>
		/// Gets or sets the sectors per track.
		/// </summary>
		/// <value>The sectors per track.</value>
		public byte SectorsPerTrack { get; set; } = 9;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="DSKDataFormat"/> is Proprietary Protected.
		/// </summary>
		/// <value><c>true</c> if protected; otherwise, <c>false</c>.</value>
		[CustomOptionBoolean("_Proprietary protected")]
		public bool Protected { get; set; } = false;

		public byte TracksPerSide { get; set; } = 40;
		public byte NumberOfFormattedSides { get; set; } = 1;
		public byte Density { get; set; } = 1;

		public int SectorSize { get; set; } = 256;
		private long start = 0;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			start = Accessor.Position;
			reader.Endianness = Endianness.BigEndian;

			string volumeName = reader.ReadFixedLengthString(10);
			volumeName = volumeName.Trim(); // it is space-filled to the right

			VolumeName = volumeName;
			TotalAllocationUnits = reader.ReadInt16();
			SectorsPerTrack = reader.ReadByte();

			string signature = reader.ReadFixedLengthString(3);
			if (signature != "DSK")
				throw new InvalidDataFormatException("file does not contain 'DSK' signature");

			char protection = reader.ReadChar();
			if (protection == 'P')
				Protected = true;

			TracksPerSide = reader.ReadByte();
			NumberOfFormattedSides = reader.ReadByte();
			Density = reader.ReadByte();

			reader.Seek(start + 56, SeekOrigin.Begin);
			byte[] allocationBitmap = reader.ReadBytes(200); // The map can control up to 1600 256-byte sectors

			// =====  FILE DESCRIPTOR INDEX RECORD, (Sector l) =====
			// 
			// This sector contains up to 127 two-byte entries. Each of these points to a File Descriptor Record, and are alphabetically sorted according
			// to the file name in the File Descriptor Record. The list starts at the beginning of the block, and ends with a zero entry.
			// 
			// As the file descriptors are alphabetically sorted, a binary search can be used to find any given filename. This limits the maximum number
			// of searches to 7 if more than 63 files are defined. Generally if between 2 * *(N - 1) and 2 * *n files are defined, a file search will take
			// at the most N disc searches. To obtain faster directory response times, data blocks are normally allocated in the area above sector 222, the
			// area below this being used for file descriptors and only used for file data when there are no more sectors available above > 22.
			SeekToSector(1);

			int totalIndices = 0;
			short[] indices = new short[127];
			for (int i = 0; i < 127; i++)
			{
				short index = reader.ReadInt16();
				indices[i] = index;

				if (index == 0)
					break;

				totalIndices++;
			}

			// we should be at offset 256 by now
			for (int i = 0; i < totalIndices; i++)
			{
				SeekToSector(indices[i]);

				string filename = reader.ReadFixedLengthString(10);
				filename = filename.Trim();

				// reserved for an extension of the number of data chain pointers - never implemented, so always 0.
				ushort reserved1 = reader.ReadUInt16();

				DSKFileStatusFlags flags = (DSKFileStatusFlags)reader.ReadByte();
				byte nRecordsPerAU = reader.ReadByte();
				ushort nL2RecordsAllocated = reader.ReadUInt16();
				byte eofOffset = reader.ReadByte();
				byte logicalRecordSize = reader.ReadByte();
				ushort nL3RecordsAllocated = reader.ReadUInt16();

				reader.Seek(8, SeekOrigin.Current); // reserved

				long offset = 0;
				long length = 0;

				for (int j = 0; j < 76; j++)
				{
					byte b1 = reader.ReadByte();
					byte b2 = reader.ReadByte();

					long sectorOffset = (long)b1 | (byte)((long)b2 << 4);
					if (offset == 0)
						offset = sectorOffset;

					byte b3 = reader.ReadByte();

					if (b1 == 0 && b2 == 0 && b3 == 0)
						break;

					long sectorCount = (long)((byte)(b3 << 4) | b2 >> 4) + 1;
					Console.WriteLine("adding file {0} : start {1} length {2} end {3}", filename, sectorOffset, sectorCount, sectorOffset + sectorCount);

					length = sectorCount;
				}

				File file = fsom.AddFile(filename);
				file.Size = (length * 256);
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset * 256);
				file.Properties.Add("length", length * 256);
				file.DataRequest += File_DataRequest;
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			long length = (long)file.Properties["length"];
			Reader reader = (Reader)file.Properties["reader"];

			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}


		private void SeekToSector(long sector)
		{
			Accessor.Seek(start + (sector * SectorSize), SeekOrigin.Begin);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.Endianness = Endianness.BigEndian;

			writer.WriteFixedLengthString(VolumeName, 10, ' ');
			writer.WriteInt16(TotalAllocationUnits);
			writer.WriteByte(SectorsPerTrack);
			writer.WriteFixedLengthString("DSK");

			if (Protected)
				writer.WriteChar('P');
			else
				writer.WriteChar(' ');

			writer.WriteByte(TracksPerSide);
		}
	}
}
