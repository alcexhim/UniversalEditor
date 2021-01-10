//
//  PACDataFormat.cs - provides a DataFormat for manipulating archives in PAC format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker
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

namespace UniversalEditor.DataFormats.FileSystem.PAC
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in PAC format.
	/// </summary>
	public class PACDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Sources.Add("http://wiki.xentax.com/index.php/PAC");
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			Reader reader = base.Accessor.Reader;
			short nFiles = reader.ReadInt16();
			for (short i = 0; i < nFiles; i++)
			{
				byte fNameLength = reader.ReadByte();
				string fName = reader.ReadFixedLengthString(13);
				short fLength = reader.ReadInt16();
				int fOffset = reader.ReadInt32();

				File f = fsom.AddFile(fName);
				f.Properties.Add("reader", reader);
				f.Properties.Add("offset", fOffset);
				f.Properties.Add("length", fLength);
				f.DataRequest += f_DataRequest;
			}
		}

		void f_DataRequest(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Reader reader = (Reader)f.Properties["reader"];
			int offset = (int)f.Properties["offset"];
			short length = (short)f.Properties["length"];

			reader.Accessor.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			Writer writer = base.Accessor.Writer;

			writer.WriteInt16((short)fsom.Files.Count);
			File[] files = fsom.GetAllFiles();
			int offset = 2 + (20 * files.Length);

			foreach (File file in files)
			{
				byte[] data = file.GetData();
				writer.WriteFixedLengthString(file.Name, (byte)file.Name.Length);
				writer.WriteInt32(offset);
				writer.WriteInt16((short)data.Length);
				offset += (short)data.Length;
			}
		}
	}
}
