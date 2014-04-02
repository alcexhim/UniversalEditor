using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Moero.DownhillNight
{
	public class PKDDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Moero Downhill Night PKD archive", new byte?[][] { new byte?[] { (byte)'P', (byte)'A', (byte)'C', (byte)'K' } }, new string[] { "*.pkd" } );
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string signature = br.ReadFixedLengthString(4);
			if (signature != "PACK") throw new InvalidDataFormatException();

			byte[] encryptedData = br.ReadToEnd();
			for (int i = 0; i < encryptedData.Length; i++)
			{
				encryptedData[i] = (byte)(encryptedData[i] ^ 0xC5);
			}

			br = new IO.Reader(new MemoryAccessor(encryptedData));

			uint fileCount = br.ReadUInt32();
			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(128).TrimNull();
				uint length = br.ReadUInt32();
				uint offset = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("reader", br);
				file.Properties.Add("length", length);
				file.Properties.Add("offset", offset);
				file.DataRequest += file_DataRequest;
				file.Size = length;
			}

            br.Close();
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint length = (uint)file.Properties["length"];
			uint offset = (uint)file.Properties["offset"];

			br.Accessor.Seek(offset, SeekOrigin.Begin);
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			// create a Writer in memory for the unencrypted data
            MemoryAccessor ma = new MemoryAccessor();
			IO.Writer bw = new IO.Writer(ma);


			File[] files = fsom.GetAllFiles();
			
			uint offset = 4;
			foreach (File file in files)
			{
				offset += 136;
			}

			bw.WriteUInt32((uint)files.Length);
			foreach (File file in files)
			{
				bw.WriteFixedLengthString(file.Name, 128);
				bw.WriteUInt32((uint)file.Size);
				bw.WriteUInt32((uint)offset);
				offset += (uint)file.Size;
			}
			bw.Flush();
			bw.Close();

			// switch over to dataformat's Writer and encrypt the data
			bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("PACK");
			byte[] data = ma.ToArray();
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = (byte)(data[i] ^ 0xC5);
			}
			bw.WriteBytes(data);
			bw.Flush();
		}
	}
}
