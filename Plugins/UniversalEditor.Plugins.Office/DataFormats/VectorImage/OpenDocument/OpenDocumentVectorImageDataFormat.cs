//
//  OpenDocumentVectorImageDataFormat.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
using MBS.Framework.Drawing;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.DataFormats.Package.OpenDocument;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Multimedia.VectorImage;
using UniversalEditor.ObjectModels.Package;

namespace UniversalEditor.Plugins.Office.DataFormats.VectorImage.OpenDocument
{
	public class OpenDocumentVectorImageDataFormat : OpenDocumentDataFormat
	{
		private static DataFormatReference _dfr;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(VectorImageObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			objectModels.Push(new PackageObjectModel());
			base.BeforeLoadInternal(objectModels);
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			PackageObjectModel package = (objectModels.Pop() as PackageObjectModel);
			if (package == null) throw new ObjectModelNotSupportedException();

			VectorImageObjectModel vec = (objectModels.Pop() as VectorImageObjectModel);
			if (vec == null) throw new ObjectModelNotSupportedException();

			XMLDataFormat xdf = new XMLDataFormat();
			MarkupObjectModel mom_content = new MarkupObjectModel();
			File file_content_xml = package.FileSystem.Files["content.xml"];
			if (file_content_xml == null)
				throw new InvalidDataFormatException("content.xml not found in root directory");

			Document.Load(mom_content, xdf, new EmbeddedFileAccessor(file_content_xml));

			MarkupTagElement tagContent = mom_content.FindElementUsingSchema(OpenDocumentXMLSchemas.Office, "document-content") as MarkupTagElement;
			if (tagContent == null)
				throw new InvalidDataFormatException("content.xml does not contain a top-level document-content tag");

			MarkupTagElement tagBody = tagContent.FindElementUsingSchema(OpenDocumentXMLSchemas.Office, "body") as MarkupTagElement;
			if (tagBody != null)
			{
				MarkupTagElement tagDrawing = tagBody.FindElementUsingSchema(OpenDocumentXMLSchemas.Office, "drawing") as MarkupTagElement;
				if (tagDrawing != null)
				{
					MarkupTagElement tagPage = tagDrawing.FindElementUsingSchema(OpenDocumentXMLSchemas.Draw, "page") as MarkupTagElement;
					if (tagPage != null)
					{
						MarkupAttribute attName = tagPage.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Draw, "name");
						if (attName != null)
						{

						}

						for (int i = 0; i < tagPage.Elements.Count; i++)
						{
							MarkupTagElement tag = tagPage.Elements[i] as MarkupTagElement;
							if (tag == null) continue;

							if (tag.Name == "line" && tag.XMLSchema == OpenDocumentXMLSchemas.Draw)
							{
								MarkupAttribute attX1 = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "x1");
								MarkupAttribute attY1 = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "y1");
								MarkupAttribute attX2 = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "x2");
								MarkupAttribute attY2 = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "y2");
								if (attX1 == null || attY1 == null || attX2 == null || attY2 == null)
								{
									continue;
								}

								UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems.LineVectorItem lvi = new UniversalEditor.ObjectModels.Multimedia.VectorImage.VectorItems.LineVectorItem();

								// if stroke color is null
								lvi.Style.BorderColor = Colors.Black; // Colors.CornflowerBlue;

								lvi.X1 = Measurement.Parse(attX1.Value);
								lvi.X2 = Measurement.Parse(attX2.Value);
								lvi.Y1 = Measurement.Parse(attY1.Value);
								lvi.Y2 = Measurement.Parse(attY2.Value);
								lvi.Bounds = new Rectangle(lvi.X1.GetValue(MeasurementUnit.Pixel), lvi.Y1.GetValue(MeasurementUnit.Pixel), lvi.X2.GetValue(MeasurementUnit.Pixel) - lvi.X1.GetValue(MeasurementUnit.Pixel), lvi.Y2.GetValue(MeasurementUnit.Pixel) - lvi.Y1.GetValue(MeasurementUnit.Pixel));
								vec.Items.Add(lvi);
							}
							else if (tag.Name == "custom-shape" && tag.XMLSchema == OpenDocumentXMLSchemas.Draw)
							{
								MarkupAttribute attX = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "x");
								MarkupAttribute attY = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "y");
								MarkupAttribute attWidth = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "width");
								MarkupAttribute attHeight = tag.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Svg, "height");

								MarkupTagElement tagEnhancedGeometry = tag.FindElementUsingSchema(OpenDocumentXMLSchemas.Draw, "enhanced-geometry") as MarkupTagElement;
								if (tagEnhancedGeometry != null)
								{
									MarkupAttribute attType = tagEnhancedGeometry.FindAttributeUsingSchema(OpenDocumentXMLSchemas.Draw, "type");
									if (attType != null)
									{

									}
								}
							}
						}
					}
				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);

			VectorImageObjectModel vec = (objectModels.Pop() as VectorImageObjectModel);
			if (vec == null) throw new ObjectModelNotSupportedException();

			PackageObjectModel package = new PackageObjectModel();
			objectModels.Push(package);
		}
	}
}
