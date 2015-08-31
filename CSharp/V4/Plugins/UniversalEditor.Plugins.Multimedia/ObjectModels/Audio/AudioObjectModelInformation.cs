using System;
using UniversalEditor.ObjectModels.PropertyList;
namespace UniversalEditor.ObjectModels.Multimedia.Audio
{
	public class AudioObjectModelInformation
	{
		private string mvarSongTitle = string.Empty;
		private string mvarAlbumTitle = string.Empty;
		private string mvarCreator = string.Empty;
		private string mvarComments = string.Empty;
		private DateTime mvarDateCreated = DateTime.Now;
		private int mvarFadeOutDelay = 0;
		private int mvarFadeOutLength = 0;
		private string mvarSongArtist = string.Empty;
		private string mvarGeneratorTitle = string.Empty;
		private string mvarGeneratorAuthor = string.Empty;
		private Version mvarGeneratorVersion = new Version(1, 0, 0, 0);
		public string SongTitle
		{
			get
			{
				return this.mvarSongTitle;
			}
			set
			{
				this.mvarSongTitle = value;
			}
		}
		public string AlbumTitle
		{
			get
			{
				return this.mvarAlbumTitle;
			}
			set
			{
				this.mvarAlbumTitle = value;
			}
		}
		public string Creator
		{
			get
			{
				return this.mvarCreator;
			}
			set
			{
				this.mvarCreator = value;
			}
		}
		public string Comments
		{
			get
			{
				return this.mvarComments;
			}
			set
			{
				this.mvarComments = value;
			}
		}
		public DateTime DateCreated
		{
			get
			{
				return this.mvarDateCreated;
			}
			set
			{
				this.mvarDateCreated = value;
			}
		}
		public int FadeOutDelay
		{
			get
			{
				return this.mvarFadeOutDelay;
			}
			set
			{
				this.mvarFadeOutDelay = value;
			}
		}
		public int FadeOutLength
		{
			get
			{
				return this.mvarFadeOutLength;
			}
			set
			{
				this.mvarFadeOutLength = value;
			}
		}
		public string SongArtist
		{
			get
			{
				return this.mvarSongArtist;
			}
			set
			{
				this.mvarSongArtist = value;
			}
		}
		public string GeneratorTitle
		{
			get
			{
				return this.mvarGeneratorTitle;
			}
			set
			{
				this.mvarGeneratorTitle = value;
			}
		}
		public string GeneratorAuthor
		{
			get
			{
				return this.mvarGeneratorAuthor;
			}
			set
			{
				this.mvarGeneratorAuthor = value;
			}
		}
		public Version GeneratorVersion
		{
			get
			{
				return this.mvarGeneratorVersion;
			}
			set
			{
				this.mvarGeneratorVersion = value;
			}
		}

		private string mvarGenre = String.Empty;
		public string Genre { get { return mvarGenre; } set { mvarGenre = value; } }

		private int mvarTrackNumber = -1;
		public int TrackNumber { get { return mvarTrackNumber; } set { mvarTrackNumber = value; } }

		private Property.PropertyCollection mvarCustomProperties = new Property.PropertyCollection();
		public Property.PropertyCollection CustomProperties { get { return mvarCustomProperties; } }

		public void Clear()
		{
			mvarAlbumTitle = String.Empty;
			mvarComments = String.Empty;
			mvarCreator = String.Empty;
			mvarCustomProperties.Clear();
			mvarDateCreated = DateTime.Now;
			mvarFadeOutDelay = 0;
			mvarFadeOutLength = 0;
			mvarGeneratorAuthor = String.Empty;
			mvarGeneratorTitle = String.Empty;
			mvarGeneratorVersion = new Version(1, 0, 0, 0);
			mvarGenre = String.Empty;
			mvarSongArtist = String.Empty;
			mvarSongTitle = String.Empty;
			mvarTrackNumber = -1;
		}
	}
}
