//
//  AINDataFormat.cs - provides a DataFormat for manipulating archives in AIN format
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

namespace UniversalEditor.DataFormats.FileSystem.AIN
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in AIN format.
	/// </summary>
	public class AINDataFormat : DataFormat
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
			byte signature = reader.ReadByte();
			if (signature != 0x21)
				throw new InvalidDataFormatException();

			AINCompressionType compressionType = (AINCompressionType)reader.ReadByte();

			int unknown1 = reader.ReadInt32();
			if (unknown1 != 0)
				throw new InvalidDataFormatException();

			ushort unknown2 = reader.ReadUInt16();
			ushort filecount = reader.ReadUInt16();
			for (ushort i = 0; i < filecount; i++)
			{
				uint maybeChecksum = reader.ReadUInt32();
				uint fileLength = reader.ReadUInt32();
				uint unknown3 = reader.ReadUInt32();
				ushort unknown4 = reader.ReadUInt16();

				File file = fsom.AddFile(i.ToString());
				file.Properties.Add("offset", base.Accessor.Position);
				file.Properties.Add("length", fileLength);
				file.DataRequest += File_DataRequest;
				file.Size = fileLength;

				base.Accessor.Seek(fileLength, SeekOrigin.Current);
			}
		}

		void File_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];

			Accessor.Seek(offset, SeekOrigin.Begin);
			byte[] data = Accessor.Reader.ReadBytes(length);

			e.Data = data;
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
