using System;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Audio.Waveform.FLAC.Internal
{
	public class FLACMetadataBlockStreamInfo : FLACMetadataBlock
	{
		private short mvarMinimumBlockSize = 0;
		private short mvarMaximumBlockSize = 0;
		private int mvarMinimumFrameSize = 0;
		private int mvarMaximumFrameSize = 0;
		private int mvarSampleRate = 0;
		private byte mvarChannelCount = 0;
		private byte mvarBitsPerSample = 0;
		private long mvarTotalSamplesInStream = 0L;
		private short mvarMD5Signature = 0;
		public short MinimumBlockSize
		{
			get
			{
				return this.mvarMinimumBlockSize;
			}
			set
			{
				this.mvarMinimumBlockSize = value;
			}
		}
		public short MaximumBlockSize
		{
			get
			{
				return this.mvarMaximumBlockSize;
			}
			set
			{
				this.mvarMaximumBlockSize = value;
			}
		}
		public int MinimumFrameSize
		{
			get
			{
				return this.mvarMinimumFrameSize;
			}
			set
			{
				this.mvarMinimumFrameSize = value;
			}
		}
		public int MaximumFrameSize
		{
			get
			{
				return this.mvarMaximumFrameSize;
			}
			set
			{
				this.mvarMaximumFrameSize = value;
			}
		}
		public int SampleRate
		{
			get
			{
				return this.mvarSampleRate;
			}
			set
			{
				this.mvarSampleRate = value;
			}
		}
		public byte ChannelCount
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
		public byte BitsPerSample
		{
			get
			{
				return this.mvarBitsPerSample;
			}
			set
			{
				this.mvarBitsPerSample = value;
			}
		}
		public long TotalSamplesInStream
		{
			get
			{
				return this.mvarTotalSamplesInStream;
			}
			set
			{
				this.mvarTotalSamplesInStream = value;
			}
		}
		public short MD5Signature
		{
			get
			{
				return this.mvarMD5Signature;
			}
			set
			{
				this.mvarMD5Signature = value;
			}
		}
	}
}
