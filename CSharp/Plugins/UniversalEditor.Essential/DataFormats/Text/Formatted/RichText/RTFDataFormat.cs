using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Text.Formatted;
using UniversalEditor.ObjectModels.Text.Formatted.Items;

namespace UniversalEditor.DataFormats.Text.Formatted.RichText
{
	public class RTFDataFormat : DataFormat
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

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			FormattedTextObjectModel ftom = (objectModel as FormattedTextObjectModel);
			if (ftom == null) throw new ObjectModelNotSupportedException();

			Writer writer = base.Accessor.Writer;
			writer.Write("{\\rtf1");
			// writer.WriteLine("\\ansi\\ansicpg1252");

			if (ftom.DefaultFont != null && ftom.Fonts.Contains(ftom.DefaultFont))
			{
				writer.Write("\\deff" + ftom.Fonts.IndexOf(ftom.DefaultFont));
			}
			// writer.Write("\\deflang1033\\uc1");

			if (ftom.Fonts.Count > 0)
			{
				writer.Write("{\\fonttbl");
				foreach (FormattedTextFont font in ftom.Fonts)
				{
					writer.Write("{\\f" + ftom.Fonts.IndexOf(font).ToString() + " " + font.Name + ";}");
				}
				writer.Write("}");
			}

			foreach (FormattedTextItem item in ftom.Items)
			{
				RenderItem(writer, item);
			}
			writer.WriteLine(" }");
		}

		private void RenderItem(Writer writer, FormattedTextItem item)
		{
			if (item is FormattedTextItemHyperlink)
			{
				FormattedTextItemHyperlink itm = (item as FormattedTextItemHyperlink);
				writer.Write("{\\field{\\*\\fldinst {HYPERLINK \"" + itm.TargetURL + "\"}}{\\fldrslt {");
				foreach (FormattedTextItem itm1 in itm.Items)
				{
					RenderItem(writer, itm1);
				}
				writer.Write("}}}");
			}
			else if (item is FormattedTextItemBold)
			{
				writer.Write("{\b ");
				FormattedTextItemBold itm = (item as FormattedTextItemBold);
				foreach (FormattedTextItem itm1 in itm.Items)
				{
					RenderItem(writer, itm1);
				}
				writer.Write("}");
			}
			else if (item is FormattedTextItemLiteral)
			{
				writer.Write((item as FormattedTextItemLiteral).Text);
			}
		}
	}
}
