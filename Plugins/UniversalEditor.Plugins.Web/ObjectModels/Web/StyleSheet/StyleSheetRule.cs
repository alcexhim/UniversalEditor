//
//  StyleSheetRule.cs - represents a collection of style sheet attributes for a particular object (selector)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.ObjectModels.Web.StyleSheet
{
	/// <summary>
	/// Represents a collection of style sheet attributes for a particular object (selector).
	/// </summary>
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

		public bool MatchesSelector(string selector)
		{
			// TODO: actually make this implement CSS standards
			if (mvarObjectName == selector) return true;
			return false;
		}
	}
}
