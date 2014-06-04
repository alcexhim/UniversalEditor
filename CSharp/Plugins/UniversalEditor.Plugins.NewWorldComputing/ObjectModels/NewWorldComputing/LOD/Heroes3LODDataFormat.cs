using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.ObjectModels.NewWorldComputing.LOD
{
    public class Heroes3LODDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic III archive", new byte?[][] { new byte?[] { (byte)'L', (byte)'O', (byte)'D', 0 } }, new string[] { "*.lod" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            string magic = br.ReadFixedLengthString(4); // LOD\0
            if (magic != "LOD\0") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

            uint unknown1 = br.ReadUInt32();
            uint fileCount = br.ReadUInt32();

            byte[] unknown = br.ReadBytes(80);
            for (int i = 0; i < fileCount; i++)
            {
                File f = new File();
                f.Name = br.ReadNullTerminatedString(16);
                uint offset = br.ReadUInt32();

                uint u1 = br.ReadUInt32();
                uint u2 = br.ReadUInt32();

                uint length = br.ReadUInt32();
                offsets.Add(f, offset);
                lengths.Add(f, length);

                f.DataRequest += new DataRequestEventHandler(f_DataRequest);
                fsom.Files.Add(f);
            }
        }

        #region Data Request
        private Dictionary<File, uint> offsets = new Dictionary<File, uint>();
        private Dictionary<File, uint> lengths = new Dictionary<File, uint>();
        private void f_DataRequest(object sender, DataRequestEventArgs e)
        {
            IO.BinaryReader br = new IO.BinaryReader(System.IO.File.Open(base.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read));
            File send = (sender as File);
            br.BaseStream.Position = offsets[send];
            byte[] compressedData = br.ReadBytes(lengths[send]);

            byte[] uncompressedData = Compression.CompressionStream.Decompress(Compression.CompressionMethod.Zlib, compressedData);
            e.Data = uncompressedData;
            br.Close();
        }
        #endregion

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
