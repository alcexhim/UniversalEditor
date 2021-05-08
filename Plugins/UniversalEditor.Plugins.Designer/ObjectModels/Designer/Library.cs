//
//  Library.cs - represents a collection of existing Components referenced by a component designer layout
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
	/// Represents a collection of existing <see cref="Component" />s referenced by a component designer layout.
	/// </summary>
	public class Library : ICloneable
	{
		public class LibraryCollection
			: System.Collections.ObjectModel.Collection<Library>
		{

		}

		public Library()
		{
			Components.Add(new Component(DesignerObjectGuids.Common, "Common", new Property[]
			{
				new Property(DesignerPropertyGuids.Common.BackgroundImage, "Background image")
			}));
		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="Library" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="Library" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="Library" />.
		/// </summary>
		/// <value>The title of this <see cref="Library" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets a collection of <see cref="Component" /> instances representing the component designer components provided by this <see cref="Library" />.
		/// </summary>
		/// <value>The component designer components provided by this <see cref="Library" />.</value>
		public Component.ComponentCollection Components { get; set; } = new Component.ComponentCollection();

		public object Clone()
		{
			Library clone = new Library();
			clone.ID = ID;
			clone.Title = (Title.Clone() as string);
			for (int i = 0; i < Components.Count; i++)
			{
				clone.Components.Add(Components[i].Clone() as Component);
			}
			return clone;
		}
	}
}
