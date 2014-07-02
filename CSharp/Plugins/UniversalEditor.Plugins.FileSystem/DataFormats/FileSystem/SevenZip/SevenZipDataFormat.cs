using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.SevenZip
{
    public class SevenZipDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("7-ZIP archive", new byte?[][] { new byte?[] { (byte)'7', (byte)'z', 0xBC, 0xAF, 0x27, 0x1C } }, new string[] { "*.7z" });
                _dfr.ContentTypes.Add("application/x-7z-compressed");
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            br.Accessor.Position = 0;

            string signature1 = br.ReadFixedLengthString(2); // 7z
            if (signature1 != "7z") throw new InvalidDataFormatException("File does not begin with 7z");

            int signature2 = br.ReadInt32();
            if (signature2 != 0x1C27AFBC) throw new InvalidDataFormatException("File does not begin with LE: 0x1C27AFBC");

            short u1a = br.ReadInt16();					// 1024		1024
            int u1b = br.ReadInt16();					// 25464	-6047
            short u2a = br.ReadInt16();					// 24321	7104
            short compressedSize = br.ReadInt16();		// 58		240 - this is not compressed size

            int u3 = br.ReadInt32();

            short u4a = br.ReadInt16();
            short u4b = br.ReadInt16();

            int u5 = br.ReadInt32();
            short u6 = br.ReadInt16();
            short u7 = br.ReadInt16();
            short u8 = br.ReadInt16();

			// byte[] compressedData = br.ReadBytes(compressedSize);

			int u9 = br.ReadInt32();
			int u10 = br.ReadInt32();
			int u11 = br.ReadInt32();
			int u12 = br.ReadInt32();
			
			short decompressedSize = br.ReadInt16();

			short u14 = br.ReadInt16();
			short u15 = br.ReadInt16();
			short u16 = br.ReadInt16();
			short u17 = br.ReadInt16();
			short u18 = br.ReadInt16();
			short u19 = br.ReadInt16();

			short fileNameLength = br.ReadInt16();
			fileNameLength--;
			string fileName = br.ReadFixedLengthString(fileNameLength, IO.Encoding.UTF16LittleEndian);
			fileName = fileName.TrimNull();

			short u20 = br.ReadInt16();
			short u21 = br.ReadInt16();
			int u22 = br.ReadInt32();
			int u23 = br.ReadInt32();
			short u24 = br.ReadInt16();
			short u25 = br.ReadInt16();
			short u26 = br.ReadInt16();
			int u27 = br.ReadInt32();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
