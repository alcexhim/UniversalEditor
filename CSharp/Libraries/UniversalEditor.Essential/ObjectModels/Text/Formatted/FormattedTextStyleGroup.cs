using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public class FormattedTextStyleGroup
	{
		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private FormattedTextStyle.TextStyleCollection mvarStyles = new FormattedTextStyle.TextStyleCollection();
		public FormattedTextStyle.TextStyleCollection Styles { get { return mvarStyles; } }
	}
}
