//
//  RichTextMarkupItemGroup.cs - represents a RichTextMarkupItem that can contain other RichTextMarkupItems
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
	/// Represents a <see cref="RichTextMarkupItem" /> that can contain other <see cref="RichTextMarkupItem" />s.
	/// </summary>
	public class RichTextMarkupItemGroup : RichTextMarkupItem
	{
		private RichTextMarkupItem.RichTextMarkupItemCollection mvarItems = null;
		public RichTextMarkupItem.RichTextMarkupItemCollection Items { get { return mvarItems; } }

		public RichTextMarkupItemGroup(params RichTextMarkupItem[] items)
		{
			mvarItems = new RichTextMarkupItemCollection(this);
			foreach (RichTextMarkupItem item in items)
			{
				mvarItems.Add(item);
			}
		}

		public override object Clone()
		{
			RichTextMarkupItemGroup clone = new RichTextMarkupItemGroup();
			foreach (RichTextMarkupItem item in mvarItems)
			{
				clone.Items.Add(item.Clone() as RichTextMarkupItem);
			}
			return clone;
		}
	}
}
