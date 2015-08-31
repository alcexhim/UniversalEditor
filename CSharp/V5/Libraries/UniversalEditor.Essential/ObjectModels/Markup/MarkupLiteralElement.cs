using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupLiteralElement : MarkupElement
	{
		public override object Clone()
		{
			return new MarkupLiteralElement
			{
				Name = base.Name,
				Namespace = base.Namespace,
				Value = base.Value
			};
		}
	}
}
