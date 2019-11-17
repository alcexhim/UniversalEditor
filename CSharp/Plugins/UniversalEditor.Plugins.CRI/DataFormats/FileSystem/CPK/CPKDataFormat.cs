// Universal Editor file format module for SEGA UMD CPK archive files
// Copyright (C) 2011  Mike Becker
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.

using System;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Database;
using UniversalEditor.ObjectModels.FileSystem;

using UniversalEditor.Plugins.CRI.DataFormats.Database.UTF;

namespace UniversalEditor.Plugins.CRI.DataFormats.FileSystem.CPK
{
	public class CPKDataFormat : DataFormat
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

        protected override void LoadInternal (ref ObjectModel objectModel)
		{
			ObjectModels.FileSystem.FileSystemObjectModel fsom = (objectModel as ObjectModels.FileSystem.FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Reader br = Accessor.Reader;
        	
            // Rebuilt based on cpk_unpack
			// Rebuilt AGAIN based on github.com/esperknight/CriPakTools
			string CPK = br.ReadFixedLengthString (4);
			int unknown1 = br.ReadInt32();

			long utf_size = br.ReadInt64(); // size of UTF not including "@UTF"
			byte[] utf_data = br.ReadBytes(utf_size + 4);

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

			DatabaseTable dtUTF = utf_om.Tables[0];
			// UTF table parsing works now, so no need to hardcode toc offset - WOOHOO!!!
            ulong tocOffset = (ulong)dtUTF.Records[0].Fields["TocOffset"].Value;
            br.Seek((long)tocOffset, IO.SeekOrigin.Begin);

            string tocSignature = br.ReadFixedLengthString(4);
			unknown1 = br.ReadInt32();

			// UTF table for TOC
			utf_size = br.ReadInt64(); // size of UTF not including "@UTF"
			utf_data = br.ReadBytes(utf_size + 4);

			ma = new MemoryAccessor(utf_data);
			utf_signature = ma.Reader.ReadFixedLengthString(4);
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

			utf_om = new DatabaseObjectModel();
			Document.Load(utf_om, utf_df, ma);
			DatabaseTable dtUTF2 = utf_om.Tables[0];

            if (objectModel is DatabaseObjectModel)
            {
        		(objectModel as DatabaseObjectModel).Tables.Add (dtUTF);
        	}
			else if (objectModel is FileSystemObjectModel)
			{
        		for (int i = 0; i < dtUTF2.Records.Count; i++)
				{
					string dirName = (string)dtUTF2.Records[i].Fields["DirName"].Value;
					string fileTitle = (string)dtUTF2.Records[i].Fields["FileName"].Value;
					string fileName = fileTitle;
					if (!String.IsNullOrEmpty(dirName))
					{
						fileName = dirName + '/' + fileTitle;
					}

					uint decompressedLength = (uint)dtUTF2.Records[i].Fields["FileSize"].Value;
					uint compressedLength = (uint)dtUTF2.Records[i].Fields["ExtractSize"].Value;
					ulong offset = (ulong)dtUTF2.Records[i].Fields["FileOffset"].Value;
					ulong lContentOffset = (ulong)dtUTF.Records[0].Fields["TocOffset"].Value;
					offset += lContentOffset;

					File f = fsom.AddFile(fileName);
					f.Properties.Add("DecompressedLength", decompressedLength);
					f.Properties.Add("CompressedLength", compressedLength);
					f.Properties.Add("Offset", offset);
					f.Size = decompressedLength;
					f.DataRequest += f_DataRequest;
				}
			}
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			uint decompressedLength = (uint)f.Properties["DecompressedLength"];
			uint compressedLength = (uint)f.Properties["CompressedLength"];
			ulong offset = (ulong)f.Properties["Offset"];
			Reader br = base.Accessor.Reader;

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

		private DatabaseTable BuildHeaderUTF(FileSystemObjectModel fsom)
		{
			File[] files = fsom.GetAllFiles();

			DatabaseTable dt = new DatabaseTable();
			dt.Fields.Add("UpdateDateTime");
			dt.Fields.Add("FileSize");
			dt.Fields.Add("ContentOffset");
			dt.Fields.Add("ContentSize");
			dt.Fields.Add("TocOffset");
			dt.Fields.Add("TocSize");
			dt.Fields.Add("TocCrc");
			dt.Fields.Add("EtocOffset");
			dt.Fields.Add("EtocSize");
			dt.Fields.Add("ItocOffset");
			dt.Fields.Add("ItocSize");
			dt.Fields.Add("ItocCrc");
			dt.Fields.Add("GtocOffset");
			dt.Fields.Add("GtocSize");
			dt.Fields.Add("GtocCrc");
			dt.Fields.Add("EnabledPackedSize");
			dt.Fields.Add("EnabledDataSize");
			dt.Fields.Add("TotalDataSize");
			dt.Fields.Add("Tocs");
			dt.Fields.Add("Files");
			dt.Fields.Add("Groups");
			dt.Fields.Add("Attrs");
			dt.Fields.Add("TotalFiles");
			dt.Fields.Add("Directories");
			dt.Fields.Add("Updates");
			dt.Fields.Add("Version");
			dt.Fields.Add("Revision");
			dt.Fields.Add("Align");
			dt.Fields.Add("Sorted");
			dt.Fields.Add("EID");
			dt.Fields.Add("CpkMode");
			dt.Fields.Add("Tvers");
			dt.Fields.Add("Comment");
			dt.Fields.Add("Codec");
			dt.Fields.Add("DpkItoc");

			dt.Records.Add(new DatabaseRecord(new DatabaseField[]
			{
				new DatabaseField("UpdateDateTime", (ulong)1),
				new DatabaseField("FileSize", null),
				new DatabaseField("ContentOffset", (ulong)153600),
				new DatabaseField("ContentSize", (ulong)421693440),
				new DatabaseField("TocOffset", (ulong)2048),
				new DatabaseField("TocSize", (ulong)117896),
				new DatabaseField("TocCrc", null),
				new DatabaseField("EtocOffset", (ulong)421847040),
				new DatabaseField("EtocSize", (ulong)21768),
				new DatabaseField("ItocOffset", (ulong)131072),
				new DatabaseField("ItocSize", (ulong)21744),
				new DatabaseField("ItocCrc", null),
				new DatabaseField("GtocOffset", null),
				new DatabaseField("GtocSize", null),
				new DatabaseField("GtocCrc", null),
				new DatabaseField("EnabledPackedSize", (ulong)837335552),
				new DatabaseField("EnabledDataSize", (ulong)837335552),
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
				new DatabaseField("Align", (ushort)2048),
				new DatabaseField("Sorted", (ushort)1),
				new DatabaseField("EID", (ushort)1),
				new DatabaseField("CpkMode", (uint)2),
				new DatabaseField("Tvers", "CPKMC2.14.00, DLL2.74.00"),
				new DatabaseField("Comment", null),
				new DatabaseField("Codec", (uint)0),
				new DatabaseField("DpkItoc", (uint)0)
			}));

			return dt;
		}


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

		protected override void SaveInternal (ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			IO.Writer bw = Accessor.Writer;
			
			bw.Flush ();
			bw.Close ();
		}
	}
}

