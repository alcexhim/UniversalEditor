using System;
using UniversalEditor.ObjectModels.Multimedia3D.Model;
namespace UniversalEditor.DataFormats.Multimedia3D.Model.Cinema4D
{
	public class Cinema4DDataFormat : DataFormat
	{
        protected override DataFormatReference MakeReferenceInternal()
        {
            DataFormatReference dfr = base.MakeReferenceInternal();
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
