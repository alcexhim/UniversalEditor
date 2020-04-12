//
//  AudioTrack.cs - represents a track of audio data in a video file
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
using System.Collections.ObjectModel;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	/// <summary>
	/// Represents a track of audio data in a video file.
	/// </summary>
	public class AudioTrack : ICloneable
	{
		public class AudioTrackCollection : Collection<AudioTrack>
		{
		}

		/// <summary>
		/// Gets or sets the name of the audio track.
		/// </summary>
		/// <value>The name of the audio track.</value>
		public string Name { get; set; } = string.Empty;
		/// <summary>
		/// Gets or sets the <see cref="WaveformAudioObjectModel" /> containing the audio data for this audio track.
		/// </summary>
		/// <value>The <see cref="WaveformAudioObjectModel" /> containing the audio data for this audio track.</value>
		public WaveformAudioObjectModel ObjectModel { get; set; } = new WaveformAudioObjectModel();

		public object Clone()
		{
			return new AudioTrack
			{
				Name = this.Name,
				ObjectModel = this.ObjectModel.Clone() as WaveformAudioObjectModel
			};
		}
	}
}
