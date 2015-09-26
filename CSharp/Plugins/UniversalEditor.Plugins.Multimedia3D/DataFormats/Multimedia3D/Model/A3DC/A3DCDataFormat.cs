using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.A3DC
{
    public class A3DCDataFormat : DataFormat
    {
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.BinaryReader br = base.Stream.BinaryReader;

            string A3DCsignature = br.ReadFixedLengthString(15);
            if (A3DCsignature != "#A3DC__________") throw new InvalidDataFormatException();

            short u0 = br.ReadInt16();      // 10
            short u1 = br.ReadInt16();      // 32
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
