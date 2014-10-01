using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class Literal : FormattedTextItem
	{
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		public override object Clone()
		{
			Literal clone = new Literal();
			clone.Text = (mvarText.Clone() as string);
			return clone;
		}
	}
}
