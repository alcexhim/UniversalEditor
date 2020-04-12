//
//  ADXDataFormat.cs - provides a DataFormat for manipulating audio files in ADX format
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2020 Mike Becker
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
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.CRI.ADX
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating audio files in ADX format.
	/// </summary>
	public class ADXDataFormat : DataFormat
	{
		public byte[] Signature { get; set; } = new byte[] { 0x80, 0x00 };

		public ushort CopyrightOffset { get { return (ushort)(AdditionalPadding.Length + 2); } }
		/// <summary>
		/// Gets or sets a value indicating the encoding type for the ADX codec.
		/// </summary>
		/// <value>The encoding type for the ADX codec.</value>
		public ADXEncodingType EncodingType { get; set; } = ADXEncodingType.StandardADX;
		public byte BlockSize { get; set; } = 0;
		public byte SampleBitDepth { get; set; } = 0;
		public byte ChannelCount { get; set; } = 0;
		public uint SampleRate { get; set; } = 0;
		public uint TotalSamples { get; set; } = 0;
		public ushort HighpassFrequency { get; set; } = 0;

		/// <summary>
		/// Gets or sets a value indicating the version of the ADX codec in use.
		/// </summary>
		public ADXVersion Version { get; set; } = ADXVersion.ADXVersion3;

		public byte Flags { get; set; } = 0;
		public uint Unknown { get; set; } = 0;
		public bool LoopEnabled { get; set; } = false;
		public uint LoopBeginSampleIndex { get; set; } = 0;
		public uint LoopBeginByteIndex { get; set; } = 0;
		public uint LoopEndSampleIndex { get; set; } = 0;
		public uint LoopEndByteIndex { get; set; } = 0;
		public byte[] AdditionalPadding { get; set; } = new byte[] { };

		public double[] GetPredictionCoefficients()
		{
			double M_PI = Math.Acos(-1.0);
			double a, b, c;
			a = Math.Sqrt(2.0) - Math.Cos(2.0 * M_PI * ((double)HighpassFrequency / SampleRate));
			b = Math.Sqrt(2.0) - 1.0;
			c = (a - Math.Sqrt((a + b) * (a - b))) / b; //(a+b)*(a-b) = a*a-b*b, however the simpler formula loses accuracy in floating point
			double[] coefficient = new double[] { c * 2.0, -(c * c) };
			return coefficient;
		}
		public uint decode_adx_standard(ref short[] buffer, uint samples_needed, bool looping_enabled)
		{
			int[] past_samples = new int[ChannelCount * 2]; // Previously decoded samples from each channel, zeroed at start (size = 2*channel_count)
			uint sample_index = 0; // sample_index is the index of sample set that needs to be decoded next

			// buffer is where the decoded samples will be put
			// samples_needed states how many sample 'sets' (one sample from every channel) need to be decoded to fill the buffer
			// looping_enabled is a boolean flag to control use of the built-in loop
			// Returns the number of sample 'sets' in the buffer that could not be filled (EOS)
			uint samples_per_block = (uint)((BlockSize - 2) * 8 / SampleBitDepth);
			short[] scale = new short[ChannelCount];
			if (looping_enabled && LoopEnabled) looping_enabled = false;

			// Loop until the requested number of samples are decoded, or the end of file is reached
			while (samples_needed > 0 && sample_index < TotalSamples)
			{
				// Calculate the number of samples that are left to be decoded in the current block
				uint sample_offset = sample_index % samples_per_block;
				uint samples_can_get = samples_per_block - sample_offset;

				// Clamp the samples we can get during this run if they won't fit in the buffer
				if (samples_can_get > samples_needed)
				{
					samples_can_get = samples_needed;
				}

				// Clamp the number of samples to be acquired if the stream isn't long enough or the loop trigger is nearby
				if (looping_enabled && sample_index + samples_can_get > LoopEndSampleIndex)
				{
					samples_can_get = LoopEndSampleIndex - sample_index;
				}
				else if (sample_index + samples_can_get > TotalSamples)
				{
					samples_can_get = TotalSamples - sample_index;
				}

				// Calculate the bit address of the start of the frame that sample_index resides in and record that location
				ulong started_at = (ulong)(CopyrightOffset + 4 + sample_index / samples_per_block * BlockSize * ChannelCount) * 8;

				// Read the scale values from the start of each block in this frame
				for (uint i = 0; i < ChannelCount; ++i)
				{
					// assuming system.io.binaryreader br

					// br.Seek( started_at + mvarBlockSize * i * 8 );
					// scale[i] = br.ReadInt16();
				}

				// Pre-calculate the stop value for sample_offset
				uint sample_endoffset = sample_offset + samples_can_get;

				// Save the bitstream address of the first sample immediately after the scale in the first block of the frame
				started_at += 16;
				double[] coefficient = GetPredictionCoefficients();
				int sample_error = 0;
				int ptrBuffer = 0;
				while (sample_offset < sample_endoffset)
				{
					for (uint i = 0; i < ChannelCount; ++i)
					{
						// Predict the next sample
						double sample_prediction = coefficient[0] * past_samples[i * 2 + 0] + coefficient[1] * past_samples[i * 2 + 1];

						// Seek to the sample offset, read and sign extend it to a 32bit integer
						// Implementing sign extension is left as an exercise for the reader
						// The sign extension will also need to include a endian adjustment if there are more than 8 bits

						// br.Seek( started_at + mvarSampleBitDepth * sample_offset + mvarBlockSize * 8 * i );
						// int sample_error = br.Read( mvarSampleBitDepth );
						// sample_error = sign_extend( sample_error, mvarSampleBitDepth);

						// Scale the error correction value
						sample_error *= scale[i];

						// Calculate the sample by combining the prediction with the error correction
						int sample = sample_error + (int)sample_prediction;

						// Update the past samples with the newer sample
						past_samples[i * 2 + 1] = past_samples[i * 2 + 0];
						past_samples[i * 2 + 0] = sample;

						// Clamp the decoded sample to the valid range for a 16bit integer
						if (sample > 32767)
						{
							sample = 32767;
						}
						else if (sample < -32768)
						{
							sample = -32768;
						}

						// Save the sample to the buffer then advance one place
						buffer[ptrBuffer] = (short)sample; ptrBuffer++;
					}
					++sample_offset;  // We've decoded one sample from every block, advance block offset by 1
					++sample_index;   // This also means we're one sample further into the stream
					--samples_needed; // And so there is one less set of samples that need to be decoded
				}

				// Check if we hit the loop end marker, if we did we need to jump to the loop start
				if (looping_enabled && sample_index == LoopEndSampleIndex)
				{
					sample_index = LoopBeginSampleIndex;
				}
			}
			return samples_needed;
		}

		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(WaveformAudioObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null)
				throw new ObjectModelNotSupportedException();

			Reader reader = base.Accessor.Reader;

			throw new NotImplementedException();
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			WaveformAudioObjectModel wave = (objectModel as WaveformAudioObjectModel);
			if (wave == null)
				throw new ObjectModelNotSupportedException();

			Writer bw = base.Accessor.Writer;
			bw.WriteBytes(Signature);
			bw.WriteUInt16(CopyrightOffset);
			bw.WriteByte((byte)EncodingType);
			bw.WriteByte(BlockSize);
			bw.WriteByte(SampleBitDepth);
			bw.WriteByte(ChannelCount);
			bw.WriteUInt32(SampleRate);
			bw.WriteUInt32(TotalSamples);
			bw.WriteUInt16(HighpassFrequency);
			bw.WriteByte((byte)Version);
			bw.WriteByte(Flags);
			bw.WriteUInt32(Unknown);

			if (Version == ADXVersion.ADXVersion3 || Version == ADXVersion.ADXVersion3DifferentDecoder)
			{
				// Write the v3 fields here
				bw.WriteBoolean(LoopEnabled);
				bw.WriteUInt32(LoopBeginSampleIndex);
				bw.WriteUInt32(LoopBeginByteIndex);
				bw.WriteUInt32(LoopEndSampleIndex);
				bw.WriteUInt32(LoopEndByteIndex);
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
			}
			else if (Version == ADXVersion.ADXVersion4 || Version == ADXVersion.ADXVersion4WithoutLooping)
			{
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
				bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
				bw.WriteBoolean(LoopEnabled);
				bw.WriteUInt32(LoopBeginSampleIndex);
				bw.WriteUInt32(LoopBeginByteIndex);
				bw.WriteUInt32(LoopEndSampleIndex);
				bw.WriteUInt32(LoopEndByteIndex);
			}
			bw.WriteBytes(AdditionalPadding);
			bw.WriteFixedLengthString("(c)CRI");

			// Audio data starts here!
			throw new NotImplementedException();
		}
	}
}
