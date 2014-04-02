﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Homeworld
{
    public class HomeworldDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Homeworld VCE/WXD archive", new byte?[][] { new byte?[] { (byte)'V', (byte)'C', (byte)'E', (byte)'0' }, new byte?[] { (byte)'W', (byte)'X', (byte)'D', (byte)'1' } }, new string[] { "*.wxd", "*.vce" });
                _dfr.ExportOptions.Add(new CustomOptionChoice("Version", "Format &version:", true,
                    new CustomOptionFieldChoice("Version \"VCE0\"", (uint)0),
                    new CustomOptionFieldChoice("Version \"WXD1\"", (uint)1)
                ));
            }
            return _dfr;
        }

        private uint mvarVersion = 0;
        public uint Version { get { return mvarVersion; } set { mvarVersion = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader br = base.Accessor.Reader;
            string header = br.ReadFixedLengthString(4);
            uint unknown1 = br.ReadUInt32();

            while (!br.EndOfStream)
            {
                string tag = br.ReadFixedLengthString(4);
                switch (tag)
                {
                    case "INFO":
                    {
                        uint unknown2 = br.ReadUInt32();
                        break;
                    }
                    case "DATA":
                    {
                        uint fileLength = br.ReadUInt32();
                        byte[] fileData = br.ReadBytes(fileLength);
                        fsom.Files.Add((fsom.Files.Count + 1).ToString().PadLeft(8, '0'), fileData);
                        break;
                    }
                }
            }
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;
            if (mvarVersion >= 1)
            {
                bw.WriteFixedLengthString("WXD1");
            }
            else
            {
                bw.WriteFixedLengthString("VCE0");
            }

            uint unknown1 = 0;
            bw.WriteUInt32(unknown1);

            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString("DATA");

                byte[] data = file.GetDataAsByteArray();
                bw.WriteUInt32((uint)data.Length);
                bw.WriteBytes(data);
            }
        }
    }
}
