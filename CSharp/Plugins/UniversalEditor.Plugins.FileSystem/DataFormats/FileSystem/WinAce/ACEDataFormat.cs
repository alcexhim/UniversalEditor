using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.WinAce
{
    public class ACEDataFormat : DataFormat
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
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Reader br = base.Accessor.Reader;
            if (br.Accessor.Length < 14) throw new InvalidDataFormatException("File must be at least 14 bytes in length");
            br.Accessor.Position = 0;

            crc_init();

            ushort HEAD_CRC = br.ReadUInt16();          // 37941
            ushort HEAD_SIZE = br.ReadUInt16();         // 49
            
            #region header
            byte HEAD_TYPE = br.ReadByte();
            ACEArchiveFlags HEAD_FLAGS = (ACEArchiveFlags)br.ReadUInt16();     // 36866 - comment, 36864 - normal

            string ACESIGN = br.ReadFixedLengthString(7);
            if (ACESIGN != "**ACE**") throw new InvalidDataFormatException("File does not contain \"**ACE**\" signature");

            byte VER_MOD = br.ReadByte();           // 20
            byte VER_CR = br.ReadByte();            // 20
            byte HOST_CR = br.ReadByte();           // 2
            byte VOL_NUM = br.ReadByte();           // 0
            uint TIME_CR = br.ReadUInt32();         // 1117062890
            uint TIME_22 = br.ReadUInt32();         // 2017284461

            byte commenLeng = br.ReadByte();
            byte commenLeng1 = br.ReadByte();
            ushort RES2 = br.ReadUInt16();
            byte AV_SIZE = br.ReadByte();
            string AV = br.ReadFixedLengthString(AV_SIZE);

            if ((HEAD_FLAGS & ACEArchiveFlags.HasComment) == ACEArchiveFlags.HasComment)
            {
                ushort commentSize = br.ReadUInt16();
                byte[] comment = br.ReadBytes(commentSize);
            }

            #endregion

            while (!br.EndOfStream)
            {
                ushort r0 = br.ReadUInt16();                    // 47000
                ushort r1 = br.ReadUInt16();                    // 47
                ushort r2 = br.ReadUInt16();                    // 257
                byte un1 = br.ReadByte();                       // 128

                uint compressedLength = br.ReadUInt32();
                uint decompressedLength = br.ReadUInt32();

                uint unknown1 = br.ReadUInt32();                    // 1117044212
                uint unknown2 = br.ReadUInt32();                    // 32
                uint file_crc = br.ReadUInt32();                    // 2754832349
                uint unknown4 = br.ReadUInt32();                    // 656128
                ushort unknown5 = br.ReadUInt16();                  // 17748
                ushort filenameLength = br.ReadUInt16();
                string filename = br.ReadFixedLengthString(filenameLength);
                byte[] compressedData = br.ReadBytes(compressedLength);

                byte[] decompressedData = compressedData;
                switch (unknown4)
                {
                    case 656130:
                    {
                        // decompressedData = UniversalEditor.Compression.LZW.LZWStream.Decompress(compressedData);
                        break;
                    }
                }

                fsom.Files.Add(filename, decompressedData);
            }
        }

        /*
        private void dcpr_comm_init()
        {
            int i = comm_cpr_size > size_rdb * 4 ? size_rdb * 4 : comm_cpr_size;
            if (!f_err)
            {
                // memcpy(buf_rd, comm, i);
            }

            code_rd = buf_rd[0];
            rpos = bits_rd = 0;
        }

        private void dcpr_comm(int comm_size)
        {
            short[] hash = new short[comm_cpr_hf(255 + 255) + 1];
            int dpos = 0, c, pos, len, hs;
    
            if (comm_cpr_size != 0)
            {
                dcpr_comm_init();
                len = code_rd >> (32 - 15);
                addbits(15);
                if (len >= comm_size)
                {
                    len = comm_size - 1;
                }
                if (read_wd(maxwd_mn, dcpr_code_mn, dcpr_wd_mn, max_cd_mn))
                {
                    do
                    {
                        if (dpos > 1)
                        {
                            pos = hash[hs = comm_cpr_hf(comm[dpos - 1], comm[dpos - 2])];
                            hash[hs] = dpos;
                        }
                        addbits(dcpr_wd_mn[(c = dcpr_code_mn[code_rd >> (32 - maxwd_mn)])]);
                        if (rpos == size_rdb - 3)
                        {
                            rpos = 0;
                        }
                        if (c > 255)
                        {
                            c -= 256;
                            c += 2;
                            while (c--)
                            {
                                comm[dpos++] = comm[pos++];
                            }
                        }
                        else
                        {
                            comm[dpos++] = c;
                        }
                    }
                    while (dpos < len);
                }
                comm[len] = 0;
            }
        }

        */

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Writer bw = base.Accessor.Writer;

            crc_init();

            byte[] header = new byte[0];
            #region header
            {
                MemoryAccessor mah = new MemoryAccessor();
                IO.Writer bwh = new IO.Writer(mah);

                byte HEAD_TYPE = 0;
                bwh.WriteByte(HEAD_TYPE);

                ACEArchiveFlags HEAD_FLAGS = (ACEArchiveFlags)36864;
                bwh.WriteUInt16((ushort)HEAD_FLAGS);

                bwh.WriteFixedLengthString("**ACE**");

                byte VER_MOD = 20;
                bwh.WriteByte(VER_MOD);

                byte VER_CR = 20;
                bwh.WriteByte(VER_CR);

                byte HOST_CR = 2;
                bwh.WriteByte(HOST_CR);

                byte VOL_NUM = 0;                         
                bwh.WriteByte(VOL_NUM);                       
                                                          
                uint TIME_CR = 1117062890;                
                bwh.WriteUInt32(TIME_CR);                       
                                                          
                uint TIME_22 = 2017284461;                
                bwh.WriteUInt32(TIME_22);                       

                ushort RES1 = 0;
                bwh.WriteUInt16(RES1);

                ushort RES2 = 0;
                bwh.WriteUInt16(RES2);

                string AV = "*UNREGISTERED VERSION*";
                byte AV_SIZE = (byte)AV.Length;
                bwh.WriteByte(AV_SIZE);
                bwh.WriteFixedLengthString(AV);
                bwh.Flush();
                bwh.Close();

                header = mah.ToArray();
            }
            #endregion

            ushort HEAD_CRC = (ushort)crc_calculate(CRCMASK, header);

            bw.WriteUInt16(HEAD_CRC); // 37941
            bw.WriteUInt16((ushort)header.Length);

            bw.WriteBytes(header);

            foreach (File file in fsom.Files)
            {
                ushort r0 = 47000;
                bw.WriteUInt16(r0);  
                ushort r1 = 47;
                bw.WriteUInt16(r1);
                ushort r2 = 257;
                bw.WriteUInt16(r2);

                byte un1 = 128;
                bw.WriteByte(un1);

                byte[] decompressedData = file.GetData();
                byte[] compressedData = decompressedData;

                bw.WriteUInt32((uint)compressedData.Length);
                bw.WriteUInt32((uint)decompressedData.Length);

                uint unknown1 = 1117044212;                    
                bw.WriteUInt32(unknown1);                            
                uint unknown2 = 32;                            
                bw.WriteUInt32(unknown2);

                uint file_crc = 2754832349;
                file_crc = (uint)crc_calculate(CRCMASK, decompressedData);
                bw.WriteUInt32(file_crc);
                uint unknown4 = 656128;
                bw.WriteUInt32(unknown4);
                ushort unknown5 = 17748;
                bw.WriteUInt16(unknown5);

                bw.WriteUInt16((ushort)file.Name.Length);
                bw.WriteFixedLengthString(file.Name);
                bw.WriteBytes(compressedData);
            }
        }

        private const uint CRCMASK = 0xFFFFFFFF;
        private const uint CRCPOLY = 0xEDB88320;
        private uint[] crctable;

        private void crc_init()   // initializes CRC table
        {
            crctable = new uint[256];
            uint r = 0, j = 0;
            for (uint i = 0; i <= 255; i++)
            {
                for (r = i, j = 8; j != 0; j--)
                {
                    r = (uint)(((r & 1) != 0) ? (r >> 1) ^ CRCPOLY : (r >> 1));
                }
                crctable[i] = r;
            }
        }

// Updates crc from addr till addr+len-1
//
        private uint crc_calculate(uint crc, byte[] addr)
        {
            for (int i = 0; i < addr.Length; i++)
            {
                crc = crctable[(byte)(crc ^ addr[i])] ^ (crc >> 8);
            }
            return crc;
        }
    }
}
