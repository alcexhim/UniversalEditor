//
//  FormattedTextStyleGroup.cs - represents a named group of FormattedTextStyles in a FormattedTextObjectModel
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

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	/// <summary>
	/// Represents a named group of <see cref="FormattedTextStyle" />s in a FormattedTextObjectModel.
	/// </summary>
	public class FormattedTextStyleGroup
	{
		/// <summary>
		/// Gets or sets the name of this <see cref="FormattedTextStyleGroup" />.
		/// </summary>
		/// <value>The name of this <see cref="FormattedTextStyleGroup" />.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="FormattedTextStyle" /> instances representing the styles included in this <see cref="FormattedTextStyleGroup" />.
		/// </summary>
		/// <value>The styles included in this <see cref="FormattedTextStyleGroup" />.</value>
		public FormattedTextStyle.TextStyleCollection Styles { get; } = new FormattedTextStyle.TextStyleCollection();
	}
}
