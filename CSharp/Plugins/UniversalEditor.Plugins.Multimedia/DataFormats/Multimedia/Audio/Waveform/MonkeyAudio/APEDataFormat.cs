using System;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MonkeyAudio
{
	public class APEDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("Monkey's Audio", new string[] { "*.ape" });
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
