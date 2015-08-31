using System;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioExtendedHeader
	{
		private bool mvarEnabled = false;
		private short mvarValidBitsPerSample = 0;
		private int mvarChannelMask = 0;
		private Guid mvarSubFormatGUID = Guid.Empty;
		public bool Enabled
		{
			get
			{
				return this.mvarEnabled;
			}
			set
			{
				this.mvarEnabled = value;
			}
		}
		public short ValidBitsPerSample
		{
			get
			{
				return this.mvarValidBitsPerSample;
			}
			set
			{
				this.mvarValidBitsPerSample = value;
			}
		}
		public int ChannelMask
		{
			get
			{
				return this.mvarChannelMask;
			}
			set
			{
				this.mvarChannelMask = value;
			}
		}
		public Guid SubFormatGUID
		{
			get
			{
				return this.mvarSubFormatGUID;
			}
			set
			{
				this.mvarSubFormatGUID = value;
			}
		}
	}
}
