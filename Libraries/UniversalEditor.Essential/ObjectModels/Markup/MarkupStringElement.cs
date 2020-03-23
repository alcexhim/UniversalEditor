using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupStringElement : MarkupElement
	{
		public override object Clone()
		{
			return new MarkupStringElement
			{
				Name = base.Name,
				Namespace = base.Namespace,
				Value = base.Value
			};
		}
	}
}
