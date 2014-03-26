using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;
namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class NotepadPlusPlaylistDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Filters.Add("Notepad++ session", new byte?[][] { new byte?[] { new byte?(60), new byte?(63), new byte?(120), new byte?(109), new byte?(108) } }, new string[] { "session.xml" });
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
