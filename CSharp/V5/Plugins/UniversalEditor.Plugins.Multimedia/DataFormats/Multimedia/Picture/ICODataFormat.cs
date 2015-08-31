using System;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture
{
	public class ICODataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Windows icon", new byte?[][] { new byte?[] { new byte?(0), new byte?(0), new byte?(0), new byte?(1) } }, new string[] { "*.ico" });
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
