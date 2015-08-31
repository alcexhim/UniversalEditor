using System;
using System.Collections.ObjectModel;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;
namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	public class AudioTrack : ICloneable
	{
		public class AudioTrackCollection : Collection<AudioTrack>
		{
		}
		private string mvarName = string.Empty;
		private WaveformAudioObjectModel mvarObjectModel = new WaveformAudioObjectModel();
		public string Name
		{
			get
			{
				return this.mvarName;
			}
			set
			{
				this.mvarName = value;
			}
		}
		public WaveformAudioObjectModel ObjectModel
		{
			get
			{
				return this.mvarObjectModel;
			}
			set
			{
				this.mvarObjectModel = value;
			}
		}
		public object Clone()
		{
			return new AudioTrack
			{
				Name = this.mvarName, 
				ObjectModel = this.mvarObjectModel.Clone() as WaveformAudioObjectModel
			};
		}
	}
}
