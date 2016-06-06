using System;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemItalic : FormattedTextItemContainer
	{
		public override object Clone()
		{
			FormattedTextItemItalic clone = new FormattedTextItemItalic();
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}

