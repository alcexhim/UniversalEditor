using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;

namespace UniversalEditor.DataFormats.FileSystem.MoPaQ
{
    public static class MPQEncryption
    {
        static MPQEncryption()
        {
            Initialize();
        }

        private static uint[] cryptTable = null;

        /// <summary>
        /// Prepares the crypt table. This has to be performed before any encryption.
        /// </summary>
        public static void Initialize()
        {
            if (cryptTable != null) return;

            cryptTable = new uint[256 * 5];

            int seed = 0x00100001;

            for (int i = 0; i < 256; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    int k = i + j * 256;

                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    int temp1 = (seed & 0xffff) << 16;

                    seed = (seed * 125 + 3) % 0x2AAAAB;
                    int temp2 = (seed & 0xffff);

                    cryptTable[k] = (uint)(temp1 | temp2);
                }
            }
        }


        /// <summary>
        /// Decrypt a block of data.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="offset"></param>
        /// <param name="size"></param>
        /// <param name="key"></param>
        public static byte[] Decrypt(byte[] data, int key)
        {
            uint seed = 0xeeeeeeee;

            IO.Reader br = new IO.Reader(new MemoryAccessor(data));
            br.Endianness = IO.Endianness.LittleEndian;

            MemoryAccessor ma = new MemoryAccessor();
            IO.Writer bw = new IO.Writer(ma);

            for (int i = 0; i < br.Accessor.Length / 4; ++i)
            {
                uint value = br.ReadUInt32();

                seed += cryptTable[4 * 256 + (key & 0xff)];
                value = (uint)(value ^ (key+seed));

                key = (int)((uint)(((~key) << 0x15) + 0x11111111) | ((uint)key >> 0x0b));
                seed = value + seed + (seed << 5 ) + 3;

                bw.WriteUInt32((uint)value);
            }

            bw.Flush();
            bw.Close();
            return ma.ToArray();
        }

        public static uint ComputeHash(String str, MPQHashMode mode)
        {
            uint seed1 = 0x7fed7fed;
            uint seed2 = 0xeeeeeeee;

            String upper = str.ToUpper();

            for (int i = 0; i < upper.Length; ++i)
            {
                int ch = (int)upper[i];
                seed1 = cryptTable[(int)mode * 256 + ch] ^ (seed1 + seed2);
                seed2 = (uint)(ch + seed1 + seed2 + (seed2 << 5) + 3);
            }

            return seed1;
        }
    }
}
