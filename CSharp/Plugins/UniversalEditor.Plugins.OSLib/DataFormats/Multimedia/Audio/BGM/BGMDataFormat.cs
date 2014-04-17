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
			short[] usamples = OSLDecode(sampleData.Length / 2, sampleData);

			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			wave.RawSamples = usamples;
			wave.Header.BitsPerSample = 16;
			wave.Header.ChannelCount = 1;
			wave.Header.SampleRate = sampleRate;
			wave.Header.BlockAlignment = 2;
			wave.Header.DataRate = sampleRate * 2;
		}

		private readonly sbyte[] ima_index_table /*[16]*/ =
		{
		  -1, -1, -1, -1, 2, 4, 7, 12,
		  -1, -1, -1, -1, 2, 4, 7, 12
		};
		private readonly short[] ima_step_table /*[89]*/ =
		{
			  7,    8,    9,   10,   11,   12,   13,   14,   16,   17,
			 19,   21,   23,   25,   28,   31,   34,   37,   41,   45,
			 50,   55,   60,   66,   73,   80,   88,   97,  107,  118,
			130,  143,  157,  173,  190,  209,  230,  253,  279,  307,
			337,  371,  408,  449,  494,  544,  598,  658,  724,  796,
			876,  963, 1060, 1166, 1282, 1411, 1552, 1707, 1878, 2066,
		   2272, 2499, 2749, 3024, 3327, 3660, 4026, 4428, 4871, 5358,
		   5894, 6484, 7132, 7845, 8630, 9493,10442,11487,12635,13899,
		  15289,16818,18500,20350,22385,24623,27086,29794,32767
		};

		private static int ima_get_diff(int step, byte nibble, ref int predictor)
		{
            int diff = step >> 3;
            if ((nibble & 1) != 0) diff += step >> 2;
            if ((nibble & 2) != 0) diff += step >> 1;
            if ((nibble & 4) != 0) diff += step;
            if ((nibble & 7) == 7) diff += step >> 1;
            if ((nibble & 8) != 0) diff = -diff;
            return diff;
		}

		private short[] OSLDecode(int sampleCount, byte[] data)
		{
            // each byte represents two IMA nibbles, which each represent a signed 16-bit PCM sample
            // so therefore each byte represents 2 Int16 samples
            short[] samples = new short[data.Length * 2];

			int len = data.Length;

            int predictor = 0;
            int step_index = 0;
            byte nibble = 0;

            for (int i = 0; i < data.Length; i++)
            {
                int step = 0;

                nibble = (byte)(data[i] & 0x0F);
                ima_process_nibble(nibble, ref step_index, ref predictor, ref step);
                samples[(int)((double)i * 2)] = (short)predictor;
                
                nibble = (byte)(data[i] >> 4);
                ima_process_nibble(nibble, ref step_index, ref predictor, ref step);
                samples[(int)((double)i * 2) + 1] = (short)predictor;
            }
			return samples;
		}

        private void ima_process_nibble(byte nibble, ref int step_index, ref int predictor, ref int step)
        {
            if (step_index < 0) step_index = 0;
            if (step_index > ima_step_table.Length - 1) step_index = ima_step_table.Length - 1;

            step = ima_step_table[step_index];
            int diff = ima_get_diff(step, nibble, ref predictor);

            step_index += ima_index_table[nibble & 0x07];
            
            predictor = predictor + diff;
            if (predictor < Int16.MinValue) predictor = Int16.MinValue;
            if (predictor > Int16.MaxValue) predictor = Int16.MaxValue;
        }

		protected override void SaveInternal(ObjectModel objectModel)
		{
			throw new NotImplementedException();
		}
	}
}
