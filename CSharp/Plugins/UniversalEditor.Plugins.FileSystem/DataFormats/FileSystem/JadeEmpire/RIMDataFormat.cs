using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.JadeEmpire
{
	public class RIMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Jade Empire RIM archive", new byte?[][] { new byte?[] { (byte)'R', (byte)'I', (byte)'M', (byte)' ', (byte)'V', (byte)'1', (byte)'.', (byte)'0' } }, new string[] { "*.rim" });
			}
			return _dfr;
		}

		private uint mvarVersion = 1;
		public uint Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private uint mvarHeaderPaddingSize = 96;
		public uint HeaderPaddingSize { get { return mvarHeaderPaddingSize; } set { mvarHeaderPaddingSize = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

			IO.Reader br = base.Accessor.Reader;
			string header = br.ReadFixedLengthString(8);
			if (header != "RIM V1.0") throw new InvalidDataFormatException("File does not begin with \"RIM V1.0\"");

			uint unknown = br.ReadUInt32();
			uint fileCount = br.ReadUInt32();
			uint directoryOffset = br.ReadUInt32();     // 24 (header size) + 96 (padding size) = 120
			mvarHeaderPaddingSize = directoryOffset - 24;

			mvarVersion = br.ReadUInt32();

			// 96 bytes of padding?
			br.Accessor.Position = directoryOffset;

			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = br.ReadFixedLengthString(16);
				uint fileTypeID = br.ReadUInt32();      // 2002, 3011, 3016, 3017
				uint fileID = br.ReadUInt32();
				uint fileOffset = br.ReadUInt32();
				uint fileLength = br.ReadUInt32();

				File file = new File();
				file.Name = fileName;
				file.Properties.Add("FileTypeID", fileTypeID);
				file.Properties.Add("FileID", fileID);
				file.Properties.Add("reader", br);
				file.Properties.Add("offset", fileOffset);
				file.Properties.Add("length", fileLength);
				file.DataRequest += file_DataRequest;
				fsom.Files.Add(file);
			}

			// null padding to a multiple of 128 bytes
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
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("RIM V1.0");
			bw.WriteUInt32((uint)0);
			bw.WriteUInt32((uint)fsom.Files.Count);
			bw.WriteUInt32((uint)(24 + mvarHeaderPaddingSize)); // 24 + 96 (padding size)
			bw.WriteUInt32(mvarVersion);

			bw.WriteBytes(new byte[mvarHeaderPaddingSize]);

			uint fileID = 0;
			uint offset = (uint)(bw.Accessor.Position + (32 * fsom.Files.Count));

			foreach (File file in fsom.Files)
			{
				bw.WriteFixedLengthString(file.Name, 16);
				bw.WriteUInt32((uint)0);
				bw.WriteUInt32(fileID);
				bw.WriteUInt32(offset);
				bw.WriteUInt32((uint)file.Size);

				offset += (uint)file.Size;

				uint paddingCount = (uint)(offset % 128);
				offset += paddingCount;
			}
			foreach (File file in fsom.Files)
			{
				bw.WriteBytes(file.GetDataAsByteArray());

				uint paddingCount = (uint)(offset % 128);
				bw.WriteBytes(new byte[paddingCount]);
			}

			bw.Flush();
		}
	}
}
