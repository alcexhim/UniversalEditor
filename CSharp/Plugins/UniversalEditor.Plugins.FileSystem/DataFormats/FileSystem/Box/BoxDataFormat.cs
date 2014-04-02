using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.Box
{
    public class BoxDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Mike Becker's BOX archive", new byte?[][] { new byte?[] { (byte)'B', (byte)'O', (byte)'X', (byte)' ', (byte)'F', (byte)'I', (byte)'L', (byte)'E' } }, new string[] { "*.box" });
                _dfr.ExportOptions.Add(new CustomOptionChoice("NumberSize", "Number size:", true,
                    new CustomOptionFieldChoice("8-bit, 1 byte per number", 1),
                    new CustomOptionFieldChoice("16-bit, 2 bytes per number", 2),
                    new CustomOptionFieldChoice("24-bit, 3 bytes per number", 3),
                    new CustomOptionFieldChoice("32-bit, 4 bytes per number", 4, true),
                    new CustomOptionFieldChoice("64-bit, 8 bytes per number", 8)));
                _dfr.ExportOptions.Add(new CustomOptionNumber("AllocationSize", "Allocation size: ", 512));
                _dfr.ExportOptions.Add(new CustomOptionText("Comment", "Comment: "));
            }
            return _dfr;
        }

        private byte mvarNumberSize = 4;
        /// <summary>
        /// The size of an SNUM or a UNUM field.
        /// </summary>
        public byte NumberSize { get { return mvarNumberSize; } set { mvarNumberSize = value; } }

        public ulong ReadUNum(IO.Reader br)
        {
            switch (mvarNumberSize)
            {
                case 1: return br.ReadByte();
                case 2: return br.ReadUInt16();
                case 3: return br.ReadUInt24();
                case 4: return br.ReadUInt32();
                case 8: return br.ReadUInt64();
            }
            throw new InvalidOperationException("Invalid number size (" + mvarNumberSize.ToString() + ")");
        }
        public long ReadSNum(IO.Reader br)
        {
            switch (mvarNumberSize)
            {
                case 1: return br.ReadSByte();
                case 2: return br.ReadInt16();
                case 3: return br.ReadInt24();
                case 4: return br.ReadInt32();
                case 8: return br.ReadInt64();
            }
            throw new InvalidOperationException("Invalid number size (" + mvarNumberSize.ToString() + ")");
        }

        public void WriteUNum(IO.Writer bw, ulong value)
        {
            switch (mvarNumberSize)
            {
                case 1: bw.WriteByte((byte)value); return;
                case 2: bw.WriteUInt16((ushort)value); return;
                case 3: bw.WriteUInt24((uint)value); return;
                case 4: bw.WriteUInt32((uint)value); return;
                case 8: bw.WriteUInt64((ulong)value); return;
            }
            throw new InvalidOperationException("Invalid number size (" + mvarNumberSize.ToString() + ")");
        }
        public void WriteSNum(IO.Writer bw, long value)
        {
            switch (mvarNumberSize)
            {
                case 1: bw.WriteSByte((sbyte)value); return;
                case 2: bw.WriteInt16((short)value); return;
                case 3: bw.WriteInt24((int)value); return;
                case 4: bw.WriteInt32((int)value); return;
                case 8: bw.WriteInt64((long)value); return;
            }
            throw new InvalidOperationException("Invalid number size (" + mvarNumberSize.ToString() + ")");
        }

        private ulong mvarAllocationSize = 512;
        public ulong AllocationSize { get { return mvarAllocationSize; } set { mvarAllocationSize = value; } }

        private string mvarComment = String.Empty;
        public string Comment { get { return mvarComment; } set { mvarComment = value; } }

        private bool mvarExternal = false;
        public bool External { get { return mvarExternal; } set { mvarExternal = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Reader brf = base.Accessor.Reader;
            IO.Reader br = brf;

            string BOX_FILE = brf.ReadFixedLengthString(8);
            if (BOX_FILE != "BOX FILE") throw new InvalidDataFormatException("File does not begin with \"BOX FILE\"");
            // BOX FILE is a HUGE file format. it's designed to hold a plethora of data, and be fast
            // and efficient on any type of system. therefore we use NUMBER everywhere.

            byte numsize = brf.ReadByte();
            mvarExternal = brf.ReadBoolean();
            if (mvarExternal)
            {
                string FileName = null;
                if (Accessor is FileAccessor) FileName = (Accessor as FileAccessor).FileName;
                if (FileName == null) throw new InvalidOperationException("External BOX FILE not mapped to a section list");

                FileName = System.IO.Path.ChangeExtension(FileName, "lst");
                if (!System.IO.File.Exists(FileName)) throw new InvalidOperationException("External BOX FILE not mapped to a section list");

                IO.Reader brl = new IO.Reader(new FileAccessor(FileName));
                string BOX_LIST = brl.ReadFixedLengthString(8);
                if (BOX_LIST != "BOX LIST") throw new InvalidDataFormatException("File does not begin with \"BOX LIST\"");

                br = brl;
            }

            // its primary purpose is not to store files, but to store sections. this is similar to
            // the Versatile Container file format.
            ulong sectionCount = ReadUNum(br);
            mvarAllocationSize = ReadUNum(br);

            // comment can store arbitrary text data relating to this box
            mvarComment = br.ReadFixedLengthString(64);

            // sections are named with a 64-byte name, followed by an offset and virtual size. this
            // means that theoretically sections can be located anywhere in the file.
            for (ulong i = 0; i < sectionCount; i++)
            {
                string sectionName = br.ReadFixedLengthString(64);
                ulong sectionOffset = ReadUNum(br);
                ulong sectionVirtualSize = ReadUNum(br);

                File file = new File();
                file.Name = sectionName;
                file.Properties.Add("brf", brf);
                file.Properties.Add("offset", sectionOffset);
                file.Properties.Add("length", sectionVirtualSize);
                file.DataRequest += file_DataRequest;
            }
        }

        private void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);
            
            IO.Reader br = (file.Properties["brf"] as IO.Reader);
            ulong sectionOffset = (ulong)file.Properties["offset"];
            ulong sectionLength = (ulong)file.Properties["length"];
            br.Accessor.Seek((long)sectionOffset, SeekOrigin.Begin);
            byte[] fileData = br.ReadBytes(sectionLength);
            e.Data = fileData;
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.Writer bw = base.Accessor.Writer;

            bw.WriteFixedLengthString("BOX FILE");
            bw.WriteByte(mvarNumberSize);
            bw.WriteBoolean(mvarExternal);
            if (mvarExternal)
            {
            }

            WriteUNum(bw, (ulong)fsom.Files.Count);
            WriteUNum(bw, mvarAllocationSize);
            bw.WriteFixedLengthString(mvarComment, 64);

            ulong sectionOffset = (ulong)(bw.Accessor.Position + (fsom.Files.Count * (64 + (2 * mvarNumberSize))));
            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 64);
                WriteUNum(bw, sectionOffset);
                WriteUNum(bw, (ulong)file.Size);

                sectionOffset += (ulong)file.Size;
            }
            foreach (File file in fsom.Files)
            {
                bw.WriteBytes(file.GetDataAsByteArray());

                ulong allocationPadding = ((ulong)file.Size / mvarAllocationSize);
                ulong rem = allocationPadding * mvarAllocationSize;
                ulong r = (ulong)file.Size - rem;
                bw.WriteBytes(new byte[r]);
            }
            bw.Flush();
        }
    }
}
