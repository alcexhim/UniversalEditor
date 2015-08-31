using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Playlist;
using UniversalEditor.ObjectModels.PropertyList;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Playlist
{
	public class PLSDataFormat : WindowsConfigurationDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("PLS playlist", new byte?[][] { new byte?[] { new byte?(91), new byte?(112), new byte?(108), new byte?(97), new byte?(121), new byte?(108), new byte?(105), new byte?(115), new byte?(116), new byte?(93) } }, new string[] { "*.pls" });
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			dfr.ContentTypes.Add("audio/x-scpls");
			return dfr;
		}

		private int mvarVersion = 2;
		public int Version { get { return mvarVersion; } set { mvarVersion = value; } }

		
		public PLSDataFormat()
		{
			base.PropertyValuePrefix = String.Empty;
			base.PropertyValueSuffix = String.Empty;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new PropertyListObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			PropertyListObjectModel plom = objectModels.Pop() as PropertyListObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			if (plom.Groups["playlist"] != null && plom.Groups["playlist"].Properties["NumberOfEntries"] != null)
			{
				if (plom.Groups["playlist"].Properties["Version"] == null)
				{
					this.mvarVersion = int.Parse(plom.Groups["playlist"].Properties["Version"].Value.ToString());
				}
				int numberOfEntries = int.Parse(plom.Groups["playlist"].Properties["NumberOfEntries"].Value.ToString());
				for (int i = 1; i <= numberOfEntries; i++)
				{
					if (plom.Groups["playlist"].Properties["File" + i.ToString()] != null)
					{
						string FileName = plom.Groups["playlist"].Properties["File" + i.ToString()].Value.ToString();
						PlaylistEntry entry = new PlaylistEntry();
						entry.FileName = FileName;
						if (plom.Groups["playlist"].Properties["Title" + i.ToString()] != null)
						{
							entry.Title = plom.Groups["playlist"].Properties["Title" + i.ToString()].Value.ToString();
						}
						if (plom.Groups["playlist"].Properties["Length" + i.ToString()] != null)
						{
							entry.Length = long.Parse(plom.Groups["playlist"].Properties["Length" + i.ToString()].Value.ToString());
						}
						pom.Entries.Add(entry);
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			PropertyListObjectModel plom = new PropertyListObjectModel();
			Group grpPlaylist = new Group();
			grpPlaylist.Name = "playlist";
			grpPlaylist.Properties.Add("NumberOfEntries", pom.Entries.Count);
			grpPlaylist.Properties.Add("Version", mvarVersion);
			foreach (PlaylistEntry entry in pom.Entries)
			{
				int i = pom.Entries.IndexOf(entry) + 1;
				grpPlaylist.Properties.Add("File" + i.ToString(), entry.FileName);
				if (!String.IsNullOrEmpty(entry.Title))
				{
					grpPlaylist.Properties.Add("Title" + i.ToString(), entry.Title);
				}
				grpPlaylist.Properties.Add("Length" + i.ToString(), entry.Length);
			}
			plom.Groups.Add(grpPlaylist);
			objectModels.Push(plom);
		}
	}
}
