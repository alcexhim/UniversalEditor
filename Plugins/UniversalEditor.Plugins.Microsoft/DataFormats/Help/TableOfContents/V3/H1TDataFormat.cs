//
//  H1TDataFormat.cs - provides a DataFormat to manipulate Microsoft Help 1.x table of contents files
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
using UniversalEditor.ObjectModels.Help.TableOfContents;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Help.TableOfContents.V3
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate Microsoft Help 1.x table of contents files.
	/// </summary>
	public class H1TDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(TableOfContentsObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);

			TableOfContentsObjectModel toc = (objectModels.Pop() as TableOfContentsObjectModel);
			if (toc == null) throw new ObjectModelNotSupportedException();

			MarkupTagElement tagHelpTOC = (mom.Elements["HelpTOC"] as MarkupTagElement);
			if (tagHelpTOC == null) throw new InvalidDataFormatException("File does not begin with top-level 'HelpTOC' element");

			foreach (MarkupElement elNode in tagHelpTOC.Elements)
			{
				MarkupTagElement tagNode = (elNode as MarkupTagElement);
				if (tagNode == null) continue;
				if (tagNode.FullName != "HelpTOCNode") continue;

				LoadHelpTOCNode(tagNode, toc.Nodes);
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			TableOfContentsObjectModel toc = (objectModels.Pop() as TableOfContentsObjectModel);
			if (toc == null) throw new ObjectModelNotSupportedException();

			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagHelpTOC = new MarkupTagElement();
			tagHelpTOC.FullName = "HelpTOC";

			foreach (TOCNode node in toc.Nodes)
			{
				SaveHelpTOCNode(node, tagHelpTOC);
			}

			mom.Elements.Add(tagHelpTOC);

			objectModels.Push(mom);
		}

		private void LoadHelpTOCNode(MarkupTagElement tag, TOCNode.TOCNodeCollection coll)
		{
			TOCNode node = new TOCNode();

			MarkupAttribute attUrl = tag.Attributes["Url"];
			if (attUrl != null) node.Location = attUrl.Value;

			MarkupAttribute attTitle = tag.Attributes["Title"];
			if (attTitle != null) node.Location = attTitle.Value;

			foreach (MarkupElement el1 in tag.Elements)
			{
				MarkupTagElement tag1 = (el1 as MarkupTagElement);
				if (tag1 == null) continue;
				if (tag1.FullName != "HelpTOCNode") continue;

				LoadHelpTOCNode(tag1, node.Nodes);
			}

			coll.Add(node);
		}

		private void SaveHelpTOCNode(TOCNode node, MarkupTagElement tag)
		{
			MarkupTagElement tag1 = new MarkupTagElement();
			tag1.FullName = "HelpTOCNode";
			tag1.Attributes.Add("Url", node.Location);
			tag1.Attributes.Add("Title", node.Title);

			foreach (TOCNode node1 in node.Nodes)
			{
				SaveHelpTOCNode(node1, tag1);
			}

			tag.Elements.Add(tag1);
		}
	}
}
