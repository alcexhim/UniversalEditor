//
//  Cell.cs
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
	public class Cell
	{
		private Cell(int row, int column)
		{
			RowIndex = row;
			ColumnIndex = column;
		}

		public int RowIndex { get; private set; } = 0;
		public int ColumnIndex { get; private set; } = 0;
		public Sheet Parent { get; private set; } = null;

		public class CellCollection
		{
			private Sheet _parent = null;
			public CellCollection(Sheet parent)
			{
				_parent = parent;
			}

			private System.Collections.Generic.Dictionary<CellIndex, Cell> _this = new System.Collections.Generic.Dictionary<CellIndex, Cell>();
			public Cell this[int row, int column]
			{
				get
				{
					if (!_this.ContainsKey(new CellIndex(row, column)))
					{
						Cell c = new Cell(row, column);
						c.Parent = _parent;
						_this[new CellIndex(row, column)] = c;
					}
					return _this[new CellIndex(row, column)];
				}
			}
		}

		public object Value { get; set; } = null;
	}
}
