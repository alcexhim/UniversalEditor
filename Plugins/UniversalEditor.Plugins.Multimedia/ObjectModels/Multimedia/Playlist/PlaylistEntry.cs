using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia.Playlist
{
	public class PlaylistEntry
	{
		public class PlaylistEntryCollection : Collection<PlaylistEntry>
		{
		}
		private string mvarTitle = string.Empty;
		private string mvarAuthor = string.Empty;
		private PlaylistAlbumInformation mvarAlbum = new PlaylistAlbumInformation();
		private int mvarTrackNumber = 0;
		private string mvarAlbumArtImageURL = string.Empty;
		private string mvarAbstract = string.Empty;
		private string mvarCopyright = string.Empty;
		private long mvarOffset = 0L;
		private long mvarLength = 0L;
		private string mvarFileName = string.Empty;
		private Dictionary<string, string> mvarCustomInformation = new Dictionary<string, string>();
		public string Title
		{
			get
			{
				return this.mvarTitle;
			}
			set
			{
				this.mvarTitle = value;
			}
		}
		public string Author
		{
			get
			{
				return this.mvarAuthor;
			}
			set
			{
				this.mvarAuthor = value;
			}
		}
		public PlaylistAlbumInformation Album
		{
			get
			{
				return this.mvarAlbum;
			}
		}
		public int TrackNumber
		{
			get
			{
				return this.mvarTrackNumber;
			}
			set
			{
				this.mvarTrackNumber = value;
			}
		}
		public string AlbumArtImageURL
		{
			get
			{
				return this.mvarAlbumArtImageURL;
			}
			set
			{
				this.mvarAlbumArtImageURL = value;
			}
		}
		public string Abstract
		{
			get
			{
				return this.mvarAbstract;
			}
			set
			{
				this.mvarAbstract = value;
			}
		}
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
			clone.Abstract = this.mvarAbstract;
			clone.Album.Artist = this.mvarAlbum.Artist;
			clone.Album.Title = this.mvarAlbum.Title;
			clone.AlbumArtImageURL = this.mvarAlbumArtImageURL;
			clone.Author = this.mvarAuthor;
			clone.Copyright = this.mvarCopyright;
			foreach (KeyValuePair<string, string> kvp in this.mvarCustomInformation)
			{
				clone.CustomInformation.Add(kvp.Key, kvp.Value);
			}
			clone.FileName = this.mvarFileName;
			clone.Title = this.mvarTitle;
			clone.TrackNumber = this.mvarTrackNumber;
			return clone;
		}
	}
}
