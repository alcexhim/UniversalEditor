//
//  ModelTextParameter.cs - describes a ModelParameter whose value is a text string
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

using System;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Parameters
{
	/// <summary>
	/// Describes a <see cref="ModelParameter" /> whose value is a text string.
	/// </summary>
	public class ModelTextParameter : ModelParameter
	{
		/// <summary>
		/// Gets or sets the default value of the <see cref="ModelParameter" />.
		/// </summary>
		/// <value>The default value of the <see cref="ModelParameter" />.</value>
		public string DefaultValue { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets a value indicating whether this
		/// <see cref="T:UniversalEditor.ObjectModels.Multimedia3D.Model.Parameters.ModelTextParameter"/> requires a value chosen from the
		/// <see cref="AvailableValues" /> list of valid values.
		/// </summary>
		/// <value><c>true</c> if this parameter requires a value chosen from the list of valid values; otherwise, <c>false</c>.</value>
		public bool RequireChoice { get; set; } = false;
		/// <summary>
		/// Gets a collection of <see cref="string" /> instances representing valid values that can be chosen for this <see cref="ModelTextParameter" />.
		/// </summary>
		/// <value>The valid values that can be chosen for this <see cref="ModelTextParameter" />.</value>
		public System.Collections.Specialized.StringCollection AvailableValues { get; } = new System.Collections.Specialized.StringCollection();

		public override object Clone()
		{
			ModelTextParameter clone = new ModelTextParameter();
			clone.DefaultValue = (DefaultValue.Clone() as string);
			clone.RequireChoice = RequireChoice;
			foreach (string availableValue in AvailableValues)
			{
				clone.AvailableValues.Add(availableValue.Clone() as string);
			}
			return clone;
		}
	}
}
