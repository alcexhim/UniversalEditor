//
//  FormattedTextItem.cs - the abstract base class from which all formatting commands in a FormattedTextObjectModel derive
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
	/// The abstract base class from which all formatting commands in a <see cref="FormattedTextObjectModel" /> derive.
	/// </summary>
	public abstract class FormattedTextItem : ICloneable
	{
		public class FormattedTextItemCollection
			: System.Collections.ObjectModel.Collection<FormattedTextItem>
		{

		}

		/// <summary>
		/// Gets or sets the <see cref="FormattedTextStyleGroup" /> which represents the base style group for the <see cref="FormattedTextItem" />.
		/// </summary>
		/// <value>The <see cref="FormattedTextStyleGroup" /> which represents the base style group for the <see cref="FormattedTextItem" />.</value>
		public FormattedTextStyleGroup BaseStyleGroup { get; set; } = null;
		/// <summary>
		/// Gets a collection of <see cref="FormattedTextStyle" /> instances representing additional styles to apply to this
		/// <see cref="FormattedTextItem" />.
		/// </summary>
		/// <value>Additional styles to apply to this <see cref="FormattedTextItem" />.</value>
		public FormattedTextStyle.TextStyleCollection Styles { get; } = new FormattedTextStyle.TextStyleCollection();
		/// <summary>
		/// Gets or sets the text rendered within this <see cref="FormattedTextItem" />.
		/// </summary>
		/// <value>The text rendered within this <see cref="FormattedTextItem" />.</value>
		public string Text { get; set; } = String.Empty;

		public abstract object Clone();
	}
}
