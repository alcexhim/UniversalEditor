using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Super6
{
    public class Super6DataFormat : DataFormat
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
            ModelObjectModel model = (objectModel as ModelObjectModel);
            if (model == null) throw new ObjectModelNotSupportedException();

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;
            byte[] signature = br.ReadBytes(4);
            byte[] realsig = new byte[] { 0xFF, 0xFF, 0xFF, 0x9B };
            if (!signature.Match(realsig)) throw new InvalidDataFormatException("File does not begin with 0xFF, 0xFF, 0xFF, 0x9B");

            uint unknown1 = br.ReadUInt32();
            uint sectionCount = br.ReadUInt32();
            uint firstSectionOffset = br.ReadUInt32();
            for (uint i = 0; i < sectionCount; i++)
            {
                uint unknown3 = br.ReadUInt32();
                string sectionName = br.ReadFixedLengthString(32).TrimNull();
                uint sectionLength = br.ReadUInt32();
                uint sectionOffset = br.ReadUInt32();
                sectionOffset += firstSectionOffset;

                uint unknown2 = br.ReadUInt32();
            }

            uint unknown6 = br.ReadUInt32();
            uint unknown7 = br.ReadUInt32();
            uint unknown8 = br.ReadUInt32();
            uint unknown9 = br.ReadUInt32();
            uint unknown10 = br.ReadUInt32();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
