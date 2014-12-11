using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression.Modules.LZRW1;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats
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
				_dfr.Filters.Add("Chaos Works Engine volume", new byte?[][] { new byte?[] { 0x02, 0x42, 0x02, 0x43 }, new byte?[] { 0x02, 0x42, 0x02, 0x42 } }, new string[] { "*.vol" });
				_dfr.Filters[0].MagicByteOffsets = new int[] { -4 };
				_dfr.ExportOptions.Add(new CustomOptionBoolean("Compressed", "&Compress this archive using the LZRW1 algorithm", true));
                _dfr.Sources.Add("Based on a requested QuickBMS script by WRS from xentax.com");
			}
			return _dfr;
		}
		
		private bool mvarCompressed = false;
		public bool Compressed { get { return mvarCompressed; } set { mvarCompressed = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

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
            	mvarCompressed = false;
                throw new NotSupportedException("Volume is uncompressed (unknown method)");
            }
            else if (version == 0x43024202)
            {
                // expected version
                mvarCompressed = true;
            }
            else
            {
                throw new InvalidDataFormatException("Unsupported volume (0x" + version.ToString("X") + ")");
            }

            // get FNAME basename

            MemoryAccessor ma = new MemoryAccessor();
            Writer bwms = new Writer(ma);

            LZRW1CompressionModule module = new LZRW1CompressionModule();

            Accessor.Position = 0;
            while (!br.EndOfStream)
            {
            	if (br.Remaining == 20)
            	{
            		// we are done reading the file, because we've encountered the footer!
            		break;
                }

                short CHUNKSIZE = br.ReadInt16();

                byte[] compressed = br.ReadBytes(CHUNKSIZE);

                byte[] decompressed = module.Decompress(compressed);
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
                int fileSize = brms.ReadInt32();
                int unknown1 = brms.ReadInt32();
                int fileOffset = brms.ReadInt32();
                int unknown2 = brms.ReadInt32();
                int unknown3 = brms.ReadInt32();

                File file = new File();
                file.Size = fileSize;
                file.Properties.Add("length", fileSize);
                file.Properties.Add("offset", fileOffset);
                file.Properties.Add("reader", brms);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
		}

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            int length = (int)file.Properties["length"];
            int offset = (int)file.Properties["offset"];
            Reader brms = (Reader)file.Properties["reader"];

            brms.Accessor.Position = offset;
            byte[] data = brms.ReadBytes(length);
            e.Data = data;
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
