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
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.ImportOptions.Add(new CustomOptionText("EncryptionKey", "Encryption &key: ", "bogomojo"));
				_dfr.ExportOptions.Add(new CustomOptionBoolean("Encrypted", "&Encrypt the data with the specified key"));
                _dfr.ExportOptions.Add(new CustomOptionText("EncryptionKey", "Encryption &key: ", "bogomojo"));
            }
            return _dfr;
        }

		private bool mvarEncrypted = false;
		/// <summary>
		/// Determines whether the archive is to be encrypted with the key specified in the <see cref="EncryptionKey" /> property.
		/// </summary>
		public bool Encrypted { get { return mvarEncrypted; } set { mvarEncrypted = value; } }

        private string mvarEncryptionKey = "bogomojo";
		/// <summary>
		/// The key with which to encrypt the files in the archive when <see cref="Encrypted" /> is true.
		/// </summary>
        public string EncryptionKey { get { return mvarEncryptionKey; } set { mvarEncryptionKey = value; } }

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
            byte[] key = System.Text.Encoding.ASCII.GetBytes(mvarEncryptionKey);
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
				Array.Resize(ref decompressedData, (int)decompressedSize);
            }
            e.Data = decompressedData;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
			FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
			if (fsom == null) throw new ObjectModelNotSupportedException();

			File[] files = fsom.GetAllFiles();

			IO.Writer writer = base.Accessor.Writer;
			writer.WriteBytes(new byte[] { 0xBE, 0xEF, 0xCA, 0xFE });

			writer.WriteUInt32((uint)files.Length);

			byte[][] compressedDatas = new byte[files.Length][];

			long offset = 8;
			for (int i = 0; i < files.Length; i++)
			{
				offset += 4;
				offset += files[i].Name.Length;
				offset += 32;
			}

			for (int i = 0; i < files.Length; i++)
			{
				File file = files[i];
				writer.WriteUInt32((uint)file.Name.Length);
				writer.WriteFixedLengthString(file.Name);

				long dateTime = 0;
				writer.WriteInt64(dateTime);
				writer.WriteInt64(offset);

				byte[] decompressedData = file.GetData();
				byte[] compressedData = CompressionModules.Zlib.Compress(decompressedData);

				if (mvarEncrypted)
				{
					byte[] key = System.Text.Encoding.ASCII.GetBytes(mvarEncryptionKey);
					// simple XOR "decryption" method
					int k = 0;
					for (int j = 0; j < compressedData.Length; j++)
					{
						compressedData[j] ^= key[k];
						k++;
						if (k >= key.Length) k = 0;
					}
				}

				compressedDatas[i] = compressedData;

				writer.WriteInt64(compressedData.Length);
				writer.WriteInt64(decompressedData.Length);

				offset += compressedData.Length;
			}

			for (int i = 0; i < files.Length; i++)
			{
				writer.WriteBytes(compressedDatas[i]);
			}
			writer.Flush();
        }
    }
}
