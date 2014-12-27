using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public class FormattedTextFont : ICloneable
	{
		public class FormattedTextFontCollection
			: System.Collections.ObjectModel.Collection<FormattedTextFont>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public object Clone()
		{
			FormattedTextFont clone = new FormattedTextFont();
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}
	}
}
