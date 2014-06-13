using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing.LOD
{
    public class Magic6LODDataFormat : DataFormat
    {
    	private string mvarGameID = String.Empty;
    	public string GameID { get { return mvarGameID; } set { mvarGameID = value; } }
    	
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader reader = base.Accessor.Reader;
            string magic = reader.ReadFixedLengthString(4); // LOD\0
            if (magic != "LOD\0") throw new InvalidDataFormatException("File does not begin with \"LOD\\0\"");

            string gameID = reader.ReadFixedLengthString(9);
            byte[] unknown = reader.ReadBytes(256 - 13);
            string dir = reader.ReadFixedLengthString(16);

            uint dirstart = reader.ReadUInt32();
            uint dirlength = reader.ReadUInt32();
            uint unknown2 = reader.ReadUInt32();
            uint fileCount = reader.ReadUInt32();

            reader.Accessor.Position = dirstart;
            for (uint i = 0; i < fileCount; i++)
            {
                File f = new File();
                f.Name = reader.ReadFixedLengthString(16);

                uint offset = reader.ReadUInt32();
                uint length = reader.ReadUInt32();
                f.Properties.Add("offset", offset);
                f.Properties.Add("length", length);
                f.Properties.Add("reader", reader);
                f.Size = length;
                f.DataRequest += new DataRequestEventHandler(f_DataRequest);

                uint u1 = reader.ReadUInt32();
                uint u2 = reader.ReadUInt32();
            }
        }

        #region Data Request
        private void f_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            IO.Reader reader = (IO.Reader)file.Properties["reader"];
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            reader.Seek(offset, IO.SeekOrigin.Begin);
            e.Data = reader.ReadBytes(length);
        }
        #endregion

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
