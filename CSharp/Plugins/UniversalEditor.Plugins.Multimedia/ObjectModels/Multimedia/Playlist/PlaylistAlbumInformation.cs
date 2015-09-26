using System;
namespace UniversalEditor.ObjectModels.Multimedia.Playlist
{
	public class PlaylistAlbumInformation
	{
		private string mvarTitle = string.Empty;
		private string mvarArtist = string.Empty;
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
		public string Artist
		{
			get
			{
				return this.mvarArtist;
			}
			set
			{
				this.mvarArtist = value;
			}
		}
	}
}
