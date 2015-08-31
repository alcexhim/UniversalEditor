using System;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture
{
	public class GraphicsInterchangeDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("CompuServe Graphics Interchange Format", new byte?[][] { new byte?[] { new byte?(71), new byte?(105), new byte?(102), new byte?(56), new byte?(57), new byte?(97) } }, new string[] { "*.gif" });
			dfr.Capabilities.Add(typeof(PictureObjectModel), DataFormatCapabilities.All);
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
