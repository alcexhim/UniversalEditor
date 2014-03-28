using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.Cinema4D
{
	public class Cinema4DDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("CINEMA 4D scene", new string[] { "*.c4d" });
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
