using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public class RichTextMarkupItemLiteral : RichTextMarkupItem
	{
		private string mvarContent = String.Empty;
		public string Content { get { return mvarContent; } set { mvarContent = value; } }

		public RichTextMarkupItemLiteral(string content = "")
		{
			mvarContent = content;
		}

		public override object Clone()
		{
			RichTextMarkupItemLiteral clone = new RichTextMarkupItemLiteral(mvarContent.Clone() as string);
			return clone;
		}
	}
}
