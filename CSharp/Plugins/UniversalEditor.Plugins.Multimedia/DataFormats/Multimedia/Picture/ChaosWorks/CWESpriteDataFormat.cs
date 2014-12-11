using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.DataFormats.Multimedia.Picture.ChaosWorks
{
    public class CWESpriteDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Chaos Works Engine sprite", new byte?[][] { new byte?[] { (byte)'C', (byte)'W', (byte)'E', (byte)' ', (byte)'s', (byte)'p', (byte)'r', (byte)'i', (byte)'t', (byte)'e' } }, new string[] { "*.sph" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            PictureObjectModel pic = (objectModel as PictureObjectModel);

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;

            string CWE_sprite = br.ReadNullTerminatedString();
            if (CWE_sprite != "CWE sprite") throw new InvalidDataFormatException();
            uint unknown1 = br.ReadUInt32();        // always the same?
            uint unknown2 = br.ReadUInt32();        // always the same?

             // ushort unknown3 = br.ReadUInt16();

            pic.Width = br.ReadByte();
            pic.Height = br.ReadByte();

            br.Accessor.Position = 512;
            for (int x = 0; x < pic.Width; x++)
            {
                for (int y = 0; y <  pic.Height; y++)
                {
                    byte r = br.ReadByte();
                    byte g = br.ReadByte();
                    byte b = br.ReadByte();

                    pic.SetPixel(Color.FromRGBA(r, g, b), x, y);
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
