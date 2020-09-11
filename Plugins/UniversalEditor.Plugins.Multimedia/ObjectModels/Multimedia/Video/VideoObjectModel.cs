//
//  VideoObjectModel.cs - provides an ObjectModel for manipulating video files
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

namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating video files.
	/// </summary>
	public class VideoObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Path = new string[] { "Multimedia", "Video" };
			return omr;
		}

		/// <summary>
		/// Gets a collection of <see cref="VideoTrack" /> instances representing the video tracks in this <see cref="VideoObjectModel" />.
		/// </summary>
		/// <value>The video tracks in this video.</value>
		public VideoTrack.VideoTrackCollection VideoTracks { get; } = new VideoTrack.VideoTrackCollection();
		/// <summary>
		/// Gets a collection of <see cref="AudioTrack" /> instances representing the audio tracks in this <see cref="VideoObjectModel" />.
		/// </summary>
		/// <value>The audio tracks in this video.</value>
		public AudioTrack.AudioTrackCollection AudioTracks { get; } = new AudioTrack.AudioTrackCollection();

		public override void Clear()
		{
			AudioTracks.Clear();
			VideoTracks.Clear();
		}

		public override void CopyTo(ObjectModel destination)
		{
			VideoObjectModel clone = (destination as VideoObjectModel);
			foreach (AudioTrack track in this.AudioTracks)
			{
				clone.AudioTracks.Add(track.Clone() as AudioTrack);
			}
			foreach (VideoTrack track2 in this.VideoTracks)
			{
				clone.VideoTracks.Add(track2.Clone() as VideoTrack);
			}
		}
	}
}
