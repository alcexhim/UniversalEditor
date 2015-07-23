using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.ARC
{
	public class ARCDataFormat : DataFormat
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
			br.Endianness = IO.Endianness.LittleEndian;
			byte magic = br.ReadByte();
			if (magic != 0x1A) throw new InvalidDataFormatException("File does not begin with 0x1A");

			byte compressionMethod = br.ReadByte();
			string fileName = br.ReadNullTerminatedString(12);
			int compressedFileSize = br.ReadInt32();
			int fileDateMSDOS = br.ReadInt32();
			short crc16 = br.ReadInt16();
			int decompressedFileSize = br.ReadInt32();
			switch (compressionMethod)
			{
				case 20:
				case 0x15:
				case 0x16:
				case 0x17:
				case 0x18:
				case 0x19:
				case 0x1a:
				case 0x1b:
				case 0x1c:
				case 0x1d:
				{
					byte headerLength = br.ReadByte();
					byte headerType = br.ReadByte();
					byte headerSubType = br.ReadByte();
					byte[] data = br.ReadBytes((uint)headerLength);
					switch (headerType)
					{
						case 20:
						{
							break;
						}
						case 0x15:
						{
							break;
						}
					}
					break;
				}
			}
			byte[] compressedData = br.ReadBytes(compressedFileSize);
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteByte((byte)0x1a);
			for (int i = 0; i < fsom.Files.Count; i++)
			{
				byte compressionMethod = 1;
				bw.WriteByte(compressionMethod);
				bw.WriteFixedLengthString(fsom.Files[i].Name, 12);
				byte[] decompressedData = fsom.Files[i].GetData();
				byte[] compressedData = null;
				switch (compressionMethod)
				{
					case 1:
					{
						compressedData = decompressedData;
						break;
					}
					case 20:
					case 0x15:
					case 0x16:
					case 0x17:
					case 0x18:
					case 0x19:
					case 0x1a:
					case 0x1b:
					case 0x1c:
					case 0x1d:
					{
						byte headerLength = 0;
						bw.WriteByte(headerLength);
						byte headerType = 0;
						bw.WriteByte(headerType);
						byte headerSubType = 0;
						bw.WriteByte(headerSubType);
						byte[] data = new byte[headerLength];
						bw.WriteBytes(data);
						switch (headerType)
						{
							case 20:
							{
								break;
							}
							case 0x15:
							{
								break;
							}
						}
						break;
					}
				}
				int compressedFileSize = compressedData.Length;
				bw.WriteInt32(compressedFileSize);
				int fileDateMSDOS = 0;
				bw.WriteInt32(fileDateMSDOS);
				short crc16 = 0;
				bw.WriteInt16(crc16);
				int decompressedFileSize = decompressedData.Length;
				bw.WriteInt32(decompressedFileSize);
				bw.WriteBytes(compressedData);
			}
		}
	}
}
