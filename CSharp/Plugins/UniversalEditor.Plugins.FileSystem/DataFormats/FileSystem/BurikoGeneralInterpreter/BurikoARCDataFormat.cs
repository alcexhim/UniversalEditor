using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.BurikoGeneralInterpreter
{
	public class BurikoARCDataFormat : DataFormat
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
			string PackFile____ = br.ReadFixedLengthString(12);
			if (PackFile____ != "PackFile    ") throw new InvalidDataFormatException("File does not begin with \"PackFile    \"");

			int fileCount = br.ReadInt32();
			int FileDataOffset = 16 + (fileCount * 32);

			for (int i = 0; i < fileCount; i++)
			{
				string FileName = br.ReadFixedLengthString(16);
				if (FileName.Contains('\0')) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

				int FileOffset = br.ReadInt32();
				int FileSize = br.ReadInt32();
				int reserved1 = br.ReadInt32();
				int reserved2 = br.ReadInt32();

				File file = new File();
				file.Name = FileName;
				file.Size = FileSize;
				file.Source = new EmbeddedFileSource(br, FileDataOffset + FileOffset, FileSize);
				fsom.Files.Add(file);
			}
		}
		
		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PackFile    ");

			bw.WriteInt32(fsom.Files.Count);

			int fileOffset = 0;

			foreach (File file in fsom.Files)
			{
				int i = fsom.Files.IndexOf(file);

				bw.WriteFixedLengthString(file.Name, 16);

				bw.WriteInt32(fileOffset);

				int length = (int)file.Source.GetLength();
				bw.WriteInt32(length);

				int reserved1 = 0;
				bw.WriteInt32(reserved1);

				int reserved2 = 0;
				bw.WriteInt32(reserved2);

				fileOffset += length;
			}
			foreach (File file in fsom.Files)
			{
				file.WriteTo(bw);
			}
		}
	}
}
