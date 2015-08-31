using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemHyperlink : FormattedTextItemContainer
	{
		private string mvarTargetURL = String.Empty;
		public string TargetURL { get { return mvarTargetURL; } set { mvarTargetURL = value; } }

		public FormattedTextItemHyperlink(string targetURL = "", string title = "")
		{
			mvarTargetURL = targetURL;
			base.Items.Add(new FormattedTextItemLiteral(title));
		}

		public override object Clone()
		{
			FormattedTextItemHyperlink clone = new FormattedTextItemHyperlink();
			clone.TargetURL = (mvarTargetURL.Clone() as string);
			foreach (FormattedTextItem item in Items)
			{
				clone.Items.Add(item.Clone() as FormattedTextItem);
			}
			return clone;
		}
	}
}
