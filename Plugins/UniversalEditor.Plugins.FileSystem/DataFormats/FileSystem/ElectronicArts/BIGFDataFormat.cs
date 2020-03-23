using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.ElectronicArts
{
	public class BIGFDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
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
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			string header = br.ReadFixedLengthString(4);
			if (header != "BIGF") throw new InvalidDataFormatException("File does not start with BIGF");

			uint archiveSize = br.ReadUInt32();
			uint fileCount = br.ReadUInt32();
			uint firstFileOffset = br.ReadUInt32();

			// TODO: figure out what firstFileOffset points to... the data or the entry
			// br.Accessor.Seek(firstFileOffset, SeekOrigin.Begin);
			for (uint i = 0; i < fileCount; i++)
			{
				uint offset = br.ReadUInt32();
				uint length = br.ReadUInt32();
				string filename = br.ReadNullTerminatedString();

				File file = new File();
				file.Name = filename;
				file.Source = new EmbeddedFileSource(br, offset, length);
				fsom.Files.Add(file);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("BIGF");

			uint archiveSize = 0;
			long archiveSizePos = bw.Accessor.Position;
			bw.WriteUInt32(archiveSize);

			bw.WriteUInt32((uint)fsom.Files.Count);
			
			uint offset = 16;
			foreach (File file in fsom.Files)
			{
				offset += (uint)(8 + file.Name.Length + 1);
			}
			bw.WriteUInt32(offset);

			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteNullTerminatedString(file.Name);
			}
			foreach (File file in fsom.Files)
			{
				file.WriteTo(bw);
			}
		}
	}
}
