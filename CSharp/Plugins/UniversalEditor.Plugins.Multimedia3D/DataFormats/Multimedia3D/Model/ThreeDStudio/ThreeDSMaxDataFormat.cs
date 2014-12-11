using System;

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.ThreeDStudio
{
	public class ThreeDSMaxDataFormat : DataFormat
	{
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("3D Studio MAX model", new string[] { "*.max" });
            dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
            return dfr;
        }
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
