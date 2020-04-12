//
//  VoicebankSample.cs - represents a sample of audio for a phoneme in a synthesizer voicebank file
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
using System.Collections.ObjectModel;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank
{
	/// <summary>
	/// Represents a sample of audio for a phoneme in a synthesizer voicebank file.
	/// </summary>
	public class VoicebankSample : ICloneable
	{
		public class VoicebankSampleCollection : Collection<VoicebankSample>
		{
			private Dictionary<string, VoicebankSample> phonemesByName = new Dictionary<string, VoicebankSample>();
			public VoicebankSample this[string Name] { get { return phonemesByName[Name]; } }

			public VoicebankSample Add(string Name, byte[] data)
			{
				VoicebankSample vp = new VoicebankSample();
				vp.Name = Name;
				vp.Data = data;
				this.Add(vp);
				return vp;
			}
			public bool Contains(string Name)
			{
				return this.phonemesByName.ContainsKey(Name);
			}
			protected override void InsertItem(int index, VoicebankSample item)
			{
				base.InsertItem(index, item);
				if (!this.phonemesByName.ContainsKey(item.Name))
				{
					this.phonemesByName.Add(item.Name, item);
				}
			}
		}
		/// <summary>
		/// Gets or sets the name of this sample.
		/// </summary>
		/// <value>The name of this sample.</value>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// The frequency at which this sample was recorded. Used to pitch-shift the sample.
		/// </summary>
		public int Frequency { get; set; } = 0;
		/// <summary>
		/// Gets or sets the number of channels in this sample.
		/// </summary>
		/// <value>The number of channels in this sample.</value>
		public short ChannelCount { get; set; } = 0;
		public int Dummy { get; set; } = 0;
		/// <summary>
		/// The waveform audio data for this sample.
		/// </summary>
		public WaveformAudioObjectModel Waveform { get; set; } = null;
		/// <summary>
		/// The maximum frequency in which to use this particular sample.
		/// </summary>
		/// <remarks>To use the sample for exactly one note, ensure that both MaximumFrequency and MinimumFrequency are set to the same value, matching the desired note's frequency.</remarks>
		public int MaximumFrequency { get; set; } = -1; // 440?
		/// <summary>
		/// The maximum frequency in which to use this particular sample.
		/// </summary>
		/// <remarks>To use the sample for exactly one note, ensure that both MaximumFrequency and MinimumFrequency are set to the same value, matching the desired note's frequency.</remarks>
		public int MinimumFrequency { get; set; } = -1; // 440?
		/// <summary>
		/// The phoneme that is represented by this sample. May be null for non-vocal samples.
		/// </summary>
		public string Phoneme { get; set; } = null;
		/// <summary>
		/// Gets or sets the source audio data for this sample.
		/// </summary>
		/// <value>The source audio data for this sample.</value>
		public byte[] Data { get; set; } = new byte[0];
		/// <summary>
		/// Gets or sets the file name of the source audio data for this sample.
		/// </summary>
		/// <value>The file name of the source audio data for this sample.</value>
		public string FileName { get; set; } = string.Empty;

		public object Clone()
		{
			return new VoicebankSample
			{
				Name = this.Name,
				FileName = this.FileName,
				Data = this.Data
			};
		}
	}
}
