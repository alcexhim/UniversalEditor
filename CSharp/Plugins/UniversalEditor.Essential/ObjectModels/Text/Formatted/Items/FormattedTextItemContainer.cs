using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public abstract class FormattedTextItemContainer : FormattedTextItem
	{
		private FormattedTextItem.FormattedTextItemCollection mvarItems = new FormattedTextItemCollection();
		public FormattedTextItem.FormattedTextItemCollection Items { get { return mvarItems; } }
	}
}
