using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.BGM
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
				_dfr.Sources.Add("https://www.assembla.com/code/oslibmod/subversion/nodes/trunk/bgm.h?rev=52");
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;
			
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

			wave.RawData = sampleData;
			wave.Header.BitsPerSample = 16;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = sampleRate;
			wave.Header.BlockAlignment = 2;
			wave.Header.DataRate = sampleRate * 2;
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;

			writer.WriteFixedLengthString("OSLBGM v01\0");

			writer.WriteByte(0);
			writer.WriteUInt32(1); // always 1
			writer.WriteInt32(wave.Header.SampleRate); // sampling rate
			writer.WriteByte((byte)wave.Header.ChannelCount); // mono or stereo
			writer.WriteBytes(new byte[32]); // reserved
			writer.WriteByte(0);

			byte[] sampleData = wave.RawData;
			OSLCompressionModule ocm = new OSLCompressionModule();
			sampleData = ocm.Compress(sampleData);
			writer.WriteBytes(sampleData);
		}
	}
}
