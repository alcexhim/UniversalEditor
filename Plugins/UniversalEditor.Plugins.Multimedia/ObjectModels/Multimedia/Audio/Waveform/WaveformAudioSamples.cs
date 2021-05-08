using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioSamples : ICloneable
	{
		private WaveformAudioObjectModel _wave = null;
		internal WaveformAudioSamples(WaveformAudioObjectModel wave)
		{
			_wave = wave;
		}

		public short this[int index]
		{
			get
			{
				if (_RawData != null)
				{
					return _RawData[index];
				}
				else
				{
					WaveformAudioSampleRequestEventArgs ee = new WaveformAudioSampleRequestEventArgs(index, 1);
					_wave.OnSampleRequest(ee);
					return ee.Samples[0];
				}
			}
		}

		public short[] this[int index, int length]
		{
			get
			{
				if (_RawData != null)
				{
					short[] rawData = new short[length];
					Array.Copy(_RawData, index, rawData, 0, rawData.Length);
					return rawData;
				}
				else
				{
					WaveformAudioSampleRequestEventArgs ee = new WaveformAudioSampleRequestEventArgs(index, length);
					_wave.OnSampleRequest(ee);
					return ee.Samples;
				}
			}
		}

		private short[] _RawData = null;
		public short[] RawData
		{
			get
			{
				return _RawData;
			}
		}

		public WaveformAudioSamples(short[] rawdat)
		{
			_RawData = rawdat;
		}

		public object Clone()
		{
			WaveformAudioSamples clone = new WaveformAudioSamples(RawData);
			return clone;
		}

		public int Length
		{
			get
			{
				if (_RawData != null)
				{
					return _RawData.Length;
				}

				WaveformAudioSampleLengthRequestEventArgs ee = new WaveformAudioSampleLengthRequestEventArgs();
				_wave.OnSampleLengthRequest(ee);
				return ee.Length;
			}
		}
	}
}
