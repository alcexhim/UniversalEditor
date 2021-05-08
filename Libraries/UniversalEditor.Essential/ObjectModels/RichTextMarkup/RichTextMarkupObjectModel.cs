//
//  RichTextMarkupObjectModel.cs - provides an ObjectModel for manipulating markup in Rich Text Format
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

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating markup in Rich Text Format.
	/// </summary>
	/// <remarks>
	/// Because the Rich Text Format is so versatile, the RTF data format that provides the Formatted Text <see cref="ObjectModel" /> is implemented
	/// first as a subclass on top of the <see cref="RichTextMarkupObjectModel" />. This way we can provide generic RTF parsing functionality in a base
	/// class, and Formatted Text-specific functionality in the subclass.
	/// </remarks>
	public class RichTextMarkupObjectModel : ObjectModel
	{
		/// <summary>
		/// Gets a collection of <see cref="RichTextMarkupItem" /> instances representing the items in this <see cref="RichTextMarkupObjectModel" />.
		/// </summary>
		/// <value>The items in this <see cref="RichTextMarkupObjectModel" />.</value>
		public RichTextMarkupItem.RichTextMarkupItemCollection Items { get; } = new RichTextMarkupItem.RichTextMarkupItemCollection();

		public override void Clear()
		{
			Items.Clear();
		}
		public override void CopyTo(ObjectModel where)
		{
			RichTextMarkupObjectModel clone = (where as RichTextMarkupObjectModel);
			foreach (RichTextMarkupItem item in Items)
			{
				clone.Items.Add(item.Clone() as RichTextMarkupItem);
			}
		}
	}
}
