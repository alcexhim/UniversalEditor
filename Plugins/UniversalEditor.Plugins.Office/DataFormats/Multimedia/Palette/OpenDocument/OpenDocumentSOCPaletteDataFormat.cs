//
//  OpenDocumentSOCPaletteDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
using System.Linq;
using MBS.Framework.Drawing;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Multimedia.Palette;

namespace UniversalEditor.DataFormats.Multimedia.Palette.OpenDocument
{
	public class OpenDocumentSOCPaletteDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();

				_dfr.Capabilities.Clear();
				_dfr.Capabilities.Add(typeof(PaletteObjectModel), DataFormatCapabilities.All);
				_dfr.Title = "OpenDocument / OpenOffice / LibreOffice color palette";
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new MarkupObjectModel());
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			PaletteObjectModel palette = (objectModels.Pop() as PaletteObjectModel);
			if (palette == null) throw new ObjectModelNotSupportedException();

			MarkupTagElement tagColorTable = mom.FindElementUsingSchema(OpenDocumentXMLSchemas.Office2004, "color-table") as MarkupTagElement;
			string drawSchema = OpenDocumentXMLSchemas.DrawUrn;
			if (tagColorTable == null)
			{
				tagColorTable = mom.FindElementUsingSchema(OpenDocumentXMLSchemas.Office2000, "color-table") as MarkupTagElement;
				drawSchema = OpenDocumentXMLSchemas.Draw2000;
			}

			if (tagColorTable == null)
				throw new InvalidDataFormatException("could not find color-table tag in OpenOffice 2000 or 2004 schema");

			foreach (MarkupTagElement tagColor in tagColorTable.Elements.OfType<MarkupTagElement>())
			{
				if (!(tagColor.XMLSchema == drawSchema && tagColor.Name == "color")) continue;

				MarkupAttribute attName = tagColor.FindAttributeUsingSchema(drawSchema, "name");
				MarkupAttribute attColor = tagColor.FindAttributeUsingSchema(drawSchema, "color");
				if (attColor == null) continue;

				PaletteEntry entry = new PaletteEntry();
				entry.Name = attName?.Value;
				entry.Color = Color.Parse(attColor.Value);
				palette.Entries.Add(entry);
			}
		}
	}
}
