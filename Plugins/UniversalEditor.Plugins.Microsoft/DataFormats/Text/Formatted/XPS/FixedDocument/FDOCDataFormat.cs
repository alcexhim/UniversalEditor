//
//  FDOCDataFormat.cs - provides a DataFormat to manipulate FixedDocument files in a Microsoft XML Paper Specification (XPS) document
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
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedDocument;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS.FixedDocument
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate FixedDocument files in a Microsoft XML Paper Specification (XPS) document.
	/// </summary>
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
