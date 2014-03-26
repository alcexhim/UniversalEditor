using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupCommentElement : MarkupElement
	{
        public MarkupCommentElement()
        {
        }
        public MarkupCommentElement(string value)
        {
            base.Value = value;
        }
		public override object Clone()
		{
			return new MarkupCommentElement
			{
				Name = base.Name,
				Namespace = base.Namespace,
				Value = base.Value
			};
		}
		public override string ToString()
		{
			return "<!-- " + base.Value + " -->";
		}
	}
}
