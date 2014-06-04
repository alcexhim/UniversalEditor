using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.NewWorldComputing.Map;

namespace UniversalEditor.DataFormats.NewWorldComputing.Map.Heroes3
{
    public class Heroes3MapDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(MapObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic III map", new byte?[][] { new byte?[] { 0x0E, 0x00, 0x00, 0x00 }, new byte?[] { 0x15, 0x00, 0x00, 0x00 }, new byte?[] { 0x1C, 0x00, 0x00, 0x00 }, new byte?[] { 0x33, 0x00, 0x00, 0x00 } }, new string[] { "*.h3m" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            MapObjectModel map = (objectModel as MapObjectModel);

            #region decompress the map file
            IO.BinaryReader br = base.Stream.BinaryReader;
            byte[] gzipped = br.ReadToEnd();
            byte[] decompressed = UniversalEditor.Compression.Gzip.GzipStream.Decompress(gzipped);
            br = new IO.BinaryReader(decompressed);
            #endregion

            Heroes3GameType u0 = (Heroes3GameType)br.ReadInt32();
            byte discard = br.ReadByte();
            int mapsize = br.ReadByte();
            int unknown = br.ReadInt32();

            map.Name = br.ReadInt32String();
            map.Description = br.ReadInt32String();

            short u1 = br.ReadInt16();
            short u2 = br.ReadInt16();
            short u3 = br.ReadInt16();
            byte u7 = br.ReadByte();

            short a1 = br.ReadInt16();
            short a2 = br.ReadInt16();
            short a3 = br.ReadInt16();

            string name = br.ReadInt32String();


            /*
            string sectTitle = br.ReadInt32String();
            string sectDesc = br.ReadInt32String();
            */
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            IO.BinaryWriter bw = new IO.BinaryWriter(ms);




            bw.Flush();
            bw.Close();

            #region compress the map file
            bw = base.Stream.BinaryWriter;
            byte[] decompressed = ms.ToArray();
            byte[] gzipped = UniversalEditor.Compression.Gzip.GzipStream.Compress(decompressed);
            bw.Write(gzipped);
            bw.Flush();
            #endregion
        }
    }
}
