using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.FileSystem.FileSources;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.ALZ
{
	public class ALZDataFormat : DataFormat
	{
		private const byte MAX_VERSION = 1;

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ContentTypes.Add("application/x-alz-compressed");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Reader br = base.Accessor.Reader;

			if (br.Accessor.Length < 4) throw new InvalidDataFormatException("File must be at least 4 bytes in length");

			string ALZ = br.ReadFixedLengthString(3);
			byte b = br.ReadByte();
			if (ALZ != "ALZ" || b != 1) throw new InvalidDataFormatException("File does not begin with ALZ, 1");
			br.Accessor.Position -= 4;

			while (!br.EndOfStream)
			{
				int chunkID = br.ReadInt32();
				switch (chunkID)
				{
					case 0x015A4C41:    // ALZ[soh]
					{
						int unknown = br.ReadInt32();
						break;
					}
					case 0x015A4C42:    // BLZ[soh]
					{
						short fileNameLength = br.ReadInt16();
						byte unknown1 = br.ReadByte();
						int timestamp = br.ReadInt32();

						int seconds = (((int)timestamp) << 1) & 0x3e;
						int minutes = (((int)timestamp) >> 5) & 0x3f;
						int hours = (((int)timestamp) >> 11) & 0x1f;
						int days = (int)(timestamp >> 16) & 0x1f;
						int months = ((int)(timestamp >> 21) & 0x0f);
						int years = (((int)(timestamp >> 25) & 0x7f) + 1980);
						
						int formatChecksum = br.ReadInt32();
						int unknown4 = br.ReadInt32();

						short compressedLength = br.ReadInt16();
						short decompressedLength = br.ReadInt16();
						string filename = br.ReadFixedLengthString(fileNameLength);

						File file = fsom.AddFile(filename);
						file.ModificationTimestamp = new DateTime(years, months, days, hours, minutes, seconds);
						file.Size = decompressedLength;
						file.Source = new EmbeddedFileSource(br, br.Accessor.Position, compressedLength, new FileSourceTransformation[]
						{
							new FileSourceTransformation(FileSourceTransformationType.Output, delegate(object sender, System.IO.Stream inputStream, System.IO.Stream outputStream)
							{
								UniversalEditor.Compression.CompressionModule module = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Deflate);
								module.Decompress(inputStream, outputStream);
							})
						});

						br.Accessor.Seek(compressedLength, SeekOrigin.Current);
						break;
					}
					case 0x015A4C43:    // CLZ[soh]
					{
						long unknown = br.ReadInt64();
						break;
					}
					case 0x025A4C43:    // CLZ[?]
					{
						// end of stream
						break;
					}
				}
			}
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			IO.Writer bw = base.Accessor.Writer;

			bw.WriteInt32((int)0x015A4C41);          // ALZ[soh]
			bw.WriteInt32((int)10);                  // not sure what this is, maybe version number?

			foreach (File file in fsom.Files)
			{
				bw.WriteInt32((int)0x015A4C42);      // BLZ[soh]
				bw.WriteInt16((short)file.Name.Length);

				byte unknown1 = 0;
				bw.WriteByte(unknown1);

				#region chunk
				int timestamp = 0x41736CF0;
				// 2012-11-19 12:39:32
				DateTime dtRef = new DateTime(2012, 11, 19, 12, 39, 32);
				DateTime dtEpcch = dtRef.Subtract(new TimeSpan(0, 0, timestamp));

				DateTime dtEpoch = new DateTime(1980, 1, 1);
				DateTime dt = dtEpoch.AddSeconds(timestamp);

				/*
				DateTime dtEpoch = new DateTime(1978, 2, 2, 5, 26, 44);
				DateTime dt = DateTime.Now;

				TimeSpan ts = dt.Subtract(dtEpoch);
				timestamp = (int)ts.TotalSeconds;
				*/
				bw.WriteInt32(timestamp);

				int formatChecksum = 0x00020020;
				bw.WriteInt32(formatChecksum);
				#endregion
				#region chunk
				uint unknown4 = 0xF83D6BFC;
				bw.WriteUInt32(unknown4);

				short decompressedLength = (short)file.Source.GetLength();

				MemoryAccessor ma = new MemoryAccessor();
				Writer writer1 = new Writer(ma);
				short compressedLength = (short) file.WriteTo(writer1, new FileSourceTransformation[]
				{
					new FileSourceTransformation(FileSourceTransformationType.Output, delegate(object sender, System.IO.Stream inputStream, System.IO.Stream outputStream)
					{
						UniversalEditor.Compression.CompressionModule module = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Deflate);
						module.Compress(inputStream, outputStream);
					})
				});
				writer1.Flush();
				writer1.Close();

				bw.WriteInt16((short)compressedLength);
				bw.WriteInt16((short)decompressedLength);

				bw.WriteFixedLengthString(file.Name);
				bw.WriteBytes(ma.ToArray());
				#endregion
			}

			bw.WriteInt32(0x015A4C43);
			bw.WriteBytes(new byte[8]);
			bw.WriteInt32(0x025A4C43);
		}
	}
}