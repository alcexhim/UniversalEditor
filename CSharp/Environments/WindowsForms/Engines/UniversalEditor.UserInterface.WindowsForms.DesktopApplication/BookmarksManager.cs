using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;
using UniversalEditor;

internal static class BookmarksManager
{
	private static System.Collections.Specialized.StringCollection mvarFileNames = new System.Collections.Specialized.StringCollection();
	public static System.Collections.Specialized.StringCollection FileNames { get { return mvarFileNames; } }

	private static string mvarDataFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
	{
		Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
		"Mike Becker's Software",
		"Universal Editor",
		"Bookmarks.xml"
	});
	public static string DataFileName { get { return mvarDataFileName; } set { mvarDataFileName = value; } }

	private static Version mvarFormatVersion = new Version(1, 0);

	public static void Load()
	{
		MarkupObjectModel mom = new MarkupObjectModel();
		XMLDataFormat xml = new XMLDataFormat();

		if (!System.IO.File.Exists(mvarDataFileName)) return;

		Document.Load(mom, xml, new FileAccessor(mvarDataFileName), true);

		MarkupTagElement tagBookmarks = (mom.Elements["Bookmarks"] as MarkupTagElement);
		if (tagBookmarks == null) return;

		MarkupAttribute attVersion = tagBookmarks.Attributes["Version"];
		if (attVersion != null)
		{
			mvarFormatVersion = new Version(attVersion.Value);
		}

		foreach (MarkupElement elDocument in tagBookmarks.Elements)
		{
			MarkupTagElement tagBookmark = (elDocument as MarkupTagElement);
			if (tagBookmark == null) continue;
			if (tagBookmark.FullName != "Bookmark") continue;

			MarkupAttribute attFileName = tagBookmark.Attributes["FileName"];
			if (attFileName == null) continue;

			mvarFileNames.Add(attFileName.Value);
		}
	}
	public static void Save()
	{
		MarkupObjectModel mom = new MarkupObjectModel();
		XMLDataFormat xml = new XMLDataFormat();

		MarkupPreprocessorElement xmlp = new MarkupPreprocessorElement();
		xmlp.FullName = "xml";
		xmlp.Value = "version=\"1.0\" encoding=\"UTF-8\"";
		mom.Elements.Add(xmlp);

		MarkupTagElement tagBookmarks = new MarkupTagElement();
		tagBookmarks.FullName = "Bookmarks";
		tagBookmarks.Attributes.Add("Version", mvarFormatVersion.ToString());

		mom.Elements.Add(tagBookmarks);

		if (mvarFileNames.Count > 0)
		{
			foreach (string fileName in mvarFileNames)
			{
				MarkupTagElement tagBookmark = new MarkupTagElement();
				tagBookmark.FullName = "Bookmark";
				tagBookmark.Attributes.Add("FileName", fileName);
				tagBookmarks.Elements.Add(tagBookmark);
			}
		}

		Document.Save(mom, xml, new FileAccessor(mvarDataFileName, true, true), true);
	}
}