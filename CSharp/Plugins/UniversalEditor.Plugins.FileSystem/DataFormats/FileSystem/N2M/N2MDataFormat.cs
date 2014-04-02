using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.N2M
{
    public class N2MDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("N2M/C2AR archive", new byte?[][] { new byte?[] { (byte)'C', (byte)'2', (byte)'A', (byte)'R' } }, new string[] { "*.n2m" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Reader br = base.Accessor.Reader;

            string unknown = br.ReadFixedLengthString(10);
            int TSIZE = br.ReadInt32();
            do
            {
                int ZSIZE = br.ReadInt32();
                int SIZE = TSIZE;
                long pos = br.Accessor.Position;
                if (TSIZE >= 0x800000) SIZE = 0x800000;
                byte[] data = br.ReadBytes(ZSIZE);
                TSIZE -= SIZE;
                pos += ZSIZE;
                br.Accessor.Position = pos;
            }
            while (TSIZE != 0);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
