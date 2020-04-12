//
//  PLSDataFormat.cs - provides a DataFormat for manipulating playlists in PLS format
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
using System.Collections.Generic;
using UniversalEditor.DataFormats.PropertyList;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating playlists in PLS format.
	/// </summary>
	public class PLSDataFormat : WindowsConfigurationDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = new DataFormatReference(GetType());
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			dfr.ContentTypes.Add("audio/x-scpls");
			return dfr;
		}

		private int mvarVersion = 2;
		public int Version { get { return mvarVersion; } set { mvarVersion = value; } }


		public PLSDataFormat()
		{
			PropertyValuePrefix = String.Empty;
			PropertyValueSuffix = String.Empty;
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
