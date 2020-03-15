using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression.Modules.LZRW1;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.ChaosWorks
{
	public class ChaosWorksVOLDataFormat : DataFormat
	{
		DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ExportOptions.Add(new CustomOptionBoolean("Compressed", "&Compress this archive using the LZRW1 algorithm", true));
				_dfr.ExportOptions.Add(new CustomOptionChoice("FormatVersion", "Format &version", true, new CustomOptionFieldChoice[]
				{
					new CustomOptionFieldChoice("Version 1 (Fire Fight)", ChaosWorksVOLFormatVersion.V1, true),
					new CustomOptionFieldChoice("Version 2 (Akimbo, Excessive Speed)", ChaosWorksVOLFormatVersion.V2)
				}));
				_dfr.Sources.Add("Based on a requested QuickBMS script by WRS from xentax.com");
			}
			return _dfr;
		}

		static LZRW1CompressionModule lzrw1 = new LZRW1CompressionModule();

		public bool Compressed { get; set; } = true;
		public ChaosWorksVOLFormatVersion FormatVersion { get; set; } = ChaosWorksVOLFormatVersion.V1;

		private long startOfFile = 0;

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) return;

			fsom.AdditionalDetails.Add("ChaosWorks.VOL.Label", "Label");

			Reader br = Accessor.Reader;
			startOfFile = Accessor.Position;

			Accessor.Seek(-8, SeekOrigin.End);
			// check to see if we're reading V2 or not
			string trailerSignature = br.ReadFixedLengthString(8);
			if (trailerSignature == " cweVOLF")
			{
				FormatVersion = ChaosWorksVOLFormatVersion.V2;
			}
			else
			{
				FormatVersion = ChaosWorksVOLFormatVersion.V1;
			}

			Accessor.Seek(startOfFile, SeekOrigin.Begin);

			switch (FormatVersion)
			{
				case ChaosWorksVOLFormatVersion.V1:
				{
					LoadInternalV1(fsom);
					break;
				}
				case ChaosWorksVOLFormatVersion.V2:
				{
					LoadInternalV2(fsom);
					break;
				}
			}
		}

		private void LoadInternalV1(FileSystemObjectModel fsom)
		{
			Reader br = Accessor.Reader;

			Accessor.Seek(-20, SeekOrigin.End);
			// 20 byte header at the end of the file

			int decompressedSize = br.ReadInt32();
			int dataToRead = br.ReadInt32();
			int fileCount = br.ReadInt32();
			int fileListOffset = br.ReadInt32();
			int version = br.ReadInt32();

			// Version check made by client v1.2
			if (version == 0x42024202)
			{
				Compressed = false;
				throw new NotSupportedException("Volume is uncompressed (unknown method)");
			}
			else if (version == 0x43024202)
			{
				// expected version
				Compressed = true;
			}
			else
			{
				throw new InvalidDataFormatException("Unsupported volume (0x" + version.ToString("X") + ")");
			}

			// get FNAME basename

			MemoryAccessor ma = new MemoryAccessor();
			Writer bwms = new Writer(ma);

			Accessor.Seek(startOfFile, SeekOrigin.Begin);

			while (!br.EndOfStream)
			{
				if (br.Remaining <= 20)
				{
					// we are done reading the file, because we've encountered the footer!
					break;
				}

				short CHUNKSIZE = br.ReadInt16();
				Console.WriteLine("cwe-vol: reading chunk size {0}", CHUNKSIZE);

				byte[] compressed = br.ReadBytes(CHUNKSIZE);
				byte[] decompressed = lzrw1.Decompress(compressed);

				bwms.WriteBytes(decompressed);

				if (br.EndOfStream) break;
			}
			bwms.Flush();

			ma.Position = 0;

			Reader brms = new Reader(ma);
			if (fileListOffset > brms.Accessor.Length) throw new InvalidDataFormatException("File must be larger than file list offset (" + fileListOffset.ToString() + ")");
			brms.Accessor.Position = fileListOffset;
			if (brms.Accessor.Position >= (brms.Accessor.Length - (572 * fileCount))) throw new InvalidDataFormatException("No file data is present or the archive is too small");

			for (int f = 0; f < fileCount; f++)
			{
				string fileType = brms.ReadFixedLengthString(260);
				string fileName = brms.ReadFixedLengthString(292);
				fileName = fileName.TrimNull();
				fileType = fileType.TrimNull();

				Console.WriteLine("cwe-vol: adding file {0} with type {1}", fileName, fileType);

				int fileSize = brms.ReadInt32();
				int unknown1 = brms.ReadInt32();
				int fileOffset = brms.ReadInt32();
				int unknown2 = brms.ReadInt32();
				int unknown3 = brms.ReadInt32();

				File file = fsom.AddFile(fileName);
				file.AdditionalDetails.Add(fsom.AdditionalDetails["ChaosWorks.VOL.Label"], fileType);
				file.Size = fileSize;
				file.Source = new EmbeddedFileSource(brms, fileOffset, fileSize);
			}
		}
		private void LoadInternalV2(FileSystemObjectModel fsom)
		{
			List<Internal.ChaosWorksVOLV2ChunkInfo> list = new List<Internal.ChaosWorksVOLV2ChunkInfo>();
			// first read the chunk headers
			Reader reader = Accessor.Reader;
			while (!reader.EndOfStream)
			{
				long offset = reader.Accessor.Position;

				string VF = reader.ReadFixedLengthString(2);
				if (VF != "VF")
				{
					// bail out
					reader.Seek(-2, SeekOrigin.Current);
					break;
				}

				ChaosWorksVOLV2CompressionFlag flag = (ChaosWorksVOLV2CompressionFlag) reader.ReadUInt16();

				uint compressedLength = reader.ReadUInt32();
				uint decompressedLength = reader.ReadUInt32();
				if ((flag & ChaosWorksVOLV2CompressionFlag.Compressed) == ChaosWorksVOLV2CompressionFlag.Compressed)
				{
					// compressed
				}
				else
				{
					// uncompressed
					compressedLength = decompressedLength; // this is weird
				}

				list.Add(new Internal.ChaosWorksVOLV2ChunkInfo(offset, ((flag & ChaosWorksVOLV2CompressionFlag.Compressed) == ChaosWorksVOLV2CompressionFlag.Compressed), compressedLength, decompressedLength));
				// skip the data, we don't need to read it just yet
				reader.Seek(compressedLength, SeekOrigin.Current);
			}

			reader.Seek(list[list.Count - 1].DataOffset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(list[list.Count - 1].CompressedLength);
			byte[] decompressedData = zlib.Decompress(compressedData, (int)list[list.Count - 1].DecompressedLength);

			MemoryAccessor ma = new MemoryAccessor(decompressedData);
			uint stringTableOffset = ma.Reader.ReadUInt32();

			List<Internal.ChaosWorksVOLV2FileInfo> fileInfos = new List<Internal.ChaosWorksVOLV2FileInfo>();
			while (ma.Position < stringTableOffset)
			{
				uint unknown = ma.Reader.ReadUInt32();
				uint chunkOffset = ma.Reader.ReadUInt32();
				uint stringTableOffsetToLabel = ma.Reader.ReadUInt32();
				uint always0xFFFFFFFF = ma.Reader.ReadUInt32();
				uint stringTableOffsetToFileName = ma.Reader.ReadUInt32();
				uint fileLength = ma.Reader.ReadUInt32();
				uint unknown2 = ma.Reader.ReadUInt32();
				uint unknown3 = ma.Reader.ReadUInt32();
				uint unknown4 = ma.Reader.ReadUInt32();

				fileInfos.Add(new Internal.ChaosWorksVOLV2FileInfo(chunkOffset, stringTableOffsetToLabel, stringTableOffsetToFileName, fileLength));

				ma.SavePosition();
				ma.Seek(stringTableOffset + 8 + stringTableOffsetToLabel, SeekOrigin.Begin);
				string label = ma.Reader.ReadNullTerminatedString();
				ma.Seek(stringTableOffset + 8 + stringTableOffsetToFileName, SeekOrigin.Begin);
				string fileName = ma.Reader.ReadNullTerminatedString();

				ma.LoadPosition();

				File f = fsom.AddFile(fileName);
				f.AdditionalDetails.Add(fsom.AdditionalDetails["ChaosWorks.VOL.Label"], label);
				f.Properties.Add("reader", reader);
				f.Properties.Add("fileinfo", fileInfos[fileInfos.Count - 1]);
				f.Size = fileInfos[fileInfos.Count - 1].FileLength;
				f.DataRequest += F_DataRequestV2;
			}
		}

		void F_DataRequestV2(object sender, DataRequestEventArgs e)
		{
			File f = (sender as File);
			Internal.ChaosWorksVOLV2FileInfo fileinfo = (Internal.ChaosWorksVOLV2FileInfo)f.Properties["fileinfo"];
			Reader reader = (Reader)f.Properties["reader"];

			reader.Seek(fileinfo.ChunkOffset, SeekOrigin.Begin);
			string VF = reader.ReadFixedLengthString(2);
			if (VF != "VF")
				throw new InvalidDataFormatException("chunk does not begin with 'VF'");

			ChaosWorksVOLV2CompressionFlag flag = (ChaosWorksVOLV2CompressionFlag)reader.ReadUInt16();

			uint compressedLength = reader.ReadUInt32();
			uint decompressedLength = reader.ReadUInt32();
			byte[] decompressedData = null;
			if ((flag & ChaosWorksVOLV2CompressionFlag.Compressed) == ChaosWorksVOLV2CompressionFlag.Compressed)
			{
				// compressed
				byte[] compressedData = reader.ReadBytes(compressedLength);
				decompressedData = zlib.Decompress(compressedData);
			}
			else
			{
				// uncompressed
				compressedLength = decompressedLength; // this is weird
				decompressedData = reader.ReadBytes(compressedLength);
			}
			e.Data = decompressedData;
		}


		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null)
				throw new ObjectModelNotSupportedException();

			switch (FormatVersion)
			{
				case ChaosWorksVOLFormatVersion.V1:
				{
					SaveInternalV1(fsom);
					break;
				}
				case ChaosWorksVOLFormatVersion.V2:
				{
					SaveInternalV1(fsom);
					break;
				}
			}
		}

		private void SaveInternalV1(FileSystemObjectModel fsom)
		{
			Writer writer = Accessor.Writer;

			MemoryAccessor ma = new MemoryAccessor();

			File[] files = fsom.GetAllFiles();

			int fileListOffset = 0;
			for (int i = 0; i < files.Length; i++)
			{
				byte[] filedata = files[i].GetData();
				ma.Writer.WriteBytes(filedata);
				fileListOffset += filedata.Length;
			}
			ma.Flush();

			byte[] decompressedData = ma.ToArray();
			byte[] compressedData = lzrw1.Compress(decompressedData);
			writer.WriteBytes(compressedData);

			writer.WriteInt32(decompressedData.Length);
			writer.WriteInt32(compressedData.Length);
			writer.WriteInt32(files.Length);
			writer.WriteInt32(fileListOffset);
			writer.WriteInt32(Compressed ? 0x43024202 : 0x42024202);
		}

		private Compression.Modules.Zlib.ZlibCompressionModule zlib = new Compression.Modules.Zlib.ZlibCompressionModule();

		private void SaveInternalV2(FileSystemObjectModel fsom)
		{
			Writer writer = Accessor.Writer;

			File[] files = fsom.GetAllFiles();

			int fileListOffset = 0;
			Internal.ChaosWorksVOLV2FileInfo[] infos = new Internal.ChaosWorksVOLV2FileInfo[files.Length];
			uint chunkOffset = 0, fileNameOffset = 0, labelOffset = 0;
			for (int i = 0; i < files.Length; i++)
			{
				infos[i].ChunkOffset = chunkOffset;
				infos[i].FileNameOffset = fileNameOffset;
				infos[i].LabelOffset = labelOffset;
				uint written = WriteV2CompressedChunk(writer, files[i].GetData());
				chunkOffset += written;
			}

			// table of contents
			MemoryAccessor ma = new MemoryAccessor();
			Writer maw = new Writer(ma);
			maw.WriteUInt32((uint)(36 * files.Length)); // offset within this chunk to string table (not including the 8 bytes in this header)
			maw.WriteInt32(0);
			for (int i = 0; i < files.Length; i++)
			{
				maw.WriteUInt32(infos[i].ChunkOffset);
				maw.WriteUInt32(infos[i].LabelOffset);
				maw.WriteUInt32(0xFFFFFFFF);
				maw.WriteUInt32(infos[i].FileNameOffset);
				maw.WriteUInt32(0);
				maw.WriteUInt32(0);
				maw.WriteUInt32(0);
				maw.WriteUInt32(0);
				maw.WriteUInt32(0);
			}
		}

		private uint WriteV2CompressedChunk(Writer writer, byte[] decompressedData)
		{
			byte[] compressedData = zlib.Compress(decompressedData);
			writer.WriteFixedLengthString("VF");
			writer.WriteUInt16(0x8000); // compressed
			writer.WriteUInt32((uint)compressedData.Length);
			writer.WriteUInt32((uint)decompressedData.Length);
			writer.WriteBytes(compressedData);
			return (uint)(compressedData.Length + 12);
		}
	}
}
