using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Compression;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Garena
{
    public class YanghxDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Garena compressed archive", new byte?[][] { new byte?[] { (byte)'y', (byte)'a', (byte)'n', (byte)'g', (byte)'h', (byte)'x', (byte)0, (byte)0 } }, new string[] { "*" });
                _dfr.Filters[0].HintComparison = DataFormatHintComparison.MagicOnly;
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;
            string yanghx = br.ReadFixedLengthString(8);
            if (yanghx != "yanghx\0\0") throw new InvalidDataFormatException("File does not begin with \"yanghx\", 0, 0");

            int u1 = br.ReadInt32();
            byte[] bkey = br.ReadBytes(4);
            byte[] data = br.ReadToEnd();

            data = CompressionModules.Gzip.Decompress(data);
            
            System.IO.File.WriteAllBytes(@"C:\Temp\LoLPH\001.dat", data);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
