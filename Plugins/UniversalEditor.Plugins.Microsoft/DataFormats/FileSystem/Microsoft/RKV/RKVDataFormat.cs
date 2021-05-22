//
//  RKVDataFormat.cs - implements Microsoft XNA RKV archive format
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework.Settings;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.RKV
{
	/// <summary>
	/// Implements Microsoft XNA RKV archive format (e.g. Blade Kitten, Game Room, Star Wars: The Clone Wars - Repulic Heroes and others).
	/// </summary>
	public class RKVDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "Microsoft XNA RKV archive";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				 _dfr.ExportOptions.SettingsGroups[0].Settings.Add(new TextSetting(nameof(ArchiveName), "Archive _name"));
			}
			return _dfr;
		}

		public string ArchiveName { get; set; } = String.Empty;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;
			br.Endianness = Endianness.BigEndian;

			/*
				compression type: lzf (type 2)
			 */

			ArchiveName = br.ReadFixedLengthString(0x40);
			long info_off = br.ReadInt64();
			long files = br.ReadInt64();

			base.Accessor.Seek(info_off, SeekOrigin.Begin);

			for (long i = 0; i < files; i++)
			{
				string fileName = br.ReadFixedLengthString(0x40);
				fileName = fileName.TrimNull();

				long timestamp = br.ReadInt64();
				long offset = br.ReadInt64();
				int crc = br.ReadInt32();
				int size = br.ReadInt32();
				int zsize = br.ReadInt32();
				int zero = br.ReadInt32();

				File f = fsom.AddFile(fileName);
				f.Size = size;
				f.Properties.Add("reader", br);
				f.Properties.Add("offset", offset);
				f.Properties.Add("size", size);
				f.Properties.Add("zsize", zsize);
				f.DataRequest += f_DataRequest;

				br.Seek(zsize, SeekOrigin.Current);
			}
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (File)sender;
			Reader br = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			int size = (int)file.Properties["size"];
			int zsize = (int)file.Properties["zsize"];

			br.Accessor.Seek(offset, SeekOrigin.Begin);
			if (size == zsize)
			{
				e.Data = br.ReadBytes(size);
			}
			else
			{
				byte type = br.ReadByte();
				switch (type)
				{
					case 2:
						break;
					default:
						Console.WriteLine("RKV: unknown compression method " + type.ToString());
						break;
				}
				e.Data = br.ReadBytes(zsize - 1); // first byte is 0x02
			}
			// clog NAME OFFSET ZSIZE SIZE
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer bw = Accessor.Writer;
			bw.Endianness = Endianness.BigEndian;

			/*
				compression type: lzf (type 2)
			 */

			bw.WriteFixedLengthString(ArchiveName, 0x40);
			long info_off = 0x40 + 8 + 8;
			bw.WriteInt64(info_off);

			File[] files = fsom.GetAllFiles();
			bw.WriteInt64(files.Length);

			long offset = info_off + (files.Length * (0x40 + 64));

			for (long i = 0; i < files.Length; i++)
			{
				bw.WriteFixedLengthString(files[i].Name, 0x40);
				bw.WriteInt64(files[i].ModificationTimestamp.ToBinary());
				bw.WriteInt64(offset);
				int crc = 0;
				bw.WriteInt32(crc);

				byte[] decompressedData = files[i].GetData();
				byte[] compressedData = decompressedData;

				bw.WriteInt32(decompressedData.Length);
				bw.WriteInt32(compressedData.Length);

				bw.WriteInt32(0);

				bw.WriteBytes(compressedData);

				offset += compressedData.Length;
			}
		}
	}
}
