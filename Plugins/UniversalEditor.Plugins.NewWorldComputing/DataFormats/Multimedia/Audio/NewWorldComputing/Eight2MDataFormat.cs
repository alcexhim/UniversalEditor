using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.NewWorldComputing
{
	/// <summary>
	///		The .82M files can be played with any sound editor capable of reading raw wave data at a
	///		rate of 22050Hz, 8-Bit Unsigned, Mono.
	/// </summary>
	/// <remarks>
	///		Thanks to Terry Butler, http://www.terrybutler.co.uk/misc/heroes-of-might-and-magic-ii/
	///		for his research on this file format.
	/// </remarks>
	public class Eight2MDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null) _dfr = base.MakeReferenceInternal();
			_dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			_dfr.Sources.Add("http://www.terrybutler.co.uk/misc/heroes-of-might-and-magic-ii/");
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			wave.Header.BitsPerSample = 8;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = 22050;
			wave.RawData = reader.ReadToEnd();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			if (wave.Header.BitsPerSample != 8 || wave.Header.ChannelCount != 1 || wave.Header.SampleRate != 22050)
			{
				// TODO: should we throw an exception or attempt to convert the audio (how?)
			}
			writer.WriteBytes(wave.RawData);
		}
	}
}
