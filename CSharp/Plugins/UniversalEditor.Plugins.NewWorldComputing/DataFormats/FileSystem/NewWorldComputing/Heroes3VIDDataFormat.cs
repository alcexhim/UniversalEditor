using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing
{
	public class Heroes3VIDDataFormat : DataFormat
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

			IO.Reader reader = base.Accessor.Reader;
			uint fileCount = reader.ReadUInt32();
			uint offset = 0;
			for (uint i = 0; i < fileCount; i++)
			{
				File file = new File();
				file.Name = reader.ReadNullTerminatedString(40);

				uint length = reader.ReadUInt32();

				file.DataRequest += new DataRequestEventHandler(file_DataRequest);
				file.Size = length;
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                file.Properties.Add("reader", reader);
				fsom.Files.Add(file);

				offset += length;
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			File[] files = fsom.GetAllFiles();
			bw.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				bw.WriteNullTerminatedString(file.Name, 40);
				bw.WriteUInt32((uint)file.Size);
			}
			foreach (File file in files)
			{
				bw.WriteBytes(file.GetData());
			}
		}

		#region Data Request
		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			string FileName = String.Empty;
			if (Accessor is FileAccessor)
			{
				FileName = (Accessor as FileAccessor).FileName;
			}

            File file = (sender as File);
            IO.Reader reader = (IO.Reader)file.Properties["reader"];
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
			reader.Seek(offset, IO.SeekOrigin.Begin);
			e.Data = reader.ReadBytes(length);
		}
		#endregion
	}
}
