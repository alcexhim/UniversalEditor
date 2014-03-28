using System;

using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.ThreeDStudio
{
	public class ThreeDSMaxDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
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
