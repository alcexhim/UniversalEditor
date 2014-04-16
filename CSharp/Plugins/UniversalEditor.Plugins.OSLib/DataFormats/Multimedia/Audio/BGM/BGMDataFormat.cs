using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.BGM
{
	class BGMDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("OSLib BGM audio", new byte?[][] { new byte?[] { (byte)'O', (byte)'S', (byte)'L', (byte)'B', (byte)'G', (byte)'M', (byte)' ', (byte)'v', (byte)'0', (byte)'1', (byte)0 } }, new string[] { "*.bgm" });
				_dfr.Sources.Add("https://www.assembla.com/code/oslibmod/subversion/nodes/trunk/bgm.h?rev=52");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader reader = base.Accessor.Reader;
			string header = reader.ReadFixedLengthString(11);
			if (header != "OSLBGM v01\0") throw new InvalidDataFormatException();

			int format = reader.ReadInt32(); // always 1
			int sampleRate = reader.ReadInt32(); // sampling rate
			byte nbChannels = reader.ReadByte(); // mono or stereo
			byte[] reserved = reader.ReadBytes(32); // reserved


		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
