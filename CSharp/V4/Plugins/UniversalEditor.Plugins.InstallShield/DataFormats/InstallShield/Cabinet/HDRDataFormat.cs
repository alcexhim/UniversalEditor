using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.DataFormats.InstallShield.Cabinet
{
    public class HDRDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        public override DataFormatReference MakeReference()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReference();
                _dfr.Capabilities.Add(typeof(FileSystemObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("InstallShield cabinet header", new byte?[][] { new byte?[] { (byte)'I', (byte)'S', (byte)'c', (byte)'(' } }, new string[] { "*.hdr" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
        }
    }
}
