using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioHeader
	{
		private ushort mvarFormatTag = 1;
		private short mvarChannelCount = 1;
		private int mvarDataRate = 88200;
		private short mvarBlockAlignment = 2;
		public ushort FormatTag
		{
			get
			{
				return this.mvarFormatTag;
			}
			set
			{
				this.mvarFormatTag = value;
			}
		}

		public WaveformAudioKnownFormat Format
		{
			get { return (WaveformAudioKnownFormat)mvarFormatTag; }
			set { mvarFormatTag = (ushort)value; }
		}


		public short ChannelCount
		{
			get
			{
				return this.mvarChannelCount;
			}
			set
			{
				this.mvarChannelCount = value;
			}
		}
		private int mvarSampleRate = 44100;
		public int SampleRate { get { return mvarSampleRate; } set { mvarSampleRate = value; } }
		public int DataRate { get { return mvarDataRate; } set { mvarDataRate = value; } }

		public short BlockAlignment
		{
			get
			{
				return this.mvarBlockAlignment;
			}
			set
			{
				this.mvarBlockAlignment = value;
			}
		}

		private short mvarBitsPerSample = 16;
		public short BitsPerSample { get { return mvarBitsPerSample; } set { mvarBitsPerSample = value; } }
	}
}
