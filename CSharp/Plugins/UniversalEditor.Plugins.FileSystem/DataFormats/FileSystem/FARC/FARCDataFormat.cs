using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FARC
{
    public class FARCDataFormat : DataFormat
    {
        // As of 2014-06-20:
        // Format       Status
        // ------------ -------------
        // FArc         Load, Save
        // FARC         Load
        // FArC         Load


        // this was mentioned on 
        // http://forum.xentax.com/viewtopic.php?f=10&t=9639
        // could this mean anything? -v-
        // PS : for each files same header (0x10 bytes - maybe key for decrypt??) - 6D4A249C8529DE62C8E3893931C9E0BC 
        // The files from Project Diva F are the same way, except they have a different header -- 69173ED8F50714439F6240AA7466C37A

        // EDAT v4
        // key
        // 6D4BF3D7245DB294B6C3F9E32AA57E79
        // kgen key
        // D1DF87B5C1471B360ACE21315A339C06

        // (it's not keys for FARC)

        // I guess it's like AES / XOR because all Sony FS use this.
        // EBOOT - AES
        // PSARC - AES
        // PGD - AES + XOR

        private DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);

                _dfr.ExportOptions.Add(new CustomOptionBoolean("Encrypted", "&Encrypt the data with the specified key"));
                _dfr.ExportOptions.Add(new CustomOptionBoolean("Compressed", "&Compress the data with the gzip algorithm"));

                _dfr.Filters.Add("FArC archive", new byte?[][]
                {
                    new byte?[] { (byte)'F', (byte)'A', (byte)'r', (byte)'C' },
                    new byte?[] { (byte)'F', (byte)'A', (byte)'r', (byte)'c' },
                    new byte?[] { (byte)'F', (byte)'A', (byte)'R', (byte)'C' }
                },
                new string[] { "*.farc" });
            }
            return _dfr;
        }

        private bool mvarEncrypted = false;
        public bool Encrypted { get { return mvarEncrypted; } set { mvarEncrypted = value; } }

        private bool mvarCompressed = false;
        public bool Compressed { get { return mvarCompressed; } set { mvarCompressed = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            Reader reader = base.Accessor.Reader;
            reader.Endianness = IO.Endianness.BigEndian;

            string FArC = reader.ReadFixedLengthString(4);
            int directorySize = reader.ReadInt32();
            int dummy = reader.ReadInt32();

            switch (FArC)
            {
                case "FArC":
                {
                    mvarCompressed = true;
                    mvarEncrypted = false;

                    while (reader.Accessor.Position < directorySize)
                    {
                        string FileName = reader.ReadNullTerminatedString();
                        int offset = reader.ReadInt32();
                        int compressedSize = reader.ReadInt32();
                        int decompressedSize = reader.ReadInt32();

                        File file = fsom.AddFile(FileName);
                        if (decompressedSize == 0)
                        {
                            decompressedSize = compressedSize;
                        }
                        file.Size = decompressedSize;

                        file.Properties.Add("reader", reader);
                        file.Properties.Add("offset", offset);
                        file.Properties.Add("CompressedSize", compressedSize);
                        file.Properties.Add("DecompressedSize", compressedSize);
                        file.DataRequest += file_DataRequest;
                    }
                    break;
                }
                case "FArc":
                {
                    mvarCompressed = false;
                    mvarEncrypted = false;

                    // uncompressed, unencrypted
                    while (reader.Accessor.Position - 12 < directorySize - 4)
                    {
                        string FileName = reader.ReadNullTerminatedString();
                        int offset = reader.ReadInt32();
                        int length = reader.ReadInt32();

                        File file = fsom.AddFile(FileName);
                        file.Size = length;

                        file.Properties.Add("reader", reader);
                        file.Properties.Add("offset", offset);
                        file.Properties.Add("length", length);
                        file.DataRequest += file_DataRequest;
                    }
                    break;
                }
                case "FARC":
                {
                    mvarCompressed = true;
                    mvarEncrypted = true;

                    // Encrypted, compressed

                    uint flag0 = reader.ReadUInt32();
                    uint flag1 = reader.ReadUInt32();
                    uint flag2 = reader.ReadUInt32();
                    uint flag3 = reader.ReadUInt32();

                    while (reader.Accessor.Position < directorySize + 8)
                    {
                        string FileName = reader.ReadNullTerminatedString();
                        int offset = reader.ReadInt32();
                        int compressedSize = reader.ReadInt32();
                        int decompressedSize = reader.ReadInt32();

                        File file = fsom.AddFile(FileName);
                        file.Size = compressedSize;

                        file.Properties.Add("reader", reader);
                        file.Properties.Add("compressedLength", compressedSize);
                        file.Properties.Add("decompressedLength", decompressedSize);
                        file.Properties.Add("offset", offset);
                        file.DataRequest += file_DataRequest;
                    }
                    break;
                }
                default:
                {
                    throw new InvalidDataFormatException("Unrecognized FARC signature: \"" + FArC + "\"");
                }
            }

        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader reader = (IO.Reader)file.Properties["reader"];
            
            int offset = (int)file.Properties["offset"];
            reader.Seek(offset, SeekOrigin.Begin);

            if (mvarCompressed && mvarEncrypted)
            {
                int compressedLength = (int)file.Properties["compressedLength"];
                int decompressedLength = (int)file.Properties["decompressedLength"];

                byte[] compressedData = reader.ReadBytes(compressedLength);

                // data encrypted? we have to decrypt it
                byte[][] keys = new byte[][]
                {
                    // Virtua Fighter 5
                    new byte[] { 0x6D, 0x4A, 0x24, 0x9C, 0x85, 0x29, 0xDE, 0x62, 0xC8, 0xE3, 0x89, 0x39, 0x31, 0xC9, 0xE0, 0xBC },
                    // Project DIVA VF5
                    new byte[] { 0x70, 0x72, 0x6F, 0x6A, 0x65, 0x63, 0x74, 0x5F, 0x64, 0x69, 0x76, 0x61, 0x2E, 0x62, 0x69, 0x6E },

                    // Other keys, don't know what they're for
                    new byte[] { 0x69, 0x17, 0x3E, 0xD8, 0xF5, 0x07, 0x14, 0x43, 0x9F, 0x62, 0x40, 0xAA, 0x74, 0x66, 0xC3, 0x7A },
                    new byte[] { 0x6D, 0x4B, 0xF3, 0xD7, 0x24, 0x5D, 0xB2, 0x94, 0xB6, 0xC3, 0xF9, 0xE3, 0x2A, 0xA5, 0x7E, 0x79 },
                    new byte[] { 0xD1, 0xDF, 0x87, 0xB5, 0xC1, 0x47, 0x1B, 0x36, 0x0A, 0xCE, 0x21, 0x31, 0x5A, 0x33, 0x9C, 0x06 }
                };

                byte[] encryptedData = null;
                bool foundMatch = false;
                for (int k = 0; k < keys.Length; k++)
                {
                    encryptedData = compressedData;
                    try
                    {
                        encryptedData = Decrypt(compressedData, keys[k]);
                        foundMatch = true;
                        break;
                    }
                    catch (CryptographicException ex1)
                    {
                        continue;
                    }
                }
                if (!foundMatch)
                {
                    UniversalEditor.UserInterface.HostApplication.Messages.Add(UserInterface.HostApplicationMessageSeverity.Warning, "No valid encryption keys were available to process this file", file.Name);
                    return;
                }

                // FIXME:   Project DIVA F key seems to work (without throwing an invalid padding error) but is not in a known compression
                //          method...
                System.IO.File.WriteAllBytes(@"C:\Temp\test.dat", encryptedData);
                e.Data = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(encryptedData);
            }
            else if (mvarCompressed && !mvarEncrypted)
            {
                int compressedLength = (int)file.Properties["compressedLength"];
                int decompressedLength = (int)file.Properties["decompressedLength"];

                byte[] compressedData = reader.ReadBytes(compressedLength);
                e.Data = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Decompress(compressedData);
            }
            else if (!mvarCompressed && !mvarEncrypted)
            {
                int length = (int)file.Properties["length"];
                
                e.Data = reader.ReadBytes(length);
            }
        }

        private byte[] Encrypt(byte[] data, byte[] key)
        {
            byte[] input = data;
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            aes.Key = key;

            System.Security.Cryptography.ICryptoTransform xform = aes.CreateEncryptor();
            int blockCount = input.Length / xform.InputBlockSize;
            for (int i = 0; i < blockCount; i++)
            {
                if (i == blockCount - 1)
                {
                    byte[] output2 = xform.TransformFinalBlock(input, i * xform.InputBlockSize, xform.InputBlockSize);
                }
                else
                {
                    int l = xform.TransformBlock(input, i * xform.InputBlockSize, xform.InputBlockSize, input, i * xform.InputBlockSize);
                }
            }
            return input;
        }
        private byte[] Decrypt(byte[] data, byte[] key)
        {
            byte[] input = data;
            System.Security.Cryptography.AesManaged aes = new System.Security.Cryptography.AesManaged();
            aes.Key = key;

            System.Security.Cryptography.ICryptoTransform xform = aes.CreateDecryptor();
            int blockCount = input.Length / xform.InputBlockSize;
            for (int i = 0; i < blockCount; i++)
            {
                if (i == blockCount - 1)
                {
                    byte[] output2 = xform.TransformFinalBlock(input, i * xform.InputBlockSize, xform.InputBlockSize);
                }
                else
                {
                    int l = xform.TransformBlock(input, i * xform.InputBlockSize, xform.InputBlockSize, input, i * xform.InputBlockSize);
                }
            }
            return input;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            File[] files = fsom.GetAllFiles();

            Writer writer = base.Accessor.Writer;
            writer.Endianness = Endianness.BigEndian;

            if (mvarCompressed && mvarEncrypted)
            {
                writer.WriteFixedLengthString("FARC");
            }
            else if (mvarCompressed && !mvarEncrypted)
            {
                writer.WriteFixedLengthString("FArC");

                int directorySize = 0;
                int dummy = 32;

                int offset = 8;

                foreach (File file in files)
                {
                    directorySize += file.Name.Length + 1;
                    directorySize += 12;
                }
                writer.WriteInt32(directorySize);
                writer.WriteInt32(dummy);

                offset += directorySize;

                Dictionary<File, byte[]> compressedDatas = new Dictionary<File, byte[]>();
                foreach (File file in files)
                {
                    writer.WriteNullTerminatedString(file.Name);
                    writer.WriteInt32(offset);

                    byte[] decompressedData = file.GetDataAsByteArray();
                    byte[] compressedData = Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Gzip).Compress(decompressedData);
                    writer.WriteInt32(compressedData.Length);
                    writer.WriteInt32(decompressedData.Length);

                    compressedDatas.Add(file, compressedData);
                }
                foreach (File file in files)
                {
                    writer.WriteBytes(compressedDatas[file]);
                }
            }
            else if (!mvarCompressed && !mvarEncrypted)
            {
                writer.WriteFixedLengthString("FArc");
                int directorySize = 4;
                int dummy = 32;
                foreach (File file in files)
                {
                    directorySize += 8 + (file.Name.Length + 1);
                }
                writer.WriteInt32(directorySize);
                writer.WriteInt32(dummy);
                
                int offset = directorySize + 8;

                foreach (File file in files)
                {
                    writer.WriteNullTerminatedString(file.Name);
                    writer.WriteInt32(offset);
                    writer.WriteInt32((int)file.Size);
                    offset += (int)file.Size;
                }

                // writer.Align(96);

                foreach (File file in files)
                {
                    writer.WriteBytes(file.GetDataAsByteArray());
                }
            }
            else
            {
                throw new NotSupportedException("Unsupported parameters");
            }
            writer.Flush();
        }
    }
}
