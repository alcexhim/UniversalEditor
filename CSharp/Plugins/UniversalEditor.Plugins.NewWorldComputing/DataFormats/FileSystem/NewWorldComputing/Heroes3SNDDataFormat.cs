using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors.File;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing
{
    public class Heroes3SNDDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Heroes of Might and Magic SND archive", new string[] { "*.snd" });
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            uint fileCount = br.ReadUInt32();
            for (uint i = 0; i < fileCount; i++)
            {
                File file = new File();
                file.Name = String.Join(".", br.ReadFixedLengthString(12).Split(new char[] { '\0' }));

                byte[] unknown28 = br.ReadBytes(28);
                uint offset = br.ReadUInt32();
                uint length = br.ReadUInt32();

                offsets.Add(file, offset);
                lengths.Add(file, length);

                file.DataRequest += new DataRequestEventHandler(file_DataRequest);
                file.Size = length;
                fsom.Files.Add(file);
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
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

        }
    }
}
