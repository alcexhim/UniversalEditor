//
//  RecentFileManager.cs - provides a simple way of tracking recently-accessed files
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2021 Mike Becker's Software
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

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// Provides a simple way of tracking recently-accessed files, including
	/// appending new filenames to the list and retrieving the previously-stored
	/// filenames in the appropriate order.
	/// </summary>
	public class RecentFileManager
	{
		/// <summary>
		/// The backing store for the list of file names. This could be a S.C.G.
		/// List`1 or a Specialized StringCollection. However, changing this
		/// IMPLEMENTATION DETAIL should not change the public-facing API of
		/// <see cref="RecentFileManager" />.
		/// </summary>
		/// <value>Implementation detail.</value>
		private List<string> FileNames { get; } = new List<string>();

		/// <summary>
		/// Appends the given <paramref name="filename" /> to the end of the
		/// Recent Files list, if it does not already exist. The position of the
		/// existing file name in the list is unmodified.
		/// </summary>
		/// <returns><c>true</c>, if the file name was newly appended to the list;
		/// <c>false</c> if the file name already existed in the list.</returns>
		/// <param name="filename">The file name to append to the list.</param>
		public bool AppendFileName(string filename)
		{
			if (!FileNames.Contains(filename))
			{
				FileNames.Add(filename);
				return true;
			}
			return false;
		}

		/// <summary>
		/// Retrieves the list of Recent Files in the correct order (i.e., most
		/// recently-added file name first).
		/// </summary>
		/// <returns>The sorted list of recent file names.</returns>
		public string[] GetFileNames()
		{
			List<string> list = new List<string>(FileNames);
			list.Reverse(); // stored in reverse order
			return list.ToArray();
		}

		/// <summary>
		/// Gets or sets the maximum number of file names that can be stored in
		/// this <see cref="RecentFileManager" />. When a unique file name is
		/// added to the list with the <see cref="AppendFileName(string)" />
		/// method, the oldest file name in the list is discarded.
		/// </summary>
		/// <value>
		/// The maximum number of file names tracked by this
		/// <see cref="RecentFileManager" />.
		/// </value>
		public int MaximumDocumentFileNames { get; set; } = 5;

		/// <summary>
		/// Gets or sets the full path to the XML file where the
		/// <see cref="RecentFileManager"/> data is stored.
		/// </summary>
		/// <value>
		/// The full path to the XML file where the
		/// <see cref="RecentFileManager" /> data is stored.
		/// </value>
		public string DataFileName { get; set; } = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
		{
			Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
			"Mike Becker's Software",
			"Universal Editor",
			"RecentItems.xml"
		});

		/// <summary>
		/// Gets the <see cref="Version" /> of the XML serialization format used.
		/// </summary>
		private Version mvarFormatVersion = new Version(1, 0);

		/// <summary>
		/// Loads the Recent File data from the XML file specified by
		/// <see cref="DataFileName" />.
		/// </summary>
		public void Load()
		{
			MarkupObjectModel mom = new MarkupObjectModel();
			XMLDataFormat xml = new XMLDataFormat();

			if (!System.IO.File.Exists(DataFileName)) return;

			Document.Load(mom, xml, new FileAccessor(DataFileName, false, false, true));

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
				MarkupAttribute attDocumentsMaximum = tagDocuments.Attributes["Maximum"];
				if (attDocumentsMaximum != null)
				{
					MaximumDocumentFileNames = Int32.Parse(attDocumentsMaximum.Value);
				}
				foreach (MarkupElement elDocument in tagDocuments.Elements)
				{
					MarkupTagElement tagDocument = (elDocument as MarkupTagElement);
					if (tagDocument == null) continue;
					if (tagDocument.FullName != "Document") continue;

					MarkupAttribute attFileName = tagDocument.Attributes["FileName"];
					if (attFileName == null) continue;

					FileNames.Insert(0, attFileName.Value);
				}
			}
		}

		/// <summary>
		/// Saves the Recent File data to the XML file specified by
		/// <see cref="DataFileName" />.
		/// </summary>
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

			if (FileNames.Count > 0)
			{
				MarkupTagElement tagDocuments = new MarkupTagElement();
				tagDocuments.FullName = "Documents";
				tagDocuments.Attributes.Add("Maximum", MaximumDocumentFileNames.ToString());
				for (int i = 0; i < Math.Min(FileNames.Count, MaximumDocumentFileNames); i++)
				{
					MarkupTagElement tagDocument = new MarkupTagElement();
					tagDocument.FullName = "Document";
					tagDocument.Attributes.Add("FileName", FileNames[FileNames.Count - i - 1]);
					tagDocuments.Elements.Add(tagDocument);
				}
				tagRecentItems.Elements.Add(tagDocuments);
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
