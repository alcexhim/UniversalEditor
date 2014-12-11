using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Motion;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Biovision
{
    /// <summary>
    /// Provides a DataFormat for BioVision Hierarchy (BVH) and QAvimator (AVM) files.
    /// </summary>
    public class BVHDataFormat : DataFormat
    {
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("Biovision Hierarchy animation data", new byte?[][] { new byte?[] { (byte)'H', (byte)'I', (byte)'E', (byte)'R', (byte)'A', (byte)'R', (byte)'C', (byte)'H', (byte)'Y' } }, new string[] { "*.bvh" });
            dfr.Filters.Add("Qavimator motion data", new byte?[][] { new byte?[] { (byte)'H', (byte)'I', (byte)'E', (byte)'R', (byte)'A', (byte)'R', (byte)'C', (byte)'H', (byte)'Y' } }, new string[] { "*.avm" });
            return dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.Reader tr = base.Accessor.Reader;

        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            IO.Writer tw = base.Accessor.Writer;


        }
    }
}
