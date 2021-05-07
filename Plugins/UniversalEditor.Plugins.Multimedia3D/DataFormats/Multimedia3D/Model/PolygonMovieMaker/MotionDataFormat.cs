using System;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Motion;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker
{
	public class MotionDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Polygon Movie Maker motion data", new byte?[][] { new byte?[] { new byte?(86), new byte?(111), new byte?(99), new byte?(97), new byte?(108), new byte?(111), new byte?(105), new byte?(100), new byte?(32), new byte?(77), new byte?(111), new byte?(116), new byte?(105), new byte?(111), new byte?(110), new byte?(32), new byte?(68), new byte?(97), new byte?(116), new byte?(97), new byte?(32), new byte?(48), new byte?(48), new byte?(48), new byte?(50) } }, new string[] { "*.vmd" });
			dfr.Capabilities.Add(typeof(MotionObjectModel), DataFormatCapabilities.All);
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
