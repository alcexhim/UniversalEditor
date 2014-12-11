using System;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.AdvancedAudioCodec
{
	public class AACDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Advanced Audio Codec", new string[] { "*.aac" });
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
