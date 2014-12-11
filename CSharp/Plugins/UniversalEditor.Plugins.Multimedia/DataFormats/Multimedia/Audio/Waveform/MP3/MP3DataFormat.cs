using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MP3
{
	public class MP3DataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Filters.Add("MPEG-2 layer III audio", new byte?[][] { new byte?[] { new byte?(73), new byte?(68), new byte?(51) } }, new string[] { "*.mp3" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			Reader br = base.Accessor.Reader;
			string ID3 = br.ReadFixedLengthString(3);
			if (ID3 == "ID3")
			{
				int lz = br.ReadInt32();
			}
			else
			{
				br.Accessor.Seek(-3L, SeekOrigin.Current);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			Writer bw = base.Accessor.Writer;
			if (wave.Information.CustomProperties.Count > 0)
			{
				bw.WriteFixedLengthString("ID3");
			}
			bw.Flush();
		}
	}
}
