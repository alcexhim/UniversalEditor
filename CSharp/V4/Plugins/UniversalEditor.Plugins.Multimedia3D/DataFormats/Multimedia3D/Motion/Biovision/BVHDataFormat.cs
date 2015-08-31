using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia3D.Motion;

namespace UniversalEditor.DataFormats.Multimedia3D.Motion.Biovision
{
    /// <summary>
    /// Provides a DataFormat for BioVision Hierarchy (BVH) and QAvimator (AVM) files.
    /// </summary>
    public class BVHDataFormat : DataFormat
    {
		private static DataFormatReference _dfr = null;
        protected override DataFormatReference MakeReferenceInternal()
        {
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
			}
            return _dfr;
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
