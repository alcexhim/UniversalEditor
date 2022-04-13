//
//  DPKDataFormat.cs - provides a DataFormat for manipulating archives in Merscom DPK format
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

using MBS.Framework;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Merscom.DataFormats.FileSystem.DPK
{
	/// <summary>
	/// Provides a <see cref="DataFormat" />for manipulating archives in Merscom DPK format.
	/// </summary>
	[DataFormatImplementationStatus(DataFormatImplementationArea.Load, ImplementationStatus.Complete)]
	public class DPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Title = "Merscom Data Package (DPK)";
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			fsom.AdditionalDetails.Add("compressedSize", "Compressed size");

			Reader br = base.Accessor.Reader;

			string DPK4 = br.ReadFixedLengthString(4);
			if (DPK4 != "DPK4")
				throw new InvalidDataFormatException();

			uint archiveLength = br.ReadUInt32();
			uint tocLength = br.ReadUInt32();
			uint u3 = br.ReadUInt32();
			for (int i = 0; i < u3; i++)
			{
				uint recordSize = br.ReadUInt32(); // size of file TOC record
				uint decompressedLength = br.ReadUInt32();
				uint compressedLength = br.ReadUInt32();									// 371				39325
				uint offset = br.ReadUInt32();									// 292196			292567

				string nam = br.ReadNullTerminatedString();                 // action.accfg
				br.Align(4);

				File file = fsom.AddFile(nam);

				file.Properties["compressedLength"] = compressedLength;
				file.Properties["offset"] = offset;
				file.Properties["reader"] = Accessor.Reader;
				file.SetAdditionalDetail("compressedSize", compressedLength);

				file.Size = decompressedLength;

				file.DataRequest += File_DataRequest;
			}
		}

		private Guid KnownSettingsGuids_EnableLogging = new Guid("{39e459e8-e8be-4836-8274-fd1c26d498cf}");

		private Compression.CompressionModule zlib { get; } = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Zlib);

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (File)sender;

			uint compressedLength = (uint)file.Properties["compressedLength"];
			uint offset = (uint)file.Properties["offset"];
			Reader reader = (Reader)file.Properties["reader"];

			if (Application.Instance.GetSetting<bool>(KnownSettingsGuids_EnableLogging))
			{
				Console.WriteLine(String.Format("ue: merscom: dpk: unpacking '{0}' (at {1}, compressed size {2}, decompressed size {3})", file.Name, offset, compressedLength, file.Size));
			}

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedLength);

			if (compressedLength == file.Size)
			{
				// file is not compressed!
				e.Data = compressedData;
			}
			else
			{
				byte[] decompressedData = zlib.Decompress(compressedData);
				e.Data = decompressedData;
			}
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;

			bw.WriteFixedLengthString("DPK4");

			File[] files = fsom.GetAllFiles();
			uint archiveLength = (uint)(16 + (16 * files.Length));
			uint tocLength = (uint)(16 * files.Length);
			for (int i = 0; i < files.Length; i++)
			{
				tocLength += (uint) (files[i].Name.Length + 1).Align(4);
			}

			long archiveLengthOffset = bw.Accessor.Position;
			bw.WriteUInt32(archiveLength);

			bw.WriteUInt32(tocLength);
			bw.WriteUInt32((uint)files.Length);

			uint[] offsets = new uint[files.Length];
			uint[] compressedLengths = new uint[files.Length];

			long tocOffset = bw.Accessor.Position;
			bw.Accessor.Seek(tocLength, SeekOrigin.Current); // skip over TOC for now, will come back and write it later

			uint offset = (uint)(tocOffset + tocLength);
			offsets[0] = offset;
			for (int i = 0; i < files.Length; i++)
			{
				byte[] decompressedData = files[i].GetData();
				byte[] compressedData = zlib.Compress(decompressedData);
				if (compressedData.Length >= decompressedData.Length)
				{
					// no point in using the zlib
					bw.WriteBytes(decompressedData);
					offset += (uint)decompressedData.Length;
					compressedLengths[i] = (uint)decompressedData.Length;
				}
				else
				{
					bw.WriteBytes(compressedData);
					offset += (uint)compressedData.Length;
					compressedLengths[i] = (uint)compressedData.Length;
				}

				if (i < files.Length - 1)
				{
					offsets[i + 1] = offset;
				}
			}

			// now we can write the TOC
			bw.Accessor.Seek(tocOffset, SeekOrigin.Begin);
			for (int i = 0; i < files.Length; i++)
			{
				uint recordSize = (uint)(12 + (files[i].Name.Length + 1).Align(4));
				bw.WriteUInt32(recordSize);

				uint decompressedLength = (uint)files[i].Size;
				bw.WriteUInt32(decompressedLength);
				bw.WriteUInt32(compressedLengths[i]);
				bw.WriteUInt32(offsets[i]);
				bw.WriteNullTerminatedString(files[i].Name);
				bw.Align(4);
			}

			bw.Accessor.Seek(archiveLengthOffset, SeekOrigin.Begin);
			bw.WriteUInt32((uint)bw.Accessor.Length);
		}
	}
}
