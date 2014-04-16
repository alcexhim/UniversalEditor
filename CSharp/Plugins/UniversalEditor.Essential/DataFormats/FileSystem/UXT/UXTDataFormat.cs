using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.UXT
{
    public class UXTDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Universal Editor extension", new byte?[][] { new byte?[] { (byte)'U', (byte)'n', (byte)'i', (byte)'v', (byte)'e', (byte)'r', (byte)'s', (byte)'a', (byte)'l', (byte)' ', (byte)'E', (byte)'d', (byte)'i', (byte)'t', (byte)'o', (byte)'r', (byte)' ', (byte)'e', (byte)'x', (byte)'t', (byte)'e', (byte)'n', (byte)'s', (byte)'i', (byte)'o', (byte)'n', (byte)' ', (byte)'f', (byte)'i', (byte)'l', (byte)'e', (byte)0 } }, new string[] { "*.uxt" });
                _dfr.ExportOptions.Add(new CustomOptionText("Comment", "Comment: "));
            }
            return _dfr;
        }

        public const int MAX_VERSION = 1;



        private int mvarVersion = MAX_VERSION;
        public int Version { get { return mvarVersion; } set { mvarVersion = value; } }

        private string mvarComment = String.Empty;
        public string Comment { get { return mvarComment; } set { mvarComment = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            Reader br = base.Accessor.Reader;

            string signature = br.ReadFixedLengthString(4);
            if (signature != "UXt!") throw new InvalidDataFormatException("File does not begin with \"UxT!\"");

            mvarVersion = br.ReadInt32();
            if (mvarVersion > MAX_VERSION) throw new InvalidDataFormatException("Cannot read version (" + mvarVersion.ToString() + ", expected <= " + MAX_VERSION.ToString() + ")");

            mvarComment = br.ReadNullTerminatedString();

            long entryCount = br.ReadInt64();
            for (long i = 0; i < entryCount; i++)
            {
                Internal.FileInfo fi = new Internal.FileInfo();
                string filename = br.ReadFixedLengthString(240);
                filename = filename.TrimNull();
                fi.offset = br.ReadInt64();
                fi.length = br.ReadUInt64();

                File file = new File();
                file.Name = filename;
                file.Size = (long)fi.length;
                file.Properties.Add("fileinfo", fi);
                file.Properties.Add("reader", br);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            Reader br = (Reader)file.Properties["reader"];
            Internal.FileInfo fi = (Internal.FileInfo)file.Properties["fileinfo"];
            br.Accessor.Position = fi.offset;
            e.Data = br.ReadBytes(fi.length);
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("UXt!");
            bw.WriteInt32(mvarVersion);
            bw.WriteNullTerminatedString(mvarComment);
            bw.WriteInt64(fsom.Files.LongCount<File>());

            long offset = bw.Accessor.Position + (256 * fsom.Files.Count);
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 240);
                bw.WriteInt64(offset);
                bw.WriteUInt64((ulong)file.Size);
                offset += (long)file.Size;
            }
            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());
            }

            bw.Flush();
        }
    }
}
