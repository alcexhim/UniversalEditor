//
//  Design.cs - represents a component designer layout in a DesignerObjectModel
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
using MBS.Framework.Drawing;

namespace UniversalEditor.ObjectModels.Designer
{
	/// <summary>
	/// Represents a component designer layout in a <see cref="DesignerObjectModel" />.
	/// </summary>
	public class Design : ICloneable
	{
		public class DesignCollection
			: System.Collections.ObjectModel.Collection<Design>
		{

		}

		/// <summary>
		/// Gets a collection of <see cref="ComponentInstance" /> instances representing the instances of components in this component designer layout.
		/// </summary>
		/// <value>The instances of components in this component designer layout.</value>
		public ComponentInstance.ComponentInstanceCollection ComponentInstances { get; } = new ComponentInstance.ComponentInstanceCollection();

		public Dimension2D Size { get; set; } = new Dimension2D(600, 400);

		public object Clone()
		{
			Design clone = new Design();
			for (int i = 0; i < ComponentInstances.Count; i++)
			{
				clone.ComponentInstances.Add(ComponentInstances[i].Clone() as ComponentInstance);
			}
			clone.Size = Size;
			return clone;
		}
	}
}
