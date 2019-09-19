//
//  ADXDocument.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2010-2019 Mike Becker
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
using System.Text;
using UniversalEditor.IO;

namespace UniversalEditor.DataFormats.Multimedia.Audio.Waveform.CRI.ADX
{
    public class ADXDataFormat : DataFormat
    {
        private byte[] mvarSignature = new byte[] { 0x80, 0x00 };
        public byte[] Signature { get { return mvarSignature; } set { mvarSignature = value; } }

        public ushort CopyrightOffset { get { return (ushort)(mvarAdditionalPadding.Length + 2); } }

        private ADXEncodingType mvarEncodingType = ADXEncodingType.StandardADX;
        public ADXEncodingType EncodingType { get { return mvarEncodingType; } set { mvarEncodingType = value; } }

        private byte mvarBlockSize = 0;
        public byte BlockSize { get { return mvarBlockSize; } set { mvarBlockSize = value; } }
        private byte mvarSampleBitDepth = 0;
        public byte SampleBitDepth { get { return mvarSampleBitDepth; } set { mvarSampleBitDepth = value; } }
        private byte mvarChannelCount = 0;
        public byte ChannelCount { get { return mvarChannelCount; } set { mvarChannelCount = value; } }
        private uint mvarSampleRate = 0;
        public uint SampleRate { get { return mvarSampleRate; } set { mvarSampleRate = value; } }
        private uint mvarTotalSamples = 0;
        public uint TotalSamples { get { return mvarTotalSamples; } set { mvarTotalSamples = value; } }
        private ushort mvarHighpassFrequency = 0;
        public ushort HighpassFrequency { get { return mvarHighpassFrequency; } set { mvarHighpassFrequency = value; } }

        private ADXVersion mvarVersion = ADXVersion.ADXVersion3;
        public ADXVersion Version { get { return mvarVersion; } set { mvarVersion = value; } }

        private byte mvarFlags = 0;
        public byte Flags { get { return mvarFlags; } set { mvarFlags = value; } }

        private uint mvarUnknown = 0;
        public uint Unknown { get { return mvarUnknown; } set { mvarUnknown = value; } }

        private bool mvarLoopEnabled = false; // uint
        public bool LoopEnabled { get { return mvarLoopEnabled; } set { mvarLoopEnabled = value; } }

        private uint mvarLoopBeginSampleIndex = 0;
        public uint LoopBeginSampleIndex { get { return mvarLoopBeginSampleIndex; } set { mvarLoopBeginSampleIndex = value; } }
        private uint mvarLoopBeginByteIndex = 0;
        public uint LoopBeginByteIndex { get { return mvarLoopBeginByteIndex; } set { mvarLoopBeginByteIndex = value; } }
        private uint mvarLoopEndSampleIndex = 0;
        public uint LoopEndSampleIndex { get { return mvarLoopEndSampleIndex; } set { mvarLoopEndSampleIndex = value; } }
        private uint mvarLoopEndByteIndex = 0;
        public uint LoopEndByteIndex { get { return mvarLoopEndByteIndex; } set { mvarLoopEndByteIndex = value; } }

        private byte[] mvarAdditionalPadding = new byte[] { };
        public byte[] AdditionalPadding { get { return mvarAdditionalPadding; } set { mvarAdditionalPadding = value; } }

