using System;
using UniversalEditor.ObjectModels.Multimedia.Picture;
namespace UniversalEditor.DataFormats.Multimedia.Picture
{
	public class CURDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Windows cursor", new byte?[][] { new byte?[] { new byte?(0), new byte?(0), new byte?(0), new byte?(2) } }, new string[] { "*.cur" });
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
