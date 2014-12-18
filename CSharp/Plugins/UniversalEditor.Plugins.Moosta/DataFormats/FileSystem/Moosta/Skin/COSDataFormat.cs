using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

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
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", br.Accessor.Position);
				file.Properties.Add("length", fileSize);
				file.Size = fileSize;
				file.DataRequest += file_DataRequest;

				br.Accessor.Seek(fileSize, SeekOrigin.Current);
			}
		}

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			int length = (int)file.Properties["length"];
			br.Accessor.Seek(offset, SeekOrigin.Begin);

			e.Data = br.ReadBytes(length);
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
				bw.WriteBytes(file.GetDataAsByteArray());
			}
			bw.Flush();
		}
	}
}
