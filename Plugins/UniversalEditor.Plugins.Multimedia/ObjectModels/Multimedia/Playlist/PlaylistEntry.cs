//
//  PlaylistEntry.cs - represents an entry in a PlaylistObjectModel
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

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia.Playlist
{
	/// <summary>
	/// Represents an entry in a <see cref="PlaylistObjectModel" />.
	/// </summary>
	public class PlaylistEntry
	{
		public class PlaylistEntryCollection : Collection<PlaylistEntry>
		{
		}
		
		private string mvarCopyright = string.Empty;
		private long mvarOffset = 0L;
		private long mvarLength = 0L;
		private string mvarFileName = string.Empty;
		private Dictionary<string, string> mvarCustomInformation = new Dictionary<string, string>();
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public PlaylistAlbumInformation Album { get; } = new PlaylistAlbumInformation();
		public int TrackNumber { get; set; } = 0;
		public string AlbumArtImageURL { get; set; } = string.Empty;
		public string Abstract { get; set; } = string.Empty;
		public string Copyright
		{
			get
			{
				return this.mvarCopyright;
			}
			set
			{
				this.mvarCopyright = value;
			}
		}
		public long Offset
		{
			get
			{
				return this.mvarOffset;
			}
			set
			{
				this.mvarOffset = value;
			}
		}
		public long Length
		{
			get
			{
				return this.mvarLength;
			}
			set
			{
				this.mvarLength = value;
			}
		}
		public string FileName
		{
			get
			{
				return this.mvarFileName;
			}
			set
			{
				this.mvarFileName = value;
			}
		}
		public Dictionary<string, string> CustomInformation
		{
			get
			{
				return this.mvarCustomInformation;
			}
		}
		public object Clone()
		{
			PlaylistEntry clone = new PlaylistEntry();
			clone.Abstract = this.Abstract;
			clone.Album.Artist = this.Album.Artist;
			clone.Album.Title = this.Album.Title;
			clone.AlbumArtImageURL = this.AlbumArtImageURL;
			clone.Author = this.Author;
			clone.Copyright = this.mvarCopyright;
			foreach (KeyValuePair<string, string> kvp in this.mvarCustomInformation)
			{
				clone.CustomInformation.Add(kvp.Key, kvp.Value);
			}
			clone.FileName = this.mvarFileName;
			clone.Title = this.Title;
			clone.TrackNumber = this.TrackNumber;
			return clone;
		}
	}
}
