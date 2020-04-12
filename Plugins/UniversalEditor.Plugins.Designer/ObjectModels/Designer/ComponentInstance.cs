//
//  ComponentInstance.cs - represents an instance of a Component in a component designer layout
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
	/// Represents an instance of a <see cref="Component" /> in a component designer layout.
	/// </summary>
	public class ComponentInstance
	{
		public class ComponentInstanceCollection
			: System.Collections.ObjectModel.Collection<ComponentInstance>
		{

		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="ComponentInstance" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="ComponentInstance" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the <see cref="Component" /> for which this <see cref="ComponentInstance" /> is an instance.
		/// </summary>
		/// <value>The <see cref="Component" /> for which this <see cref="ComponentInstance" /> is an instance.</value>
		public Component Component { get; set; } = null;
		/// <summary>
		/// Gets a collection of <see cref="ConnectionValue" /> instances representing connections this <see cref="ComponentInstance" /> has to other <see cref="ComponentInstance" />s.
		/// </summary>
		/// <value>The connections this <see cref="ComponentInstance" /> has to other <see cref="ComponentInstance" />s.</value>
		public ConnectionValue.ConnectionValueCollection ConnectionValues { get; } = new ConnectionValue.ConnectionValueCollection();
		/// <summary>
		/// Gets a collection of <see cref="PropertyValue" /> instances representing instance-specific values for the properties of the associated <see cref="Component" />.
		/// </summary>
		/// <value>The instance-specific values for the properties of the associated <see cref="Component" />.</value>
		public PropertyValue.PropertyValueCollection PropertyValues { get; } = new PropertyValue.PropertyValueCollection();
		/// <summary>
		/// The distance between the left edge of the design and the left edge of the component.
		/// </summary>
		public Measurement X { get; set; } = new Measurement(0, MeasurementUnit.Pixel);
		/// <summary>
		/// The distance between the top edge of the design and the top edge of the component.
		/// </summary>
		public Measurement Y { get; set; } = new Measurement(0, MeasurementUnit.Pixel);
		/// <summary>
		/// The position of this component in the front/back order if the layout is two-dimensional, or along the Z axis if the layout is three-dimensional.
		/// </summary>
		public Measurement Z { get; set; } = new Measurement(0, MeasurementUnit.Pixel);
		/// <summary>
		/// The width of the component.
		/// </summary>
		public Measurement Width { get; set; } = new Measurement(0, MeasurementUnit.Pixel);
		/// <summary>
		/// The height of the component.
		/// </summary>
		public Measurement Height { get; set; } = new Measurement(0, MeasurementUnit.Pixel);
		/// <summary>
		/// The depth of the component.
		/// </summary>
		public Measurement Depth { get; set; } = new Measurement(0, MeasurementUnit.Pixel);

	}
}
