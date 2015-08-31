using System;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Audio.Waveform;

namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.MP3
{
	public class MP3DataFormat : DataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("MPEG-2 layer III audio", new byte?[][] { new byte?[] { new byte?(73), new byte?(68), new byte?(51) } }, new string[] { "*.mp3" });
			dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			BinaryReader br = base.Stream.BinaryReader;
			string ID3 = br.ReadFixedLengthString(3);
			if (ID3 == "ID3")
			{
				int lz = br.ReadInt32();
			}
			else
			{
				br.BaseStream.Seek(-3L, System.IO.SeekOrigin.Current);
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = objectModel as WaveformAudioObjectModel;
			BinaryWriter bw = base.Stream.BinaryWriter;
			if (wave.Information.CustomProperties.Count > 0)
			{
				bw.WriteFixedLengthString("ID3");
			}
			bw.Flush();
		}
	}
}
