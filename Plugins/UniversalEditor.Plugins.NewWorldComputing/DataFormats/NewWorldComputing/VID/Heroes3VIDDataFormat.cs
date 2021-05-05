using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors.File;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.NewWorldComputing.VID
{
    public class Heroes3VIDDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic VID archive", new string[] { "*.vid" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            uint fileCount = br.ReadUInt32();
            uint offset = 0;
            for (uint i = 0; i < fileCount; i++)
            {
                File file = new File();
                file.Name = br.ReadNullTerminatedString(40);

                uint length = br.ReadUInt32();

                offsets.Add(file, offset);
                lengths.Add(file, length);

                file.DataRequest += new DataRequestEventHandler(file_DataRequest);
                fsom.Files.Add(file);

                offset += length;
            }
        }

        #region Data Request
        private Dictionary<File, uint> offsets = new Dictionary<File, uint>();
        private Dictionary<File, uint> lengths = new Dictionary<File, uint>();
        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            string FileName = String.Empty;
            if (Accessor is FileAccessor)
            {
                FileName = (Accessor as FileAccessor).FileName;
            }

            IO.BinaryReader br = new IO.BinaryReader(System.IO.File.Open(FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read));
            File send = (sender as File);
            br.BaseStream.Position = offsets[send];
            e.Data = br.ReadBytes(lengths[send]);
            br.Close();
        }
        #endregion

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
