using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class Container : FormattedTextItem
	{
		private FormattedTextItem.FormattedTextItemCollection mvarItems = new FormattedTextItemCollection();
		public FormattedTextItem.FormattedTextItemCollection Items { get { return mvarItems; } }

		public override object Clone()
		{
			Container clone = new Container();
			foreach (FormattedTextItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}
