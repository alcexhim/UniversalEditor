//
//  PropertyStringFormattingPart.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.Plugins.Genealogy.ObjectModels.FamilyTree.StringFormatting.FormattingParts
{
	public class PropertyStringFormattingPart : StringFormattingPart
	{
		public string PropertyName { get; set; } = null;

		public PropertyStringFormattingPart(string propertyName)
		{
			PropertyName = propertyName;
		}

		protected override string ToStringInternal(object obj)
		{
			if (obj != null)
			{
				Type t = obj.GetType();
				System.Reflection.PropertyInfo pi = t.GetProperty(PropertyName, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				object objV = pi.GetValue(obj, null);
				if (objV != null)
					return objV.ToString();
			}
			return null;
		}
	}
}
