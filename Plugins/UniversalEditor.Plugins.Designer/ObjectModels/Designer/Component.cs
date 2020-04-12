//
//  Component.cs - represents a component in a component designer layout
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
	/// Represents a component in a component designer layout.
	/// </summary>
	public class Component
	{
		public class ComponentCollection
			: System.Collections.ObjectModel.Collection<Component>
		{

		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="Component" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="Component" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="Component" />.
		/// </summary>
		/// <value>The title of this <see cref="Component" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="Property" /> instances representing properties associated with this <see cref="Component" />.
		/// </summary>
		/// <value>The properties associated with this <see cref="Component" />.</value>
		public Property.PropertyCollection Properties { get; } = new Property.PropertyCollection();
	}
}
