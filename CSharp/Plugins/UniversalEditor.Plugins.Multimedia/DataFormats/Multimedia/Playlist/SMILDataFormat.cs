using System;
using System.Collections.Generic;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Multimedia.Playlist;

namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class SMILDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();
			dfr.Filters.Add("SMIL/Windows Media/Zune playlist", new byte?[][] { new byte?[] { new byte?(60), new byte?(63), new byte?(119), new byte?(112), new byte?(108) }, new byte?[] { new byte?(60), new byte?(63), new byte?(122), new byte?(112), new byte?(108) }, new byte?[] { new byte?(60), new byte?(63), new byte?(120), new byte?(109), new byte?(108) } }, new string[] { "*.wpl", "*.zpl", "*.smil" });
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			dfr.ContentTypes.AddRange("application/smil+xml", "application/vnd.ms-wpl");
			return dfr;
		}
		protected override bool IsObjectModelSupported(ObjectModel objectModel)
		{
			MarkupObjectModel mom = objectModel as MarkupObjectModel;
			return mom.Elements.Count > 0 && mom.Elements[0].FullName == "smil";
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
			MarkupTagElement head = (mom.FindElement("smil", "head") as MarkupTagElement);
			if (head != null)
			{
				foreach (MarkupElement headEl in head.Elements)
				{
					if (headEl is MarkupTagElement)
					{
						MarkupTagElement tagEl = headEl as MarkupTagElement;
						string name2 = tagEl.Name;
						if (name2 != null)
						{
							if (!(name2 == "meta"))
							{
								if (!(name2 == "author"))
								{
									if (name2 == "title")
									{
										pom.Title = tagEl.Value;
									}
								}
								else
								{
									pom.Author = tagEl.Value;
								}
							}
							else
							{
								if (tagEl.Attributes["name"] != null)
								{
									string name = tagEl.Attributes["name"].Value;
									string value = string.Empty;
									if (tagEl.Attributes["content"] != null)
									{
										value = tagEl.Attributes["content"].Value;
									}
									pom.CustomInformation.Add(name, value);
								}
							}
						}
					}
				}
			}
			MarkupTagElement seq = (mom.FindElement("smil", "body", "seq") as MarkupTagElement);
			if (seq != null)
			{
				foreach (MarkupElement el in seq.Elements)
				{
					if (el is MarkupTagElement)
					{
						MarkupTagElement tag = el as MarkupTagElement;
						if (tag.Name == "media" && tag.Attributes["src"] != null)
						{
							PlaylistEntry entry = new PlaylistEntry();
							entry.FileName = tag.Attributes["src"].Value;
							if (tag.Attributes["albumTitle"] != null)
							{
								entry.Album.Title = tag.Attributes["albumTitle"].Value;
							}
							if (tag.Attributes["albumArtist"] != null)
							{
								entry.Album.Artist = tag.Attributes["albumArtist"].Value;
							}
							if (tag.Attributes["trackTitle"] != null)
							{
								entry.Title = tag.Attributes["trackTitle"].Value;
							}
							if (tag.Attributes["trackArtist"] != null)
							{
								entry.Author = tag.Attributes["trackArtist"].Value;
							}
							if (tag.Attributes["duration"] != null)
							{
								entry.Length = long.Parse(tag.Attributes["duration"].Value);
							}
							else
							{
								entry.Length = -1L;
							}
							pom.Entries.Add(entry);
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
			MarkupTagElement smil = new MarkupTagElement();
			smil.Name = "smil";
			MarkupTagElement head = new MarkupTagElement();
			head.Name = "head";
			MarkupTagElement guid = new MarkupTagElement();
			guid.Name = "guid";
			guid.Value = "{" + pom.ID.ToString().ToUpper() + "}";
			head.Elements.Add(guid);
			if (!string.IsNullOrEmpty(pom.Title))
			{
				MarkupTagElement tagPlaylistTitle = new MarkupTagElement();
				tagPlaylistTitle.Name = "title";
				tagPlaylistTitle.Value = pom.Title;
				head.Elements.Add(tagPlaylistTitle);
			}
			if (!string.IsNullOrEmpty(pom.Author))
			{
				MarkupTagElement tagPlaylistAuthor = new MarkupTagElement();
				tagPlaylistAuthor.Name = "author";
				tagPlaylistAuthor.Value = pom.Author;
				head.Elements.Add(tagPlaylistAuthor);
			}
			foreach (KeyValuePair<string, string> kvp in pom.CustomInformation)
			{
				MarkupTagElement tagMeta = new MarkupTagElement();
				tagMeta.Name = "meta";
				tagMeta.Attributes.Add("name", kvp.Key);
				tagMeta.Attributes.Add("content", kvp.Value);
				head.Elements.Add(tagMeta);
			}
			smil.Elements.Add(head);
			MarkupTagElement body = new MarkupTagElement();
			body.Name = "body";
			MarkupTagElement seq = new MarkupTagElement();
			seq.Name = "seq";
			foreach (PlaylistEntry entry in pom.Entries)
			{
				MarkupTagElement media = new MarkupTagElement();
				media.Name = "media";
				media.Attributes.Add("src", entry.FileName);
				if (!string.IsNullOrEmpty(entry.Album.Title))
				{
					media.Attributes.Add("albumTitle", entry.Album.Title);
				}
				if (!string.IsNullOrEmpty(entry.Album.Artist))
				{
					media.Attributes.Add("albumArtist", entry.Album.Artist);
				}
				if (!string.IsNullOrEmpty(entry.Title))
				{
					media.Attributes.Add("trackTitle", entry.Title);
				}
				if (!string.IsNullOrEmpty(entry.Author))
				{
					media.Attributes.Add("trackArtist", entry.Author);
				}
				if (entry.Length > -1L)
				{
					media.Attributes.Add("duration", entry.Length.ToString());
				}
				seq.Elements.Add(media);
			}
			body.Elements.Add(seq);
			smil.Elements.Add(body);
			mom.Elements.Add(smil);
			objectModels.Push(mom);
		}
	}
}
