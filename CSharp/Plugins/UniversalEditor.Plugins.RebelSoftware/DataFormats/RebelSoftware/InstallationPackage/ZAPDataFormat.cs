using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.RebelSoftware.InstallationPackage
{
	public class ZAPDataFormat : DataFormat
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

		private uint? mvarArchiveOffset = null;
		/// <summary>
		/// The offset to the beginning of this archive file, useful for self-extracting executables (i.e. the length of the SFX stub executable).
		/// </summary>
		public uint? ArchiveOffset { get { return mvarArchiveOffset; } set { mvarArchiveOffset = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			if (mvarArchiveOffset != null)
			{
				reader.Seek(mvarArchiveOffset.Value, SeekOrigin.Begin);
			}

			uint magic = reader.ReadUInt32();
			if (magic != 0x0C) throw new InvalidDataFormatException("File does not begin with { 0x0C, 0x00, 0x00, 0x00 }");

			uint unknown1 = reader.ReadUInt32();	// 9462627
			uint unknown2 = reader.ReadUInt32();	// 9988745

			while (reader.Remaining > 4)
			{
				uint headerSize = reader.ReadUInt32();	// not including file name

				uint fileNameLength = reader.ReadUInt32();
				uint unknown4 = reader.ReadUInt32();
				uint unknown5a = reader.ReadUInt32();
				uint decompressedLength = reader.ReadUInt32();
				
				uint recordLength = reader.ReadUInt32(); // including header size

				string fileName = reader.ReadFixedLengthString(fileNameLength);

				uint footerSize = reader.ReadUInt32();
				uint unknown7 = reader.ReadUInt32();
				uint unknown8 = reader.ReadUInt32();
				uint unknown9 = reader.ReadUInt32();

				uint compressedLength = recordLength - footerSize;

				long offset = reader.Accessor.Position;

				reader.Seek(compressedLength, SeekOrigin.Current);

				File file = fsom.AddFile(fileName);
				file.Properties.Add("reader", reader);
				file.Properties.Add("offset", offset);
				file.Properties.Add("CompressedLength", compressedLength);
				file.Properties.Add("DecompressedLength", decompressedLength);
				file.DataRequest += file_DataRequest;
				file.Size = decompressedLength;
			}
			
			if (reader.Remaining == 4)
			{
				// offset to the beginning of the archive file
				mvarArchiveOffset = reader.ReadUInt32();
			}
		}

		private UniversalEditor.Compression.Modules.Zlib.ZlibCompressionModule compressor = new UniversalEditor.Compression.Modules.Zlib.ZlibCompressionModule();

		private void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			Reader reader = (Reader)file.Properties["reader"];
			long offset = (long)file.Properties["offset"];
			uint compressedLength = (uint)file.Properties["CompressedLength"];
			uint decompressedLength = (uint)file.Properties["DecompressedLength"];

			reader.Seek(offset, SeekOrigin.Begin);
			byte[] compressedData = reader.ReadBytes(compressedLength);
			byte[] decompressedData = compressor.Decompress(compressedData);

			byte[] compressedDataDeflated = new byte[compressedData.Length - 6];
			Array.Copy(compressedData, 2, compressedDataDeflated, 0, compressedDataDeflated.Length);

			decompressedData = UniversalEditor.Compression.CompressionModules.Zlib.Decompress(compressedData);
			
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
