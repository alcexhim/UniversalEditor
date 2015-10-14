using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.Microsoft.POD
{
	public class PODDataFormat : DataFormat
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

		private string mvarComment = String.Empty;
		public string Comment { get { return mvarComment; } set { mvarComment = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			int fileCount = reader.ReadInt32();
			mvarComment = reader.ReadFixedLengthString(80).TrimNull();

			for (int i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(32).TrimNull();

				int length = reader.ReadInt32();
				int offset = reader.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Size = length;
				file.Source = new EmbeddedFileSource(reader, offset, length);
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			File[] files = fsom.GetAllFiles();
			writer.WriteInt32(files.Length);

			writer.WriteFixedLengthString(mvarComment, 80);

			int offset = 84 + (40 * files.Length);
			
			foreach (File file in files)
			{
				writer.WriteFixedLengthString(file.Name, 32);
				
				int length = (int)file.Source.GetLength();
				writer.WriteInt32(length);
				writer.WriteInt32(offset);

				offset += length;
			}
		}
	}
}
