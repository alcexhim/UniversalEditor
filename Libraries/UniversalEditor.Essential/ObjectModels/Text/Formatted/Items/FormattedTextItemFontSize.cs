using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemFontSize : FormattedTextItemContainer
	{
		private int mvarValue = 0;
		public int Value { get { return mvarValue; } set { mvarValue = value; } }

		public FormattedTextItemFontSize(int value = 0, params FormattedTextItem[] items)
		{
			mvarValue = value;
			foreach (FormattedTextItem item in items)
			{
				base.Items.Add(item);
			}
		}

		public override object Clone()
		{
			FormattedTextItemFontSize clone = new FormattedTextItemFontSize();
			clone.Value = mvarValue;
			return clone;
		}
	}
}
