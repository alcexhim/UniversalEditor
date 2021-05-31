//
//  EditorSelection.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using System.Collections.Generic;

namespace UniversalEditor
{
	public abstract class Selection
	{
		public class SelectionCollection
			: System.Collections.ObjectModel.ObservableCollection<Selection>
		{
		}

		public abstract object Content { get; set; }

		protected abstract void DeleteInternal();

		/// <summary>
		/// Removes the selected content represented by this <see cref="Selection" /> from the <see cref="ObjectModel" />.
		/// </summary>
		public void Delete()
		{
			DeleteInternal();
			Content = null;
		}
	}
	public abstract class Selection<TObjectModel, TSelection> : Selection where TObjectModel : ObjectModel
	{
		public override object Content
		{
			get
			{
				if (SelectedItems.Count == 0)
					return null;
				if (SelectedItems.Count == 1)
					return SelectedItems[0];
				return SelectedItems.ToArray();
			}
			set
			{
				if (value == null)
				{
					SelectedItems.Clear();
				}
				else if (value is TSelection)
				{
					SelectedItems.Clear();
					SelectedItems.Add((TSelection)value);
				}
				else if (value is TSelection[])
				{
					SelectedItems = new List<TSelection>((TSelection[])value);
				}
			}
		}

		public TObjectModel ObjectModel { get; private set; } = default(TObjectModel);
		public TSelection SelectedItem { get { return SelectedItems[0]; } set { SelectedItems.Clear(); SelectedItems.Add(value); } }
		public List<TSelection> SelectedItems { get; private set; } = new List<TSelection>();

		protected Selection(TObjectModel objectModel, TSelection selectedItem)
		{
			ObjectModel = objectModel;
			SelectedItem = selectedItem;
		}
		protected Selection(TObjectModel objectModel, TSelection[] selectedItems)
		{
			ObjectModel = objectModel;
			SelectedItems = new List<TSelection>(selectedItems);
		}
	}
}
