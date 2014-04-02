using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HPK
{
    public class HPKDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Tropico HPK/BPUL archive", new byte?[][] { new byte?[] { (byte)'B', (byte)'P', (byte)'U', (byte)'L' }, new byte?[] { (byte)'Z', (byte)'L', (byte)'I', (byte)'B' } }, new string[] { "*.hpk" });
                _dfr.ExportOptions.Add(new CustomOptionBoolean("Compressed", "Compress the file using Zlib", false));
            }
            return _dfr;
        }

        private bool mvarCompressed = false;
        public bool Compressed { get { return mvarCompressed; } set { mvarCompressed = value; } }

        private enum EntryType
        {
            File = 0,
            Folder = 1
        }
        private struct EntryInfo
        {
            public int SequenceNumber;
            public string Name;
            public EntryType Type;
        }
        private struct FileInfo
        {
            public int offset;
            public int length;
        }

        private Dictionary<int, Folder> folderSequenceEntries = new Dictionary<int, Folder>();

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;

            string BPUL = br.ReadFixedLengthString(4);
            if (BPUL != "BPUL" && BPUL != "ZLIB") throw new InvalidDataFormatException("File does not begin with \"BPUL\" or \"ZLIB\"");

            int decompressedFileSize = br.ReadInt32();        // 36
            int u2 = br.ReadInt32();        // 1
            int u3 = br.ReadInt32();        // -1

            if (BPUL == "ZLIB")
            {
                byte[] zlibdata = br.ReadToEnd();
                byte[] data = UniversalEditor.Compression.CompressionModules.Zlib.Decompress(zlibdata);
                if (data.Length != decompressedFileSize) throw new InvalidDataFormatException("Decompressed size does not match stored value!");

                br = new IO.Reader(data);
                br.Accessor.Position = 16;
                mvarCompressed = true;
            }
            else
            {
                mvarCompressed = false;
            }

            int u4 = br.ReadInt32();        // 0
            int u5 = br.ReadInt32();        // 0
            int u6 = br.ReadInt32();        // 1

            int offsetToFileOffsets = br.ReadInt32();
            List<FileInfo> fis = new List<FileInfo>();

            #region Read File Offsets and Lengths
            long curpos = br.Accessor.Position;
            br.Accessor.Position = offsetToFileOffsets;
            while (!br.EndOfStream)
            {
                FileInfo fi = new FileInfo();
                fi.offset = br.ReadInt32();
                fi.length = br.ReadInt32();
                fis.Add(fi);
            }
            br.Accessor.Position = curpos;
            #endregion

            int sizeOfFileInfoBlock = br.ReadInt32(); // 8 * (filecount + 1)
            int sanityCheck = (8 * fis.Count);
            if (sanityCheck != sizeOfFileInfoBlock) throw new InvalidDataFormatException("Size of file info block doesn't match!"); 

            for (int i = 1; i < fis.Count; i++)
            {
                EntryInfo entry = ReadEntry(br);
                LoadEntry(br, fsom, entry, fis, ref i);
            }
        }

        private void LoadEntry(IO.Reader br, FileSystemObjectModel fsom, EntryInfo entry, List<FileInfo> fis, ref int i)
        {
            FileInfo fi = fis[i];
            switch (entry.Type)
            {
                case EntryType.Folder:
                {
                    Folder folder = new Folder();
                    folder.Name = entry.Name;

                    long pos = br.Accessor.Position;
                    br.Accessor.Position = fi.offset;
                    byte[] folderData = br.ReadBytes(fi.length);
                    br.Accessor.Position = pos;

                    IO.Reader brf = new IO.Reader(folderData);

                    int j = 1;
                    while (!brf.EndOfStream)
                    {
                        EntryInfo ei = ReadEntry(brf);

                        // map this file entry to a folder
                        folderSequenceEntries.Add(ei.SequenceNumber, folder);

                        i += 1;
                        LoadEntry(br, fsom, ei, fis, ref i);

                        j++;
                    }

                    if (folderSequenceEntries.ContainsKey(entry.SequenceNumber))
                    {
                        folderSequenceEntries[entry.SequenceNumber].Folders.Add(folder);
                    }
                    else
                    {
                        fsom.Folders.Add(folder);
                    }
                    break;
                }
                case EntryType.File:
                {
                    File file = new File();
                    file.Name = entry.Name;
                    file.Size = fi.length;
                    file.Properties.Add("offset", fi.offset);
                    file.Properties.Add("length", fi.length);
                    file.Properties.Add("reader", br);
                    file.DataRequest += file_DataRequest;

                    if (folderSequenceEntries.ContainsKey(entry.SequenceNumber))
                    {
                        folderSequenceEntries[entry.SequenceNumber].Files.Add(file);
                    }
                    else
                    {
                        fsom.Files.Add(file);
                    }
                    break;
                }
            }
        }

        private EntryInfo ReadEntry(IO.Reader br)
        {
            EntryInfo ei = new EntryInfo();
            ei.SequenceNumber = br.ReadInt32();
            ei.Type = (EntryType)br.ReadInt32();

            short fnamelen = br.ReadInt16();
            ei.Name = br.ReadFixedLengthString(fnamelen);
            return ei;
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            int offset = (int)file.Properties["offset"];
            int length = (int)file.Properties["length"];
            IO.Reader br = (IO.Reader)file.Properties["reader"];

            br.Accessor.Position = offset;
            e.Data = br.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            // TODO: Fix this... it generates a BAD output file!! and it doesn't know how to
            // generate EntryInfos... :(

            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Writer bwf = base.Accessor.Writer;

            MemoryAccessor ma = new MemoryAccessor();
            IO.Writer bw = new IO.Writer(ma);
            bw.WriteFixedLengthString("BPUL");
            int headerSize = 36;

            bw.WriteInt32(headerSize);
            bw.WriteInt32((int)1);
            bw.WriteInt32((int)-1);



            bw.WriteInt32((int)0);
            bw.WriteInt32((int)0);
            bw.WriteInt32((int)1);

            int offsetToFileOffsets = headerSize;
            foreach (File file in fsom.Files)
            {
                offsetToFileOffsets += (int)file.Size;
                offsetToFileOffsets += file.Name.Length;
                offsetToFileOffsets += 10;
            }
            bw.WriteInt32(offsetToFileOffsets);
            
            List<FileInfo> fis = new List<FileInfo>();


            #region Create the root directory entry
            FileInfo fiRoot = new FileInfo();
            fiRoot.offset = 36;
            foreach (File file in fsom.Files)
            {
                fiRoot.length += (file.Name.Length + 10);
            }
            fis.Add(fiRoot);
            #endregion

            int seq = 2;
            int ofs = (int)(bw.Accessor.Position + fiRoot.length + 4);
            List<EntryInfo> eis = new List<EntryInfo>();
            foreach (File file in fsom.Files)
            {
                FileInfo fi = new FileInfo();
                fi.offset = ofs;
                fi.length = (int)file.Size;
                ofs += fi.length;

                EntryInfo ei = new EntryInfo();
                ei.Name = file.Name;
                ei.SequenceNumber = seq;
                ei.Type = EntryType.File;
                seq++;

                fis.Add(fi);
                eis.Add(ei);
            }

            int sizeOfFileInfoBlock = (8 * fis.Count);
            bw.WriteInt32(sizeOfFileInfoBlock);

            foreach (EntryInfo ei in eis)
            {
                bw.WriteInt32(ei.SequenceNumber);
                bw.WriteInt32((int)ei.Type);

                bw.WriteInt16((short)ei.Name.Length);
                bw.WriteFixedLengthString(ei.Name);
            }

            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());
            }

            foreach (FileInfo fi in fis)
            {
                bw.WriteInt32(fi.offset);
                bw.WriteInt32(fi.length);
            }

            bw.Close();



            byte[] decompressed = ma.ToArray();
            if (mvarCompressed)
            {
                byte[] compressed = CompressionModules.Zlib.Compress(decompressed);
                bwf.WriteFixedLengthString("ZLIB");
                bwf.WriteInt32((int)decompressed.Length);
                bwf.WriteInt32((int)1);
                bwf.WriteInt32((int)-1);
                bwf.WriteBytes(compressed);
            }
            else
            {
                bwf.WriteBytes(decompressed);
            }
        }
    }
}
