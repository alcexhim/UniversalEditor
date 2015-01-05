using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public abstract class RichTextMarkupItem : ICloneable
	{
		public class RichTextMarkupItemCollection
			: System.Collections.ObjectModel.Collection<RichTextMarkupItem>
		{
			private RichTextMarkupItemGroup mvarParent = null;
			public RichTextMarkupItemCollection(RichTextMarkupItemGroup parent = null)
			{
				mvarParent = parent;
			}

			protected override void InsertItem(int index, RichTextMarkupItem item)
			{
				base.InsertItem(index, item);
				item.Parent = mvarParent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, RichTextMarkupItem item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = mvarParent;
			}
			protected override void ClearItems()
			{
				foreach (RichTextMarkupItem item in this)
				{
					item.Parent = null;
				}
				base.ClearItems();
			}
		}

		public abstract object Clone();

		public RichTextMarkupItemGroup Parent { get; set; }
	}
}
