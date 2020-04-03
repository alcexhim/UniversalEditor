using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Eighting.FPK
{
	public class FPKDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionNumber(nameof(DataAlignment), "Data _alignment (in bytes): ", 16, 0, UInt32.MaxValue));
				_dfr.Sources.Add("http://wiki.xentax.com/index.php?title=Bleach_%28PSP%29");
			}
			return _dfr;
		}

		private uint mvarDataAlignment = 16;
		public uint DataAlignment { get { return mvarDataAlignment; } set { mvarDataAlignment = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			string header1 = reader.ReadFixedLengthString(2);
			ushort header2 = reader.ReadUInt16();
			if (!(header1 == "xJ" && header2 == 0)) throw new InvalidDataFormatException("File does not begin with 'xJ', 0x00, 0x00");

			uint fileCount = reader.ReadUInt32();
			mvarDataAlignment = reader.ReadUInt32();
			uint archiveSize = reader.ReadUInt32();

			for (uint i = 0; i < fileCount; i++)
			{
				string fileName = reader.ReadFixedLengthString(36).TrimNull();
				uint offset = reader.ReadUInt32();
				uint compressedLength = reader.ReadUInt32();
				uint decompressedLength = reader.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Size = decompressedLength;
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressedLength", compressedLength);
				file.Properties.Add("DecompressedLength", decompressedLength);
				file.DataRequest += file_DataRequest;
			}
		}

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			uint offset = (uint)file.Properties["offset"];
			uint CompressedLength = (uint)file.Properties["CompressedLength"];
			uint DecompressedLength = (uint)file.Properties["DecompressedLength"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte flag = reader.ReadByte();
			bool compressed = (flag == 255);

			byte[] compressedData = reader.ReadBytes(CompressedLength);
			byte[] decompressedData = compressedData;
			if (compressed)
			{

			}
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.WriteFixedLengthString("xJ");
			writer.WriteUInt16(0);

			File[] files = fsom.GetAllFiles();
			writer.WriteUInt32((uint)files.Length);
			writer.WriteUInt32(mvarDataAlignment);

			uint archiveSize = 16;
			long archiveSizePosition = base.Accessor.Position;
			writer.WriteUInt32(archiveSize);

			uint offset = (uint)(16 + (48 * files.Length));

			byte[][] compressedDatas = new byte[files.Length][];
			bool[] compressed = new bool[files.Length];
			for (int i = 0; i < files.Length; i++)
			{
				File file = files[i];
				writer.WriteFixedLengthString(file.Name, 36);
				writer.WriteUInt32(offset);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = decompressedData;

				compressedDatas[i] = compressedData;
				compressed[i] = false;

				writer.WriteUInt32((uint)compressedData.Length);
				writer.WriteUInt32((uint)decompressedData.Length);

				offset += (uint)(compressedData.Length + 1);

				long count = writer.CalculateAlignment(offset, mvarDataAlignment); // (offset % mvarDataAlignment);
				offset += (uint)count;
			}
			for (int i = 0; i < files.Length; i++)
			{
				if (compressed[i])
				{
					writer.WriteByte(255);
				}
				else
				{
					writer.WriteByte(0);
				}
				writer.WriteBytes(compressedDatas[i]);
				writer.Align((int)mvarDataAlignment);
			}

			base.Accessor.Seek(archiveSizePosition, SeekOrigin.Begin);
			writer.WriteUInt32((uint)base.Accessor.Length);

			writer.Flush();
		}
	}
}
