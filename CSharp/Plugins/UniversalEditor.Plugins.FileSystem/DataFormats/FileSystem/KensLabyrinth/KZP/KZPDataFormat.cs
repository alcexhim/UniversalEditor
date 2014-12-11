using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.KensLabyrinth.KZP
{
	public class KZPDataFormat : DataFormat
	{
		private DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Ken's Labyrinth KZP archive", new string[] { "*.kzp" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			ushort fileCount = reader.ReadUInt16();
			for (ushort i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(8).TrimNull();
				uint offset = reader.ReadUInt32();
				uint length = 0;
				if (i == fileCount - 1)
				{
					length = (uint)(base.Accessor.Length - offset);
				}
				else
				{
					string nextFileName = reader.ReadFixedLengthString(8);
					uint nextFileOffset = reader.ReadUInt32();
					reader.Seek(-12, SeekOrigin.Current);
					length = (uint)(nextFileOffset - offset);
				}

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

			Writer writer = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();
			writer.WriteUInt16((ushort)files.Length);

			uint offset = (uint)(2 + (12 * (ushort)files.Length));
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				writer.WriteFixedLengthString(files[i].Name, 8);
				writer.WriteUInt32(offset);
				offset += (uint)files[i].Size;
			}
			for (ushort i = 0; i < (ushort)files.Length; i++)
			{
				writer.WriteBytes(files[i].GetDataAsByteArray());
			}
			writer.Flush();
		}
	}
}
