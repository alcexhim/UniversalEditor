using System;
using UniversalEditor.ObjectModels.Multimedia.Audio.Voicebank;
namespace UniversalEditor.ObjectModels.Multimedia.Audio.Synthesized
{
	public class SynthesizedAudioObjectModel : AudioObjectModel
	{
		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Synthesized audio sequence";
            omr.Path = new string[] { "Multimedia", "Audio", "Synthesized Audio" };
			return omr;
		}

		private short mvarChannelCount = 2;
		public short ChannelCount { get { return mvarChannelCount; } set { mvarChannelCount = value; } }
		
		private string mvarName = string.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private double mvarTempo = 120.0;
		public double Tempo { get { return mvarTempo; } set { mvarTempo = value; } }

		private SynthesizedAudioTrack.SynthesizedAudioTrackCollection mvarTracks = new SynthesizedAudioTrack.SynthesizedAudioTrackCollection();
		public SynthesizedAudioTrack.SynthesizedAudioTrackCollection Tracks { get { return mvarTracks; } }

		private VoicebankObjectModel.VoicebankObjectModelCollection mvarVoices = new VoicebankObjectModel.VoicebankObjectModelCollection();
		public VoicebankObjectModel.VoicebankObjectModelCollection Voices { get { return mvarVoices; } }

		public override void CopyTo(ObjectModel destination)
		{
			SynthesizedAudioObjectModel clone = destination as SynthesizedAudioObjectModel;
			clone.Name = (this.mvarName.Clone() as string);
			clone.Tempo = this.mvarTempo;
			foreach (SynthesizedAudioTrack track in this.mvarTracks)
			{
				clone.Tracks.Add(track.Clone() as SynthesizedAudioTrack);
			}
		}
		public override void Clear()
		{
			this.mvarName = string.Empty;
			this.mvarTempo = 120.0;
			this.mvarTracks.Clear();
		}
	}
}
