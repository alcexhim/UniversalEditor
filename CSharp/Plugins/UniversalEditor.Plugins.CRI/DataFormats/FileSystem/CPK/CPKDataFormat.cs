//
//  CPKDataFormat.cs - implementation of CRI Middleware CPK archive
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.Plugins.CRI.DataFormats.Database.UTF;

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK
{
	/// <summary>
	/// A <see cref="DataFormat" /> for loading and saving <see cref="FileSystemObjectModel" /> archives in CRI Middleware CPK format.
	/// </summary>
	public class CPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		/// <summary>
		/// Creates a <see cref="DataFormatReference" /> containing metadata about the <see cref="CPKDataFormat" />.
		/// </summary>
		/// <returns>The <see cref="DataFormatReference" /> which contains metadata about the <see cref="CPKDataFormat" />.</returns>
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionText(nameof(VersionString), "_Version string", "CPKMC2.14.00, DLL2.74.00"));
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(SectorAlignment), "Sector _alignment", 2048, 0, 2048));
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets the version string which contains information about the library which created the archive. The default value is "CPKMC2.14.00, DLL2.74.00" which is the version string that official CRI Middleware CPK tools use.
		/// </summary>
		/// <value>The version string.</value>
		public string VersionString { get; set; } = "CPKMC2.14.00, DLL2.74.00";

		/// <summary>
		/// Gets or sets the sector alignment, in bytes, of the CPK archive. The default value is 2048.
		/// </summary>
		/// <value>The file alignment.</value>
		public int SectorAlignment { get; set; } = 2048;

		// these are mainly for the benefit of the CRI Extensions for FileSystemEditor

		private byte[] _HeaderData = null;
		/// <summary>
		/// Returns the raw data from the initial "CPK " chunk of this file, or NULL if this chunk does not exist.
		/// </summary>
		/// <value>The raw data from the "CPK " chunk.</value>
		public byte[] HeaderData { get { return _HeaderData; } }

		private byte[] _TocData = null;
		/// <summary>
		/// Returns the raw data from the "TOC " chunk of this file, or NULL if this chunk does not exist.
		/// </summary>
		/// <value>The raw data from the "TOC " chunk.</value>
		public byte[] TocData { get { return _TocData; } }

		private byte[] _ITocData = null;
		/// <summary>
		/// Returns the raw data from the "ITOC" chunk of this file, or NULL if this chunk does not exist.
		/// </summary>
		/// <value>The raw data from the "ITOC" chunk.</value>
		public byte[] ITocData { get { return _ITocData; } }

		private byte[] _GTocData = null;
		/// <summary>
		/// Returns the raw data from the "GTOC" chunk of this file, or NULL if this chunk does not exist.
		/// </summary>
		/// <value>The raw data from the "GTOC" chunk.</value>
		public byte[] GTocData { get { return _GTocData; } }

		private byte[] _ETocData = null;
		/// <summary>
		/// Returns the raw data from the final "ETOC" chunk of this file, or NULL if this chunk does not exist.
		/// </summary>
		/// <value>The raw data from the "ETOC" chunk.</value>
		public byte[] ETocData { get { return _ETocData; } }

		/// <summary>
		/// Loads the <see cref="ObjectModel" /> data from the input <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> into which to load archive content.</param>
		protected override void LoadInternal (ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = Accessor.Reader;

			// Rebuilt based on cpk_unpack
			// Rebuilt AGAIN based on github.com/esperknight/CriPakTools
			DatabaseObjectModel utf_om = ReadUTF("CPK ", br, out _HeaderData);

			DatabaseTable dtUTF = utf_om.Tables[0];
			// UTF table parsing works now, so no need to hardcode toc offset - WOOHOO!!!
            ulong tocOffset = (ulong)dtUTF.Records[0].Fields["TocOffset"].Value;
            br.Seek((long)tocOffset, IO.SeekOrigin.Begin);

			utf_om = ReadUTF("TOC ", br, out _TocData);

			DatabaseTable dtUTFTOC = utf_om.Tables[0];

			ulong itocOffset = (ulong)dtUTF.Records[0].Fields["ItocOffset"].Value;
			br.Seek((long)itocOffset, IO.SeekOrigin.Begin);

			utf_om = ReadUTF("ITOC", br, out _ITocData);

			DatabaseTable dtUTFITOC = utf_om.Tables[0];

			if (objectModel is DatabaseObjectModel)
            {
        		(objectModel as DatabaseObjectModel).Tables.Add (dtUTF);
        	}
			else if (objectModel is FileSystemObjectModel)
			{
        		for (int i = 0; i < dtUTFTOC.Records.Count; i++)
				{
					string dirName = (string)dtUTFTOC.Records[i].Fields["DirName"].Value;
					string fileTitle = (string)dtUTFTOC.Records[i].Fields["FileName"].Value;
					string fileName = fileTitle;
					if (!String.IsNullOrEmpty(dirName))
					{
						fileName = dirName + '/' + fileTitle;
					}

					uint decompressedLength = (uint)dtUTFTOC.Records[i].Fields["FileSize"].Value;
					uint compressedLength = (uint)dtUTFTOC.Records[i].Fields["ExtractSize"].Value;
					ulong offset = (ulong)dtUTFTOC.Records[i].Fields["FileOffset"].Value;
					ulong lContentOffset = (ulong)dtUTF.Records[0].Fields["TocOffset"].Value;
					offset += lContentOffset;

					File f = fsom.AddFile(fileName);

					if (dtUTFTOC.Records[i].Fields["ID"] != null)
						f.Properties.Add("ID", (uint) dtUTFTOC.Records[i].Fields["ID"].Value);

					f.Properties.Add("DecompressedLength", decompressedLength);
					f.Properties.Add("CompressedLength", compressedLength);
					f.Properties.Add("Offset", offset);
					f.Properties.Add("Reader", br);
					f.Size = decompressedLength;
					f.DataRequest += f_DataRequest;
				}
			}

			ulong etocOffset = (ulong)dtUTF.Records[0].Fields["EtocOffset"].Value;
			br.Seek((long)etocOffset, SeekOrigin.Begin);

			utf_om = ReadUTF("ETOC", br, out _ETocData);
			DatabaseTable dtUTFETOC = utf_om.Tables[0];

			if (objectModel is DatabaseObjectModel)
			{
				(objectModel as DatabaseObjectModel).Tables.Add(dtUTFETOC);
			}
			else if (objectModel is FileSystemObjectModel)
			{
				for (int i = 0; i < dtUTFETOC.Records.Count; i++)
				{
					ulong updateDateTime = (ulong)dtUTFETOC.Records[i].Fields["UpdateDateTime"].Value;
					string localDir = (string)dtUTFETOC.Records[i].Fields["LocalDir"].Value;

					if (i >= fsom.Files.Count)
						continue;

					File f = fsom.Files[i];

					byte[] updateDateTimeBytes = BitConverter.GetBytes(updateDateTime);
					// remember, the CPK is big-endian, but the UTF is little-endian
					ushort updateDateTimeYear = BitConverter.ToUInt16(new byte[] { updateDateTimeBytes[6], updateDateTimeBytes[7] }, 0);
					byte updateDateTimeMonth = updateDateTimeBytes[5];
					byte updateDateTimeDay = updateDateTimeBytes[4];
					byte updateDateTimeHour = updateDateTimeBytes[3];
					byte updateDateTimeMinute = updateDateTimeBytes[2];
					byte updateDateTimeSecond = updateDateTimeBytes[1];
					byte updateDateTimeMs = updateDateTimeBytes[0];

					f.ModificationTimestamp = new DateTime(updateDateTimeYear, updateDateTimeMonth, updateDateTimeDay, updateDateTimeHour, updateDateTimeMinute, updateDateTimeSecond, updateDateTimeMs);
				}
			}
		}

		private DatabaseObjectModel ReadUTF(string v, Reader br, out byte[] data)
		{
			string tocSignature = br.ReadFixedLengthString(4);
			if (tocSignature != v)
				throw new InvalidDataFormatException();

			int unknown1 = br.ReadInt32();

			// UTF table for TOC
			long utf_size = br.ReadInt64(); // size of UTF not including "@UTF"
			byte[] utf_data = br.ReadBytes(utf_size + 8);

			MemoryAccessor ma = new MemoryAccessor(utf_data);
			string utf_signature = ma.Reader.ReadFixedLengthString(4);
			if (utf_signature != "@UTF")
			{
				// encrypted?
				utf_data = DecryptUTF(utf_data);
				ma = new MemoryAccessor(utf_data);
			}
			else
			{
				ma.Reader.Seek(-4, IO.SeekOrigin.Current);
			}

			UTFDataFormat utf_df = new UTFDataFormat();
			DatabaseObjectModel utf_om = new DatabaseObjectModel();
			Document.Load(utf_om, utf_df, ma);

			data = utf_data;
			return utf_om;
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			uint decompressedLength = (uint)f.Properties["DecompressedLength"];
			uint compressedLength = (uint)f.Properties["CompressedLength"];
			ulong offset = (ulong)f.Properties["Offset"];
			Reader br = (Reader)f.Properties["Reader"];

			br.Accessor.Position = (long)offset;

			byte[] decompressedData = null;
			if (compressedLength == 0)
			{
				decompressedData = br.ReadBytes(decompressedLength);
			}
			else
			{
				byte[] compressedData = br.ReadBytes(compressedLength);
				decompressedData = /*compress()*/ compressedData;
			}

			e.Data = decompressedData;
		}

		private DatabaseObjectModel BuildHeaderUTF(FileSystemObjectModel fsom, int tocsize, ulong contentOffset, ulong contentSize, ulong etocOffset, ulong etocLength, ulong itocOffset, ulong itocLength)
		{
			File[] files = fsom.GetAllFiles();

			DatabaseTable dt = new DatabaseTable();
			dt.Name = "CpkHeader";
			dt.Fields.Add("UpdateDateTime", null, typeof(Int64));
			dt.Fields.Add("FileSize", null, typeof(Int64));
			dt.Fields.Add("ContentOffset", null, typeof(Int64));
			dt.Fields.Add("ContentSize", null, typeof(Int64));
			dt.Fields.Add("TocOffset", null, typeof(Int64));
			dt.Fields.Add("TocSize", null, typeof(Int64));
			dt.Fields.Add("TocCrc", null, typeof(uint));
			dt.Fields.Add("EtocOffset", null, typeof(Int64));
			dt.Fields.Add("EtocSize", null, typeof(Int64));
			dt.Fields.Add("ItocOffset", null, typeof(Int64));
			dt.Fields.Add("ItocSize", null, typeof(Int64));
			dt.Fields.Add("ItocCrc", null, typeof(uint));
			dt.Fields.Add("GtocOffset", null, typeof(Int64));
			dt.Fields.Add("GtocSize", null, typeof(Int64));
			dt.Fields.Add("GtocCrc", null, typeof(uint));
			dt.Fields.Add("EnabledPackedSize", null, typeof(Int64));
			dt.Fields.Add("EnabledDataSize", null, typeof(Int64));
			dt.Fields.Add("TotalDataSize", null, typeof(Int64));
			dt.Fields.Add("Tocs", null, typeof(uint));
			dt.Fields.Add("Files", null, typeof(uint));
			dt.Fields.Add("Groups", null, typeof(uint));
			dt.Fields.Add("Attrs", null, typeof(uint));
			dt.Fields.Add("TotalFiles", null, typeof(uint));
			dt.Fields.Add("Directories", null, typeof(uint));
			dt.Fields.Add("Updates", null, typeof(uint));
			dt.Fields.Add("Version", null, typeof(Int16));
			dt.Fields.Add("Revision", null, typeof(Int16));
			dt.Fields.Add("Align", null, typeof(Int16));
			dt.Fields.Add("Sorted", null, typeof(Int16));
			dt.Fields.Add("EID", null, typeof(Int16));
			dt.Fields.Add("CpkMode", null, typeof(uint));
			dt.Fields.Add("Tvers", null, typeof(string));
			dt.Fields.Add("Comment", null, typeof(string));
			dt.Fields.Add("Codec", null, typeof(uint));
			dt.Fields.Add("DpkItoc", null, typeof(uint));

			// cri, go home, you're drunk
			ulong enabledPackedSize = 0;
			for (uint i = 0; i < files.Length; i++)
			{
				enabledPackedSize += (ulong) files[i].Size;
			}
			enabledPackedSize *= 2;

			ulong enabledDataSize = enabledPackedSize;

			dt.Records.Add(new DatabaseRecord(new DatabaseField[]
			{
				new DatabaseField("UpdateDateTime", (ulong)1),
				new DatabaseField("FileSize", null),
				new DatabaseField("ContentOffset", contentOffset), // 18432 , should be 20480
				new DatabaseField("ContentSize", contentSize),		// 8217472, should be 8564736 (347264 difference!)
				new DatabaseField("TocOffset", (ulong)SectorAlignment),
				new DatabaseField("TocSize", (ulong)tocsize),
				new DatabaseField("TocCrc", null),
				new DatabaseField("EtocOffset", etocOffset),
				new DatabaseField("EtocSize", etocLength),
				new DatabaseField("ItocOffset", itocOffset),
				new DatabaseField("ItocSize", itocLength),
				new DatabaseField("ItocCrc", null),
				new DatabaseField("GtocOffset", null),
				new DatabaseField("GtocSize", null),
				new DatabaseField("GtocCrc", null),
				new DatabaseField("EnabledPackedSize", enabledPackedSize),		//16434944 in diva2script.cpk
				new DatabaseField("EnabledDataSize", enabledDataSize),
				new DatabaseField("TotalDataSize", null),
				new DatabaseField("Tocs", null),
				new DatabaseField("Files", (uint)files.Length),
				new DatabaseField("Groups", (uint)0),
				new DatabaseField("Attrs", (uint)0),
				new DatabaseField("TotalFiles", null),
				new DatabaseField("Directories", null),
				new DatabaseField("Updates", null),
				new DatabaseField("Version", (ushort)7),
				new DatabaseField("Revision", (ushort)0),
				new DatabaseField("Align", (ushort)SectorAlignment),
				new DatabaseField("Sorted", (ushort)1),
				new DatabaseField("EID", (ushort)1),
				new DatabaseField("CpkMode", (uint)2),
				new DatabaseField("Tvers", VersionString),
				new DatabaseField("Comment", null),
				new DatabaseField("Codec", (uint)0),
				new DatabaseField("DpkItoc", (uint)0)
			}));

			DatabaseObjectModel db = new DatabaseObjectModel();
			db.Tables.Add(dt);
			return db;
		}

		private ulong GetUtfTableSize(DatabaseTable dt)
		{
			ulong size = (ulong)(32 + (dt.Fields.Count * 5));
			for (int i = 0; i < dt.Fields.Count; i++)
			{
				if (dt.Fields[i].Value == null)
				{
					// perrow
					for (int j = 0; j < dt.Records.Count; j++)
					{
						size += (ulong)UTFDataFormat.GetLengthForDataType(UTFDataFormat.UTFDataTypeForSystemDataType(dt.Fields[i].DataType));
					}
				}
			}
			return size;
		}

		private struct IDOFFSET
		{
			public int INDEX;
			public uint ID;
			public ulong OFFSET;
			public ulong SIZE;

			public IDOFFSET(int index, uint id, ulong offset, ulong size)
			{
				INDEX = index;
				ID = id;
				OFFSET = offset;
				SIZE = size;
			}
		}

		private DatabaseObjectModel BuildTocUTF(File[] files, ulong initialFileOffset, ref IDOFFSET[] sortedOffsets)
		{
			DatabaseTable dt = new DatabaseTable();
			dt.Name = "CpkTocInfo";
			dt.Fields.Add("DirName", String.Empty, typeof(string));
			dt.Fields.Add("FileName", null, typeof(string));
			dt.Fields.Add("FileSize", null, typeof(uint));
			dt.Fields.Add("ExtractSize", null, typeof(uint));
			dt.Fields.Add("FileOffset", null, typeof(ulong));
			dt.Fields.Add("ID", null, typeof(uint));
			dt.Fields.Add("UserString", "<NULL>", typeof(string));

			ulong offset = initialFileOffset;
			offset -= (ulong) SectorAlignment; // idk?

			List<IDOFFSET> offsets = new List<IDOFFSET>(files.Length);
			for (int i = 0; i < files.Length; i++)
			{
				offsets.Add(new IDOFFSET(i, files[i].GetProperty<uint>("ID", (uint)0), 0, (ulong) files[i].Size));
			}
			offsets.Sort((x, y) => x.ID.CompareTo(y.ID));

			sortedOffsets = offsets.ToArray();

			for (int i = 0; i < files.Length; i++)
			{
				offsets[i] = new IDOFFSET(offsets[i].INDEX, offsets[i].ID, offset, offsets[i].SIZE);
				offset += offsets[i].SIZE;
				offset = offset.RoundUp(SectorAlignment);
			}
			offsets.Sort((x, y) => x.INDEX.CompareTo(y.INDEX));

			for (int i = 0; i < files.Length; i++)
			{
				dt.Records.Add(new DatabaseRecord(new DatabaseField[]
				{
					new DatabaseField("DirName", null),
					new DatabaseField("FileName", files[i].Name),
					new DatabaseField("FileSize", (uint)files[i].Size),
					new DatabaseField("ExtractSize", (uint)files[i].Size),
					new DatabaseField("FileOffset", offsets[i].OFFSET),
					new DatabaseField("ID", files[i].GetProperty<uint>("ID", (uint)i)),
					new DatabaseField("UserString", "<NULL>")
				}));
			}

			DatabaseObjectModel db = new DatabaseObjectModel();
			db.Tables.Add(dt);
			return db;
		}
		private DatabaseObjectModel BuildEtocUTF(File[] files)
		{
			DatabaseTable dt = new DatabaseTable();
			dt.Name = "CpkEtocInfo";
			dt.Fields.Add("UpdateDateTime", null, typeof(ulong));
			dt.Fields.Add("LocalDir", String.Empty, typeof(string));

			for (int i = 0; i < files.Length; i++)
			{
				DateTime updateDateTime = files[i].ModificationTimestamp;

				// yeaaaaahhh
				byte[] updateDateTimeBytes = new byte[8];
				updateDateTimeBytes[0] = (byte)updateDateTime.Millisecond;
				updateDateTimeBytes[1] = (byte)updateDateTime.Second;
				updateDateTimeBytes[2] = (byte)updateDateTime.Minute;
				updateDateTimeBytes[3] = (byte)updateDateTime.Hour;
				updateDateTimeBytes[4] = (byte)updateDateTime.Day;
				updateDateTimeBytes[5] = (byte)updateDateTime.Month;

				byte[] updateDateTimeYear = BitConverter.GetBytes((ushort)updateDateTime.Year);
				updateDateTimeBytes[6] = updateDateTimeYear[0];
				updateDateTimeBytes[7] = updateDateTimeYear[1];

				dt.Records.Add(new DatabaseRecord(new DatabaseField[]
				{
					new DatabaseField("UpdateDateTime", BitConverter.ToUInt64(updateDateTimeBytes, 0)),
					new DatabaseField("LocalDir", null)
				}));
			}

			dt.Records.Add(new DatabaseRecord(new DatabaseField[]
			{
				new DatabaseField("UpdateDateTime", (ulong)0),
				new DatabaseField("LocalDir", null)
			}));

			DatabaseObjectModel db = new DatabaseObjectModel();
			db.Tables.Add(dt);
			return db;
		}
		private DatabaseObjectModel BuildItocUTF(IDOFFSET[] entries)
		{
			DatabaseTable dt = new DatabaseTable();
			dt.Name = "CpkExtendId";
			dt.Fields.Add("ID", null, typeof(int));
			dt.Fields.Add("TocIndex", null, typeof(int));

			for (int i = 0; i < entries.Length; i++)
			{
				dt.Records.Add(new DatabaseRecord(new DatabaseField[]
				{
					new DatabaseField("ID", entries[i].ID),
					new DatabaseField("TocIndex", entries[i].INDEX)
				}));
			}

			DatabaseObjectModel db = new DatabaseObjectModel();
			db.Tables.Add(dt);
			return db;
		}

		/// <summary>
		/// Applies a simple cipher to decrypt an encrypted UTF sector.
		/// </summary>
		/// <returns>The decrypted data.</returns>
		/// <param name="input">The data to decrypt.</param>
		private byte[] DecryptUTF(byte[] input)
		{
			byte[] result = new byte[input.Length];

			int m = 0x0000655f, t = 0x00004115;
			for (int i = 0; i < input.Length; i++)
			{
				byte d = input[i];
				d = (byte)(d ^ (byte)(m & 0xff));
				result[i] = d;
				m *= t;
			}

			return result;
		}

		/// <summary>
		/// Writes the <see cref="ObjectModel" /> data to the output <see cref="Accessor" />.
		/// </summary>
		/// <param name="objectModel">A <see cref="FileSystemObjectModel" /> containing the archive content to write.</param>
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();
			
			IO.Writer bw = Accessor.Writer;
			bw.WriteFixedLengthString("CPK ");

			bw.WriteInt32(255); // unknown1

			UTFDataFormat dfUTF = new UTFDataFormat();

			ulong contentOffset = 16;
			ulong etocLength = 0;
			ulong itocLength = 0;
			ulong headerLength = 0;
			ulong tocLength = 0;

			IDOFFSET[] sortedOffsets = null;
			{
				// TODO: replace all these calls to build methods with a simple calculation function e.g. UTFDataFormat.GetTableSize() ...
				// there is no reason to go through the entire file list just to calculate how big a table should be - mind those variable-length strings though
				DatabaseObjectModel _tmp_om = BuildHeaderUTF(fsom, 0, 0, 0, 0, 0, 0, 0);		// 704
				MemoryAccessor _tmp_ma = new MemoryAccessor();
				Document.Save(_tmp_om, dfUTF, _tmp_ma);
				contentOffset += (ulong)_tmp_ma.Length;
				contentOffset = contentOffset.RoundUp(SectorAlignment);

				headerLength = (ulong) _tmp_ma.Length;

				_tmp_om = BuildTocUTF(files, 0, ref sortedOffsets);								// 117880
				_tmp_ma = new MemoryAccessor();
				Document.Save(_tmp_om, dfUTF, _tmp_ma);

				contentOffset += 16;
				contentOffset += (ulong)_tmp_ma.Length;
				contentOffset = contentOffset.RoundUp(SectorAlignment);
				tocLength = (ulong) _tmp_ma.Length;

				_tmp_om = BuildItocUTF(sortedOffsets);
				_tmp_ma = new MemoryAccessor();
				Document.Save(_tmp_om, dfUTF, _tmp_ma);

				contentOffset = contentOffset.RoundToNearestPowerOf2(); // this is done before the ITOC, apparently.

				contentOffset += 16;
				contentOffset += (ulong)_tmp_ma.Length;
				contentOffset = contentOffset.RoundUp(SectorAlignment);
				itocLength = (ulong) _tmp_ma.Length;											// 21728

				_tmp_om = BuildEtocUTF(files);
				_tmp_ma = new MemoryAccessor();
				Document.Save(_tmp_om, dfUTF, _tmp_ma);
				// contentOffset += (ulong)_tmp_ma.Length;									// lol wtf contentoffset isn't affected by ETOC...
				etocLength = (ulong) _tmp_ma.Length;											// 21752
			}

			DatabaseObjectModel utfTOC = BuildTocUTF(files, contentOffset, ref sortedOffsets);

			MemoryAccessor maUTFTOC = new MemoryAccessor();
			Document.Save(utfTOC, dfUTF, maUTFTOC);

			byte[] utfTOC_data = maUTFTOC.ToArray();


			ulong contentSize = 0;
			for (uint i = 0; i < files.Length; i++)
			{
				contentSize += (ulong)files[i].Size;
				contentSize = contentSize.RoundUp(SectorAlignment);
			}

			ulong itocOffset = 16 + headerLength;
			itocOffset = itocOffset.RoundUp(SectorAlignment);
			itocOffset += (16 + tocLength);
			itocOffset = itocOffset.RoundUp(SectorAlignment);

			itocOffset = itocOffset.RoundToNearestPowerOf2();

			ulong etocOffset = 0;
			etocOffset = contentOffset + contentSize;

			DatabaseObjectModel utfHeader = BuildHeaderUTF(fsom, utfTOC_data.Length + 16 /*includes 16-byte 'TOC ' header from CPK*/, contentOffset, contentSize, etocOffset, etocLength + 16, itocOffset, itocLength + 16);
			MemoryAccessor maUTFHeader = new MemoryAccessor();
			Document.Save(utfHeader, dfUTF, maUTFHeader);

			byte[] utfHeader_data = maUTFHeader.ToArray();
			bw.WriteInt64(utfHeader_data.Length);
			bw.WriteBytes(utfHeader_data);

			bw.Align(SectorAlignment);
			bw.Accessor.Seek(-6, SeekOrigin.Current);
			bw.WriteFixedLengthString("(c)CRI");

			bw.WriteFixedLengthString("TOC ");
			bw.WriteInt32(255); // unknown1

			bw.WriteInt64(utfTOC_data.Length);
			bw.WriteBytes(utfTOC_data);

			bw.Align(SectorAlignment);

			// here comes the ITOC (indexes TOC) UTF table chunk.
			DatabaseObjectModel utfITOC = BuildItocUTF(sortedOffsets);
			MemoryAccessor maUTFITOC = new MemoryAccessor();
			Document.Save(utfITOC, dfUTF, maUTFITOC);

			byte[] utfITOC_data = maUTFITOC.ToArray();
			bw.Accessor.Seek(bw.Accessor.Position.RoundToNearestPowerOf2(), SeekOrigin.Begin);

			bw.WriteFixedLengthString("ITOC");
			bw.WriteInt32(255);
			bw.WriteInt64(utfITOC_data.Length);
			bw.WriteBytes(utfITOC_data);

			// here comes the file data. each file is aligned to FileAlignment bytes, apparently.
			bw.Align(SectorAlignment);
			for (uint i = 0; i < sortedOffsets.Length; i++)
			{
				bw.WriteBytes(files[sortedOffsets[i].INDEX].GetData());
				bw.Align(SectorAlignment);
			}

			DatabaseObjectModel utfETOC = BuildEtocUTF(files);
			MemoryAccessor maUTFETOC = new MemoryAccessor();
			Document.Save(utfETOC, dfUTF, maUTFETOC);

			byte[] utfETOC_data = maUTFETOC.ToArray();
			bw.Align(SectorAlignment);
			bw.WriteFixedLengthString("ETOC");
			bw.WriteInt32(255);
			bw.WriteInt64(utfETOC_data.Length);
			bw.WriteBytes(utfETOC_data);
		}
	}
}

