//
//  NotepadPlusPlaylistDataFormat.cs - provides a DataFormat for manipulating Notepad++ session files
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

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating Notepad++ session files.
	/// </summary>
	public class NotepadPlusPlaylistDataFormat : XMLDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override bool IsObjectModelSupported(ObjectModel omb)
		{
			MarkupObjectModel mom = (omb as MarkupObjectModel);
			if (mom == null) return false;
			return (mom.Elements.Count > 0 && mom.Elements[0].FullName == "NotepadPlus");
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);
			MarkupObjectModel mom = objectModels.Pop() as MarkupObjectModel;
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			MarkupTagElement tagMainView = mom.FindElement("NotepadPlus", "Session", "mainView") as MarkupTagElement;
			if (tagMainView != null)
			{
				foreach (MarkupElement el in tagMainView.Elements)
				{
					MarkupTagElement tag = el as MarkupTagElement;
					if (tag != null)
					{
						if (tag.Name == "File")
						{
							if (tag.Attributes["filename"] != null)
							{
								string fileName = tag.Attributes["filename"].Value;
								PlaylistEntry entry = new PlaylistEntry();
								entry.FileName = fileName;
								pom.Entries.Add(entry);
							}
						}
					}
				}
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			PlaylistObjectModel pom = objectModels.Pop() as PlaylistObjectModel;
			MarkupObjectModel mom = new MarkupObjectModel();
			MarkupTagElement tagNotepadPlus = new MarkupTagElement();
			tagNotepadPlus.Name = "NotepadPlus";
			MarkupTagElement tagSession = new MarkupTagElement();
			tagSession.Name = "Session";
			tagSession.Attributes.Add("activeView", "0");
			MarkupTagElement tagMainView = new MarkupTagElement();
			tagMainView.Name = "mainView";
			tagMainView.Attributes.Add("activeIndex", "0");
			foreach (PlaylistEntry entry in pom.Entries)
			{
				MarkupTagElement tagFile = new MarkupTagElement();
				tagFile.Name = "File";
				tagFile.Attributes.Add("firstVisibleLine", "4");
				tagFile.Attributes.Add("xOffset", "0");
				tagFile.Attributes.Add("scrollWidth", "886935");
				tagFile.Attributes.Add("startPos", "440");
				tagFile.Attributes.Add("endPos", "440");
				tagFile.Attributes.Add("selMode", "0");
				tagFile.Attributes.Add("lang", "Normal Text");
				tagFile.Attributes.Add("encoding", "-1");
				tagFile.Attributes.Add("filename", entry.FileName);
				tagMainView.Elements.Add(tagFile);
			}
			tagSession.Elements.Add(tagMainView);
			MarkupTagElement tagSubView = new MarkupTagElement();
			tagSubView.Name = "subView";
			tagSubView.Attributes.Add("activeIndex", "0");
			tagSession.Elements.Add(tagSubView);
			tagNotepadPlus.Elements.Add(tagSession);
			mom.Elements.Add(tagNotepadPlus);
			objectModels.Push(mom);
		}
	}
}
