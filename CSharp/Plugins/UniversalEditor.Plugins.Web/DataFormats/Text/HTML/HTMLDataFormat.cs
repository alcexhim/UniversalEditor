using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;

using UniversalEditor.ObjectModels.Text.Plain;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.Items;

namespace UniversalEditor.DataFormats.Text.HTML
{
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
				_dfr.Filters.Add("HyperText Markup Language", new string[] { "*.htm", "*.html" });

				_dfr.ExportOptions.Add(new CustomOptionText("Title", "&Title: "));
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
			if (segment is Literal)
			{
				Literal item = (segment as Literal);
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
