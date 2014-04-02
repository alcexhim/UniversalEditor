using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SidMeier.Pirates.FPK
{
	public class FPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Sid Meier's PIRATES! FPK archive", new byte?[][] { new byte?[] { 0x02, 0x00, 0x00, 0x00 } }, new string[] { "*.fpk" });
			}
			return _dfr;
		}

		private byte[] mvarFileNameEncryptionKey = new byte[] { 0x01 };

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.BinaryReader br = base.Stream.BinaryReader;

			// magic number 0x00000002
			int version = br.ReadInt32();
			if (version != 2) throw new InvalidDataFormatException("File does not begin with 0x02, 0x00, 0x00, 0x00");

			int fileCount = br.ReadInt32();
			for (int i = 0; i < fileCount; i++)
			{
				int fileNameLength = br.ReadInt32();
				string fileName = br.ReadFixedLengthString(fileNameLength);
				
				StringBuilder sb = new StringBuilder();
				int k = 0;
				for (int j = 0; j < fileName.Length; j++)
				{
					char q = (char)(((int)fileName[j] + mvarFileNameEncryptionKey[k]) % 255);
					sb.Append(q);
					k++;
					if (k == mvarFileNameEncryptionKey.Length) k = 0;
				}
				fileName = sb.ToString();

				int unknown1 = br.ReadInt32();
				int unknown2 = br.ReadInt32();
				int length = br.ReadInt32();
				int offset = br.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("length", length);
				file.Properties.Add("offset", offset);
				file.Properties.Add("reader", br);
				file.DataRequest += file_DataRequest;
			}
		}

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			if (file == null) return;

			int length = (int)(file.Properties["length"]);
			int offset = (int)(file.Properties["offset"]);
			IO.BinaryReader br = (IO.BinaryReader)(file.Properties["reader"]);

			br.BaseStream.Seek(offset, System.IO.SeekOrigin.Begin);
			e.Data = br.ReadBytes(length);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.BinaryWriter bw = base.Stream.BinaryWriter;

			// magic number 0x00000002
			bw.Write((int)0x00000002);

			File[] files = fsom.GetAllFiles();
			bw.Write((int)files.Length);

			int global_offset = 0;
			const int FILE_RECORD_SIZE = 20; // + FileNameLength
			foreach (File file in files)
			{
				global_offset += FILE_RECORD_SIZE;
				global_offset += file.Name.Length;
			}

			foreach (File file in files)
			{
				int fileNameLength = file.Name.Length;
				bw.Write(fileNameLength);
				string fileName = file.Name;

				StringBuilder sb = new StringBuilder();
				int k = 0;
				for (int j = 0; j < fileName.Length; j++)
				{
					char q = (char)(((int)fileName[j] + mvarFileNameEncryptionKey[k]) % 255);
					sb.Append(q);
					k++;
					if (k == mvarFileNameEncryptionKey.Length) k = 0;
				}
				fileName = sb.ToString();
				bw.WriteFixedLengthString(fileName);

				int unknown1 = 0;
				bw.Write(unknown1);
				int unknown2 = 0;
				bw.Write(unknown2);

				int length = (int)file.Size;
				int offset = global_offset;
			}
		}
	}
}
