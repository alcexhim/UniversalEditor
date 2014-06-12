using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.JackOrlando
{
	public class JackOrlandoDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Jack Orlando archive", new byte?[][] { new byte?[] { (byte)'P', (byte)'A', (byte)'K', (byte)0 } }, new string[] { "*.pak", "*.phk", "*.ph2" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Reader br = base.Accessor.Reader;
			string PAK = br.ReadFixedLengthString(4);
			if (PAK != "PAK\0") throw new InvalidDataFormatException("File does not begin with \"PAK\", 0");

			uint fileCount = br.ReadUInt32();
			uint firstFileOffset = br.ReadUInt32();
			uint unknown = br.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();
				uint fileNameLength = br.ReadUInt32();
				string fileName = br.ReadNullTerminatedString();

				// do we require this?
				if (fileName.Length != (fileNameLength - 1)) throw new InvalidDataFormatException("File name length mismatch");

				File file = new File();
				file.Name = fileName;
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", fileOffset);
				file.Properties.Add("length", fileLength);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint length = (uint)file.Properties["length"];

			br.Accessor.Position = offset;
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PAK\0");

			bw.WriteUInt32((uint)fsom.Files.Count);

			uint firstFileOffset = 16;
			foreach (File file in fsom.Files)
			{
				firstFileOffset += (uint)(12 + (file.Name.Length + 1));
			}
			bw.WriteUInt32(firstFileOffset);

			uint offset = firstFileOffset;
			foreach (File file in fsom.Files)
			{
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32((uint)file.Name.Length);
				bw.WriteNullTerminatedString(file.Name);
				offset += (uint)file.Size;
			}

			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetDataAsByteArray());
			}
			bw.Flush();
		}
	}
}