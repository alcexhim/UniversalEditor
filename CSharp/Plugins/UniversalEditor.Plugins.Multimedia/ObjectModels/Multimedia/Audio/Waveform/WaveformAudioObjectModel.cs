using System;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.PropertyList;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	public class WaveformAudioObjectModel : AudioObjectModel
	{
		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Waveform (raw) audio";
			omr.Path = new string[] { "Multimedia", "Audio", "Waveform Audio" };
			return omr;
		}

		private WaveformAudioHeader mvarHeader = new WaveformAudioHeader();
		private WaveformAudioExtendedHeader mvarExtendedHeader = new WaveformAudioExtendedHeader();
		private byte[] mvarRawData = null;
		private short[] mvarRawSamples = new short[0];
		public WaveformAudioHeader Header
		{
			get
			{
				return this.mvarHeader;
			}
		}
		public WaveformAudioExtendedHeader ExtendedHeader
		{
			get
			{
				return this.mvarExtendedHeader;
			}
		}
		public byte[] RawData
		{
			get { return mvarRawData; }
			set
			{
				this.mvarRawData = value;

                IO.Reader br = new IO.Reader(new MemoryAccessor(value));
                List<short> samples = new List<short>();
                while (!br.EndOfStream)
                {
                    try
                    {
                        short sample = 0;
                        switch (mvarHeader.BitsPerSample)
                        {
                            case 8:
                            {
                                sample = br.ReadByte();
                                break;
                            }
                            case 16:
                            {
                                sample = br.ReadInt16();
                                break;
                            }
                        }
                        samples.Add(sample);
                    }
                    catch (System.IO.IOException ex)
                    {
                        break;
                    }
                }
                mvarRawSamples = samples.ToArray();
			}
		}
		public short[] RawSamples
		{
			get { return mvarRawSamples; }
			set { mvarRawSamples = value; }
		}
		public override void CopyTo(ObjectModel destination)
		{
			WaveformAudioObjectModel clone = destination as WaveformAudioObjectModel;
			clone.ExtendedHeader.ChannelMask = this.mvarExtendedHeader.ChannelMask;
			clone.ExtendedHeader.Enabled = this.mvarExtendedHeader.Enabled;
			clone.ExtendedHeader.SubFormatGUID = this.mvarExtendedHeader.SubFormatGUID;
			clone.ExtendedHeader.ValidBitsPerSample = this.mvarExtendedHeader.ValidBitsPerSample;
			clone.Header.BitsPerSample = this.mvarHeader.BitsPerSample;
			clone.Header.BlockAlignment = this.mvarHeader.BlockAlignment;
			clone.Header.ChannelCount = this.mvarHeader.ChannelCount;
			clone.Header.DataRate = this.mvarHeader.DataRate;
			clone.Header.FormatTag = this.mvarHeader.FormatTag;
			clone.Header.SampleRate = this.mvarHeader.SampleRate;
			clone.RawData = (this.mvarRawData.Clone() as byte[]);
			clone.RawSamples = (this.mvarRawSamples.Clone() as short[]);

			clone.Information.AlbumTitle = (Information.AlbumTitle.Clone() as string);
			clone.Information.Comments = (Information.Comments.Clone() as string);
			clone.Information.Creator = (Information.Creator.Clone() as string);
			foreach (Property property in Information.CustomProperties)
			{
				clone.Information.CustomProperties.Add(property.Clone() as Property);
			}
			clone.Information.DateCreated = Information.DateCreated;
			clone.Information.FadeOutDelay = Information.FadeOutDelay;
			clone.Information.FadeOutLength = Information.FadeOutLength;
			clone.Information.Genre = (Information.Genre.Clone() as string);
			clone.Information.SongArtist = (Information.SongArtist.Clone() as string);
			clone.Information.SongTitle = (Information.SongTitle.Clone() as string);
			clone.Information.TrackNumber = Information.TrackNumber;
		}
		public override void Clear()
		{
			mvarRawData = new byte[0];
			mvarHeader = new WaveformAudioHeader();
			mvarExtendedHeader = new WaveformAudioExtendedHeader();

			Information.Clear();

			mvarRawSamples = new short[0];
		}
	}
}
