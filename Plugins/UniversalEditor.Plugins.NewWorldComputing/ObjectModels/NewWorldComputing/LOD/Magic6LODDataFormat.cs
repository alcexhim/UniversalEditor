using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.ObjectModels.NewWorldComputing.LOD
{
    public class Magic6LODDataFormat : DataFormat
    {
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            string magic = br.ReadFixedLengthString(4); // LOD\0
            if (magic != "LOD\0") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

            string gameID = br.ReadFixedLengthString(9);
            byte[] unknown = br.ReadBytes(256 - 13);
            string dir = br.ReadFixedLengthString(16);

            uint dirstart = br.ReadUInt32();
            uint dirlength = br.ReadUInt32();
            uint unknown2 = br.ReadUInt32();
            uint fileCount = br.ReadUInt32();

            br.BaseStream.Position = dirstart;
            for (uint i = 0; i < fileCount; i++)
            {
                File f = new File();
                f.Name = br.ReadFixedLengthString(16);

                uint offset = br.ReadUInt32();
                uint length = br.ReadUInt32();
                offsets.Add(f, offset);
                lengths.Add(f, length);
                f.DataRequest += new DataRequestEventHandler(f_DataRequest);

                uint u1 = br.ReadUInt32();
                uint u2 = br.ReadUInt32();
            }
        }

        #region Data Request
        private Dictionary<File, uint> offsets = new Dictionary<File, uint>();
        private Dictionary<File, uint> lengths = new Dictionary<File, uint>();
        private void f_DataRequest(object sender, DataRequestEventArgs e)
        {
            IO.BinaryReader br = base.Stream.BinaryReader;
            File send = (sender as File);
            br.BaseStream.Position = offsets[send];
            e.Data = br.ReadBytes(lengths[send]);
        }
        #endregion

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
