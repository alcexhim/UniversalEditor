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
			writer.WriteLine("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033\\uc1 ");
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
				writer.WriteLine("{\\field{\\*\\fldinst {HYPERLINK \"" + itm.TargetURL + "\"}}{\\fldrslt {" + itm.Title + "}}}");
			}
		}
	}
}
