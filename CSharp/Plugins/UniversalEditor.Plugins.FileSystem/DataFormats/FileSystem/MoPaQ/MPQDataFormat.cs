using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
    public partial class MPQDataFormat : DataFormat
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

        private MPQHeader hdr;
        private uint sizeOfEachLogicalSector = 0;

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader br = base.Accessor.Reader;
            string MPQ = br.ReadFixedLengthString(3);
            byte magic2 = br.ReadByte();
            if (MPQ != "MPQ") throw new InvalidDataFormatException("File does not begin with \"MPQ\"");
            
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            switch (magic2)
            {
                case 0x1B: // MPQ_USERDATA
                {
                    break;
                }
                case 0x1A: // MPQ
                {
                    hdr = MPQHeader.Read(br);
                    sizeOfEachLogicalSector = (uint)(512 * (Math.Pow(2, hdr.wBlockSize)));
                    
                    br.Accessor.Position = hdr.dwBlockTablePos;


                    uint blockTableSize = (hdr.dwBlockTableEntryCount * 16);
                    byte[] compressedData = br.ReadBytes(blockTableSize);

                    uint compressedSize = (hdr.dwArchiveSize - hdr.dwBlockTablePos);
                    if (compressedSize != blockTableSize)
                    {
                        // block table is compressed
                    }
                    else
                    {
                        byte[] decryptedData = MPQEncryption.Decrypt(compressedData, -326913117);

                        IO.Reader brh = new IO.Reader(new MemoryAccessor(decryptedData));
                        int i = 0;
                        while (!brh.EndOfStream)
                        {
                            // Offset of the beginning of the file data, relative to the beginning of the archive.
                            uint dwFilePos = brh.ReadUInt32();

                            // Compressed file size
                            uint dwCSize = brh.ReadUInt32();

                            // Size of uncompressed file
                            uint dwFSize = brh.ReadUInt32();

                            // Flags for the file. See the table below for more informations
                            MPQBlockTableEntryFlags dwFlags = (MPQBlockTableEntryFlags)brh.ReadUInt32();

                            File file = new File();
                            file.Name = i.ToString().PadLeft(8, '0');
                            file.Size = dwFSize;
                            file.Properties.Add("offset", dwFilePos);
                            file.Properties.Add("CompressedLength", dwCSize);
                            file.Properties.Add("DecompressedLength", dwFSize);
                            file.Properties.Add("flags", dwFlags);
                            file.DataRequest += file_DataRequest;
                            fsom.Files.Add(file);
                            i++;
                        }
                    }

                    if ((ushort)hdr.wFormatVersion >= 2)
                    {
                        // Beginning with format version 3 (first time observed during beta testing of World of Warcraft - Cataclysm), MPQs can contain a HET
                        // table. The HET table is present if the HetTablePos64 member of MPQ header is set to nonzero. This table can fully replace hash table.
                        // Depending on MPQ size, the pair of HET&BET table can be more efficient than Hash&Block table. HET table can be encrypted and compressed.

                        HETTable het = HETTable.Read(br);

                        // HET hash table. Each entry is 8 bits.
                        byte[] hashTable = br.ReadBytes(het.dwHashTableSize);

                        // Array of file indexes. Bit size of each entry is taken from dwTotalIndexSize.
                        // Table size is taken from dwHashTableSize.
                    }
                    break;
                }
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            /*
            string name = String.Empty;
            int hashindex = GetHashIndexFromFileName(name);
            if (hashindex == -1) return;

            // get the block index
            // int blockindex = GetBlockIndexFromHashIndex(hashindex);
            */

            // get the block info for this file
            // ByteBuffer buf = ByteBuffer.wrap(blockTable);
            // buf.order(ByteOrder.LITTLE_ENDIAN);

            File file = (sender as File);
            uint offset = (uint)file.Properties["offset"];
            uint decompressedSize = (uint)file.Properties["DecompressedLength"];
            uint compressedSize = (uint)file.Properties["CompressedLength"];
            
            MPQBlockTableEntryFlags flags = (MPQBlockTableEntryFlags)file.Properties["flags"];

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = offset;
            byte[] compressedData = br.ReadBytes(compressedSize);

            bool encrypted = false;
            uint filekey = 0;
            if ((flags & MPQBlockTableEntryFlags.Encrypted) == MPQBlockTableEntryFlags.Encrypted)
            {
                encrypted = true;

                /*
                int idx = name.lastIndexOf('\\');
                String fname;
                if (idx == -1)
                    fname = name;
                else
                    fname = name.substring(idx);
                */

                // compute filekey
                string fname = String.Empty;
                filekey = MPQEncryption.ComputeHash(fname, MPQHashMode.EncryptionKeys);
                if ((flags & MPQBlockTableEntryFlags.DynamicKey) == MPQBlockTableEntryFlags.DynamicKey)
                {
                    filekey = (uint)((filekey + offset) ^ decompressedSize);
                }
                flags = (MPQBlockTableEntryFlags)((uint)flags & ~0x030000);
            }
            
            if ((flags & MPQBlockTableEntryFlags.Compressed) == MPQBlockTableEntryFlags.Compressed)
            {
                IO.Reader brh = new IO.Reader(new MemoryAccessor(compressedData));
                // first read the sector table
                int numsectors = (int)((decompressedSize + sizeOfEachLogicalSector - 1) / sizeOfEachLogicalSector + 1);

                byte[] filedata = new byte[decompressedSize];
                int o = 0;
                List<uint> sectorOffsets = new List<uint>();
                for (int i = 0; i < numsectors; i++)
                {
                    uint sectorOffset = brh.ReadUInt32();
                    sectorOffsets.Add(sectorOffset);
                }
                for (int i = 0; i < numsectors - 1; i++)
                {
                    brh.Accessor.Position = sectorOffsets[i];

                    uint sectorSize = 0;
                    sectorSize = (uint)(sectorOffsets[i + 1] - sectorOffsets[i]);

                    MPQCompressionType compressionMethod = (MPQCompressionType)brh.ReadByte();
                    byte[] sectorData = brh.ReadBytes(sectorSize - 1);

                    switch (compressionMethod)
                    {
                        case MPQCompressionType.Deflate:
                        {
                            sectorData = CompressionModules.Deflate.Decompress(sectorData);
                            break;
                        }
                        default:
                        {
                            brh.Accessor.Position -= sectorSize;
                            sectorData = brh.ReadBytes(sectorSize);
                            break;
                        }
                    }
                    
                    Array.Copy(sectorData, 0, filedata, o, sectorData.Length);
                    o += sectorData.Length;
                }
                brh.Close();
                e.Data = filedata;
            }
            /*
            // if (flags != -2147483136) throw new System.IO.IOException("Requiring compressed file block type, but has " + Integer.toHexString(flags) + "!");

            // extract the sector table for this file
            int sectorsize = 512 * (2 << (sectorSizeShift - 1));
            int numsectors = (int)((decompressedSize + sectorsize - 1) / sectorsize + 1);
            byte[] sectorTable = new byte[numsectors * 4];
            br.Accessor.Seek(offset);
            byte[] sectorTable = br.ReadToEnd();
            if (encrypted)
            {
                sectorTable = MPQEncryption.Decrypt(sectorTable, filekey - 1);
            }
            ByteBuffer secbuf = ByteBuffer.wrap(sectorTable);
            secbuf.order(ByteOrder.LITTLE_ENDIAN);

            //logger.trace( "numsectors="+numsectors+" * "+sectorsize );
            //for( int i=0;i<numsectors;++i )
            //    logger.trace( "sector "+i+" "+secbuf.getInt( i*4 ) );

            // extract the data
            FileChannel channel = file.getChannel();
            byte[] data = new byte[filesize];

            int sectoroffset = 0;

            for (int i = 0; i < numsectors - 1; ++i)
            {
                int offset = blockoffset + secbuf.getInt(i * 4);
                int size = secbuf.getInt(i * 4 + 4) - secbuf.getInt(i * 4);
                ByteBuffer sector = channel.map(FileChannel.MapMode.READ_ONLY, offset, size);
                sector.order(ByteOrder.LITTLE_ENDIAN);

                byte[] tmpdata = new byte[size];
                sector.get(tmpdata);

                if (encrypted)
                    decrypt(tmpdata, 0, tmpdata.length, filekey + i);

                byte compressiontype = tmpdata[0];

                //logger.trace( "size "+size+" "+sectoroffset );

                if (compressiontype == 0x02)
                {
                    Inflater inflater = new Inflater();
                    inflater.setInput(tmpdata, 1, tmpdata.length - 1);

                    try
                    {
                        sectoroffset += inflater.inflate(data, sectoroffset, data.length - sectoroffset);
                    }
                    catch (DataFormatException e)
                    {
                        throw new IOException("Failed to decompress: " + e);
                    }
                }
                else
                    if (compressiontype == 0x08)
                    {
                        // TODO
                        SlicedArray input = new SlicedArray(tmpdata, 1, tmpdata.length - 1);
                        SlicedArray output = new SlicedArray(data, sectoroffset, data.length - sectoroffset);
                        sectoroffset += Explode.explode(input, output);
                    }
                    else
                        throw new System.IO.IOException("Unknown compression type " + Integer.toHexString((int)compressiontype) + "!");
                //logger.trace( "deflated "+sectoroffset+" bytes." );
            }

            */
        }

        private int GetHashIndexFromFileName(string name)
        {
            throw new NotImplementedException();
        }

        private byte[] hashTable = new byte[1024];
        private int GetBlockIndexFromHashIndex(int hashindex)
        {
            int offset = (hashindex * 16 + 4 * 3);
            return BitConverter.ToInt32(hashTable, offset);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("MPQ");
            bw.WriteByte((byte)0x1B);


        }
    }
}
