//
//  FormattedTextItemFontSize.cs - represents a FormattedTextItemContainer which affects the font size of all FormattedTextItems it contains
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

namespace UniversalEditor.ObjectModels.Text.Formatted.Items
{
	/// <summary>
	/// Represents a <see cref="FormattedTextItemContainer" /> which affects the font size of all <see cref="FormattedTextItem" />s it contains.
	/// </summary>
	public class FormattedTextItemFontSize : FormattedTextItemContainer
	{
		public int Value { get; set; } = 0;

		public FormattedTextItemFontSize(int value = 0, params FormattedTextItem[] items)
		{
			Value = value;
			foreach (FormattedTextItem item in items)
			{
				base.Items.Add(item);
			}
		}

		public override object Clone()
		{
			FormattedTextItemFontSize clone = new FormattedTextItemFontSize();
			clone.Value = Value;
			return clone;
		}
	}
}
