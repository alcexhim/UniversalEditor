using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Auth3D.ASCII
{
    public class A3DCDataFormat : DataFormat
    {
        private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
            if (_dfr == null)
            {
                _dfr = base.MakeReferenceInternal();
                _dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
                _dfr.Filters.Add("Auth3D model (ASCII format)", new byte?[][] { new byte?[] { (byte)'#', (byte)'A', (byte)'3', (byte)'D', (byte)'C', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_', (byte)'_' } }, new string[] { "*.a3da" });
            }
            return _dfr;
        }

        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader tr = base.Accessor.Reader;

            string A3DAsignature = tr.ReadLine();
            if (A3DAsignature != "#A3DA__________") throw new InvalidDataFormatException("File does not begin with #A3DA__________");


        }

        protected override void SaveInternal(ObjectModel objectModel)
        {
            throw new NotImplementedException();
        }
    }
}
