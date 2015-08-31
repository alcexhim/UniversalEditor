using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Wavefront
{
	public class OBJDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
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
