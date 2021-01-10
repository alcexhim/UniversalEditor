//
//  PPDataFormat.cs - provides a DataFormat for manipulating Illusion PP archive files
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

namespace UniversalEditor.DataFormats.FileSystem.Illusion.PP
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Illusion PP archive files.
	/// </summary>
	public class PPDataFormat : DataFormat
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


		private static byte[] Decrypt(byte[] data)
		{
			byte[] output = (data.Clone() as byte[]);
			for (int i = 0; i < output.Length; i++)
			{
				output[i] = (byte)(-(output[i]));
			}
			return output;
		}
		private static void Decrypt(byte[] buf, int size, PPFormatInfo format)
		{
			int i;
			switch (format.codec)
			{
				case PPCodec.SB3:
				{
					// SB3/RL/base.pp
					for (i = 0; i < size; i++)
					{
						buf[i] = (byte)(-(int)buf[i]);
					}
					break;
				}
				case PPCodec.SM:
				{
					// SM and all trials
					byte[] key = format.key;
					for (i = 0; i < size; i += 4)
					{
						uint value = BitConverter.ToUInt32(buf, i);
						value ^= key[i & 7];
						byte[] bytes = BitConverter.GetBytes(value);
						
						for (int j = 0; j < 4; j++)
						{
							key[i + j] = bytes[j];
						}
					}
					break;
				}
				case PPCodec.AG3:
				{
					// AG3/DT/HAKO
					int len = size / 2;

					byte[] codeA = new byte[16];
					byte[] codeB = new byte[16];

					Array.Copy(format.key, 0, codeA, 0, 16);
					Array.Copy(format.key, 16, codeB, 0, 16);

					for (i = 0; i < len; i++)
					{
						codeA[i & 3] += codeB[i & 3];

						buf[i] = (byte)(buf[i] ^ codeA[i & 3]);
					}
					break;
				}
			}
		}


		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			Reader br = base.Accessor.Reader;
			uint unknown1 = br.ReadUInt32();
			uint unknown2 = br.ReadUInt32();

			for (uint i = 0; i < unknown1; i++)
			{
				File file = new File();
				byte[] nameBytes = br.ReadBytes(32);
				nameBytes = Decrypt(nameBytes);

				file.Name = Encoding.ShiftJIS.GetString(nameBytes);
				file.Name = file.Name.TrimNull();
				fsom.Files.Add(file);
			}

			uint offset = (uint)(br.Accessor.Position + (4 * fsom.Files.Count));
			foreach (File file in fsom.Files)
			{
				byte[] lengthData = br.ReadBytes(4);
				lengthData = Decrypt(lengthData);
				uint length = BitConverter.ToUInt32(lengthData, 0);

				file.Size = length;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.DataRequest += file_DataRequest;
				offset += length;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader br = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			br.Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] data = br.ReadBytes(length);
			data = Decrypt(data);
			e.Data = data;
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
