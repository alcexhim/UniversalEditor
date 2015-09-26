using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.Items;

using UniversalEditor.ObjectModels.RichTextMarkup;
using UniversalEditor.DataFormats.RichTextMarkup.RTML;

namespace UniversalEditor.DataFormats.Text.Formatted.RichText
{
	public class RTFDataFormat : RTMLDataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(FormattedTextObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		private RTFCharacterSet mvarCharacterSet = RTFCharacterSet.ANSI;
		/// <summary>
		/// The character set used in this document.
		/// </summary>
		public RTFCharacterSet CharacterSet { get { return mvarCharacterSet; } set { mvarCharacterSet = value; } }

		private int mvarCodePage = 1252;
		/// <summary>
		/// The ANSI code page which is used to perform the Unicode to ANSI conversion when writing
		/// RTF text.
		/// </summary>
		public int CodePage { get { return mvarCodePage; } set { mvarCodePage = value; } }

		private string mvarGenerator = String.Empty;
		/// <summary>
		/// The application which generated this RTF document. See <see cref="RTFGenerator" />
		/// for known values.
		/// </summary>
		public string Generator { get { return mvarGenerator; } set { mvarGenerator = value; } }

		private void LoadItem(RichTextMarkupItem item, IFormattedTextItemParent ftom)
		{
			if (item is RichTextMarkupItemLiteral)
			{
				RichTextMarkupItemLiteral itm = (item as RichTextMarkupItemLiteral);
				FormattedTextItemLiteral literal = new FormattedTextItemLiteral(itm.Content);
				ftom.Items.Add(literal);
			}
		}

		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new RichTextMarkupObjectModel());
		}
		protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.AfterLoadInternal(objectModels);

			RichTextMarkupObjectModel mom = (objectModels.Pop() as RichTextMarkupObjectModel);
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);

			foreach (RichTextMarkupItem item in mom.Items)
			{
				LoadItem(item, ftom);
			}
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeSaveInternal(objectModels);
			FormattedTextObjectModel ftom = (objectModels.Pop() as FormattedTextObjectModel);
			RichTextMarkupObjectModel rtml = new RichTextMarkupObjectModel();

			RichTextMarkupItemGroup grpRTF1 = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("rtf1"));
			switch (mvarCharacterSet)
			{
				case RTFCharacterSet.ANSI:
				{
					grpRTF1.Items.Add(new RichTextMarkupItemTag("ansi"));
					break;
				}
				case RTFCharacterSet.AppleMacintosh:
				{
					grpRTF1.Items.Add(new RichTextMarkupItemTag("mac"));
					break;
				}
				case RTFCharacterSet.IBMPC437:
				{
					grpRTF1.Items.Add(new RichTextMarkupItemTag("pc"));
					break;
				}
				case RTFCharacterSet.IBMPC850:
				{
					grpRTF1.Items.Add(new RichTextMarkupItemTag("pca"));
					break;
				}
			}
			grpRTF1.Items.Add(new RichTextMarkupItemTag("ansicpg" + mvarCodePage.ToString()));

			if (ftom.DefaultFont != null && ftom.Fonts.Contains(ftom.DefaultFont))
			{
				RichTextMarkupItemTag tagDEFF = new RichTextMarkupItemTag("deff" + ftom.Fonts.IndexOf(ftom.DefaultFont));
				grpRTF1.Items.Add(tagDEFF);
			}
			// writer.Write("\\deflang1033\\uc1");

			if (ftom.Fonts.Count > 0)
			{
				RichTextMarkupItemGroup grpFontTbl = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("fonttbl"));
				foreach (FormattedTextFont font in ftom.Fonts)
				{
					RichTextMarkupItemGroup grpFont = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("f" + ftom.Fonts.IndexOf(font).ToString()));
					switch (font.Family)
					{
						case FormattedTextFontFamily.Bidi: grpFont.Items.Add(new RichTextMarkupItemTag("fbidi")); break;
						case FormattedTextFontFamily.Decor: grpFont.Items.Add(new RichTextMarkupItemTag("fdecor")); break;
						case FormattedTextFontFamily.Modern: grpFont.Items.Add(new RichTextMarkupItemTag("fmodern")); break;
						case FormattedTextFontFamily.Roman: grpFont.Items.Add(new RichTextMarkupItemTag("froman")); break;
						case FormattedTextFontFamily.Script: grpFont.Items.Add(new RichTextMarkupItemTag("fscript")); break;
						case FormattedTextFontFamily.Swiss: grpFont.Items.Add(new RichTextMarkupItemTag("fswiss")); break;
						case FormattedTextFontFamily.Tech: grpFont.Items.Add(new RichTextMarkupItemTag("ftech")); break;
					}
					grpFont.Items.Add(new RichTextMarkupItemLiteral(font.Name + ";"));
					grpFontTbl.Items.Add(grpFont);
				}
				grpRTF1.Items.Add(grpFontTbl);
			}
			if (!String.IsNullOrEmpty(mvarGenerator))
			{
				RichTextMarkupItemGroup grpGenerator = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("*"), new RichTextMarkupItemTag("generator"), new RichTextMarkupItemLiteral(mvarGenerator + ";"));
				grpRTF1.Items.Add(grpGenerator);
			}
			foreach (FormattedTextItem item in ftom.Items)
			{
				RenderItem(grpRTF1, item);
			}

			rtml.Items.Add(grpRTF1);
			objectModels.Push(rtml);
		}

		private void RenderItem(RichTextMarkupItemGroup parent, FormattedTextItem item)
		{
			if (item is FormattedTextItemHyperlink)
			{
				FormattedTextItemHyperlink itm = (item as FormattedTextItemHyperlink);

				RichTextMarkupItemGroup grpField = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("field"));
				RichTextMarkupItemGroup grpAsterisk = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("*"), new RichTextMarkupItemTag("fldinst"));
				grpAsterisk.Items.Add(new RichTextMarkupItemGroup(new RichTextMarkupItemLiteral("HYPERLINK \"" + itm.TargetURL + "\"")));
				grpField.Items.Add(grpAsterisk);

				RichTextMarkupItemGroup group = new RichTextMarkupItemGroup();
				foreach (FormattedTextItem itm1 in itm.Items)
				{
					RenderItem(group, itm1);
				}
				grpField.Items.Add(new RichTextMarkupItemGroup(new RichTextMarkupItemTag("fldrslt"), group));
				parent.Items.Add(grpField);
			}
			else if (item is FormattedTextItemBold)
			{
				RichTextMarkupItemGroup group = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("b"));
				FormattedTextItemBold itm = (item as FormattedTextItemBold);
				foreach (FormattedTextItem itm1 in itm.Items)
				{
					RenderItem(group, itm1);
				}
				parent.Items.Add(group);
			}
			else if (item is FormattedTextItemLiteral)
			{
				parent.Items.Add(new RichTextMarkupItemLiteral((item as FormattedTextItemLiteral).Text));
			}
			else if (item is FormattedTextItemParagraph)
			{
				RichTextMarkupItemGroup group = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("pard"));
				FormattedTextItemParagraph itm = (item as FormattedTextItemParagraph);
				foreach (FormattedTextItem item1 in itm.Items)
				{
					RenderItem(group, item1);
				}
				group.Items.Add(new RichTextMarkupItemTag("par"));
				parent.Items.Add(group);
			}
			else if (item is FormattedTextItemFontSize)
			{
				FormattedTextItemFontSize itm = (item as FormattedTextItemFontSize);
				RichTextMarkupItemGroup group = new RichTextMarkupItemGroup(new RichTextMarkupItemTag("fs" + (itm.Value * 2).ToString()));
				foreach (FormattedTextItem item1 in itm.Items)
				{
					RenderItem(group, item1);
				}
				parent.Items.Add(group);
			}
		}
	}
}
