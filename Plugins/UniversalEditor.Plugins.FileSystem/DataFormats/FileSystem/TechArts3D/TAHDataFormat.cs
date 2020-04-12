//
//  TAHDataFormat.cs - provides a DataFormat for manipulating archives in TechArts3D TAH format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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
using System.Collections.Specialized;

using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.TechArts3D
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in TechArts3D TAH format.
	/// </summary>
	public class TAHDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "TechArts3D archive";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		public static uint GenerateHashKeyForString(ref byte[] strg)
		{
			uint key = 0xc8a4e57a;
			string tstrg = System.Text.Encoding.ASCII.GetString(strg).ToLower();
			byte[] tbstrg = System.Text.Encoding.ASCII.GetBytes(tstrg);
			uint i = 0;
			while (tbstrg[i] != 0)
			{
				key = (key << 0x13) | (key >> 13);
				key ^= tbstrg[i];
				i++;
			}
			return (key ^ (((tbstrg[(int)((IntPtr)(i - 1))] & 0x1a) != 0) ? uint.MaxValue : 0));
		}

		public CompressionMethod CompressionMethod { get; set; } = CompressionMethod.LZSS;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader br = base.Accessor.Reader;

			string TAH2 = br.ReadFixedLengthString(4);
			if (TAH2 != "TAH2")
				throw new InvalidDataFormatException();

			uint indexEntryCount = br.ReadUInt32();
			uint unknown = br.ReadUInt32();
			uint reserved = br.ReadUInt32();
			uint indexBufferSize = indexEntryCount * 8;
			uint[] hashNames = new uint[indexEntryCount];
			uint[] offsets = new uint[indexEntryCount];
			string[] fileNames = new string[indexEntryCount];
			uint[] flags = new uint[indexEntryCount];
			uint[] lengths = new uint[indexEntryCount];
			uint i = 0;
			while (i < indexEntryCount)
			{
				hashNames[i] = br.ReadUInt32();
				offsets[i] = br.ReadUInt32();
				i++;
			}
			uint directoryInfoOutputLength = br.ReadUInt32();
			uint directoryInfoInputLength = (offsets[0] - 0x10) - indexBufferSize;
			byte[] directoryInfoInputData = br.ReadBytes(directoryInfoInputLength);
			byte[] directoryInfoOutputData = new byte[directoryInfoOutputLength];
			directoryInfoOutputData = CompressionModule.FromKnownCompressionMethod(CompressionMethod).Decompress(directoryInfoInputData); // , directoryInfoOutputLength);
			MemoryAccessor msFileList = new MemoryAccessor(directoryInfoOutputData);
			Reader brFileList = new Reader(msFileList);
			uint act_str_pos = 0;
			byte[] file_path = new byte[260];
			while (directoryInfoOutputData.Length > act_str_pos)
			{
				uint pos_local = 0;
				while (directoryInfoOutputData[act_str_pos + pos_local] != 0)
				{
					if (directoryInfoOutputData[act_str_pos + pos_local] == 0x2f)
					{
						break;
					}
					pos_local++;
				}
				if (directoryInfoOutputData[act_str_pos + pos_local] != 0)
				{
					i = 0;
					while (directoryInfoOutputData[act_str_pos + i] != 0)
					{
						file_path[i] = directoryInfoOutputData[i + act_str_pos];
						i++;
					}
					file_path[i] = 0;
				}
				else
				{
					byte[] str_path = new byte[260];
					uint str_path_offset = 0;
					while (file_path[str_path_offset] != 0)
					{
						str_path_offset++;
					}
					file_path.CopyTo(str_path, 0);
					i = 0;
					i = 0;
					while (directoryInfoOutputData[act_str_pos + i] != 0)
					{
						str_path[i + str_path_offset] = directoryInfoOutputData[act_str_pos + i];
						i++;
					}
					str_path[i + str_path_offset] = 0;
					uint hash_key = GenerateHashKeyForString(ref str_path);
					for (uint h = 0; h < indexEntryCount; h++)
					{
						if ((fileNames[h] == null) && (hash_key == hashNames[h]))
						{
							byte[] filename = new byte[i + str_path_offset];
							for (int k = 0; k < (i + str_path_offset); k++)
							{
								filename[k] = str_path[k];
							}
							fileNames[h] = System.Text.Encoding.Default.GetString(filename);
							break;
						}
					}
				}
				while (directoryInfoOutputData[act_str_pos] != 0)
				{
					act_str_pos++;
				}
				act_str_pos++;
			}
			for (i = 0; i < indexEntryCount; i++)
			{
				if (fileNames[i] == null)
				{
					fileNames[i] = i.ToString("00000000") + "_" + hashNames[i].ToString();
					flags[i] ^= 1;
				}
			}
			for (i = 0; i < (indexEntryCount - 1); i++)
			{
				lengths[i] = offsets[(int)((IntPtr)(i + 1))] - offsets[i];
			}
			StringCollection szs = new StringCollection();
			for (i = 0; i < indexEntryCount; i++)
			{
				if (lengths[i] > 4)
				{
					uint data_input_length = lengths[i] - 4;
					byte[] data_input = new byte[data_input_length];
					File f = fsom.AddFile(fileNames[i]);
					f.Source = new EmbeddedFileSource(br, offsets[i], lengths[i]);
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
