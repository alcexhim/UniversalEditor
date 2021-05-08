//
//  FPAGEDataFormat.cs - provides a DataFormat to manipulate FixedPage files in a Microsoft XML Paper Specification (XPS) document
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

using System;
using System.Collections.Generic;
using System.Text;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.Text.Formatted.XPS.FixedPage;

namespace UniversalEditor.DataFormats.Text.Formatted.XPS.FixedPage
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to manipulate FixedPage files in a Microsoft XML Paper Specification (XPS) document.
	/// </summary>
	public class FPAGEDataFormat : XMLDataFormat
	{
		public XPSGenerator Generator { get; set; } = new XPSGenerator();
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
			FixedPageObjectModel fpage = (objectModels.Pop() as FixedPageObjectModel);

			MarkupTagElement tagFixedPage = (mom.Elements["FixedPage"] as MarkupTagElement);
			if (tagFixedPage != null)
			{
				MarkupAttribute attWidth = tagFixedPage.Attributes["Width"];
				MarkupAttribute attHeight = tagFixedPage.Attributes["Height"];

				foreach (MarkupElement el in tagFixedPage.Elements)
				{
					MarkupTagElement tag = (el as MarkupTagElement);
					if (tag == null) continue;

					switch (tag.Name)
					{
						case "Glyphs":
						{
							break;
						}
						case "Path":
						{
							break;
						}
					}

				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			FixedPageObjectModel fpage = (objectModels.Pop() as FixedPageObjectModel);
			MarkupObjectModel mom = new MarkupObjectModel();

			MarkupTagElement tagFixedPage = new MarkupTagElement();
			tagFixedPage.FullName = "FixedPage";
			tagFixedPage.Attributes.Add("xmlns", XPSSchemas.GetSchema(SchemaVersion, XPSSchemaType.FixedPage));

			if (!String.IsNullOrEmpty(Generator.Name))
				tagFixedPage.Elements.Add(new MarkupCommentElement(Generator.Name + " Generated! Version: " + Generator.Version.ToString()));

			// TODO: glyphs, paths, etc.
			foreach (FixedPageItem item in fpage.Items)
			{
				if (item is Glyph)
				{
					Glyph glyph = (item as Glyph);
					MarkupTagElement tagGlyph = new MarkupTagElement();
					tagGlyph.FullName = "Glyphs";
					tagGlyph.Attributes.Add("Fill", glyph.FillColor.ToHexadecimalHTML());
					// tagGlyph.Attributes.Add("FontUri", glyph.FontURI);
					tagGlyph.Attributes.Add("FontRenderingEmSize", glyph.FontRenderingEmSize.ToString());
					tagGlyph.Attributes.Add("Indices", GetIndices(glyph.Text));
					tagGlyph.Attributes.Add("UnicodeString", glyph.Text);
					tagFixedPage.Elements.Add(tagGlyph);
				}
				else if (item is Path)
				{
					MarkupTagElement tagPath = new MarkupTagElement();
					tagPath.FullName = "Path";
					tagFixedPage.Elements.Add(tagPath);
				}
			}

			mom.Elements.Add(tagFixedPage);

			objectModels.Push(mom);
			base.BeforeSaveInternal(objectModels);
		}

		public static Dictionary<char, int> _indices = new Dictionary<char, int>();
		static FPAGEDataFormat()
		{
			_indices.Add(' ', 3);
			for (int i = 0; i < 26; i++)
			{
				_indices.Add((char)((int)'A' + i), (int)(((int)'A' + i) + 35));
			}
			for (int i = 0; i < 26; i++)
			{
				_indices.Add((char)((int)'a' + i), (int)(((int)'a' + i) + 68));
			}
		}

		/// <summary>
		///  returns the indices associated with the given string
		/// </summary>
		/// <returns>The indices.</returns>
		/// <param name="value">Value.</param>
		public string GetIndices(string value)
		{
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				sb.Append(_indices[value[i]].ToString());
				if (i < value.Length - 1)
				{
					sb.Append(';');
				}
			}
			return sb.ToString();
		}
	}
}
