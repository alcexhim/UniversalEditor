using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.IO;

using UniversalEditor.ObjectModels.RichTextMarkup;

namespace UniversalEditor.DataFormats.RichTextMarkup.RTML
{
	/// <summary>
	/// Data format for expressing Rich Text Format files as MarkupObjectModel.
	/// </summary>
	public class RTMLDataFormat : DataFormat
	{
		private static DataFormatReference _dfr = null;
		protected override DataFormatReference MakeReferenceInternal()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReferenceInternal();
				_dfr.Capabilities.Add(typeof(RichTextMarkupObjectModel), DataFormatCapabilities.All);
			}
			return _dfr;
		}

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
		}

		protected override void SaveInternal(ObjectModel objectModel)
		{
			Writer writer = base.Accessor.Writer;

			RichTextMarkupObjectModel rtml = (objectModel as RichTextMarkupObjectModel);
			if (rtml == null) throw new ObjectModelNotSupportedException();

			foreach (RichTextMarkupItem item in rtml.Items)
			{
				RenderItem(writer, item);
			}
		}

		private RichTextMarkupItem lastItemRendered = null;

		private void RenderItem(Writer writer, RichTextMarkupItem item)
		{
			if (item is RichTextMarkupItemGroup)
			{
				RichTextMarkupItemGroup itm = (item as RichTextMarkupItemGroup);
				writer.Write("{");
				foreach (RichTextMarkupItem item1 in itm.Items)
				{
					RenderItem(writer, item1);
				}
				writer.Write("}");
			}
			else if (item is RichTextMarkupItemTag)
			{
				RichTextMarkupItemTag itm = (item as RichTextMarkupItemTag);
				writer.Write("\\");
				writer.Write(itm.Name);
			}
			else if (item is RichTextMarkupItemLiteral)
			{
				RichTextMarkupItemLiteral itm = (item as RichTextMarkupItemLiteral);
				if (!(lastItemRendered is RichTextMarkupItemLiteral)) writer.Write(" ");
				writer.Write(itm.Content);
			}
			lastItemRendered = item;
		}
	}
}
