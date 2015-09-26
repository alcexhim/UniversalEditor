using System;
using System.Collections.Generic;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class XSPFDataFormat : XMLDataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Clear();
			dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override bool IsObjectModelSupported(ObjectModel objectModel)
		{
			MarkupObjectModel mom = (objectModel as MarkupObjectModel);
			if (mom == null) return false;

			return ((mom.Elements.Count > 0) && (mom.Elements[0].Name == "playlist"));
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
