//
//  AudioObjectModelDocumentProperties.cs - describes general document properties for an audio file
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

namespace UniversalEditor.Plugins.Multimedia.ObjectModels.Audio
{
	/// <summary>
	/// Describes general document properties for an audio file.
	/// </summary>
	public class AudioObjectModelDocumentProperties
	{
		public string SongTitle { get; set; } = string.Empty;
		public string AlbumTitle { get; set; } = string.Empty;
		public string Creator { get; set; } = string.Empty;
		public string Comments { get; set; } = string.Empty;
		public DateTime DateCreated { get; set; } = DateTime.Now;
		public int FadeOutDelay { get; set; } = 0;
		public int FadeOutLength { get; set; } = 0;
		public string SongArtist { get; set; } = string.Empty;
		public string GeneratorTitle { get; set; } = string.Empty;
		public string GeneratorAuthor { get; set; } = string.Empty;
		public Version GeneratorVersion { get; set; } = new Version(1, 0, 0, 0);
	}
}
