using System;
using System.Collections.Generic;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocument;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocument
{
	public class FDOCDataFormat : XMLDataFormat
	{
		public FDOCDataFormat()
		{
		}
		public FDOCDataFormat(XPSSchemaVersion schemaVersion)
		{
			SchemaVersion = schemaVersion;
		}

		public XPSSchemaVersion SchemaVersion { get; set; } = XPSSchemaVersion.OpenXPS;

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			FixedDocumentObjectModel fdoc = (objectModels.Pop() as FixedDocumentObjectModel);

			MarkupTagElement tagFixedDocument = (mom.Elements["FixedDocument"] as MarkupTagElement);
			if (tagFixedDocument != null)
			{
				foreach (MarkupElement el in tagFixedDocument.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;
					if (tag.Name != "PageContent") continue;

					MarkupAttribute attSource = tag.Attributes["Source"];
					if (attSource == null) continue;

					PageContent pc = new PageContent(attSource.Value);
					fdoc.PageContents.Add(pc);
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			FixedDocumentObjectModel fdoc = (objectModels.Pop() as FixedDocumentObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagFixedDocument = new MarkupTagElement();
			tagFixedDocument.FullName = "FixedDocument";
			tagFixedDocument.Attributes.Add("xmlns", XPSSchemas.GetSchema(SchemaVersion, XPSSchemaType.FixedDocument));

			foreach (PageContent pc in fdoc.PageContents)
			{
				MarkupTagElement tag = new MarkupTagElement();
				tag.FullName = "PageContent";
				tag.Attributes.Add("Source", pc.Source);
				tagFixedDocument.Elements.Add(tag);
			}

			mom.Elements.Add(tagFixedDocument);

			objectModels.Push(mom);
			base.BeforeSaveInternal(objectModels);
		}
	}
}
