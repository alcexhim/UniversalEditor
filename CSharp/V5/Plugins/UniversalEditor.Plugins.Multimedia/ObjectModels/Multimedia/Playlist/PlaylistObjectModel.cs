using System;
using System.Collections.Generic;
namespace UniversalEditor.ObjectModels.Multimedia.Playlist
{
	public class PlaylistObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Title = "Multimedia playlist";
            omr.Path = new string[] { "Multimedia", "Playlist" };
			return omr;
		}
		private Guid mvarID = Guid.Empty;
		private Dictionary<string, string> mvarCustomInformation = new Dictionary<string, string>();
		private string mvarTitle = string.Empty;
		private string mvarAuthor = string.Empty;
		private string mvarAbstract = string.Empty;
		private string mvarCopyright = string.Empty;
		private PlaylistEntry.PlaylistEntryCollection mvarEntries = new PlaylistEntry.PlaylistEntryCollection();
		
		public Guid ID
		{
			get
			{
				return this.mvarID;
			}
			set
			{
				this.mvarID = value;
			}
		}
		public Dictionary<string, string> CustomInformation
		{
			get
			{
				return this.mvarCustomInformation;
			}
		}
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
		public PlaylistEntry.PlaylistEntryCollection Entries
		{
			get
			{
				return this.mvarEntries;
			}
		}
		public override void Clear()
		{
			this.mvarAbstract = string.Empty;
			this.mvarAuthor = string.Empty;
			this.mvarCopyright = string.Empty;
			this.mvarCustomInformation.Clear();
			this.mvarEntries.Clear();
			this.mvarID = Guid.Empty;
			this.mvarTitle = string.Empty;
		}
		public override void CopyTo(ObjectModel destination)
		{
			PlaylistObjectModel clone = destination as PlaylistObjectModel;
			clone.Abstract = (this.mvarAbstract.Clone() as string);
			clone.Author = (this.mvarAuthor.Clone() as string);
			clone.Copyright = (this.mvarCopyright.Clone() as string);
			foreach (KeyValuePair<string, string> kvp in this.mvarCustomInformation)
			{
				clone.CustomInformation.Add(kvp.Key, kvp.Value);
			}
			foreach (PlaylistEntry entry in this.mvarEntries)
			{
				clone.Entries.Add(entry.Clone() as PlaylistEntry);
			}
			clone.ID = this.mvarID;
			clone.Title = this.mvarTitle;
		}
	}
}
