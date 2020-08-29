//
//  CellSizeInfo.cs
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
	public class Column
	{
		public class ColumnCollection
		{
			private System.Collections.Generic.Dictionary<int, Column> _this = new System.Collections.Generic.Dictionary<int, Column>();

			public Sheet Parent { get; private set; } = null;

			public ColumnCollection(Sheet parent)
			{
				Parent = parent;
			}

			public Column this[int index]
			{
				get
				{
					if (!_this.ContainsKey(index))
					{
						_this[index] = new Column(index);
						_this[index].Parent = Parent;
					}
					return _this[index];
				}
			}

			public bool Contains(int index)
			{
				return _this.ContainsKey(index);
			}
		}

		private Column(int index)
		{
			Index = index;
		}

		public int Index { get; private set; } = 0;
		public Sheet Parent { get; private set; } = null;

		public int? Width { get; set; } = null;

		private string _Title = null;
		public string Title
		{
			get
			{
				if (_Title == null)
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();

					int chri = (int)'A' + Index;

					sb.Append((char)((int)'A' + Index));
					return sb.ToString();
				}
				return _Title;
			}
			set { _Title = value; }
		}
	}
}
