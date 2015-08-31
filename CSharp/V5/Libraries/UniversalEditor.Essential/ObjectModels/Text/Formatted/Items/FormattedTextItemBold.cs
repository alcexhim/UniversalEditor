using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemBold : FormattedTextItemContainer
	{
		public override object Clone()
		{
			FormattedTextItemBold clone = new FormattedTextItemBold();
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}
