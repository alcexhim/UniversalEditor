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

			Group gPlaylist = plom.Items.OfType<Group>("playlist");
			if (gPlaylist == null) throw new InvalidDataFormatException();

			Property pNumEntrie = gPlaylist.Items.OfType<Property>("NumberOfEntries");
			if (pNumEntrie == null) throw new InvalidDataFormatException();

			Property pVersion = gPlaylist.Items.OfType<Property>("Version");
			if (pVersion != null)
				mvarVersion = int.Parse(pVersion.Value.ToString());

			int numberOfEntries = int.Parse(pNumEntrie.Value.ToString());
			for (int i = 1; i <= numberOfEntries; i++)
			{
				Property pFile = gPlaylist.Items.OfType<Property>("File" + i.ToString());
				if (pFile == null) continue;

				string FileName = pFile.Value.ToString();
				PlaylistEntry entry = new PlaylistEntry();
				entry.FileName = FileName;

				Property pTitle = gPlaylist.Items.OfType<Property>("Title" + i.ToString());
				if (pTitle != null)
				{
					entry.Title = pTitle.Value.ToString();
				}

				Property pLength = gPlaylist.Items.OfType<Property>("Length" + i.ToString());
				if (pLength != null)
				{
					entry.Length = long.Parse(pLength.Value.ToString());
				}
				pom.Entries.Add(entry);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			PropertyListObjectModel plom = new PropertyListObjectModel();
			Group grpPlaylist = new Group("playlist", new PropertyListItem[]
			{
				new Property("NumberOfEntries", pom.Entries.Count),
				new Property("Version", mvarVersion)
			});
			foreach (PlaylistEntry entry in pom.Entries)
			{
				int i = pom.Entries.IndexOf(entry) + 1;
				grpPlaylist.Items.AddProperty("File" + i.ToString(), entry.FileName);
				if (!String.IsNullOrEmpty(entry.Title))
				{
					grpPlaylist.Items.AddProperty("Title" + i.ToString(), entry.Title);
				}
				grpPlaylist.Items.AddProperty("Length" + i.ToString(), entry.Length);
			}
			plom.Items.Add(grpPlaylist);
			objectModels.Push(plom);
		}
	}
}
