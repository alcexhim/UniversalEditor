//
//  DescentHOGDataFormat.cs - provides a DataFormat for manipulating archives in Descent HOG (DHF) format
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

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Descent
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Descent HOG (DHF) format.
	/// </summary>
	public class DescentHOGDataFormat : DataFormat
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
			IO.Reader br = base.Accessor.Reader;
			string DHF = br.ReadFixedLengthString(3);
			if (DHF != "DHF") throw new InvalidDataFormatException("File does not begin with \"DHF\"");

			while (!br.EndOfStream)
			{
				string FileName = br.ReadFixedLengthString(13);
				FileName = FileName.TrimNull();

				int length = br.ReadInt32();
				long offset = br.Accessor.Position;

				File file = new File();
				file.Name = FileName;
				file.Size = length;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", offset);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);

				br.Accessor.Seek(length, IO.SeekOrigin.Current);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			long offset = (long)file.Properties["offset"];
			IO.Reader reader = (IO.Reader)file.Properties["reader"];
			reader.Accessor.Seek(offset, IO.SeekOrigin.Begin);
			e.Data = reader.ReadBytes(file.Size);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("DHF");
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 13);
				bw.WriteInt32((int)file.Size);
				bw.WriteBytes(file.GetData());
			}
			bw.Flush();
		}
	}
}
