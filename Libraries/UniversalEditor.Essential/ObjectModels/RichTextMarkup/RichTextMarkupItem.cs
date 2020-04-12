//
//  RichTextMarkupItem.cs - the abstract base class from which all Rich Text Format formatting items derive
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

namespace UniversalEditor.ObjectModels.RichTextMarkup
{
	/// <summary>
	/// The abstract base class from which all Rich Text Format formatting items derive.
	/// </summary>
	public abstract class RichTextMarkupItem : ICloneable
	{
		public class RichTextMarkupItemCollection
			: System.Collections.ObjectModel.Collection<RichTextMarkupItem>
		{
			private RichTextMarkupItemGroup mvarParent = null;
			public RichTextMarkupItemCollection(RichTextMarkupItemGroup parent = null)
			{
				mvarParent = parent;
			}

			protected override void InsertItem(int index, RichTextMarkupItem item)
			{
				base.InsertItem(index, item);
				item.Parent = mvarParent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
			protected override void SetItem(int index, RichTextMarkupItem item)
			{
				this[index].Parent = null;
				base.SetItem(index, item);
				item.Parent = mvarParent;
			}
			protected override void ClearItems()
			{
				foreach (RichTextMarkupItem item in this)
				{
					item.Parent = null;
				}
				base.ClearItems();
			}
		}

		public abstract object Clone();

		public RichTextMarkupItemGroup Parent { get; set; }
	}
}
