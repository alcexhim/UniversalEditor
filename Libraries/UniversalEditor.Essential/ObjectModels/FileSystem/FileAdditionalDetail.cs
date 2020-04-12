//
//  FileAdditionalDetail.cs - represents an additional field of detail to display in a FileSystemObjectModel editor
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.FileSystem
{
	/// <summary>
	/// Represents an additional field of detail to display in a <see cref="FileSystemObjectModel" /> editor.
	/// </summary>
	public class FileAdditionalDetail
	{
		public class FileAdditionalDetailCollection
			: System.Collections.ObjectModel.Collection<FileAdditionalDetail>
		{
			private System.Collections.Generic.Dictionary<string, FileAdditionalDetail> _itemsByName = new System.Collections.Generic.Dictionary<string, FileAdditionalDetail>();
			public FileAdditionalDetail this[string name]
			{
				get
				{
					if (_itemsByName.ContainsKey(name))
						return _itemsByName[name];
					return null;
				}
			}
			public bool Contains(string name)
			{
				return _itemsByName.ContainsKey(name);
			}

			protected override void ClearItems()
			{
				base.ClearItems();
				_itemsByName.Clear();
			}
			protected override void InsertItem(int index, FileAdditionalDetail item)
			{
				base.InsertItem(index, item);
				_itemsByName[item.Name] = item;
			}
			protected override void RemoveItem(int index)
			{
				if (_itemsByName.ContainsKey(this[index].Name))
					_itemsByName.Remove(this[index].Name);
				base.RemoveItem(index);
			}

			public FileAdditionalDetail Add(string name, string title)
			{
				FileAdditionalDetail item = new FileAdditionalDetail();
				item.Name = name;
				item.Title = title;
				Add(item);
				return item;
			}
		}

		public string Name { get; set; } = null;
		public string Title { get; set; } = null;
	}
}
