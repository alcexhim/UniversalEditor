using System;
namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	public class VideoObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Video";
            omr.Path = new string[] { "Multimedia", "Video" };
			return omr;
		}
		private VideoTrack.VideoTrackCollection mvarVideoTracks = new VideoTrack.VideoTrackCollection();
		private AudioTrack.AudioTrackCollection mvarAudioTracks = new AudioTrack.AudioTrackCollection();
		public VideoTrack.VideoTrackCollection VideoTracks
		{
			get
			{
				return this.mvarVideoTracks;
			}
		}
		public AudioTrack.AudioTrackCollection AudioTracks
		{
			get
			{
				return this.mvarAudioTracks;
			}
		}
		public override void Clear()
		{
			this.mvarAudioTracks.Clear();
			this.mvarVideoTracks.Clear();
		}
		public override void CopyTo(ObjectModel destination)
		{
			VideoObjectModel clone = (destination as VideoObjectModel);
			foreach (AudioTrack track in this.mvarAudioTracks)
			{
				clone.AudioTracks.Add(track.Clone() as AudioTrack);
			}
			foreach (VideoTrack track2 in this.mvarVideoTracks)
			{
				clone.VideoTracks.Add(track2.Clone() as VideoTrack);
			}
		}
	}
}
