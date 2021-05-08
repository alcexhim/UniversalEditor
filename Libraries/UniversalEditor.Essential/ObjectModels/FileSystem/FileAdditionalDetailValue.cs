//
//  FileAdditionalDetailValue.cs - represents a value associated with an additional field of detail to display in a FileSystemObjectModel editor
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

using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Represents a value associated with an additional field of detail to display in a <see cref="FileSystemObjectModel" /> editor.
	/// </summary>
	public class FileAdditionalDetailValue
	{
		public class FileAdditionalDetailValueCollection
			: System.Collections.ObjectModel.Collection<FileAdditionalDetailValue>
		{
			private Dictionary<FileAdditionalDetail, FileAdditionalDetailValue> _itemsByDetail = new Dictionary<FileAdditionalDetail, FileAdditionalDetailValue>();
			public FileAdditionalDetailValue Add(FileAdditionalDetail detail, string value)
			{
				FileAdditionalDetailValue item = new FileAdditionalDetailValue();
				item.Detail = detail;
				item.Value = value;
				Add(item);
				return item;
			}

			public FileAdditionalDetailValue this[FileAdditionalDetail detail]
			{
				get
				{
					if (detail == null)
						return null;

					if (_itemsByDetail.ContainsKey(detail))
						return _itemsByDetail[detail];
					return null;
				}
			}
			public bool Contains(FileAdditionalDetail detail)
			{
				return _itemsByDetail.ContainsKey(detail);
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByDetail.Clear();
			}
			protected override void InsertItem(int index, FileAdditionalDetailValue item)
			{
				base.InsertItem(index, item);
				_itemsByDetail[item.Detail] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (_itemsByDetail.ContainsKey(this[index].Detail))
					_itemsByDetail.Remove(this[index].Detail);
				base.RemoveItem(index);
			}
		}

		private FileAdditionalDetailValue()
		{
		}

		public FileAdditionalDetail Detail { get; set; } = null;
		public string Value { get; set; } = null;
	}
}
