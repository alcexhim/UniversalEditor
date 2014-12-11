using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HyPack
{
    public class HyPackDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("HyPack archive", new byte?[][] { new byte?[] { (byte)'H', (byte)'y', (byte)'P', (byte)'a', (byte)'c', (byte)'k' } }, new string[] { "*.pak" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string HyPack = br.ReadFixedLengthString(6);
            if (HyPack != "HyPack") throw new InvalidDataFormatException("File does not begin with \"HyPack\"");

            byte unknown0 = br.ReadByte();
            int DirectoryCount = br.ReadInt32();

            byte unknown = br.ReadByte();

            int FileCount = br.ReadInt32();
            int FileDataOffset = 16268; // 16 + (48 * FileCount);

            for (int i = 0; i < FileCount; i++)
            {
                string FileName = br.ReadFixedLengthString(21);
                if (FileName.Contains("\0")) FileName = FileName.Substring(0, FileName.IndexOf('\0'));

                short unknown1 = br.ReadInt16();
                short unknown2 = br.ReadInt16();
                short FileOffset = br.ReadInt16();
                short unknown4 = br.ReadInt16();
                short FileLength = br.ReadInt16();
                short unknown6 = br.ReadInt16();
                short unknown7 = br.ReadInt16();
                short unknown8 = br.ReadInt16();
                short unknown9 = br.ReadInt16();
                short unknown10 = br.ReadInt16();
                short unknown11 = br.ReadInt16();
                short unknown12 = br.ReadInt16();
                short unknown13 = br.ReadInt16();

                byte FileType = br.ReadByte();

                long position = br.Accessor.Position;
                br.Accessor.Position = FileDataOffset + FileOffset;
                byte[] FileData = br.ReadBytes(FileLength);
                br.Accessor.Position = position;

                fsom.Files.Add(FileName, FileData);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {

        }
    }
}
