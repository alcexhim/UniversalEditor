using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	public class RichTextMarkupItemTag : RichTextMarkupItem
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public RichTextMarkupItemTag(string name)
		{
			mvarName = name;
		}

		public override object Clone()
		{
			RichTextMarkupItemTag clone = new RichTextMarkupItemTag(mvarName.Clone() as string);
			return clone;
		}
	}
}
