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
	public class RecentFileManager
	{
		private System.Collections.Specialized.StringCollection mvarFileNames = new System.Collections.Specialized.StringCollection();
		public System.Collections.Specialized.StringCollection FileNames { get { return mvarFileNames; } }

		private int mvarMaximumDocumentFileNames = 5;
		public int MaximumDocumentFileNames { get { return mvarMaximumDocumentFileNames; } set { mvarMaximumDocumentFileNames = value; } }

		private string mvarDataFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Mike Becker's Software",
			"Universal Editor",
			"RecentItems.xml"
		});
		public string DataFileName { get { return mvarDataFileName; } set { mvarDataFileName = value; } }

		private Version mvarFormatVersion = new Version(1, 0);

		public void Load()
		{
			MarkupObjectModel mom = new MarkupObjectModel();
			XMLDataFormat xml = new XMLDataFormat();

			if (!System.IO.File.Exists(mvarDataFileName)) return;

			Document doc = new Document(mom, xml, new FileAccessor(mvarDataFileName));

			MarkupTagElement tagRecentItems = (mom.Elements["RecentItems"] as MarkupTagElement);
			if (tagRecentItems == null) return;

			MarkupAttribute attVersion = tagRecentItems.Attributes["Version"];
			if (attVersion != null)
			{
				mvarFormatVersion = new Version(attVersion.Value);
			}

			MarkupTagElement tagSolutions = (tagRecentItems.Elements["Solutions"] as MarkupTagElement);

			MarkupTagElement tagDocuments = (tagRecentItems.Elements["Documents"] as MarkupTagElement);
			if (tagDocuments != null)
			{
				foreach (MarkupElement elDocument in tagDocuments.Elements)
				{
					MarkupTagElement tagDocument = (elDocument as MarkupTagElement);
					if (tagDocument == null) continue;
					if (tagDocument.FullName != "Document") continue;

					MarkupAttribute attFileName = tagDocument.Attributes["FileName"];
					if (attFileName == null) continue;

					mvarFileNames.Add(attFileName.Value);
				}
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

			MarkupTagElement tagRecentItems = new MarkupTagElement();
			tagRecentItems.FullName = "RecentItems";
			tagRecentItems.Attributes.Add("Version", mvarFormatVersion.ToString());

			mom.Elements.Add(tagRecentItems);

			if (mvarFileNames.Count > 0)
			{
				MarkupTagElement tagDocuments = new MarkupTagElement();
				tagDocuments.FullName = "Documents";
				tagDocuments.Attributes.Add("Maximum", mvarMaximumDocumentFileNames.ToString());
				foreach (string fileName in mvarFileNames)
				{
					MarkupTagElement tagDocument = new MarkupTagElement();
					tagDocument.FullName = "Document";
					tagDocument.Attributes.Add("FileName", fileName);
					tagDocuments.Elements.Add(tagDocument);
				}
				tagRecentItems.Elements.Add(tagDocuments);
			}

			Document.Save(mom, xml, new FileAccessor(mvarDataFileName, true, true), true);
		}
	}
}