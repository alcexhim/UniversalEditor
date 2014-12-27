using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemHyperlink : FormattedTextItem
	{
		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarTargetURL = String.Empty;
		public string TargetURL { get { return mvarTargetURL; } set { mvarTargetURL = value; } }

		public FormattedTextItemHyperlink(string targetURL = "", string title = "")
		{
			mvarTargetURL = targetURL;
			if (String.IsNullOrEmpty(title))
			{
				mvarTitle = targetURL;
			}
			else
			{
				mvarTitle = title;
			}
		}

		public override object Clone()
		{
			FormattedTextItemHyperlink clone = new FormattedTextItemHyperlink();
			clone.TargetURL = (mvarTargetURL.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
