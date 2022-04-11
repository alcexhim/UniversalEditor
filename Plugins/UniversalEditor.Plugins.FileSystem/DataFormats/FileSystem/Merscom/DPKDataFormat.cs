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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Merscom
{
	/// <summary>
	/// Provides a <see cref="DataFormat" />for manipulating archives in Merscom DPK format.
	/// </summary>
	[ImplementationStatus(DataFormatImplementationStatus.Load)]
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

			Reader br = base.Accessor.Reader;

			string DPK4 = br.ReadFixedLengthString(4);
			if (DPK4 != "DPK4")
				throw new InvalidDataFormatException();

			uint archiveLength = br.ReadUInt32();
			uint tocLength = br.ReadUInt32();
			uint u3 = br.ReadUInt32();
			for (int i = 0; i < u3; i++)
			{
				uint u4 = br.ReadUInt32();
				uint decompressedLength = br.ReadUInt32();
				uint compressedLength = br.ReadUInt32();									// 371				39325
				uint offset = br.ReadUInt32();									// 292196			292567

				string nam = br.ReadNullTerminatedString();                 // action.accfg
				br.Align(4);

				File file = fsom.AddFile(nam);

				file.Properties["compressedLength"] = compressedLength;
				file.Properties["offset"] = offset;

				file.Size = decompressedLength;

				file.DataRequest += File_DataRequest;
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (File)sender;
			uint compressedLength = (uint)file.Properties["compressedLength"];
			uint offset = (uint)file.Properties["offset"];

			Accessor.Reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = Accessor.Reader.ReadBytes(compressedLength);
			byte[] decompressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Zlib).Decompress(compressedData);
			e.Data = decompressedData;
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
