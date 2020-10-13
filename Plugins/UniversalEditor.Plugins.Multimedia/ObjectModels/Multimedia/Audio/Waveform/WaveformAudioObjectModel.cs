//
//  WaveformAudioObjectModel.cs - provides an ObjectModel for manipulating waveform audio files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Waveform
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating waveform audio files.
	/// </summary>
	public class WaveformAudioObjectModel : AudioObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Path = new string[] { "Multimedia", "Audio", "Waveform (raw) audio" };
			return omr;
		}

		private byte[] mvarRawData = null;

		public WaveformAudioHeader Header { get; private set; } = new WaveformAudioHeader();
		public WaveformAudioExtendedHeader ExtendedHeader { get; private set; } = new WaveformAudioExtendedHeader();
		public byte[] RawData
		{
			get { return mvarRawData; }
			set
			{
				this.mvarRawData = value;

				/*
				// FIXME: this is extremely immature and can be optimized with a simple for() loop...
				IO.Reader br = new IO.Reader(new MemoryAccessor(value));
				List<short> samples = new List<short>();
				while (!br.EndOfStream)
				{
					try
					{
						short sample = 0;
						switch (Header.BitsPerSample)
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
							default:
							{
								throw new ArgumentException("Bad BitsPerSample value: " + Header.BitsPerSample.ToString() + "; must be 8 or 16!");
							}
						}
						samples.Add(sample);
					}
					catch (System.IO.IOException ex)
					{
						break;
					}
				}
				RawSamples = new WaveformAudioSamples(samples.ToArray());
				*/
				RawSamples = new WaveformAudioSamples(this);
			}
		}
		public WaveformAudioSamples RawSamples { get; set; } = null;

		protected internal virtual void OnSampleRequest(WaveformAudioSampleRequestEventArgs e)
		{
			if (mvarRawData != null)
			{
				if (this.Header.BitsPerSample == 8)
				{
					short[] samples = new short[e.Length];
					for (int i = e.Offset; i < e.Offset + e.Length; i++)
					{
						samples[i - e.Offset] = (short)mvarRawData[i];
					}
					e.Samples = samples;
				}
				else if (this.Header.BitsPerSample == 16)
				{
					short[] samples = new short[e.Length];
					for (int i = e.Offset; i < e.Length; i++)
					{
						short u = BitConverter.ToInt16(new byte[] { mvarRawData[(i * 2)], mvarRawData[(i * 2) + 1] }, 0);
						samples[i] = u;
					}
					e.Samples = samples;
				}
			}
			SampleRequest?.Invoke(this, e);
		}
		public event EventHandler<WaveformAudioSampleRequestEventArgs> SampleRequest;

		protected internal void OnSampleLengthRequest(WaveformAudioSampleLengthRequestEventArgs e)
		{
			if (mvarRawData != null)
			{
				e.Length = mvarRawData.Length;
			}
			SampleLengthRequest?.Invoke(this, e);
		}
		public event EventHandler<WaveformAudioSampleLengthRequestEventArgs> SampleLengthRequest;

		public override void CopyTo(ObjectModel destination)
		{
			WaveformAudioObjectModel clone = destination as WaveformAudioObjectModel;
			clone.ExtendedHeader.ChannelMask = this.ExtendedHeader.ChannelMask;
			clone.ExtendedHeader.Enabled = this.ExtendedHeader.Enabled;
			clone.ExtendedHeader.SubFormatGUID = this.ExtendedHeader.SubFormatGUID;
			clone.ExtendedHeader.ValidBitsPerSample = this.ExtendedHeader.ValidBitsPerSample;
			clone.Header.BitsPerSample = this.Header.BitsPerSample;
			clone.Header.BlockAlignment = this.Header.BlockAlignment;
			clone.Header.ChannelCount = this.Header.ChannelCount;
			clone.Header.DataRate = this.Header.DataRate;
			clone.Header.FormatTag = this.Header.FormatTag;
			clone.Header.SampleRate = this.Header.SampleRate;
			clone.RawData = (this.mvarRawData.Clone() as byte[]);
			clone.RawSamples = (this.RawSamples.Clone() as WaveformAudioSamples);

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
			Header = new WaveformAudioHeader();
			ExtendedHeader = new WaveformAudioExtendedHeader();

			Information.Clear();

			RawSamples = new WaveformAudioSamples(new short[0]);
		}
	}
}
