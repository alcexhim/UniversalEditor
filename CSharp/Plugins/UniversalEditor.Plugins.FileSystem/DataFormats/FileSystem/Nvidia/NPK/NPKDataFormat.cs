using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Nvidia.NPK
{
    public class NPKDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.ImportOptions.Add(new CustomOptionText("Key", "&Key: ", "bogomojo"));
                _dfr.ExportOptions.Add(new CustomOptionText("Key", "&Key: ", "bogomojo"));
                _dfr.Filters.Add("Nvidia package", new byte?[][] { new byte?[] { 0xBE, 0xEF, 0xCA, 0xFE } }, new string[] { "*.npk" });
            }
            return _dfr;
        }

        private string mvarKey = "bogomojo";
        public string Key { get { return mvarKey; } set { mvarKey = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;
            byte[] signature = br.ReadBytes(4);
            if (!signature.Match(new byte[] { 0xBE, 0xEF, 0xCA, 0xFE }))
            {
                throw new InvalidDataFormatException("File does not begin with 0xBE, 0xEF, 0xCA, 0xFE");
            }

            uint fileCount = br.ReadUInt32();
            for (uint i = 0; i < fileCount; i++)
            {
                uint fileNameLength = br.ReadUInt32();
                string fileName = br.ReadFixedLengthString(fileNameLength);
                long dateTime = br.ReadInt64();
                long fileOffset = br.ReadInt64();
                long fileCompressedSize = br.ReadInt64();
                long fileDecompressedSize = br.ReadInt64();

                File file = fsom.AddFile(fileName);
                file.Size = fileDecompressedSize;
                file.Properties.Add("reader", br);
                file.Properties.Add("offset", fileOffset);
                file.Properties.Add("CompressedSize", fileCompressedSize);
                file.Properties.Add("DecompressedSize", fileDecompressedSize);
                file.DataRequest += file_DataRequest;
            }

        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            long offset = (long)file.Properties["offset"];
            long compressedSize = (long)file.Properties["CompressedSize"];
            long decompressedSize = (long)file.Properties["DecompressedSize"];
            IO.Reader br = (IO.Reader)file.Properties["reader"];

            br.Accessor.Position = offset;
            byte[] key = System.Text.Encoding.ASCII.GetBytes(mvarKey);
            byte[] compressedData = br.ReadBytes((ulong)compressedSize);
            byte[] decompressedData = null;
            try
            {
                decompressedData = CompressionModules.Zlib.Decompress(compressedData);
            }
            catch
            {
                // data invalid; file may be encrypted

                // simple XOR "decryption" method
                int j = 0;
                for (int i = 0; i < compressedData.Length; i++)
                {
                    compressedData[i] ^= key[j];
                    j++;
                    if (j >= key.Length) j = 0;
                }

                try
                {
                    decompressedData = CompressionModules.Zlib.Decompress(compressedData);
                }
                catch
                {
                    decompressedData = compressedData;
                }
            }

            if (decompressedData.Length != decompressedSize)
            {
            }
            e.Data = decompressedData;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
