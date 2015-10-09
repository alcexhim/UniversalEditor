using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Help.TableOfContents;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Help.TableOfContents.V3
{
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
