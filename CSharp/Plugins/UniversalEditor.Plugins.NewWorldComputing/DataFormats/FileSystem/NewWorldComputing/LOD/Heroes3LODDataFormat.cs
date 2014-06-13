using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing.LOD
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

            IO.Reader reader = base.Accessor.Reader;
            string magic = reader.ReadFixedLengthString(4); // LOD\0
            if (magic != "LOD\0") throw new InvalidDataFormatException("File does not begin with LOD\\0");

            uint unknown1 = reader.ReadUInt32();
            uint fileCount = reader.ReadUInt32();

            byte[] unknown = reader.ReadBytes(80);
            for (int i = 0; i < fileCount; i++)
            {
                File f = new File();
                f.Name = reader.ReadFixedLengthString(16);
                f.Name = f.Name.TrimNull();
                
                uint offset = reader.ReadUInt32();

                uint u1 = reader.ReadUInt32();
                uint u2 = reader.ReadUInt32();

                uint length = reader.ReadUInt32();
                f.Properties.Add("offset", offset);
                f.Properties.Add("length", length);
                f.Properties.Add("reader", reader);

                f.Size = length;
                f.DataRequest += new DataRequestEventHandler(f_DataRequest);
                fsom.Files.Add(f);
            }
        }

        #region Data Request
        private void f_DataRequest(object sender, DataRequestEventArgs e)
        {
            string FileName = String.Empty;
            if (Accessor is FileAccessor)
            {
                FileName = (Accessor as FileAccessor).FileName;
            }

            File file = (sender as File);
            IO.Reader reader = (IO.Reader)file.Properties["reader"];
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            reader.Seek(offset, IO.SeekOrigin.Begin);
            byte[] compressedData = reader.ReadBytes(length);

            byte[] uncompressedData = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Zlib).Decompress(compressedData);
            e.Data = uncompressedData;
        }
        #endregion

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("LOD\0");

            bw.WriteUInt32((uint)0);
            bw.WriteUInt32((uint)fsom.Files.Count);

            bw.WriteBytes(new byte[80]);

            uint offset = (uint)(bw.Accessor.Position + (32 * fsom.Files.Count));
            
            List<byte[]> CompressedDatas = new List<byte[]>();
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 16);
                bw.WriteUInt32(offset);

                bw.WriteUInt32((uint)0);
                bw.WriteUInt32((uint)0);

                byte[] compressedData = UniversalEditor.Compression.CompressionModule.FromKnownCompressionMethod(Compression.CompressionMethod.Zlib).Compress(file.GetDataAsByteArray());
                bw.WriteUInt32((uint)compressedData.Length);
                CompressedDatas.Add(compressedData);
            }

            foreach (byte[] compressedData in CompressedDatas)
            {
                bw.WriteBytes(compressedData);
            }
            bw.Flush();
        }
    }
}
