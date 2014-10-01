using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public abstract class FormattedTextItem : ICloneable
	{
		public class FormattedTextItemCollection
			: System.Collections.ObjectModel.Collection<FormattedTextItem>
		{

		}

		private FormattedTextStyleGroup mvarBaseStyleGroup = null;
		public FormattedTextStyleGroup BaseStyleGroup { get { return mvarBaseStyleGroup; } set { mvarBaseStyleGroup = value; } }

		private FormattedTextStyle.TextStyleCollection mvarStyles = new FormattedTextStyle.TextStyleCollection();
		public FormattedTextStyle.TextStyleCollection Styles { get { return mvarStyles; } }

		private string mvarText = String.Empty;
		public string Text { get { return mvarText; } set { mvarText = value; } }

		public abstract object Clone();
	}
}
