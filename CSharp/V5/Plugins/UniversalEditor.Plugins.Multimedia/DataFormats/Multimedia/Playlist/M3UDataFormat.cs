using System;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia.Playlist;
namespace UniversalEditor.DataFormats.Multimedia.Playlist
{
	public class M3UDataFormat : DataFormat
	{
		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(PlaylistObjectModel), DataFormatCapabilities.All);
			return dfr;
		}
		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			Reader tr = base.Accessor.Reader;
			if (tr.EndOfStream || tr.ReadLine() != "#EXTM3U")
			{
				throw new InvalidDataFormatException("File does not begin with \"#EXTM3U\"");
			}
			PlaylistObjectModel objm = objectModel as PlaylistObjectModel;
			PlaylistEntry nextEntry = null;
			while (!tr.EndOfStream)
			{
				string line = tr.ReadLine();
				if (!string.IsNullOrEmpty(line))
				{
					if (line.StartsWith("#"))
					{
						if (nextEntry == null)
						{
							nextEntry = new PlaylistEntry();
						}
						string[] tagNameAndValue = line.Split(new char[]
						{
							':'
						}, 2, StringSplitOptions.None);
						string tagName = tagNameAndValue[0];
						string tagValue = string.Empty;
						if (tagNameAndValue.Length > 1)
						{
							tagValue = tagNameAndValue[1];
						}
						string[] tagPropTypeAndValue = tagValue.Split(new char[]
						{
							','
						}, 2, StringSplitOptions.None);
						string tagPropType = tagPropTypeAndValue[0];
						string tagPropValue = string.Empty;
						if (tagPropTypeAndValue.Length > 1)
						{
							tagPropValue = tagPropTypeAndValue[1];
						}
						string text = tagName;
						if (text != null)
						{
							if (text == "EXTINF")
							{
								text = tagPropType;
								if (text != null)
								{
									if (text == "189")
									{
										nextEntry.Title = tagPropValue;
									}
								}
							}
						}
					}
					else
					{
						if (nextEntry == null)
						{
							nextEntry = new PlaylistEntry();
						}
						nextEntry.FileName = line;
						objm.Entries.Add(nextEntry);
						nextEntry = null;
					}
				}
			}
			if (nextEntry != null)
			{
				objm.Entries.Add(nextEntry);
				nextEntry = null;
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer tw = base.Accessor.Writer;
			tw.WriteLine("#EXTM3U");
			PlaylistObjectModel objm = (objectModel as PlaylistObjectModel);
			foreach (PlaylistEntry entry in objm.Entries)
			{
				if (!string.IsNullOrEmpty(entry.Title))
				{
					tw.WriteLine("#EXTINF:189," + entry.Title);
				}
				tw.WriteLine(entry.FileName);
			}
			tw.Flush();
		}
	}
}
