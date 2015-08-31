using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public class RichTextMarkupItemGroup : RichTextMarkupItem
	{
		private RichTextMarkupItem.RichTextMarkupItemCollection mvarItems = null;
		public RichTextMarkupItem.RichTextMarkupItemCollection Items { get { return mvarItems; } }

		public RichTextMarkupItemGroup(params RichTextMarkupItem[] items)
		{
			mvarItems = new RichTextMarkupItemCollection(this);
			foreach (RichTextMarkupItem item in items)
			{
				mvarItems.Add(item);
			}
		}

		public override object Clone()
		{
			RichTextMarkupItemGroup clone = new RichTextMarkupItemGroup();
			foreach (RichTextMarkupItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as RichTextMarkupItem);
			}
			return clone;
		}
	}
}
