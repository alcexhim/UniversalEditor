using System;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Waveform;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.OGG
{
	public class OGGDataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Ogg audio container", new string[] { "*.ogg", "*.ogm", "*.oga", "*.ogv" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
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
