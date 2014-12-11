using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.WAD
{
    public class WADDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("DOOM WAD archive", new byte?[][] { new byte?[] { (byte)'I', (byte)'W', (byte)'A', (byte)'D' } }, new string[] { "*.wad" });
                _dfr.ExportOptions.Add(new CustomOptionBoolean("UserContent", "This archive contains public content (PWAD) rather than internal content (IWAD)"));
            }
            return _dfr;
        }

        private bool mvarUserContent = false;
        public bool UserContent { get { return mvarUserContent; } set { mvarUserContent = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            IO.Reader br = base.Accessor.Reader;
            string IWAD = br.ReadFixedLengthString(4);
            if (!(IWAD == "IWAD" || IWAD == "PWAD")) throw new InvalidDataFormatException("File does not begin with \"IWAD\" or \"PWAD\"");
            mvarUserContent = (IWAD == "PWAD");

            int fileCount = br.ReadInt32();
            int offsetToDirectory = br.ReadInt32();

            br.Accessor.Position = offsetToDirectory;

            for (int i = 0; i < fileCount; i++)
            {
                int offset = br.ReadInt32();
                int length = br.ReadInt32();

                File file = new File();
                file.Name = br.ReadFixedLengthString(8).TrimNull();
                file.Size = length;
                file.Properties.Add("offset", offset);
                file.Properties.Add("length", length);
                file.Properties.Add("reader", br);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            int offset = (int)file.Properties["offset"];
            int length = (int)file.Properties["length"];
            IO.Reader br = (IO.Reader)file.Properties["reader"];

            br.Accessor.Position = offset;
            e.Data = br.ReadBytes(length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);

            IO.Writer bw = base.Accessor.Writer;
            if (mvarUserContent)
            {
                bw.WriteFixedLengthString("PWAD");
            }
            else
            {
                bw.WriteFixedLengthString("IWAD");
            }

            bw.WriteInt32((int)fsom.Files.Count);

            int offsetToDirectory = 12;
            bw.WriteInt32(offsetToDirectory);

            int offset = offsetToDirectory + (16 * fsom.Files.Count);
            foreach (File file in fsom.Files)
            {
                bw.WriteInt32(offset);
                bw.WriteInt32((int)file.Size);
                bw.WriteFixedLengthString(file.Name, 8);
                offset += (int)file.Size;
            }
            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());
            }
        }
    }
}
