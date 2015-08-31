using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Blender
{
	public class BlenderDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
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
