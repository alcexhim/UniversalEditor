using System;
using System.Collections.Generic;
using System.Linq;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Auth3D.Compiled
{
    public class A3DCDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            Reader br = base.Accessor.Reader;
            br.Endianness = Endianness.BigEndian;
            br.Accessor.Position = 0;
            string A3DCsignature = br.ReadFixedLengthString(15);
            if (A3DCsignature != "#A3DC__________") throw new InvalidDataFormatException();
            byte xA = br.ReadByte();
            uint u0 = br.ReadUInt32();      // 32
            uint u2 = br.ReadUInt32();      // 0
            short u4 = br.ReadInt16();      // 0
            uint u5 = br.ReadUInt32();      // 2097154

            short u7 = br.ReadInt16();      // 16
            short u8 = br.ReadInt16();      // 20480
            short u9 = br.ReadInt16();      // 0
            short u10 = br.ReadInt16();      // 0
            short u11 = br.ReadInt16();      // 0
            short u12 = br.ReadInt16();      // 0
            short textSize = br.ReadInt16();      // 2562
            short u14 = br.ReadInt16();      // 0
            short u15 = br.ReadInt16();      // 0
            short u16 = br.ReadInt16();      // 0
            short u17 = br.ReadInt16();      // 0
            short u18 = br.ReadInt16();      // 0
            short offsetToBinaryData = br.ReadInt16();      // 0
            short u20 = br.ReadInt16();      // 0
            short u21 = br.ReadInt16();      // 0
            short u22 = br.ReadInt16();      // 0
            short u23 = br.ReadInt16();      // 0

            string textData = br.ReadFixedLengthString(textSize);
            
            br.Accessor.Seek(offsetToBinaryData, SeekOrigin.Begin);

        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
