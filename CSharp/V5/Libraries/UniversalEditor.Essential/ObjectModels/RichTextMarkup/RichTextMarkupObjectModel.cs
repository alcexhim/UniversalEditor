using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public class RichTextMarkupObjectModel : ObjectModel
	{
		private RichTextMarkupItem.RichTextMarkupItemCollection mvarItems = new RichTextMarkupItem.RichTextMarkupItemCollection();
		public RichTextMarkupItem.RichTextMarkupItemCollection Items { get { return mvarItems; } }

		public override void Clear()
		{
			mvarItems.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			RichTextMarkupObjectModel clone = (where as RichTextMarkupObjectModel);
			foreach (RichTextMarkupItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as RichTextMarkupItem);
			}
		}
	}
}
