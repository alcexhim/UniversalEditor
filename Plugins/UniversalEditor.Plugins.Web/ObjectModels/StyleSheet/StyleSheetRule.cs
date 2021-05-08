using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StyleSheet
{
	public class StyleSheetRule : ICloneable
	{
		public class StyleSheetRuleCollection
			: System.Collections.ObjectModel.Collection<StyleSheetRule>
		{
		}

		private string mvarObjectName = String.Empty;
		public string ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

		private StyleSheetAttribute.StyleSheetAttributeCollection mvarAttributes = new StyleSheetAttribute.StyleSheetAttributeCollection();
		public StyleSheetAttribute.StyleSheetAttributeCollection Attributes { get { return mvarAttributes; } }

		private StyleSheetProperty.StyleSheetPropertyCollection mvarProperties = new StyleSheetProperty.StyleSheetPropertyCollection();
		public StyleSheetProperty.StyleSheetPropertyCollection Properties { get { return mvarProperties; } }

		public object Clone()
		{
			StyleSheetRule clone = new StyleSheetRule();
			clone.ObjectName = mvarObjectName;
			foreach (StyleSheetAttribute att in mvarAttributes)
			{
				clone.Attributes.Add(att.Clone() as StyleSheetAttribute);
			}
			foreach (StyleSheetProperty prop in mvarProperties)
			{
				clone.Properties.Add(prop.Clone() as StyleSheetProperty);
			}
			return clone;
		}
	}
}
