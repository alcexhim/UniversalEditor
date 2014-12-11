using System;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Blender
{
	public class BlenderDataFormat : DataFormat
	{
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
            dfr.Filters.Add("Blender scene", new string[] { "*.blend" });
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
