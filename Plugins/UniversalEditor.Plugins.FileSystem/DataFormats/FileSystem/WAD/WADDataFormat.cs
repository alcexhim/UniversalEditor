//
//  WADDataFormat.cs - provides a DataFormat for manipulating archives in Doom WAD format
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

namespace UniversalEditor.DataFormats.FileSystem.WAD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Doom WAD format.
	/// </summary>
	public class WADDataFormat : DataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionBoolean(nameof(UserContent), "This archive contains _public content (PWAD) rather than internal content (IWAD)"));
			}
			return _dfr;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="WADDataFormat"/> contains public content.
		/// </summary>
		/// <value><c>true</c> if the content in this WAD archive should be public; otherwise, <c>false</c>.</value>
		public bool UserContent { get; set; } = false;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Reader br = base.Accessor.Reader;
			string IWAD = br.ReadFixedLengthString(4);
			if (!(IWAD == "IWAD" || IWAD == "PWAD")) throw new InvalidDataFormatException("File does not begin with \"IWAD\" or \"PWAD\"");
			UserContent = (IWAD == "PWAD");

			int fileCount = br.ReadInt32();
			int offsetToDirectory = br.ReadInt32();

			br.Accessor.Position = offsetToDirectory;

			for (int i = 0; i < fileCount; i++)
			{
				int offset = br.ReadInt32();
				int length = br.ReadInt32();

				File file = new File();
				file.Name = br.ReadFixedLengthString(8).TrimNull();
				file.Size = length;
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			int offset = (int)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Writer bw = base.Accessor.Writer;
			if (UserContent)
			{
				bw.WriteFixedLengthString("PWAD");
			}
			else
			{
				bw.WriteFixedLengthString("IWAD");
			}

			bw.WriteInt32((int)fsom.Files.Count);

			int offsetToDirectory = 12;
			bw.WriteInt32(offsetToDirectory);

			int offset = offsetToDirectory + (16 * fsom.Files.Count);
			foreach (File file in fsom.Files)
			{
				bw.WriteInt32(offset);
				bw.WriteInt32((int)file.Size);
				bw.WriteFixedLengthString(file.Name, 8);
				offset += (int)file.Size;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
