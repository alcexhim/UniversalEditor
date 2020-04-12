//
//  HTMLDataFormat.cs - provides a DataFormat to read and write formatted text documents as HTML files
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

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.Items;

namespace UniversalEditor.DataFormats.Text.HTML
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> to read and write formatted text documents as HTML files.
	/// </summary>
	public class HTMLDataFormat : XMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = new DataFormatReference(GetType());
				_dfr.Capabilities.Add(typeof(MarkupObjectModel), DataFormatCapabilities.Bootstrap);
				_dfr.Capabilities.Add(typeof(PlainTextObjectModel), DataFormatCapabilities.All);
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);

				_dfr.ExportOptions.Add(new CustomOptionText(nameof(Title), "&Title: "));
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
			
			MarkupObjectModel html = (objectModels.Pop() as MarkupObjectModel);
			ObjectModel objectModel = objectModels.Pop();

			if (objectModel is PlainTextObjectModel)
			{
				PlainTextObjectModel text = (objectModel as PlainTextObjectModel);

				MarkupTagElement tagHTML = (html.Elements["html"] as MarkupTagElement);
				if (tagHTML == null) throw new InvalidDataFormatException("Cannot find HTML tag");

				MarkupTagElement tagHEAD = (tagHTML.Elements["head"] as MarkupTagElement);
				if (tagHEAD != null)
				{
					MarkupTagElement tagTITLE = (tagHEAD.Elements["title"] as MarkupTagElement);
					if (tagTITLE != null) mvarTitle = tagTITLE.Value;
				}

				MarkupTagElement tagBODY = (tagHTML.Elements["body"] as MarkupTagElement);
				if (tagBODY != null)
				{
					text.Text = tagBODY.Value;
				}
			}
			else if (objectModel is FormattedTextObjectModel)
			{
				FormattedTextObjectModel text = (objectModel as FormattedTextObjectModel);

				MarkupTagElement tagHTML = (html.Elements["html"] as MarkupTagElement);
				if (tagHTML == null) throw new InvalidDataFormatException("Cannot find HTML tag");

				MarkupTagElement tagHEAD = (tagHTML.Elements["head"] as MarkupTagElement);
				if (tagHEAD != null)
				{
					MarkupTagElement tagTITLE = (tagHEAD.Elements["title"] as MarkupTagElement);
					if (tagTITLE != null) mvarTitle = tagTITLE.Value;
				}
				MarkupTagElement tagBODY = (tagHTML.Elements["body"] as MarkupTagElement);
				if (tagBODY != null)
				{

				}
			}
		}

		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			ObjectModel objectModel = objectModels.Pop();

			MarkupObjectModel html = new MarkupObjectModel();
			
			#region Html
			{
				MarkupTagElement tagHTML = new MarkupTagElement();
				tagHTML.FullName = "html";
				#region Head
				{
					MarkupTagElement tagHEAD = new MarkupTagElement();
					tagHEAD.FullName = "head";

					if (!String.IsNullOrEmpty(mvarTitle))
					{
						MarkupTagElement tagTITLE = new MarkupTagElement();
						tagTITLE.FullName = "title";
						tagTITLE.Value = mvarTitle;
						tagHEAD.Elements.Add(tagTITLE);
					}

					tagHTML.Elements.Add(tagHEAD);
				}
				#endregion
				#region Body
				{
					MarkupTagElement tagBODY = new MarkupTagElement();
					tagBODY.FullName = "body";

					if (objectModel is PlainTextObjectModel)
					{
						PlainTextObjectModel text = (objectModel as PlainTextObjectModel);
						tagBODY.Value = text.Text;
					}
					else if (objectModel is FormattedTextObjectModel)
					{
						FormattedTextObjectModel text = (objectModel as FormattedTextObjectModel);
						foreach (FormattedTextItem segment in text.Items)
						{
							MarkupElement el = RenderFormattedTextItemToHTML(segment);
							if (el != null)
							{
								tagBODY.Elements.Add(el);
							}
							else
							{
								Console.WriteLine("HTML: no implementation for segment '" + segment.GetType().Name + "'");
							}
						}
					}

					tagHTML.Elements.Add(tagBODY);
				}
				#endregion
				html.Elements.Add(tagHTML);
			}
			#endregion
			objectModels.Push(html);
		}

		private MarkupElement RenderFormattedTextItemToHTML(FormattedTextItem segment)
		{
			if (segment is FormattedTextItemLiteral)
			{
				FormattedTextItemLiteral item = (segment as FormattedTextItemLiteral);
				MarkupTagElement tagSpan = new MarkupTagElement();
				tagSpan.FullName = "span";
				tagSpan.Value = item.Text;
				return tagSpan;
			}
			return null;
		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }
	}
}
