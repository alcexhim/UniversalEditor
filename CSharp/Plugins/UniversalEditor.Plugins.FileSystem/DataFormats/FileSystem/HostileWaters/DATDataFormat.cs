using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.HostileWaters
{
    public class DATDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
            }
            return _dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            uint fileCount = br.ReadUInt32();
            for (uint i = 0; i < fileCount; i++)
            {
                string fileName = br.ReadFixedLengthString(12);
                if (String.IsNullOrEmpty(fileName)) fileName = (i + 1).ToString().PadLeft(8, '0');

                uint fileLength = br.ReadUInt32();
                uint fileOffset = br.ReadUInt32();

                if ((fileOffset + fileLength) >= br.Accessor.Length) throw new InvalidDataFormatException("File offset + length is too large");

                File file = new File();
                file.Name = fileName;
                file.Size = fileLength;
                file.Properties.Add("offset", fileOffset);
                file.Properties.Add("length", fileLength);
                file.DataRequest += file_DataRequest;

                fsom.Files.Add(file);
            }
        }

        private System.IO.FileStream fs_mbx = null;

        void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            if (fs_mbx == null)
            {
                if (!(base.Accessor is FileAccessor)) throw new InvalidOperationException("Requires a file reference");

                FileAccessor acc = (base.Accessor as FileAccessor);
                string MBXFileName = System.IO.Path.ChangeExtension(acc.FileName, "mbx");
                fs_mbx = System.IO.File.Open(MBXFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }

            File file = (sender as File);
            uint offset = (uint)file.Properties["offset"];
            uint length = (uint)file.Properties["length"];
            byte[] buffer = new byte[length];
            fs_mbx.Position = offset;
            fs_mbx.Read(buffer, 0, (int)length);
            e.Data = buffer;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteUInt32((uint)fsom.Files.Count);
            uint offset = 0;
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 12);
                bw.WriteUInt32((uint)file.Size);
                bw.WriteUInt32(offset);
                offset += (uint)file.Size;
            }

            if (fs_mbx == null)
            {
                if (!(base.Accessor is FileAccessor)) throw new InvalidOperationException("Requires a file reference");

                FileAccessor acc = (base.Accessor as FileAccessor);
                string MBXFileName = System.IO.Path.ChangeExtension(acc.FileName, "mbx");
                fs_mbx = System.IO.File.Open(MBXFileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            }
            fs_mbx.Position = 0;
            foreach (File file in fsom.Files)
            {
                byte[] data = file.GetDataAsByteArray();
                fs_mbx.Write(data, 0, data.Length);
            }
            fs_mbx.Flush();
            fs_mbx.Close();
            fs_mbx = null;
        }
    }
}