        public double[] GetPredictionCoefficients()
        {
            double M_PI = Math.Acos(-1.0);
            double a, b, c;
            a = Math.Sqrt(2.0) - Math.Cos(2.0 * M_PI * ((double)mvarHighpassFrequency / mvarSampleRate));
            b = Math.Sqrt(2.0) - 1.0;
            c = (a - Math.Sqrt((a + b) * (a - b))) / b; //(a+b)*(a-b) = a*a-b*b, however the simpler formula loses accuracy in floating point
            double[] coefficient = new double[] { c * 2.0, -(c * c) };
            return coefficient;
        }
        public uint decode_adx_standard(ref short[] buffer, uint samples_needed, bool looping_enabled)
        {
            int[] past_samples = new int[mvarChannelCount * 2]; // Previously decoded samples from each channel, zeroed at start (size = 2*channel_count)
            uint sample_index = 0; // sample_index is the index of sample set that needs to be decoded next

            // buffer is where the decoded samples will be put
            // samples_needed states how many sample 'sets' (one sample from every channel) need to be decoded to fill the buffer
            // looping_enabled is a boolean flag to control use of the built-in loop
            // Returns the number of sample 'sets' in the buffer that could not be filled (EOS)
            uint samples_per_block = (uint)((mvarBlockSize - 2) * 8 / mvarSampleBitDepth);
            short[] scale = new short[mvarChannelCount];
            if (looping_enabled && mvarLoopEnabled) looping_enabled = false;

            // Loop until the requested number of samples are decoded, or the end of file is reached
            while (samples_needed > 0 && sample_index < mvarTotalSamples)
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
                if (looping_enabled && sample_index + samples_can_get > mvarLoopEndSampleIndex)
                {
                    samples_can_get = mvarLoopEndSampleIndex - sample_index;
                }
                else if (sample_index + samples_can_get > mvarTotalSamples)
                {
                    samples_can_get = mvarTotalSamples - sample_index;
                }

                // Calculate the bit address of the start of the frame that sample_index resides in and record that location
                ulong started_at = (ulong)(CopyrightOffset + 4 + sample_index / samples_per_block * mvarBlockSize * mvarChannelCount) * 8;

                // Read the scale values from the start of each block in this frame
                for (uint i = 0; i < mvarChannelCount; ++i)
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
                    for (uint i = 0; i < mvarChannelCount; ++i)
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
                if (looping_enabled && sample_index == mvarLoopEndSampleIndex)
                {
                    sample_index = mvarLoopBeginSampleIndex;
                }
            }
            return samples_needed;
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
            bw.WriteBytes(mvarSignature);
            bw.WriteUInt16(CopyrightOffset);
            bw.WriteByte((byte)mvarEncodingType);
            bw.WriteByte(mvarBlockSize);
            bw.WriteByte(mvarSampleBitDepth);
            bw.WriteByte(mvarChannelCount);
            bw.WriteUInt32(mvarSampleRate);
            bw.WriteUInt32(mvarTotalSamples);
            bw.WriteUInt16(mvarHighpassFrequency);
            bw.WriteByte((byte)mvarVersion);
            bw.WriteByte(mvarFlags);
            bw.WriteUInt32(mvarUnknown);

            if (mvarVersion == ADXVersion.ADXVersion3 || mvarVersion == ADXVersion.ADXVersion3DifferentDecoder)
            {
                // Write the v3 fields here
                bw.WriteBoolean(mvarLoopEnabled);
                bw.WriteUInt32(mvarLoopBeginSampleIndex);
                bw.WriteUInt32(mvarLoopBeginByteIndex);
                bw.WriteUInt32(mvarLoopEndSampleIndex);
                bw.WriteUInt32(mvarLoopEndByteIndex);
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
            }
            else if (mvarVersion == ADXVersion.ADXVersion4 || mvarVersion == ADXVersion.ADXVersion4WithoutLooping)
            {
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
                bw.WriteBytes(new byte[] { 0, 0, 0, 0 });
                bw.WriteBoolean(mvarLoopEnabled);
                bw.WriteUInt32(mvarLoopBeginSampleIndex);
                bw.WriteUInt32(mvarLoopBeginByteIndex);
                bw.WriteUInt32(mvarLoopEndSampleIndex);
                bw.WriteUInt32(mvarLoopEndByteIndex);
            }
            bw.WriteBytes(mvarAdditionalPadding);
            bw.WriteFixedLengthString("(c)CRI");

			// Audio data starts here!
			throw new NotImplementedException();
        }
    }
}
