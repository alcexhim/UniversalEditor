using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.CFL
{
    public class CFLDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Compressed File Library archive", new byte?[][] { new byte?[] { (byte)'C', (byte)'F', (byte)'L', (byte)'3' } }, new string[] { "*.cfl" });
                _dfr.Sources.Add("http://sol.gfxile.net/cfl/index.html");
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            Reader reader = base.Accessor.Reader;
            string CFL3 = reader.ReadFixedLengthString(4);
            if (CFL3 != "CFL3") throw new InvalidDataFormatException();

            uint offsetToDirectory = reader.ReadUInt32();
            uint decompressedLibrarySize = reader.ReadUInt32();

            uint unknown1 = reader.ReadUInt32();

            base.Accessor.Seek(offsetToDirectory, SeekOrigin.Begin);
            uint compressionType = reader.ReadUInt32();

            {
                uint flags = reader.ReadUInt32();
                uint unknown2 = reader.ReadUInt32();
                uint unknown3 = reader.ReadUInt32();

                string fileName = reader.ReadUInt16String();
            }

            uint offsetToHeader = reader.ReadUInt32();

            string signatureFooter = reader.ReadFixedLengthString(4);
            if (signatureFooter != "3CFL") throw new InvalidDataFormatException("File does not end with '3CFL'");
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
