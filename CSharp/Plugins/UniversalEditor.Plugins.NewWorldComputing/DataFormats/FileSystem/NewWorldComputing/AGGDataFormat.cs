using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Checksum;
using UniversalEditor.Checksum.Modules.CRC32;

using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.FileSystem.NewWorldComputing
{
	public class AGGDataFormat : DataFormat
    {
        private struct AGGFileEntry
        {
            public uint hash;
            public uint offset;
            public uint size;
            public string name;
        }

		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("Heroes of Might and Magic II AGG archive", new string[] { "*.agg" });
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryReader br = base.Stream.BinaryReader;
            ushort fileCount = br.ReadUInt16();

            AGGFileEntry[] files = new AGGFileEntry[fileCount];
            for (ushort i = 0; i < fileCount; i++)
            {
                files[i].hash = br.ReadUInt32();
                files[i].offset = br.ReadUInt32();
                files[i].size = br.ReadUInt32();
            }
            br.BaseStream.Seek(-(fileCount * 15), System.IO.SeekOrigin.End);
            for (ushort i = 0; i < fileCount; i++)
            {
                files[i].name = br.ReadFixedLengthString(15);
                files[i].name = files[i].name.TrimNull();

                File file = new File();
                file.Name = files[i].name;
                file.Size = files[i].size;
                file.Properties.Add("InternalData", files[i]);
                file.Properties.Add("BinaryReader", br);
                file.DataRequest += file_DataRequest;
                fsom.Files.Add(file);
            }


            // 43341516
		}

        void file_DataRequest(object sender, DataRequestEventArgs e)
        {
            File file = (sender as File);

            AGGFileEntry entry = (AGGFileEntry)file.Properties["InternalData"];
            IO.BinaryReader br = (IO.BinaryReader)file.Properties["BinaryReader"];

            br.BaseStream.Seek(entry.offset, System.IO.SeekOrigin.Begin);
            e.Data = br.ReadBytes(entry.size);

            //.Initialize(UniversalEditor.Common.Hashing.CRC32.Keys.ReversedReciprocal);
            uint hash = (uint)CRC32ChecksumModule.Calculate(e.Data);

        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) return;

            IO.BinaryWriter bw = base.Stream.BinaryWriter;
            ushort fileCount = (ushort)fsom.Files.Count;
            bw.Write(fileCount);

            uint offset = (uint)(bw.BaseStream.Position + (12 * fsom.Files.Count));
            foreach (File file in fsom.Files)
            {
                uint hash = 0;
                uint size = (uint)file.Size;

                bw.Write(hash);
                bw.Write(offset);
                bw.Write(size);

                offset += size;
            }

            for (ushort i = 0; i < fileCount; i++)
            {
                bw.Write(fsom.Files[i].GetDataAsByteArray());
            }

            foreach (File file in fsom.Files)
            {
                bw.WriteFixedLengthString(file.Name, 15);
            }
		}
	}
}
