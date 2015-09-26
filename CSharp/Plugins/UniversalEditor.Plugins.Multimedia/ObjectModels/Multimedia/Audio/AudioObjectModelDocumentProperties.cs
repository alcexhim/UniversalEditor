using System;
namespace UniversalEditor.Plugins.Multimedia.ObjectModels.Audio
{
	public class AudioObjectModelDocumentProperties
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
	}
}
