using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Moosta.Skin
{
	public class COSDataFormat : DataFormat
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
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(7);
			if (signature != "OmpSkin") throw new InvalidDataFormatException("File does not begin with \"OmpSkin\"");

			while (!br.EndOfStream)
			{
				string fileName = br.ReadLengthPrefixedString();
				int fileSize = br.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Source = new EmbeddedFileSource(br, br.Accessor.Position, fileSize);
				file.Size = fileSize;

				br.Accessor.Seek(fileSize, SeekOrigin.Current);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("OmpSkin");

			File[] files = fsom.GetAllFiles();
			foreach (File file in files)
			{
				bw.Write(file.Name);
				bw.WriteInt32((int)file.Size);
				file.WriteTo(bw);
			}
			bw.Flush();
		}
	}
}
