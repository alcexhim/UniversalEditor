//
//  VideoTrack.cs - represents a video track in a video file
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

namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	/// <summary>
	/// Represents a video track in a video file.
	/// </summary>
	public class VideoTrack : ICloneable
	{
		public class VideoTrackCollection : Collection<VideoTrack>
		{
		}

		/// <summary>
		/// Gets or sets the name of this <see cref="VideoTrack" />.
		/// </summary>
		/// <value>The name of this <see cref="VideoTrack" />.</value>
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets a collection of <see cref="VideoFrame" /> instances representing the individual frames in this <see cref="VideoTrack" />.
		/// </summary>
		/// <value>The frames in this <see cref="VideoTrack" />.</value>
		public VideoFrame.VideoFrameCollection Frames { get; } = new VideoFrame.VideoFrameCollection();

		/// <summary>
		/// Gets or sets the frame rate in frames per second for <see cref="VideoFrame" />s in this <see cref="VideoTrack" />.
		/// </summary>
		/// <value>The frame rate.</value>
		public int FrameRate { get; set; } = 24;

		public int BlockDimension { get; set; } = 8;
		public int SubBlockDimension { get; set; } = 4;

		/// <summary>
		/// Gets or sets the width of a <see cref="VideoFrame" /> in this <see cref="VideoTrack" />.
		/// </summary>
		/// <value>The width of a <see cref="VideoFrame" /> in this <see cref="VideoTrack" />.</value>
		public int Width { get; set; } = 320;
		/// <summary>
		/// Gets or sets the height of a <see cref="VideoFrame" /> in this <see cref="VideoTrack" />.
		/// </summary>
		/// <value>The height of a <see cref="VideoFrame" /> in this <see cref="VideoTrack" />.</value>
		public int Height { get; set; } = 240;

		public object Clone()
		{
			return new VideoTrack
			{
				Name = this.Name,
				Height = this.Height,
				Width = this.Width,
				BlockDimension = this.BlockDimension,
				FrameRate = this.FrameRate,
				SubBlockDimension = this.SubBlockDimension
			};
		}
	}
}
