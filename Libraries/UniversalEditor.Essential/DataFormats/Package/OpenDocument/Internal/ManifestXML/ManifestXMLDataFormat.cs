//
//  ManifestXMLDataFormat.cs
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
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.DataFormats.Package.OpenDocument.Internal.ManifestXML
{
	public class ManifestXMLDataFormat : XMLDataFormat
	{
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new MarkupObjectModel());

			base.BeforeLoadInternal(objectModels);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			MarkupObjectModel mom = (objectModels.Pop() as MarkupObjectModel);
			ManifestXMLObjectModel manifest = (objectModels.Pop() as ManifestXMLObjectModel);

			MarkupTagElement tagManifest = mom.FindElementUsingSchema(XMLSchemas.Manifest, "manifest") as MarkupTagElement;
			if (tagManifest == null)
				throw new InvalidDataFormatException("file does not contain top-level 'manifest' tag with schema 'manifest'");

			MarkupAttribute attManifestVersion = tagManifest.FindAttributeUsingSchema(XMLSchemas.Manifest, "version");
			// should be 1.3

			foreach (MarkupElement el in tagManifest.Elements)
			{
				MarkupTagElement tag = (el as MarkupTagElement);
				if (tag == null) continue;

				if (!(tag.XMLSchema == XMLSchemas.Manifest && tag.Name == "file-entry"))
					continue;

				MarkupAttribute attFullPath = tag.FindAttributeUsingSchema(XMLSchemas.Manifest, "full-path") as MarkupAttribute;
				MarkupAttribute attVersion = tag.FindAttributeUsingSchema(XMLSchemas.Manifest, "version") as MarkupAttribute;
				MarkupAttribute attMediaType = tag.FindAttributeUsingSchema(XMLSchemas.Manifest, "media-type") as MarkupAttribute;

				ManifestFileEntry entry = new ManifestFileEntry();
				if (attFullPath != null)
					entry.FullPath = attFullPath.Value;

				if (attVersion != null)
					entry.Version = attVersion.Value;

				if (attMediaType != null)
					entry.MediaType = attMediaType.Value;

				manifest.FileEntries.Add(entry);
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			// ma.Writer.WriteFixedLengthString("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<manifest:manifest xmlns:manifest=\"urn:oasis:names:tc:opendocument:xmlns:manifest:1.0\" manifest:version=\"1.2\" xmlns:loext=\"urn:org:documentfoundation:names:experimental:office:xmlns:loext:1.0\">\n <manifest:file-entry manifest:full-path=\"/\" manifest:version=\"1.2\" manifest:media-type=\"application/vnd.oasis.opendocument.presentation\"/>\n <manifest:file-entry manifest:full-path=\"Configurations2/\" manifest:media-type=\"application/vnd.sun.xml.ui.configuration\"/>\n <manifest:file-entry manifest:full-path=\"styles.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/100002010000042200000098867BFA9F3EB7C7AD.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/1000020100000422000000B2B43F3309F5F5EA0B.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"Pictures/1000020100000422000000A2277504F30A810755.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"content.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"settings.xml\" manifest:media-type=\"text/xml\"/>\n <manifest:file-entry manifest:full-path=\"Thumbnails/thumbnail.png\" manifest:media-type=\"image/png\"/>\n <manifest:file-entry manifest:full-path=\"meta.xml\" manifest:media-type=\"text/xml\"/>\n</manifest:manifest>");
			base.BeforeSaveInternal(objectModels);

			ManifestXMLObjectModel manifest = (objectModels.Pop() as ManifestXMLObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			objectModels.Push(mom);
		}
	}
}
