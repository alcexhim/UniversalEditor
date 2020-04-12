//
//  IcarusParameter.cs - the abstract base class from which ICARUS parameter implementations derive
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

namespace UniversalEditor.ObjectModels.Icarus
{
	/// <summary>
	/// The abstract base class from which ICARUS parameter implementations derive.
	/// </summary>
	public abstract class IcarusParameter
	{
		public class IcarusParameterCollection
			: System.Collections.ObjectModel.Collection<IcarusParameter>
		{
			public IcarusParameter this[string name]
			{
				get
				{
					for (int i = 0; i < Count; i++)
					{
						if (this[i].Name == name)
							return this[i];
					}
					return null;
				}
			}
		}

		public IcarusParameter(string name, IcarusExpression defaultValue = null)
		{
			Name = name;
			Description = null;
			Value = defaultValue;
		}
		public IcarusParameter(string name, string description, IcarusExpression defaultValue = null)
		{
			Name = name;
			Description = description;
			Value = defaultValue;
		}

		public string Name { get; set; } = null;
		public string Description { get; set; } = null;
		public bool ReadOnly { get; set; } = false;

		/// <summary>
		/// Gets or sets the name of the enumeration from which to display valid values.
		/// </summary>
		/// <value>The name of the enumeration.</value>
		public string EnumerationName { get; set; } = null;

		public IcarusExpression Value { get; set; } = null;
	}
}
