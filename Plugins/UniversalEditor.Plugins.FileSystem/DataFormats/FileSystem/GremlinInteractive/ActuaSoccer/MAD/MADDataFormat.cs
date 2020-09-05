//
//  MADDataFormat.cs - provides a DataFormat for manipulating archives in Gremlin Interactive MAD format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.GremlinInteractive.ActuaSoccer.MAD
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating archives in Gremlin Interactive MAD format.
	/// </summary>
	public class MADDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionChoice(nameof(FormatVersion), "Format _version", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("Type 1 (includes file names)", MADFormatVersion.Type1, true),
					new CustomOptionFieldChoice("Type 2 (does not include file names)", MADFormatVersion.Type2)
				}));
				_dfr.ImportOptions.Add(new CustomOptionChoice(nameof(FormatVersion), "Format _version", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("Type 1 (includes file names)", MADFormatVersion.Type1, true),
					new CustomOptionFieldChoice("Type 2 (does not include file names)", MADFormatVersion.Type2)
				}));
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=GRAF:Actua_Soccer_MAD");
			}
			return _dfr;
		}

		private MADFormatVersion mvarFormatVersion = MADFormatVersion.Type1;
		public MADFormatVersion FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			long currentOffset = reader.Accessor.Position;

			// calculate count of files
			if (mvarFormatVersion == MADFormatVersion.Type1)
			{
				reader.ReadFixedLengthString(16);
			}
			uint firstFileOffset = reader.ReadUInt32();

			uint fileCount = 0;
			switch (mvarFormatVersion)
			{
				case MADFormatVersion.Type1:
				{
					fileCount = (uint)((double)firstFileOffset / 24);
					break;
				}
				case MADFormatVersion.Type2:
				{
					fileCount = (uint)((double)firstFileOffset / 8);
					break;
				}
			}

			reader.Accessor.Seek(currentOffset, SeekOrigin.Begin);

			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = i.ToString().PadLeft(8, '0');
				if (mvarFormatVersion == MADFormatVersion.Type1)
				{
					fileName = reader.ReadFixedLengthString(16).TrimNull();
				}
				uint offset = reader.ReadUInt32();
				uint length = reader.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("length", length);
				file.DataRequest += file_DataRequest;
			}
		}
		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];
			reader.Seek(offset, SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			Writer writer = base.Accessor.Writer;
			uint offset = 0;

			for (uint i = 0; i < files.Length; i++)
			{
				offset += 8;
				if (mvarFormatVersion == MADFormatVersion.Type1) offset += 16;
			}
			for (uint i = 0; i < files.Length; i++)
			{
				if (mvarFormatVersion == MADFormatVersion.Type1) writer.WriteFixedLengthString(files[i].Name, 16);
				uint length = (uint)files[i].Size;
				writer.WriteUInt32(offset);
				writer.WriteUInt32(length);
				offset += length;
			}
			for (uint i = 0; i < files.Length; i++)
			{
				writer.WriteBytes(files[i].GetData());
			}
			writer.Flush();
		}
	}
}
