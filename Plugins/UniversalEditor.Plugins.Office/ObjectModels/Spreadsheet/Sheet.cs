//
//  Sheet.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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
namespace UniversalEditor.ObjectModels.Spreadsheet
{
	public class Sheet
	{

		public class SheetCollection
			: System.Collections.ObjectModel.Collection<Sheet>
		{
			private SpreadsheetObjectModel _parent = null;
			public SheetCollection(SpreadsheetObjectModel parent)
			{
				_parent = parent;
			}

			protected override void ClearItems()
			{
				for (int i = 0; i < Count; i++)
				{
					this[i].Parent = null;
				}
				base.ClearItems();
			}
			protected override void InsertItem(int index, Sheet item)
			{
				base.InsertItem(index, item);
				item.Parent = _parent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].Parent = null;
				base.RemoveItem(index);
			}
		}

		public Row.RowCollection Rows { get; private set; } = null;
		public Column.ColumnCollection Columns { get; private set; } = null;
		public Cell.CellCollection Cells { get; private set; } = null;

		public SpreadsheetObjectModel Parent { get; private set; } = null;

		public string Title { get; set; } = null;

		public Sheet()
		{
			Rows = new Row.RowCollection(this);
			Columns = new Column.ColumnCollection(this);
			Cells = new Cell.CellCollection(this);
		}
		public Sheet(string title) : this()
		{
			Title = title;
		}
	}
}
