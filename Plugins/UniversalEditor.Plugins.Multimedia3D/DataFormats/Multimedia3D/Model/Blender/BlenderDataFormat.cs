using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Blender
{
	public class BlenderDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = (objectModel as ModelObjectModel);
			if (model == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
		}
	}
}
