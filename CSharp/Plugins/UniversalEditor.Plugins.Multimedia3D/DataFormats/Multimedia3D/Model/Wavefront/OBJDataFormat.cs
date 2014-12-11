using System;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Wavefront
{
	public class OBJDataFormat : DataFormat
	{
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("Wavefront object model", new string[] { "*.obj" });
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
