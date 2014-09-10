using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.Plugins.Monolith.DataFormats.FileSystem.Monolith.REZ
{
    public class REZDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.ExportOptions.Add(new CustomOptionText("Description", "&Description:", String.Empty, 127));
                _dfr.Sources.Add("http://wiki.xentax.com/index.php?title=Monolith_REZ");
                _dfr.Filters.Add("Monolith Productions REZ archive", new string[] { "*.rez" });
            }
            return _dfr;
        }

        private string mvarDescription = String.Empty;
        public string Description { get { return mvarDescription; } set { mvarDescription = value; } }

        private uint mvarFormatVersion = 1;
        public uint FormatVersion { get { return mvarFormatVersion; } set { mvarFormatVersion = value; } }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            IO.Reader reader = base.Accessor.Reader;
            mvarDescription = reader.ReadFixedLengthString(127);
            mvarFormatVersion = reader.ReadUInt32();

            uint diroffset = reader.ReadUInt32();
            uint dirsize = reader.ReadUInt32();
			uint unknown1 = reader.ReadUInt32();
			

            throw new NotImplementedException();
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            FileSystemObjectModel fsom = (objectModel as FileSystemObjectModel);
            if (fsom == null) throw new ObjectModelNotSupportedException();

            File[] allfiles = fsom.GetAllFiles();

            IO.Writer writer = base.Accessor.Writer;
            writer.WriteFixedLengthString(mvarDescription, 127);
            writer.WriteUInt32(mvarFormatVersion);

            uint diroffset = 184; // 127 + (11 * 4) + 13
            uint dirsize = 0;

            foreach (File file in fsom.Files)
            {
                diroffset += (uint)file.GetDataAsByteArray().Length;

            }
            throw new NotImplementedException();
        }
    }
}
