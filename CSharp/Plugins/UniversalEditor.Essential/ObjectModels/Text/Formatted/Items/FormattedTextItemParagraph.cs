using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemParagraph : FormattedTextItemContainer
	{
		public override object Clone()
		{
			FormattedTextItemParagraph clone = new FormattedTextItemParagraph();
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}
