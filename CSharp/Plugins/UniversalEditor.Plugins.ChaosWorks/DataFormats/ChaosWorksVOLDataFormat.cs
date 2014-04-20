﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats
{
	public class ChaosWorksVOLDataFormat : DataFormat
	{
		DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Chaos Works Engine volume", new byte?[][] { new byte?[] { 0x02, 0x42, 0x02, 0x43 }, new byte?[] { 0x02, 0x42, 0x02, 0x42 } }, new string[] { "*.vol" });
				_dfr.Filters[0].MagicByteOffsets = new int[] { -4 };
				_dfr.ExportOptions.Add(new ExportOptionBoolean("Compressed", "&Compress this archive using the LZRW1 algorithm", true));
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

			IO.BinaryReader br = base.Stream.BinaryReader;

            br.BaseStream.Seek(-20, System.IO.SeekOrigin.End);
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
            
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bwms = new System.IO.BinaryWriter(ms);

            br.BaseStream.Position = 0;

            byte[] compressed = new byte[0];
            int i = 0;
            while (!br.EndOfStream)
            {
            	if (br.Remaining == 20)
            	{
            		// we are done reading the file, because we've encountered the footer!
            		break;
            	}
            	
                short CHUNKSIZE = br.ReadInt16();

                byte[] input = br.ReadBytes(CHUNKSIZE);
                Array.Resize<byte>(ref compressed, compressed.Length + input.Length);
                Array.Copy(input, 0, compressed, i, input.Length);
                i += input.Length;

                if (br.EndOfStream) break;
            }


            byte[] uncompressed = UniversalEditor.Compression.LZRW1.LZRW1CompressionModule.Decompress(compressed);
            bwms.Write(uncompressed, 0, uncompressed.Length);

            bwms.Flush();
            
            ms.Position = 0;

            System.IO.File.WriteAllBytes(@"C:\Temp\New Folder\test.dat", ms.ToArray());

            IO.BinaryReader brms = new IO.BinaryReader(ms);
            brms.BaseStream.Position = fileListOffset;
            if (brms.BaseStream.Position >= (brms.BaseStream.Length - (572 * fileCount))) throw new InvalidOperationException();

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
            IO.BinaryReader brms = (IO.BinaryReader)file.Properties["reader"];
        }

        protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
