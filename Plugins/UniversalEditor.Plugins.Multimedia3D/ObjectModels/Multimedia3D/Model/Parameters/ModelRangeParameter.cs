//
//  ModelRangeParameter.cs - describes a ModelParameter whose value must fall within a defined range
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Parameters
{
	/// <summary>
	/// Describes a <see cref="ModelParameter" /> whose value must fall within a defined range.
	/// </summary>
	public class ModelRangeParameter : ModelParameter
	{
		/// <summary>
		/// Gets or sets the default value of the <see cref="ModelParameter" />.
		/// </summary>
		/// <value>The default value of the <see cref="ModelParameter" />.</value>
		public double DefaultValue { get; set; } = 0;
		/// <summary>
		/// Gets or sets the minimum value of the <see cref="ModelParameter" />.
		/// </summary>
		/// <value>The minimum value of the <see cref="ModelParameter" />.</value>
		public double MinimumValue { get; set; } = 0;
		/// <summary>
		/// Gets or sets the maximum value of the <see cref="ModelParameter" />.
		/// </summary>
		/// <value>The maximum value of the <see cref="ModelParameter" />.</value>
		public double MaximumValue { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value by which the <see cref="ModelParameter" /> is incremented or decremented when using the scroll buttons or arrow keys.
		/// </summary>
		/// <value>The value by which the <see cref="ModelParameter" /> is incremented or decremented when using the scroll buttons or arrow keys.</value>
		public double SmallChange { get; set; } = 0;
		/// <summary>
		/// Gets or sets the value by which the <see cref="ModelParameter" /> is incremented or decremented when using the scroll bar or page up/page down keys.
		/// </summary>
		/// <value>The value by which the <see cref="ModelParameter" /> is incremented or decremented when using the scroll bar or page up/page down keys.</value>
		public double LargeChange { get; set; } = 0;

		public override object Clone()
		{
			ModelRangeParameter clone = new ModelRangeParameter();
			clone.DefaultValue = DefaultValue;
			clone.MinimumValue = MinimumValue;
			clone.MaximumValue = MaximumValue;
			clone.SmallChange = SmallChange;
			clone.LargeChange = LargeChange;
			return clone;
		}
	}
}
