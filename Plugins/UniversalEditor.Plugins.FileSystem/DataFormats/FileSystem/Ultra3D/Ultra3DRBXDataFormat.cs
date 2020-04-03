using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Ultra3D
{
	public class Ultra3DRBXDataFormat : DataFormat
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

			int header = br.ReadInt32();
			if (header != 0x0BA99A9E) throw new InvalidDataFormatException("File does not begin with header 0x0BA99A9E");

			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				File file = new File();
				file.Name = br.ReadFixedLengthString(12).TrimNull();

				uint offset = br.ReadUInt32();
				file.Properties.Add("offset", offset);
				file.Properties.Add("reader", br);

				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);

			uint offset = (uint)file.Properties["offset"];
			IO.Reader br = (IO.Reader)file.Properties["reader"];

			br.Accessor.Position = offset;
			uint length = br.ReadUInt32();
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteInt32(0x0BA99A9E);
			bw.WriteInt32(fsom.Files.Count);

			uint offset = (uint)(8 + ((12 + 4) * fsom.Files.Count));
			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 12);
				bw.WriteUInt32(offset);

				offset += (uint)(4 + file.Size);
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32((uint)file.Size);
				bw.WriteBytes(file.GetData());
			}
		}
	}
}
