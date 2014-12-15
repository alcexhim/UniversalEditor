using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Ultra3D
{
    public class Ultra3DTBVDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.ExportOptions.Add(new CustomOptionText("Description", "Description:", "RichRayl@CUC"));
            }
            return _dfr;
        }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string header = br.ReadFixedLengthString(8);
            if (header != "TBVolume") throw new InvalidDataFormatException();

            byte nul = br.ReadByte();                       // 0
            ushort unknown1 = br.ReadUInt16();              // 2000
            uint fileCount = br.ReadUInt32();
            ushort unknown2 = br.ReadUInt16();              // 0
            
            mvarDescription = br.ReadFixedLengthString(24).TrimNull();
            uint unknown3 = br.ReadUInt32();                // 825294546
            uint firstFileOffset = br.ReadUInt32();

            br.Accessor.Seek(firstFileOffset, SeekOrigin.Begin);
            for (uint i = 0; i < fileCount; i++)
            {
                string filename = br.ReadFixedLengthString(24).TrimNull();
                if (filename == String.Empty)
                {
                    Console.WriteLine("Ultra3DTBV: encountered empty file name, assuming end of file list");
                    break;
                }

                uint length = br.ReadUInt32();
                byte[] data = br.ReadBytes(length);

                fsom.Files.Add(filename, data);
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            bw.WriteFixedLengthString("TBVolume");

            bw.WriteByte((byte)0);
            bw.WriteUInt16((ushort)2000);
            bw.WriteUInt32((uint)fsom.Files.Count);
            bw.WriteUInt16((ushort)0);

            bw.WriteFixedLengthString(mvarDescription, 24);

            bw.WriteUInt32((uint)825294546);

            bw.WriteUInt32((uint)(600 + bw.Accessor.Position + 4));
            bw.WriteBytes(new byte[600]);

            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 24);
                bw.WriteUInt32((uint)file.Size);
                bw.WriteBytes(file.GetDataAsByteArray());
            }
        }
    }
}
