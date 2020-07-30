//
//  NTFSDataFormat.cs - provides a DataFormat for manipulating NTFS file systems
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
using System.Collections.Generic;
using UniversalEditor.DataFormats.FileSystem.Microsoft.NTFS.Attributes;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.NTFS
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating NTFS file systems.
	/// </summary>
	public class NTFSDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;
			byte[] jump = reader.ReadBytes(3);

			string id = reader.ReadFixedLengthString(8); // 'NTFS    '
			if (id != "NTFS    ") throw new InvalidDataFormatException("file does not begin with 'NTFS    '");

			BytesPerSector = reader.ReadInt16();
			SectorsPerCluster = reader.ReadByte();

			short reservedSectors = reader.ReadInt16(); // 0x0000
			byte[] reserved2 = reader.ReadBytes(3); // 00 00 00
			short reserved3 = reader.ReadInt16(); // 0x0000
			MediaDescriptor = (NTFSMediaDescriptor)reader.ReadByte();
			short reserved4 = reader.ReadInt16(); // 0x0000
			SectorsPerTrack = reader.ReadInt16();
			NumberOfHeads = reader.ReadInt16();
			HiddenSectors = reader.ReadInt32();
			int reserved5 = reader.ReadInt32(); // 0x00000000
			int reserved6 = reader.ReadInt32(); // 0x00800080
			long totalSectors = reader.ReadInt64(); // 0x00000000007FF54A
			long masterFileTableClusterNumber = reader.ReadInt64(); // 0x0000000000000004
			long masterFileTableMirrorClusterNumber = reader.ReadInt64(); // 0x000000000007FF54

			// A positive value denotes the number of clusters in a File Record Segment. A negative value denotes the amount of bytes in a File Record Segment, in which case the
			// size is 2 to the power of the absolute value. (0xF6 = -10 → 210 = 1024).
			sbyte bytesOrClustersPerFileRecordSegment = reader.ReadSByte();

			byte[] reserved7 = reader.ReadBytes(3);

			// A positive value denotes the number of clusters in an Index Buffer. A negative value denotes the amount of bytes and it uses the same algorithm for negative
			// numbers as the "Bytes or Clusters Per File Record Segment."
			byte bytesOrClustersPerIndexBuffer = reader.ReadByte();

			byte[] reserved8 = reader.ReadBytes(3);
			long volumeSerialNumber = reader.ReadInt64(); // A unique random number assigned to this partition, to keep things organized. 

			int checksum = reader.ReadInt32(); // checksum

			byte[] bootstrapCodeBlock = reader.ReadBytes(426);

			ushort endOfSectorMarker = reader.ReadUInt16();
			if (endOfSectorMarker != 0xAA55)
			{
				throw new InvalidDataFormatException("file does not contain end of sector marker");
			}

			SeekToCluster(masterFileTableClusterNumber);

			while (true)
			{
				bool retval = ReadFile(reader, out File file);
				if (!retval)
					break;

				if (file != null)
				{
					fsom.Files.Add(file);
				}
			}
		}

		private bool ReadFile(Reader reader, out File file)
		{
			long thisoffset = reader.Accessor.Position;

			string signature = reader.ReadFixedLengthString(4);
			if (signature != "FILE")
			{
				file = null;
				return false;
			}

			file = new File();

			ushort offsetToUpdateSequence = reader.ReadUInt16();
			ushort fixupEntryCount = reader.ReadUInt16();
			long logFileSequenceNumber = reader.ReadInt64();
			ushort sequenceNumber = reader.ReadUInt16();
			ushort hardLinkCount = reader.ReadUInt16();
			ushort firstAttributeOffset = reader.ReadUInt16();
			NTFSMftEntryFlags flags = (NTFSMftEntryFlags)reader.ReadUInt16();

			uint mftEntryUsedSize = reader.ReadUInt32();
			uint mftEntryAllocatedSize = reader.ReadUInt32();
			long baseFILErecordRef = reader.ReadInt64();
			ushort nextAttributeID = reader.ReadUInt16();
			ushort align = reader.ReadUInt16();
			uint mftRecordNumber = reader.ReadUInt32();

			reader.Accessor.Seek(thisoffset + firstAttributeOffset, SeekOrigin.Begin);

			// read the attributes
			while (true)
			{
				bool keepgoing = ReadAttribute(reader, out NTFSMftKnownAttribute type, out NTFSAttribute attr);
				if (attr == null && type != NTFSMftKnownAttribute.End)
				{
					Console.WriteLine("ue: ntfs: WARNING: skipping unknown attribute {0}", type);
				}

				if (type == NTFSMftKnownAttribute.Invalid)
				{
					Console.WriteLine("ue: ntfs: WARNING: caught 'invalid' attribute 0, panicking");
					break;
				}

				if (attr is NTFSFileNameAttribute)
				{
					NTFSFileNameAttribute att = (attr as NTFSFileNameAttribute);
					file.Name = att.FileName;
					file.Size = att.AllocatedSize;
				}

				if (!keepgoing)
					break;
			}

			// skip empty space
			reader.Accessor.Seek(thisoffset + mftEntryAllocatedSize, SeekOrigin.Begin);
			return true;
		}

		private bool ReadAttribute(Reader reader, out NTFSMftKnownAttribute type, out NTFSAttribute attr)
		{
			long thisoffset = reader.Accessor.Position;

			NTFSMftKnownAttribute attributeId = (NTFSMftKnownAttribute)reader.ReadInt32();
			type = attributeId;
			if (attributeId == NTFSMftKnownAttribute.End)
			{
				attr = null;
				return false;
			}

			uint attributeLength = reader.ReadUInt32();
			byte nonResidentFlag = reader.ReadByte();
			byte nameLength = reader.ReadByte();
			ushort nameOffset = reader.ReadUInt16();
			ushort flags = reader.ReadUInt16();

			ushort attributeIdentifier = reader.ReadUInt16();
			if (nonResidentFlag == 0)
			{
				uint contentSize = reader.ReadUInt32();
				ushort contentOffset = reader.ReadUInt16();

				reader.Seek(thisoffset + contentOffset, SeekOrigin.Begin);
				switch (attributeId)
				{
					case NTFSMftKnownAttribute.StandardInformation:
					{
						NTFSStandardInformationAttribute att = new NTFSStandardInformationAttribute();
						long creationDateTime = reader.ReadInt64();
						long modificationDateTime = reader.ReadInt64();
						long mftModificationDateTime = reader.ReadInt64();
						long readDateTime = reader.ReadInt64();
						int dosFilePermissions = reader.ReadInt32();
						int maxVersionCount = reader.ReadInt32();
						int versionNumber = reader.ReadInt32();
						int classId = reader.ReadInt32();
						int ownerId = reader.ReadInt32();
						int securityId = reader.ReadInt32();
						long quotaCharged = reader.ReadInt64();
						long updateSequenceNumber = reader.ReadInt64();
						attr = att;
						break;
					}
					case NTFSMftKnownAttribute.FileName:
					{
						NTFSFileNameAttribute att = new NTFSFileNameAttribute();
						long parentDirectoryFileref = reader.ReadInt64();
						long creationDateTime = reader.ReadInt64();
						att.CreationDateTime = new DateTime(creationDateTime);
						long modificationDateTime = reader.ReadInt64();
						att.ModificationDateTime = new DateTime(modificationDateTime);
						long mftModificationDateTime = reader.ReadInt64();
						long readDateTime = reader.ReadInt64();
						att.AccessDateTime = new DateTime(readDateTime);
						att.AllocatedSize = reader.ReadInt64();
						att.ActualSize = reader.ReadInt64();
						int fileNameFlags = reader.ReadInt32();
						int reparse = reader.ReadInt32();
						// int securityId = reader.ReadInt32();
						byte fileNameLength = reader.ReadByte();
						byte fileNameNamespace = reader.ReadByte();
						string fileNameUnicode = reader.ReadFixedLengthString(fileNameLength * 2, Encoding.UTF16LittleEndian);
						att.FileName = fileNameUnicode;

						attr = att;
						break;
					}
					case NTFSMftKnownAttribute.Data:
					{
						NTFSDataAttribute att = new NTFSDataAttribute();
						att.Data = reader.ReadBytes(attributeLength);
						attr = att;
						break;
					}
					case NTFSMftKnownAttribute.UnknownB0:
					{
						attr = null;
						break;
					}
					default:
					{
						attr = null;
						break;
					}
				}
			}
			else
			{
				long runlistStartingVirtualClusterNumber = reader.ReadInt64();
				long runlistEndingVirtualClusterNumber = reader.ReadInt64();
				ushort runlistOffset = reader.ReadUInt16();
				ushort compressionUnitSize = reader.ReadUInt16();
				uint unused = reader.ReadUInt32();
				ulong attributeContentAllocatedSize = reader.ReadUInt64();
				ulong attributeContentActualSize = reader.ReadUInt64();
				ulong attributeContentInitializedSize = reader.ReadUInt64();

				SeekToCluster(runlistStartingVirtualClusterNumber);

				attr = null;
			}

			long remaining = (thisoffset + attributeLength) - reader.Accessor.Position;
			if (remaining > 0)
			{
				reader.Seek(remaining, SeekOrigin.Current);
			}
			return true;
		}

		private void SeekToCluster(long clusterNumber)
		{
			Accessor.Seek(clusterNumber * BytesPerSector * SectorsPerCluster, SeekOrigin.Begin);
		}

		public short BytesPerSector { get; set; } = 0x0200;
		public byte SectorsPerCluster { get; set; } = 0x08;
		public NTFSMediaDescriptor MediaDescriptor { get; set; } = NTFSMediaDescriptor.HardDisk;
		public short SectorsPerTrack { get; set; } = 0x003F;
		public short NumberOfHeads { get; set; } = 0x00FF;
		public int HiddenSectors { get; set; } = 0x0000003F;

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = Accessor.Writer;
			writer.WriteBytes(new byte[] { 0xEB, 0x52, 0x90 });
			writer.WriteFixedLengthString("NTFS    ");

			writer.WriteInt16(BytesPerSector);
			writer.WriteByte(SectorsPerCluster);
			writer.WriteInt16(0x0000); // reservedSectors
			writer.WriteBytes(new byte[] { 0, 0, 0 });
			writer.WriteInt16(0x0000);
			writer.WriteByte((byte)MediaDescriptor);
			writer.WriteInt16(0x0000);
			writer.WriteInt16(SectorsPerTrack);
			writer.WriteInt16(NumberOfHeads);
			writer.WriteInt32(HiddenSectors);
			writer.WriteInt32(0x00000000);
			writer.WriteInt32(0x00800080); // reserved6
			writer.WriteInt64(0x00000000007FF54A); // totalSectors
			writer.WriteInt64(0x0000000000000004); // $MFT cluster number
			writer.WriteInt64(0x000000000007FF54); // $MFTMirr cluster number

			// A positive value denotes the number of clusters in a File Record Segment. A negative value denotes the amount of bytes in a File Record Segment, in which case the
			// size is 2 to the power of the absolute value. (0xF6 = -10 → 2^10 = 1024).
			writer.WriteByte(0xF6); // bytesOrClustersPerFileRecordSegment

			writer.WriteBytes(new byte[] { 0x00, 0x00, 0x00 });

			// A positive value denotes the number of clusters in an Index Buffer. A negative value denotes the amount of bytes and it uses the same algorithm for negative
			// numbers as the "Bytes or Clusters Per File Record Segment."
			writer.WriteByte(0x01);

			writer.WriteBytes(new byte[] { 0x00, 0x00, 0x00 });
			writer.WriteInt64(0x1C741BC9741BA514); // volumeSerialNumber

			writer.WriteInt32(0); // Checksum, unused

			byte[] bootstrapCodeBlock = new byte[426]; // The code that loads the rest of the operating system. This is pointed to by the first 3 bytes of this sector. 
			writer.WriteBytes(bootstrapCodeBlock);

			writer.WriteUInt16(0xAA55); // end-of-sector marker
		}

	}
}