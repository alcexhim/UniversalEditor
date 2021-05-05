//
//  ARCVDataFormat.cs - provides a DataFormat for manipulating compressed files in ARCV format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ARCV
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating compressed files in ARCV format.
	/// </summary>
	public class ARCVDataFormat : DataFormat
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = Accessor.Reader;

			string signature = reader.ReadFixedLengthString(4);
			if (signature != "ARCV")
				throw new InvalidDataFormatException("file does not begin with 'ARCV'");

			uint unknown1 = reader.ReadUInt32();
			uint unknown2 = reader.ReadUInt32();
			string filename = reader.ReadLengthPrefixedString();

			uint decompressedLength = reader.ReadUInt32();
			uint compressedLength = reader.ReadUInt32();
			uint unknown5 = reader.ReadUInt32();
			uint unknown6_maybeChecksum = reader.ReadUInt32();

			uint reserved1 = reader.ReadUInt32();
			uint reserved2 = reader.ReadUInt32();

			ushort unknown7 = reader.ReadUInt16();
			uint unknown8 = reader.ReadUInt32();
			uint unknown9 = reader.ReadUInt32();

			string chnk = reader.ReadFixedLengthString(4);
			if (chnk != "CHNK")
			{
				throw new InvalidDataFormatException();
			}


			uint unknown11 = reader.ReadUInt32();
			uint unknown12 = reader.ReadUInt32();
			uint unknown13 = reader.ReadUInt32();

			File f = fsom.AddFile(filename);
			f.Properties["reader"] = reader;
			f.Properties["offset"] = Accessor.Position;
			f.Properties["compressedLength"] = compressedLength;
			f.Properties["decompressedLength"] = decompressedLength;
			f.Size = decompressedLength;
			f.DataRequest += F_DataRequest;
		}

		void F_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader reader = (Reader)f.Properties["reader"];
			long offset = (long)f.Properties["offset"];

			uint compressedLength = (uint)f.Properties["compressedLength"];
			uint decompressedLength = (uint)f.Properties["decompressedLength"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedLength);
			byte[] decompressedData = compressedData; // TODO: decompress the data
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			throw new System.NotImplementedException();
		}
	}
}
