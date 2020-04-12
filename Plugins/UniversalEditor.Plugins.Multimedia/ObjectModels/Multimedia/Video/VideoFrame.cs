//
//  VideoFrame.cs - represents a single frame of picture data in a video
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

using UniversalEditor.ObjectModels.Multimedia.Picture;

namespace UniversalEditor.ObjectModels.Multimedia.Video
{
	/// <summary>
	/// Represents a single frame of picture data in a video.
	/// </summary>
	public class VideoFrame : ICloneable
	{
		public class VideoFrameCollection : Collection<VideoFrame>
		{
		}

		public PictureObjectModel ObjectModel { get; set; } = new PictureObjectModel();

		public object Clone()
		{
			return new VideoFrame
			{
				ObjectModel = this.ObjectModel.Clone() as PictureObjectModel
			};
		}
	}
}
