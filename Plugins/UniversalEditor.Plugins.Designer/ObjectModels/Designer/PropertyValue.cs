//
//  PropertyValue.cs - represents the value of a property in a designer object
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

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Represents the value of a property in a designer object.
	/// </summary>
	public class PropertyValue
	{
		public class PropertyValueCollection
			: System.Collections.ObjectModel.Collection<PropertyValue>
		{

		}

		/// <summary>
		/// Gets or sets the property associated with this <see cref="PropertyValue" />.
		/// </summary>
		/// <value>The property.</value>
		public Property Property { get; set; } = null;
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:UniversalEditor.ObjectModels.Designer.PropertyValue"/>
		/// has its value set.
		/// </summary>
		/// <value><c>true</c> if the value is set; otherwise, <c>false</c>.</value>
		public bool IsSet { get; set; } = false;
		/// <summary>
		/// Gets or sets the value of this <see cref="PropertyValue" />.
		/// </summary>
		/// <value>The value of the property.</value>
		public object Value { get; set; } = null;
	}
}
