﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			_dfr.Filters.Add("Heroes of Might and Magic II sound effect", new string[] { "*.82M" });
			return _dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			IO.Reader br = base.Accessor.Reader;

			wave.Header.BitsPerSample = 8;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = 22050;
			wave.RawData = br.ReadToEnd();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
