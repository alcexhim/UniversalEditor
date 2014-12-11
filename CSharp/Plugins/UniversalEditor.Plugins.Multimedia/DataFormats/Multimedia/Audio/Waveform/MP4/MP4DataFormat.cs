using System;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MP4
{
	public class MP4DataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("MPEG-4 Part 14", new byte?[][] {
			new byte?[] { new byte?(0), new byte?(0), new byte?(0), new byte?(24), new byte?(102), new byte?(116), new byte?(121), new byte?(112), new byte?(109), new byte?(112), new byte?(52), new byte?(50) }, 
			new byte?[] { new byte?(0), new byte?(0), new byte?(0), new byte?(20), new byte?(102), new byte?(116), new byte?(121), new byte?(112), new byte?(105), new byte?(115), new byte?(111), new byte?(109) },
			new byte?[] { new byte?(0), new byte?(0), new byte?(0), new byte?(28), new byte?(102), new byte?(116), new byte?(121), new byte?(112), new byte?(109), new byte?(112), new byte?(52), new byte?(50) } },
			new string[] { "*.mp4", "*.m4a", "*.m4p", "*.m4b", "*.m4r", "*.m4v" });

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
