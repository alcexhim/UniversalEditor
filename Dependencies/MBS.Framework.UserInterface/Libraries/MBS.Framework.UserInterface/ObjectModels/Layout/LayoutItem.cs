//
//  LayoutItem.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
namespace MBS.Framework.UserInterface.ObjectModels.Layout
{
	public class LayoutItem
	{
		public class LayoutItemCollection
			: System.Collections.ObjectModel.Collection<LayoutItem>
		{
			public LayoutItem FirstOfClassName(string className)
			{
				return FirstOfClassName(new string[] { className });
			}
			public LayoutItem FirstOfClassName(string[] classNames)
			{
				foreach (LayoutItem item in this)
				{
					foreach (string className in classNames) {
						if (item.ClassName == className)
							return item;
					}
				}
				return null;
			}
		}

		public string ClassName { get; set; }
		public string ID { get; set; }
		public LayoutItemCollection Items { get; } = new LayoutItemCollection();
		public LayoutItemProperty.LayoutItemPropertyCollection Attributes { get; } = new LayoutItemProperty.LayoutItemPropertyCollection();
		public LayoutItemProperty.LayoutItemPropertyCollection Properties { get; } = new LayoutItemProperty.LayoutItemPropertyCollection();
		public LayoutItemProperty.LayoutItemPropertyCollection PackingProperties { get; } = new LayoutItemProperty.LayoutItemPropertyCollection();

		public object Clone()
		{
			LayoutItem clone = new LayoutItem();
			clone.ID = (ID.Clone() as string);
			clone.ClassName = (ClassName.Clone() as string);
			foreach (LayoutItem item in Items)
			{
				clone.Items.Add(item.Clone() as LayoutItem);
			}
			foreach (LayoutItemProperty property in Attributes)
			{
				clone.Attributes.Add(property.Clone() as LayoutItemProperty);
			}
			foreach (LayoutItemProperty property in Properties)
			{
				clone.Properties.Add(property.Clone() as LayoutItemProperty);
			}
			foreach (LayoutItemProperty property in PackingProperties)
			{
				clone.PackingProperties.Add(property.Clone() as LayoutItemProperty);
			}
			return clone;
		}
		public override string ToString()
		{
			return ID + ": " + ClassName;
		}
	}
}
