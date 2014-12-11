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
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("OSLib BGM audio", new byte?[][] { new byte?[] { (byte)'O', (byte)'S', (byte)'L', (byte)'B', (byte)'G', (byte)'M', (byte)' ', (byte)'v', (byte)'0', (byte)'1', (byte)0 } }, new string[] { "*.bgm" });
				_dfr.Sources.Add("https://www.assembla.com/code/oslibmod/subversion/nodes/trunk/bgm.h?rev=52");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			IO.Reader reader = base.Accessor.Reader;
			reader.Accessor.Position = 0;

			string header = reader.ReadFixedLengthString(11);
			if (header != "OSLBGM v01\0") throw new InvalidDataFormatException();

			byte unknown1 = reader.ReadByte();
			int format = reader.ReadInt32(); // always 1
			int sampleRate = reader.ReadInt32(); // sampling rate
			byte nbChannels = reader.ReadByte(); // mono or stereo
			byte[] reserved = reader.ReadBytes(32); // reserved
			byte unknown2 = reader.ReadByte();

			byte[] sampleData = reader.ReadToEnd();
			
			OSLCompressionModule ocm = new OSLCompressionModule();
			sampleData = ocm.Decompress(sampleData);

			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			wave.RawData = sampleData;
			wave.Header.BitsPerSample = 16;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = sampleRate;
			wave.Header.BlockAlignment = 2;
			wave.Header.DataRate = sampleRate * 2;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
