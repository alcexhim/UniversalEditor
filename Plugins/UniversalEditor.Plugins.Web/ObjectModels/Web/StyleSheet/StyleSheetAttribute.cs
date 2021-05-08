//
//  StyleSheetAttribute.cs - represents a specific attribute in a style sheet rule
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
	/// Represents a specific attribute in a style sheet rule.
	/// </summary>
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
