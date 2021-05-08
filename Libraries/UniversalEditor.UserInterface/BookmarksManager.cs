using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;
using UniversalEditor;

namespace UniversalEditor.UserInterface
{
	public class BookmarksManager
	{
		public System.Collections.Specialized.StringCollection FileNames { get; } = new System.Collections.Specialized.StringCollection();
		public string DataFileName { get; set; } = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Mike Becker's Software",
			"Universal Editor",
			"Bookmarks.xml"
		});

		private Version mvarFormatVersion = new Version(1, 0);

		public void Load()
		{
			MarkupObjectModel mom = new MarkupObjectModel();
			XMLDataFormat xml = new XMLDataFormat();

			if (!System.IO.File.Exists(DataFileName)) return;

			Document.Load(mom, xml, new FileAccessor(DataFileName), true);

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

				FileNames.Add(attFileName.Value);
			}
		}
		public void Save()
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

			if (FileNames.Count > 0)
			{
				foreach (string fileName in FileNames)
				{
					MarkupTagElement tagBookmark = new MarkupTagElement();
					tagBookmark.FullName = "Bookmark";
					tagBookmark.Attributes.Add("FileName", fileName);
					tagBookmarks.Elements.Add(tagBookmark);
				}
			}

			string dir = System.IO.Path.GetDirectoryName(DataFileName);
			if (!System.IO.Directory.Exists (dir))
			{
				System.IO.Directory.CreateDirectory (dir);
			}

			Document.Save(mom, xml, new FileAccessor(DataFileName, true, true), true);
		}
	}
}
