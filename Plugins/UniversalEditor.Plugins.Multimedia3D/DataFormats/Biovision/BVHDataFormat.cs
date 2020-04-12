using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Biovision
{
    /// <summary>
    /// Provides a <see cref="DataFormat" /> for BioVision Hierarchy (BVH) and QAvimator (AVM) files.
    /// </summary>
    public class BVHDataFormat : TextDataFormat
    {
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
            dfr.Filters.Add("Biovision Hierarchy animation data", new byte?[][] { new byte?[] { (byte)'H', (byte)'I', (byte)'E', (byte)'R', (byte)'A', (byte)'R', (byte)'C', (byte)'H', (byte)'Y' } }, new string[] { "*.bvh" });
            dfr.Filters.Add("Qavimator motion data", new byte?[][] { new byte?[] { (byte)'H', (byte)'I', (byte)'E', (byte)'R', (byte)'A', (byte)'R', (byte)'C', (byte)'H', (byte)'Y' } }, new string[] { "*.avm" });
            return dfr;
        }
        protected override void LoadInternal(ref ObjectModel objectModel)
        {
            IO.TextReader tr = base.Stream.TextReader;

        }
        protected override void SaveInternal(ObjectModel objectModel)
        {
            IO.TextWriter tw = base.Stream.TextWriter;


        }
    }
}
