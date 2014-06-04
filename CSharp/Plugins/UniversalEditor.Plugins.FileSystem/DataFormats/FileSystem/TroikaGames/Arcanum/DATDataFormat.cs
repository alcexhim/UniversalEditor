using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.TroikaGames.Arcanum
{
	public class DATDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Troika Games Arcanum DAT", new string[] { "*.dat" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;

			// zlib compression
			
			// seek to end of archive and read TS
			br.Accessor.Seek(-4, SeekOrigin.End);
			
			uint TS = br.ReadUInt32();

			// seek to TS and read FSP
			uint AS = (uint)(br.Accessor.Position - 4 - TS);
			br.Accessor.Seek(AS, SeekOrigin.Begin);
			uint FSP = br.ReadUInt32();

			// seek to FSP and read FA
			br.Accessor.Seek(FSP, SeekOrigin.Begin);
			uint FA = br.ReadUInt32();

			for (uint i = 0; i < FA; i++)
			{
				uint fileNameSize = br.ReadUInt32();
				string fileName = String.Empty;

				try
				{
					fileName = br.ReadFixedLengthString(fileNameSize);
				}
				catch (OverflowException ex)
				{
					throw new InvalidDataFormatException();
				}
				fileName = fileName.TrimNull();

				uint unknown1 = br.ReadUInt32();
				uint unknown2 = br.ReadUInt32();

				uint decompressedSize = br.ReadUInt32();
				uint compressedSize = br.ReadUInt32();
				uint offset = br.ReadUInt32();

				File file = fsom.AddFile(fileName);
				file.Properties.Add("reader", br);
				file.Properties.Add("DecompressedLength", decompressedSize);
				file.Properties.Add("CompressedLength", compressedSize);
				file.Properties.Add("offset", offset);
				file.Size = decompressedSize;
				file.DataRequest += file_DataRequest;
			}
		}

		void file_DataRequest(object sender, DataRequestEventArgs e)
		{
			File file = (sender as File);
			IO.Reader br = (IO.Reader)file.Properties["reader"];
			uint DecompressedLength = (uint)file.Properties["DecompressedLength"];
			uint CompressedLength = (uint)file.Properties["CompressedLength"];
			uint offset = (uint)file.Properties["offset"];

			br.Accessor.Position = offset;
			byte[] compressedData = br.ReadBytes(CompressedLength);
			byte[] decompressedData = CompressionModules.Zlib.Decompress(compressedData);
			e.Data = decompressedData;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
