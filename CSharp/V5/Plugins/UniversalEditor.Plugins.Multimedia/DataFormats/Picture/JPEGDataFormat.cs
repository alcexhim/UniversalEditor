using System;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Picture;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Picture
{
	public class JPEGDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Joint Photographic Experts Group image", new string[] { "*.jpg", "*.jpe", "*.jpeg" } );
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
