using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Web.StyleSheet
{
	public class StyleSheetAttribute : ICloneable
	{
		public class StyleSheetAttributeCollection
			: System.Collections.ObjectModel.Collection<StyleSheetAttribute>
		{
			public StyleSheetAttribute Add(string Name, string Value)
			{
				StyleSheetAttribute att = new StyleSheetAttribute();
				att.Name = Name;
				att.Value = Value;
				base.Add(att);
				return att;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private string mvarValue = String.Empty;
		public string Value { get { return mvarValue; } set { mvarValue = value; } }

		public object Clone()
		{
			StyleSheetAttribute clone = new StyleSheetAttribute();
			clone.Name = mvarName;
			clone.Value = mvarValue;
			return clone;
		}
	}
}
