using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.AniMiku.AnimatedTexture
{
    public class AMTDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("AniMiku texture package", new string[] { "*.amt" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            int unknown = br.ReadInt32();
            int count = br.ReadInt32();
            for (int i = 0; i < count; i++)
            {
                int dataSize = br.ReadInt32();
                byte[] data = br.ReadBytes(dataSize);
                fsom.Files.Add(i.ToString().PadLeft(8, '0'), data);
            }
        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            int unknown = 150;
            bw.Write(unknown);

            bw.Write(fsom.Files.Count);
            foreach (File file in fsom.Files)
            {
                byte[] data = file.GetDataAsByteArray();
                bw.Write(data.Length);
                bw.Write(data);
            }
        }
    }
}
