using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	public class FormattedTextItemLiteral : FormattedTextItem
	{
		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		public FormattedTextItemLiteral()
		{
		}
		public FormattedTextItemLiteral(string text)
		{
			mvarText = text;
		}

		public override object Clone()
		{
			FormattedTextItemLiteral clone = new FormattedTextItemLiteral();
			clone.Text = (mvarText.Clone() as string);
			return clone;
		}
	}
}
