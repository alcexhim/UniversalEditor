using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.CompressedBG
{
    // portions from minirop's arc-reader, licensed under the zlib license (GPL compatible)
    // https://github.com/minirop/arc-reader/blob/master/cbg.c

    /*
    Arc Reader - Reading .arc files from the BGI engine
    Copyright (C) 2011 Alexander Roper (minirop@peyj.com)
    Translated from C to C# into a Universal Editor plugin by Michael Becker (alcexhim@gmail.com)

    This software is provided 'as-is', without any express or implied warranty.
    In no event will the authors be held liable for any damages arising from the use of this software.

    Permission is granted to anyone to use this software for any purpose,
    including commercial applications, and to alter it and redistribute it freely,
    subject to the following restrictions:

    1. The origin of this software must not be misrepresented;
       you must not claim that you wrote the original software.
       If you use this software in a product, an acknowledgment
       in the product documentation would be appreciated but is not required.

    2. Altered source versions must be plainly marked as such,
       and must not be misrepresented as being the original software.

    3. This notice may not be removed or altered from any source distribution.
    */

    public class CompressedBGDataFormat : DataFormat 
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null) _dfr = base.MakeReference();
            _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
            _dfr.Filters.Add("Buriko General Interpreter compressed image", new byte?[][] { new byte?[] { (byte)'C', (byte)'o', (byte)'m', (byte)'p', (byte)'r', (byte)'e', (byte)'s', (byte)'s', (byte)'e', (byte)'d', (byte)'B', (byte)'G', (byte)'_', (byte)'_', (byte)'_', (byte)0 } }, new string[] { "*.cbg" });
            return _dfr;
        }

        private int hash_update(ref int hash_val)
        {
            int eax = 0, edx = 0;
            edx = (20021 * hash_val.LowerWord());
            eax = (20021 * hash_val.UpperWord()) + (346 * hash_val) + edx.UpperWord();
            hash_val = (eax.LowerWord() << 16) + edx.LowerWord() + 1;
            return eax & 0x7FFF;
        }
        private uint hash_update(ref uint hash_val)
        {
            uint eax = 0, edx = 0;
            edx = (20021 * hash_val.LowerWord());
            eax = (20021 * hash_val.UpperWord()) + (346 * hash_val) + edx.UpperWord();
            hash_val = (eax.LowerWord() << 16) + edx.LowerWord() + 1;
            return eax & 0x7FFF;
        }

        private int method2(uint[/*256*/] table1, Internal.NodeCBG[/*511*/] table2)
        {
            int sum_of_values = 0;
            Internal.NodeCBG node = new Internal.NodeCBG();
            int cnodes = 256;
            int[] vinfo = new int[2];

            for (int i = 0; i < 256; i++)
            {
                table2[i].vv = new int[6];

                table2[i].vv[0] = ((table1[i] > 0) ? 1 : 0);
                table2[i].vv[1] = (int)table1[i];
                table2[i].vv[2] = 0;
                table2[i].vv[3] =-1;
                table2[i].vv[4] = i;
                table2[i].vv[5] = i;
                sum_of_values += (int)table1[i];
            }

            node.vv = new int[6];
            node.vv[0] = 0;
            node.vv[1] = 0;
            node.vv[2] = 1;
            node.vv[3] =-1;
            node.vv[4] =-1;
            node.vv[5] =-1;

            for (int i = 0; i < 255; i++)
            {
                table2[256 + i] = node;
            }

            while (true)
            {
                uint m;
                for(m = 0; m < 2; m++)
                {
                    uint min_value = 0xFFFFFFFF;
                    vinfo[m] = Int32.MaxValue;

                    for (int i = 0; i < cnodes; i++)
                    {
                        Internal.NodeCBG cnode = table2[i];

                        if ((cnode.vv[0] != 0) && (cnode.vv[1] < min_value))
                        {
                            vinfo[m] = i;
                            min_value = (uint)cnode.vv[1];
                        }
                    }

                    if ((uint)vinfo[m] != UInt32.MaxValue)
                    {
                        table2[vinfo[m]].vv[0] = 0;
                        table2[vinfo[m]].vv[3] = cnodes;
                    }
                }

                node.vv[0] = 1;
                node.vv[1] = (((uint)vinfo[1] != 0xFFFFFFFF) ? table2[vinfo[1]].vv[1] : 0) + table2[vinfo[0]].vv[1];
                node.vv[2] = 1;
                node.vv[3] =-1;
                node.vv[4] = vinfo[0];
                node.vv[5] = vinfo[1];

                table2[cnodes++] = node;

                if (node.vv[1] == sum_of_values) break;
            }
            return cnodes - 1;
        }

        private uint readVariable(byte[] ptr)
        {
            byte c;
            uint v = 0;
            int shift = 0;
            int i = 0;
            do
            {
                c = ptr[i];
                i++;
                v |= (uint)((c & 0x7F) << shift);
                shift += 7;
            }
            while ((c & 0x80) == 0x80);

            return v;
        }

        private static uint color_avg(uint x, uint y)
        {
            uint a = (((x & 0xFF000000) / 2) + ((y & 0xFF000000) / 2)) & 0xFF000000;
            uint r = (((x & 0x00FF0000) + (y & 0x00FF0000)) / 2) & 0x00FF0000;
            uint g = (((x & 0x0000FF00) + (y & 0x0000FF00)) / 2) & 0x0000FF00;
            uint b = (((x & 0x000000FF) + (y & 0x000000FF)) / 2) & 0x000000FF;
            return (a | r | g | b);
        }

        private static uint color_add(uint x, uint y)
        {
            uint a = ((x & 0xFF000000) + (y & 0xFF000000)) & 0xFF000000;
            uint r = ((x & 0x00FF0000) + (y & 0x00FF0000)) & 0x00FF0000;
            uint g = ((x & 0x0000FF00) + (y & 0x0000FF00)) & 0x0000FF00;
            uint b = ((x & 0x000000FF) + (y & 0x000000FF)) & 0x000000FF;

            return (a | r | g | b);
        }

        private static uint extract(byte[] src, uint bpp)
        {
            IO.Reader br = new IO.Reader(new MemoryAccessor(src));
            if (bpp == 32)
            {
                return br.ReadUInt32();
            }
            else
            {
                uint r = 0, g = 0, b = 0;
                r = br.ReadByte();
                if (bpp == 24)
                {
                    g = br.ReadByte();
                    b = br.ReadByte();
                }
                else
                {
                    g = r;
                    b = r;
                }
                return (0xff000000 | r << 16 | g << 8 | b);
            }
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureObjectModel pic = (objectModel as PictureObjectModel);
            IO.Reader br = base.Accessor.Reader;
            string magic = br.ReadFixedLengthString(16);
            if (magic != "CompressedBG___\0") throw new InvalidDataFormatException();

            ushort width = br.ReadUInt16();
            ushort height = br.ReadUInt16();
            uint bitsPerPixel = br.ReadUInt32();
            uint padding0 = br.ReadUInt32();
            uint padding1 = br.ReadUInt32();

            uint data1Length = br.ReadUInt32();
            uint data0Value = br.ReadUInt32();
            uint data0Length = br.ReadUInt32();

            byte sum_check = br.ReadByte();
            byte xor_check = br.ReadByte();
            byte unknown = br.ReadByte();

            byte sum_data = 0, xor_data = 0;

            uint[] table = new uint[256];
            Internal.NodeCBG[] table2 = new Internal.NodeCBG[511];

            byte[] data0 = br.ReadBytes(data0Length);
            for (int i = 0; i < data0Length; i++)
            {
                data0[i] -= (byte)(hash_update(ref data0Value) & 0xFF);
                sum_data += data0[i];
                xor_data ^= data0[i];
            }

            if (sum_data != sum_check || xor_data != xor_check)
            {
                throw new DataCorruptedException();
            }

            for(int i = 0; i < 256; i++)
            {
                table[i] = readVariable(data0);
            }
            data0 = null;

            uint method2_res = (uint)method2(table, table2);
            byte[] data1 = new byte[data1Length];
            #region Decrypt data
            for (int i = 0; i < data1Length; i++)
            {
                uint cvalue = method2_res;
                if (table2[method2_res].vv[2] == 1)
                {
                    byte crypted = br.ReadByte();
                    uint mask = 0x80;
                    do
                    {
                        uint value = (crypted & mask);
                        
                        // double-NOT the value
                        if (value != 0) value = 1;
                        if (value == 1) value = 0;

                        int bit = (int)value;
                        mask >>= 1;

                        cvalue = (uint)table2[cvalue].vv[4 + bit];

                        if (mask != 0)
                        {
                            crypted = br.ReadByte();
                            mask = 0x80;
                        }
                    }
                    while(table2[cvalue].vv[2] == 1);
                }

                data1[i] = (byte)cvalue;
            }

            byte[] data3 = new byte[width * height * 4];
            byte[] psrc = data1;
            byte[] pdst = data3;
            bool type = false;

            int psrci = 0, pdsti = 0;
            while (psrci < data1Length)
            {
                int len = (int)readVariable(psrc);
                if (type)
                {
                    for(int i = 0; i < len; i++)
                    {
                        pdst[i] = 0;
                    }
                }
                else
                {
                    for(int i = 0; i < len; i++)
                    {
                        pdst[i] = psrc[i];
                    }
                    psrci += len;
                }
                pdsti += len;
                type = !type;
            }
            data1 = null;
            #endregion

            uint[] data = new uint[width * height];
            int datai = 0;
            uint c = 0;

            for (int x = 0; x < width; x++)
            {
                c = color_add(c, extract(data3, bitsPerPixel));
                data[datai] = c;
                datai++;
            }
            for (int y = 1; y < height; y++)
            {
                c = color_add((data[datai - width]), extract(data3, bitsPerPixel));
                data[datai] = c;
                datai++;

                for (int x = 1;x < width;x++)
                {
                    uint moy = color_avg(c, (data[datai - width]));
                    c = color_add(moy, extract(data3, bitsPerPixel));
                    data[datai] = c;
                    datai++;
                }
            }
            data3 = null;

            byte[] pixels = new byte[width * height * 4];
            
            int pxi = 0;
            for(int px = 0; px < (width * height); px++)
            {
                byte r = 0, g = 0, b = 0, a = 0;
                if (bitsPerPixel == 32)
                {
                    a = (byte)((data[px] >> 24) & 0xFF);
                    r = (byte)((data[px] >> 16) & 0xFF);
                    g = (byte)((data[px] >> 8) & 0xFF);
                    b = (byte)(data[px] & 0xFF);
                }
                else
                {
                    b = (byte)((data[px] >> 16) & 0xFF);
                    g = (byte)((data[px] >> 8) & 0xFF);
                    r = (byte)(data[px] & 0xFF);
                    a = (byte)(0xFF);
                }

                pixels[pxi] = r;
                pixels[pxi + 1] = g;
                pixels[pxi + 2] = b;
                pixels[pxi + 3] = a;
                pxi += 4;
            }
            data = null;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}