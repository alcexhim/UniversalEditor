//
//  AudioObjectModelInformation.cs - describes general document properties for an audio file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.ObjectModels.Multimedia.Audio
{
	/// <summary>
	/// Describes general document properties for an audio file.
	/// </summary>
	public class AudioObjectModelInformation
	{
		private string mvarGeneratorTitle = string.Empty;
		private string mvarGeneratorAuthor = string.Empty;
		private Version mvarGeneratorVersion = new Version(1, 0, 0, 0);
		public string SongTitle { get; set; } = string.Empty;
		public string AlbumTitle { get; set; } = string.Empty;
		public string Creator { get; set; } = string.Empty;
		public string Comments { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public int FadeOutDelay { get; set; } = 0;
		public int FadeOutLength { get; set; } = 0;
		public string SongArtist { get; set; } = string.Empty;
		public string Genre { get; set; } = String.Empty;
		public int TrackNumber { get; set; } = -1;
		public Property.PropertyCollection CustomProperties { get; } = new Property.PropertyCollection();

		public void Clear()
		{
			AlbumTitle = String.Empty;
			Comments = String.Empty;
			Creator = String.Empty;
			CustomProperties.Clear();
			DateCreated = DateTime.Now;
			FadeOutDelay = 0;
			FadeOutLength = 0;
			mvarGeneratorAuthor = String.Empty;
			mvarGeneratorTitle = String.Empty;
			mvarGeneratorVersion = new Version(1, 0, 0, 0);
			Genre = String.Empty;
			SongArtist = String.Empty;
			SongTitle = String.Empty;
			TrackNumber = -1;
		}
	}
}
