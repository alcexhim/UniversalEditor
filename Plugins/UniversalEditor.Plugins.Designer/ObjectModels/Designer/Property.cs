//
//  Property.cs - represents a property definition associated with a Component in a component designer layout
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

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Represents a property definition associated with a <see cref="Component" /> in a component designer layout.
	/// </summary>
	public class Property
	{
		public class PropertyCollection
			: System.Collections.ObjectModel.Collection<Property>
		{
			public Property this[Guid id]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].ID == id)
							return this[i];
					}
					return null;
				}
			}
		}

		public Guid ID { get; private set; } = Guid.Empty;
		public string Title { get; set; } = String.Empty;
		public object DefaultValue { get; set; } = null;

		public Property(Guid id, string title, object defaultValue = null)
		{
			ID = id;
			Title = title;
			DefaultValue = defaultValue;
		}
	}
}
