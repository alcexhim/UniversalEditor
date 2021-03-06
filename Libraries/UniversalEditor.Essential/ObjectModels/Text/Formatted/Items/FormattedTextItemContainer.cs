//
//  FormattedTextItemContainer.cs - the abstract base class from which all FormattedTextItems which can contain other FormattedTextItems are derived
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
	/// The abstract base class from which all <see cref="FormattedTextItem" />s which can contain other <see cref="FormattedTextItem" />s are derived.
	/// </summary>
	public abstract class FormattedTextItemContainer : FormattedTextItem, IFormattedTextItemParent
	{
		/// <summary>
		/// Gets a collection of <see cref="FormattedTextItem" /> instances representing the items contained within this
		/// <see cref="FormattedTextItemContainer" />.
		/// </summary>
		/// <value>The items contained within this <see cref="FormattedTextItemContainer" />.</value>
		public FormattedTextItem.FormattedTextItemCollection Items { get; } = new FormattedTextItemCollection();
	}
}
