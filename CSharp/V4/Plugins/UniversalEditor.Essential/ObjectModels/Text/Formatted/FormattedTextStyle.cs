using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public class FormattedTextStyle
	{
		public class TextStyleCollection
			: System.Collections.ObjectModel.Collection<FormattedTextStyle>
		{

		}

		public object Clone()
		{
			FormattedTextStyle clone = new FormattedTextStyle();
			return clone;
		}
	}
}
