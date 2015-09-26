using System;
using System.Collections.Generic;
using UniversalEditor.Common;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Plugins.Multimedia.ObjectModels.Playlist;
using UniversalEditor.ObjectModels.Markup;
namespace UniversalEditor.Plugins.Multimedia.DataFormats.Playlist
{
	public class XSPFDataFormat : XMLDataFormat
	{
		public override DataFormatReference MakeReference()
		{
			DataFormatReference dfr = base.MakeReference();
			dfr.Clear();
			dfr.Filters.Add("XML Sharable Playlist File", new string[] { "*.xspf" });
			dfr.Filters[0].HintComparison = DataFormatHintComparison.Inquire;
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		public override bool IsBootstrappable(ObjectModel omb)
		{
			MarkupObjectModel mom = omb as MarkupObjectModel;
			return ((mom != null) && ((mom.Elements.Count > 0) && (mom.Elements[0].Name == "playlist")));
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
			MarkupTagElement tagPlaylist = mom.FindElement("playlist") as MarkupTagElement;
			if (tagPlaylist != null)
			{
				if (tagPlaylist.Elements["title"] != null)
				{
					pom.Title = tagPlaylist.Elements["title"].Value;
				}
				MarkupTagElement tagTrackList = tagPlaylist.Elements["trackList"] as MarkupTagElement;
				if (tagTrackList != null)
				{
					foreach (MarkupElement el in tagTrackList.Elements)
					{
						MarkupTagElement tag = el as MarkupTagElement;
						if (tag != null)
						{
							if (tag.Name == "track")
							{
								PlaylistEntry entry = new PlaylistEntry();
								if (tag.Elements["location"] != null)
								{
									string fileName = tag.Elements["location"].Value;
									if (fileName.StartsWith("file://"))
									{
										fileName = fileName.Substring(8);
										fileName =  fileName.UrlDecode();
										if (fileName.Length > 1 && fileName[1] == ':')
										{
											fileName = fileName.Replace('/', System.IO.Path.DirectorySeparatorChar);
										}
										else
										{
											fileName = "/" + fileName;
										}
									}
									if (fileName.StartsWith("//"))
									{
										fileName = fileName.Substring(1);
									}
									entry.FileName = fileName;
								}
								if (tag.Elements["title"] != null)
								{
									entry.Title = tag.Elements["title"].Value;
								}
								if (tag.Elements["creator"] != null)
								{
									entry.Author = tag.Elements["creator"].Value;
								}
								if (tag.Elements["album"] != null)
								{
									entry.Album.Title = tag.Elements["album"].Value;
								}
								if (tag.Elements["trackNum"] != null)
								{
									entry.TrackNumber = int.Parse(tag.Elements["trackNum"].Value);
								}
								if (tag.Elements["image"] != null)
								{
									entry.AlbumArtImageURL = tag.Elements["image"].Value;
								}
								if (tag.Elements["duration"] != null)
								{
									entry.Length = long.Parse(tag.Elements["duration"].Value);
								}
								pom.Entries.Add(entry);
							}
						}
					}
				}
			}
		}
	}
}
