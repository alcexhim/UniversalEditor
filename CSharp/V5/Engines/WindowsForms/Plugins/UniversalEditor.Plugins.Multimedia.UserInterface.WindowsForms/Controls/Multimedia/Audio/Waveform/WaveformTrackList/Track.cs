using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace UniversalEditor.Controls.Multimedia.Audio.Waveform.WaveformTrackList
{
	public class Track
	{
		public class TrackCollection
			: System.Collections.ObjectModel.Collection<Track>
		{
			public Track Add(string title, WaveformAudioObjectModel waveform)
			{
				Track track = new Track();
				track.Title = title;
				track.Waveform = waveform;
				Add(track);
				return track;
			}
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private WaveformAudioObjectModel mvarWaveform = null;
		public WaveformAudioObjectModel Waveform { get { return mvarWaveform; } set { mvarWaveform = value; } }

	}
}
