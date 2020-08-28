//
//  Row.cs
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
	public class Row
	{
		public class RowCollection
		{
			private System.Collections.Generic.Dictionary<int, Row> _this = new System.Collections.Generic.Dictionary<int, Row>();

			public Sheet Parent { get; private set; } = null;

			public RowCollection(Sheet parent)
			{
				Parent = parent;
			}

			public Row this[int index]
			{
				get
				{
					if (!_this.ContainsKey(index))
					{
						_this[index] = new Row(index);
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

		private Row(int index)
		{
			Index = index;
		}

		public int Index { get; private set; } = 0;
		public Sheet Parent { get; private set; } = null;

		public int? Height { get; set; } = null;

		private string _Title = null;
		public string Title
		{
			get
			{
				if (_Title == null)
					return (Index + 1).ToString();
				return _Title;
			}
			set
			{
				_Title = value;
			}
		}
	}
}
