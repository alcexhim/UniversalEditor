using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia.Picture.Collection;

namespace UniversalEditor.DataFormats.NewWorldComputing.ICN
{
    public class ICNDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(PictureCollectionObjectModel), DataFormatCapabilities.All);
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic II ICN picture", new string[] { "*.icn" });
            }
            return _dfr;
        }


        private System.Drawing.Color shadowColor = System.Drawing.Color.FromArgb(0x40, 0, 0, 0);
        private System.Drawing.Color opaqueColor = System.Drawing.Color.FromArgb(0xFF, 0, 0, 0); // non-transparent mask

        private System.Drawing.Color GetColorIndex(byte index)
        {
            int depth = 9;
            if (depth == 8)
            {
                return System.Drawing.Color.Empty;
            }
            else
            {
                // index *= 3;
                return HoMM2Palette.ColorTable[index];
            }
            return System.Drawing.Color.Empty;
        }
        
        private void SpriteDrawICN(ref PictureObjectModel pic, Internal.SpriteHeader head, ref byte[] data, bool debug)
        {
            if (data == null || data.Length == 0) return;
        
            byte c = 0;
            ushort x = 0, y = 0;

            int cur = 0;
            while (cur < data.Length)
            {
                if (data[cur] == 0)
                {
                    // 0x00 - end line
                    ++y;
                    x = 0;
                    ++cur;
                }
                else if (data[cur] < 0x80)
                {
                    // 0x7F - count data
                    c = data[cur];
                    ++cur;
                    while((c-- != 0) && (cur < data.Length))
                    {
                        pic.SetPixel(GetColorIndex(data[cur]), x, y);
                        ++x;
                        ++cur;
                    }
                }
                else if (data[cur] == 0x80)
                {
                    // 0x80 - end data
                    break;
                }
                else if (data[cur] < 0xC0)
                {
                    // 0xBF - skip data
                    x += (ushort)(data[cur] - 0x80);
                    ++cur;
                }
                else if (data[cur] == 0xC0)
                {
                    // 0xC0 - shadow
                    ++cur;
                    c = (byte)((data[cur] % 4 != 0) ? (data[cur] % 4) : data[++cur]);
                    while((c-- != 0))
                    {
                        pic.SetPixel(shadowColor, x, y);
                        ++x;
                    }
                    ++cur;
                }
                else if (data[cur] == 0xC1)
                {
                    // 0xC1
                    ++cur;
                    c = data[cur];
                    ++cur;
                    while (c-- != 0)
                    {
                        pic.SetPixel(GetColorIndex(data[cur]), x, y);
                        ++x;
                    }
                    ++cur;
                }
                else
                {
                    c = (byte)(data[cur] - 0xC0);
                    ++cur;
                    while (c-- != 0)
                    {
                        pic.SetPixel(GetColorIndex(data[cur]), x, y);
                        ++x;
                    }
                    ++cur;
                }

                if (cur > data.Length)
                {
                    throw new ArgumentOutOfRangeException("cur", cur, "must be less than max " + data.Length.ToString());
                }
            }
        }


        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureCollectionObjectModel coll = (objectModel as PictureCollectionObjectModel);
            PictureObjectModel picret = (objectModel as PictureObjectModel);
            if (coll == null && picret == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            #region header
            ushort spriteCount = br.ReadUInt16();
            uint totalSize = br.ReadUInt32();
            #endregion

            long position = br.BaseStream.Position;

            List<Internal.SpriteHeader> shs = new List<Internal.SpriteHeader>();
            for (ushort i = 0; i < spriteCount; i++)
            {
                Internal.SpriteHeader sh = new Internal.SpriteHeader();
                sh.offsetX = br.ReadInt16();
                sh.offsetY = br.ReadInt16();
                sh.width = br.ReadUInt16();
                sh.height = br.ReadUInt16();
                sh.type = br.ReadByte();
                sh.dataOffset = br.ReadUInt32();
                shs.Add(sh);
            }
            for (ushort i = 0; i < spriteCount; i++)
            {
                Internal.SpriteHeader sh = shs[i];

                PictureObjectModel pic = new PictureObjectModel();
                /*
                pic.Left = sh.offsetX;
                pic.Top = sh.offsetY;
                */
                pic.Width = sh.width;
                pic.Height = sh.height;

                uint datasize = ((i + 1 != spriteCount) ? (shs[i + 1].dataOffset - shs[i].dataOffset) : (totalSize - shs[i].dataOffset));

                br.BaseStream.Seek(position + shs[i].dataOffset, System.IO.SeekOrigin.Begin);
                byte[] data = br.ReadBytes(datasize);

                try
                {
                    SpriteDrawICN(ref pic, sh, ref data, false);
                }
                catch (InvalidOperationException)
                {
                    continue;
                }

                // pic.ToBitmap().Save(@"C:\Temp\test.bmp", System.Drawing.Imaging.ImageFormat.Bmp);

                if (picret != null)
                {
                    pic.CopyTo(picret);
                    return;
                }
                coll.Pictures.Add(pic);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
