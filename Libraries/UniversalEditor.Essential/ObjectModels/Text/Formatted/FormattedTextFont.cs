//
//  FormattedTextFont.cs - represents a font definition in a FormattedTextObjectModel
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
	/// Represents a font definition in a <see cref="FormattedTextObjectModel" />.
	/// </summary>
	public class FormattedTextFont : ICloneable
	{
		public class FormattedTextFontCollection
			: System.Collections.ObjectModel.Collection<FormattedTextFont>
		{

		}

		/// <summary>
		/// Gets or sets the name of the font to define.
		/// </summary>
		/// <value>The name of the font to define.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the family of the font to define.
		/// </summary>
		/// <value>The family of the font to define.</value>
		public FormattedTextFontFamily Family { get; set; } = FormattedTextFontFamily.None;

		public object Clone()
		{
			FormattedTextFont clone = new FormattedTextFont();
			clone.Name = (Name.Clone() as string);
			return clone;
		}
	}
}
