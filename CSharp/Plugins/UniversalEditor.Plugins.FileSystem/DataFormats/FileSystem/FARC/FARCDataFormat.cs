using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.FArC
{
    public partial class FARCDataFormat : DataFormat
    {
        private struct FileEntry
        {
            public string name;
            public int offset;
            public int compressedSize;
            public int decompressedSize;

            // for writing
            public byte[] compressedData;
        }

        private DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("FArC archive", new byte?[][] { new byte?[] { (byte)'F', (byte)'A', (byte)'r', (byte)'C' }, new byte?[] { (byte)'F', (byte)'A', (byte)'r', (byte)'c' }, new byte?[] { (byte)'F', (byte)'A', (byte)'R', (byte)'C' } }, new string[] { "*.farc" });
            }
            return _dfr;
        }

        private bool mvarEncrypted = false;
        public bool Encrypted { get { return mvarEncrypted; } set { mvarEncrypted = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            br.Endianness = IO.Endianness.BigEndian;
            string FArC = br.ReadFixedLengthString(4);
            if (!(FArC == "FArC" || FArC  == "FArc" || FArC == "FARC")) throw new InvalidDataFormatException();

            int directorySize = br.ReadInt32();
            int dummy = br.ReadInt32();

            if (FArC == "FArC")
            {
                while (br.BaseStream.Position < directorySize)
                {
                    FileEntry entry = new FileEntry();
                    entry.name = br.ReadNullTerminatedString();
                    entry.offset = br.ReadInt32();
                    entry.compressedSize = br.ReadInt32();
                    entry.decompressedSize = br.ReadInt32();

                    File file = fsom.AddFile(entry.name);
                    if (entry.decompressedSize == 0)
                    {
                        file.Size = entry.compressedSize;
                        entry.decompressedSize = entry.compressedSize;
                    }
                    else
                    {
                        file.Size = entry.decompressedSize;
                    }
                    file.Properties.Add("FileEntry", entry);
                    file.DataRequest += file_DataRequest;
                }
            }
            else if (FArC == "FArc")
            {
                while (br.BaseStream.Position - 12 < directorySize - 4)
                {
                    FileEntry entry = new FileEntry();
                    entry.name = br.ReadNullTerminatedString();
                    entry.offset = br.ReadInt32();
                    entry.compressedSize = br.ReadInt32();

                    File file = fsom.AddFile(entry.name);

                    file.Size = entry.compressedSize;
                    entry.decompressedSize = entry.compressedSize;

                    file.Properties.Add("FileEntry", entry);
                    file.DataRequest += file_DataRequest;
                }
            }
            else if (FArC == "FARC")
            {
                uint flag0 = br.ReadUInt32();
                uint flag1 = br.ReadUInt32();
                uint flag2 = br.ReadUInt32();
                uint flag3 = br.ReadUInt32();

                while (br.BaseStream.Position < directorySize + 8)
                {
                    FileEntry entry = new FileEntry();
                    entry.name = br.ReadNullTerminatedString();
                    entry.offset = br.ReadInt32();
                    entry.compressedSize = br.ReadInt32();

                    File file = fsom.AddFile(entry.name);

                    file.Size = entry.compressedSize;
                    entry.decompressedSize = br.ReadInt32();

                    file.Properties.Add("FileEntry", entry);
                    file.DataRequest += file_DataRequest;
                }
            }
            else
            {
                throw new InvalidDataFormatException("Unknown version " + FArC);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.BinaryReader br = base.Stream.BinaryReader;
            FileEntry entry = (FileEntry)file.Properties["FileEntry"];

            br.BaseStream.Position = entry.offset;

            byte[] decompressedData = new byte[entry.decompressedSize];
            if (entry.decompressedSize > 0)
            {
                byte[] compressedData = br.ReadBytes(entry.compressedSize);
                if (entry.compressedSize != entry.decompressedSize)
                {
                    try
                    {
                        decompressedData = UniversalEditor.Compression.CompressionStream.Decompress(Compression.CompressionMethod.Gzip, compressedData);
                    }
                    catch (Exception ex)
                    {
                        // data encrypted? we have to decrypt it
                        
                    }
                }
                else
                {
                    decompressedData = compressedData;
                }
            }
            else
            {
                decompressedData = br.ReadBytes(entry.compressedSize);
            }
            e.Data = decompressedData;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            bw.Endianness = IO.Endianness.BigEndian;
            int ioffset = 12, isize = 0;
            
            List<int> FileDecompressedDataLength = new List<int>();
            List<byte[]> FileCompressedData = new List<byte[]>();

            foreach (File file in fsom.Files)
            {
                ioffset += (file.Name.Length + 1) + 12;

                byte[] decompressedData = file.GetDataAsByteArray();
                byte[] compressedData = UniversalEditor.Compression.CompressionStream.Compress(Compression.CompressionMethod.Gzip, decompressedData);
                FileCompressedData.Add(compressedData);

                isize += ioffset + compressedData.Length;
            }

            bw.WriteFixedLengthString("FArC");

            int filesize = isize; 
            bw.Write(filesize);
            int dummy = 0;
            bw.Write(dummy);

            // ioffset = 12;
            int i = 0;
            foreach (File file in fsom.Files)
            {
                bw.WriteNullTerminatedString(file.Name);
                bw.Write(ioffset);

                byte[] compressedData = FileCompressedData[i];
                bw.Write(compressedData.Length);
                bw.Write((int)file.Size);

                ioffset += compressedData.Length;
                i++;
            }
            foreach (byte[] data in FileCompressedData)
            {
                bw.Write(data);
            }
            bw.Flush();
        }
    }
}
