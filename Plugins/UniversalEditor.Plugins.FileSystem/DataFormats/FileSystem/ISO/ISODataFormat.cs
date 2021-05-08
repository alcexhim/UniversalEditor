//
//  ISODataFormat.cs - provides a DataFormat for manipulating file systems in ISO format
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

namespace UniversalEditor.DataFormats.FileSystem.ISO
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating file systems in ISO format.
	/// </summary>
	public class ISODataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ContentTypes.Add("application/x-iso9660-image");

				_dfr.ExportOptions.Add(new CustomOptionText(nameof(SystemName), "System _name", String.Empty, 128));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(VolumeName), "_Volume name", String.Empty, 128));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(VolumeSetSize), "Nu_mber of volumes in this set", 1, 0, UInt16.MaxValue));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(VolumeSequenceNumber), "_Index of this volume in set", 0, 0, UInt16.MaxValue));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(VolumeSet), "Volume _set", String.Empty, 128));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Publisher), "_Publisher", String.Empty, 128));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(DataPreparer), "_Data preparer", String.Empty, 128));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Application), "_Application", "Universal Editor", 128));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(CopyrightFile), "_Copyright file", String.Empty, 38));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(AbstractFile), "Abs_tract file", String.Empty, 36));
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(BibliographicFile), "_Bibliographic file", String.Empty, 37));
			}
			return _dfr;
		}

		public string SystemName { get; set; } = String.Empty;
		public string VolumeName { get; set; } = String.Empty;
		public ushort VolumeSetSize { get; set; } = 0;
		public ushort VolumeSequenceNumber { get; set; } = 0;
		public ushort LogicalBlockSize { get; set; } = 2048;
		public string VolumeSet { get; set; } = String.Empty;
		public string Publisher { get; set; } = String.Empty;
		public string DataPreparer { get; set; } = String.Empty;
		public string Application { get; set; } = String.Empty;
		public string CopyrightFile { get; set; } = String.Empty;
		public string AbstractFile { get; set; } = String.Empty;
		public string BibliographicFile { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Reader br = base.Accessor.Reader;

			// skip system area (32768 bytes... WHY???)
			br.Accessor.Seek(32768, SeekOrigin.Begin);

			uint pathTableLocationTypeL = 0;
			uint pathTableLocationTypeM = 0;

			bool ok = true;
			while (ok)
			{
				ISOVolumeDescriptorType type = (ISOVolumeDescriptorType)br.ReadByte();
				string identifier = br.ReadFixedLengthString(5);
				byte version = br.ReadByte();

				switch (type)
				{
					case ISOVolumeDescriptorType.BootRecord:
					{
						break;
					}
					case ISOVolumeDescriptorType.Primary:
					{
						Internal.PrimaryVolumeDescriptor descriptor = ReadPrimaryVolumeDescriptor(br);
						SystemName = descriptor.systemName;
						VolumeName = descriptor.volumeName;
						VolumeSetSize = descriptor.volumeSetSize;
						VolumeSequenceNumber = descriptor.volumeSequenceNumber;
						LogicalBlockSize = descriptor.logicalBlockSize;
						VolumeSet = descriptor.volumeSet;
						Publisher = descriptor.publisher;
						DataPreparer = descriptor.dataPreparer;
						Application = descriptor.application;
						CopyrightFile = descriptor.copyrightFile;
						AbstractFile = descriptor.abstractFile;
						BibliographicFile = descriptor.bibliographicFile;
						pathTableLocationTypeL = descriptor.pathTableLocationTypeL;
						pathTableLocationTypeM = descriptor.pathTableLocationTypeM;
						break;
					}
					case ISOVolumeDescriptorType.Supplementary:
					{
						Internal.PrimaryVolumeDescriptor descriptor = ReadPrimaryVolumeDescriptor(br, true);
						SystemName = descriptor.systemName;
						VolumeName = descriptor.volumeName;
						VolumeSetSize = descriptor.volumeSetSize;
						VolumeSequenceNumber = descriptor.volumeSequenceNumber;
						LogicalBlockSize = descriptor.logicalBlockSize;
						VolumeSet = descriptor.volumeSet;
						Publisher = descriptor.publisher;
						DataPreparer = descriptor.dataPreparer;
						Application = descriptor.application;
						CopyrightFile = descriptor.copyrightFile;
						AbstractFile = descriptor.abstractFile;
						BibliographicFile = descriptor.bibliographicFile;
						pathTableLocationTypeL = descriptor.pathTableLocationTypeL;
						pathTableLocationTypeM = descriptor.pathTableLocationTypeM;

						// ushort unknown1 = br.ReadUInt16();
						// string unknown2 = br.ReadFixedLengthString(64, IO.Encoding.UTF16LittleEndian);
						break;
					}
					case ISOVolumeDescriptorType.Terminator:
					{
						break;
					}
					default:
					{
						ok = false;
						break;
					}
				}
				if (type == ISOVolumeDescriptorType.Terminator) break;
			}

			// go to the little-endian path table sector and read it
			br.Accessor.Seek(LogicalBlockSize * pathTableLocationTypeL, SeekOrigin.Begin);

			// go to the big-endian path table sector and read it
			br.Accessor.Seek(LogicalBlockSize * pathTableLocationTypeM, SeekOrigin.Begin);



			// our test file (C:\TEMP\ISOTEST\test-one-file-noextensions.iso) has file table in the
			// sector right after pathTableLocationTypeM
			br.Accessor.Seek(LogicalBlockSize * 20, SeekOrigin.Begin);

			for (int i = 0; i < 2; i++)
			{
				ushort unknown1 = br.ReadUInt16();
				uint unknown2 = br.ReadDoubleEndianUInt32();
				uint unknown3 = br.ReadDoubleEndianUInt32();
				ulong unknown4 = br.ReadUInt64();
				uint unknown5 = br.ReadUInt32();
				ushort unknown6 = br.ReadUInt16();
				ushort unknown7 = br.ReadUInt16();
			}

			while (true)
			{                                                       // file 1		file 2
																	// length of this record entry
				ushort recordLength = br.ReadUInt16();              // 48			42
				if (recordLength == 0) break;

				// index of first sector (offset is firstSector * mvarLogicalBlockSize
				uint firstSector = br.ReadDoubleEndianUInt32();     // 21			22

				// actual size of the file
				uint dataLength = br.ReadDoubleEndianUInt32();      // 44			20

				ulong unknown4 = br.ReadUInt64();                   // ...
				ushort unknown5 = br.ReadUInt16();                  // 0			0
				ushort unknown6 = br.ReadUInt16();                  // 1			1
				ushort unknown7 = br.ReadUInt16();                  // 256			256

				string fileName = br.ReadLengthPrefixedString();

				// align the reader to a multiple of 2 bytes
				br.Align(2);

				File file = fsom.AddFile(fileName);
				file.Size = dataLength;
				file.Properties.Add("reader", br);
				file.Properties.Add("sector", firstSector);
				file.Properties.Add("length", dataLength);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint sector = (uint)file.Properties["sector"];
			uint length = (uint)file.Properties["length"];

			reader.Seek((long)(LogicalBlockSize * sector), SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}

		private Internal.PrimaryVolumeDescriptor ReadPrimaryVolumeDescriptor(IO.Reader br, bool secondary = false)
		{
			Internal.PrimaryVolumeDescriptor descriptor = new Internal.PrimaryVolumeDescriptor();

			Encoding old = Accessor.DefaultEncoding;
			if (secondary)
			{
				Accessor.DefaultEncoding = Encoding.UTF16BigEndian;
			}

			descriptor.unused1 = br.ReadByte(); // Always 0x00.
			br.Endianness = Endianness.BigEndian;

			descriptor.systemName = br.ReadFixedLengthString(32); // The name of the system that can act upon sectors 0x00-0x0F for the volume.
			descriptor.volumeName = br.ReadFixedLengthString(32); // Identification of this volume.
			descriptor.unused2 = br.ReadUInt64(); // All zeroes.
			descriptor.volumeSpaceSize = br.ReadDoubleEndianUInt32();
			descriptor.unused3 = br.ReadBytes(32);
			descriptor.volumeSetSize = br.ReadDoubleEndianUInt16();
			descriptor.volumeSequenceNumber = br.ReadDoubleEndianUInt16();
			descriptor.logicalBlockSize = br.ReadDoubleEndianUInt16();
			descriptor.pathTableSize = br.ReadDoubleEndianUInt32();

			descriptor.pathTableLocationTypeL = br.ReadUInt32();
			descriptor.optionalPathTableLocationTypeL = br.ReadUInt32();

			br.Endianness = IO.Endianness.BigEndian;
			descriptor.pathTableLocationTypeM = br.ReadUInt32();
			descriptor.optionalPathTableLocationTypeM = br.ReadUInt32();
			br.Endianness = IO.Endianness.LittleEndian;

			descriptor.rootDirectoryEntry = br.ReadBytes(34);
			br.Accessor.Position -= 34;

			#region Root Directory
			ushort rootDirectoryEntrySize = br.ReadUInt16();
			uint unknown1 = br.ReadDoubleEndianUInt32();
			uint unknown2 = br.ReadDoubleEndianUInt32();
			byte[] unknown3 = br.ReadBytes(16);
			#endregion

			descriptor.volumeSet = br.ReadFixedLengthString(128);
			descriptor.publisher = br.ReadFixedLengthString(128);
			descriptor.dataPreparer = br.ReadFixedLengthString(128);
			descriptor.application = br.ReadFixedLengthString(128);
			descriptor.copyrightFile = br.ReadFixedLengthString(37);
			descriptor.abstractFile = br.ReadFixedLengthString(37);
			descriptor.bibliographicFile = br.ReadFixedLengthString(37);

			descriptor.timestampVolumeCreation = ReadDECDateTime(br);
			descriptor.timestampVolumeModification = ReadDECDateTime(br);
			descriptor.timestampVolumeExpiration = ReadDECDateTime(br);
			descriptor.timestampVolumeEffective = ReadDECDateTime(br);

			descriptor.fileStructureVersion = br.ReadByte();
			descriptor.unused4 = br.ReadByte();
			descriptor.applicationSpecific = br.ReadBytes(512);
			descriptor.reserved = br.ReadBytes(653);

			Accessor.DefaultEncoding = old;
			return descriptor;
		}

		private DateTime? ReadDECDateTime(IO.Reader br)
		{
			string szYear = br.ReadFixedLengthString(4);              // Year from 1 to 9999.
			string szMonth = br.ReadFixedLengthString(2);             // Month from 1 to 12.
			string szDay = br.ReadFixedLengthString(2);               // Day from 1 to 31.
			string szHour = br.ReadFixedLengthString(2);              // Hour from 0 to 23.
			string szMinute = br.ReadFixedLengthString(2);            // Minute from 0 to 59.
			string szSecond = br.ReadFixedLengthString(2);            // Second from 0 to 59.
			string szHundredSeconds = br.ReadFixedLengthString(2);    // Hundredths of a second from 0 to 99.

			// Time zone offset from GMT in 15 minute intervals, starting at interval -48 (west) and running up to
			// interval 52 (east). So value 0 indicates interval -48 which equals GMT-12 hours, and value 100
			// indicates interval 52 which equals GMT+13 hours.
			byte timeZoneOffset = br.ReadByte();

			try
			{
				int iYear = Int32.Parse(szYear);
				int iMonth = Int32.Parse(szMonth);
				int iDay = Int32.Parse(szDay);
				int iHour = Int32.Parse(szHour);
				int iMinute = Int32.Parse(szMinute);
				int iSecond = Int32.Parse(szSecond);
				int iHundredSeconds = Int32.Parse(szHundredSeconds);

				DateTime dt = new DateTime(iYear, iMonth, iDay, iHour, iMinute, iSecond, iHundredSeconds);
				return dt;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		private void WriteDECDateTime(IO.Writer bw, DateTime? dt)
		{
			if (dt == null)
			{
				bw.WriteFixedLengthString("0000000000000000");
			}
			else
			{
				bw.WriteFixedLengthString(dt.Value.Year.ToString(), 4, ' ');
				bw.WriteFixedLengthString(dt.Value.Month.ToString(), 2, ' ');
				bw.WriteFixedLengthString(dt.Value.Day.ToString(), 2, ' ');
				bw.WriteFixedLengthString(dt.Value.Hour.ToString(), 2, ' ');
				bw.WriteFixedLengthString(dt.Value.Minute.ToString(), 2, ' ');
				bw.WriteFixedLengthString(dt.Value.Second.ToString(), 2, ' ');
				bw.WriteFixedLengthString(dt.Value.Millisecond.ToString(), 2, ' ');
			}

			// Time zone offset from GMT in 15 minute intervals, starting at interval -48 (west) and running up to
			// interval 52 (east). So value 0 indicates interval -48 which equals GMT-12 hours, and value 100
			// indicates interval 52 which equals GMT+13 hours.
			byte timeZoneOffset = 0;

			if (dt != null)
			{
			}
			bw.WriteByte(timeZoneOffset);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;

			// skip system area
			bw.Accessor.Seek(32768, SeekOrigin.Current);

			Internal.PrimaryVolumeDescriptor descriptor = new Internal.PrimaryVolumeDescriptor();
			descriptor.systemName = SystemName; // The name of the system that can act upon sectors 0x00-0x0F for the volume.
			descriptor.volumeName = VolumeName; // Identification of this volume.
			descriptor.volumeSetSize = VolumeSetSize;
			descriptor.volumeSequenceNumber = VolumeSequenceNumber;
			descriptor.logicalBlockSize = LogicalBlockSize;
			WritePrimaryVolumeDescriptor(bw, descriptor);

			bw.Flush();
		}

		private void WritePrimaryVolumeDescriptor(IO.Writer bw, Internal.PrimaryVolumeDescriptor descriptor)
		{
			bw.WriteByte(descriptor.unused1);
			bw.WriteFixedLengthString(descriptor.systemName, 32, ' ');
			bw.WriteFixedLengthString(descriptor.volumeName, 32, ' ');
			bw.WriteUInt64(descriptor.unused2);
			bw.WriteDoubleEndianUInt32(descriptor.volumeSpaceSize);
			bw.WriteFixedLengthBytes(descriptor.unused3, 32);
			bw.WriteDoubleEndianUInt16(descriptor.volumeSetSize);
			bw.WriteDoubleEndianUInt16(descriptor.volumeSequenceNumber);
			bw.WriteDoubleEndianUInt16(descriptor.logicalBlockSize);
			bw.WriteDoubleEndianUInt32(descriptor.pathTableSize);

			bw.WriteUInt32(descriptor.pathTableLocationTypeL);
			bw.WriteUInt32(descriptor.optionalPathTableLocationTypeL);

			bw.Endianness = IO.Endianness.BigEndian;
			bw.WriteUInt32(descriptor.pathTableLocationTypeM);
			bw.WriteUInt32(descriptor.optionalPathTableLocationTypeM);
			bw.Endianness = IO.Endianness.LittleEndian;

			bw.WriteFixedLengthBytes(descriptor.rootDirectoryEntry, 34);

			bw.WriteFixedLengthString(descriptor.volumeSet, 128, ' ');
			bw.WriteFixedLengthString(descriptor.publisher, 128, ' ');
			bw.WriteFixedLengthString(descriptor.dataPreparer, 128, ' ');
			bw.WriteFixedLengthString(descriptor.application, 128, ' ');
			bw.WriteFixedLengthString(descriptor.copyrightFile, 38, ' ');
			bw.WriteFixedLengthString(descriptor.abstractFile, 36, ' ');
			bw.WriteFixedLengthString(descriptor.bibliographicFile, 37, ' ');

			WriteDECDateTime(bw, descriptor.timestampVolumeCreation);
			WriteDECDateTime(bw, descriptor.timestampVolumeModification);
			WriteDECDateTime(bw, descriptor.timestampVolumeExpiration);
			WriteDECDateTime(bw, descriptor.timestampVolumeEffective);
			bw.WriteByte(descriptor.fileStructureVersion);
			bw.WriteBytes(descriptor.unused3);
			bw.WriteFixedLengthBytes(descriptor.applicationSpecific, 512);
			bw.WriteFixedLengthBytes(descriptor.reserved, 653);
		}
	}
}
