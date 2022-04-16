//
//  GenericBrowserPopup.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019
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
using System.Collections.ObjectModel;

using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Keyboard;

namespace UniversalEditor.UserInterface.Dialogs
{
	public class GenericBrowserPopup<TObj, TRef> : SearchableDropdownListDialog
		where TObj : class, References<TRef>
		where TRef : class, ReferencedBy<TObj>
	{
		public Collection<TRef> AvailableObjects { get; } = new Collection<TRef>();
		public TObj SelectedObject { get; set; } = default(TObj);

		protected override void UpdateSearchInternal(string query)
		{
			base.UpdateSearchInternal(query);

			foreach (TRef item in AvailableObjects)
			{
				bool itemShouldFilter = false;
				string[] details = item.GetDetails();
				foreach (string detail in details)
				{
					if (detail == null) continue;
					if (detail.ToLower().Trim().Contains(query.ToLower().Trim()))
					{
						itemShouldFilter = true;
						break;
					}
				}
				if (String.IsNullOrEmpty(query.Trim()) || itemShouldFilter)
				{
					AddObjectToList(item);
				}
			}
		}

		private void AddObjectToList(TRef itmr)
		{
			string[] details = itmr.GetDetails();
			List<TreeModelRowColumn> columns = new List<TreeModelRowColumn>();

			for (int i = 0; i < details.Length; i++)
			{
				string str = details[i];
				if (String.IsNullOrEmpty(str))
				{
					if (i == 0)
					{
						str = itmr.ToString();
					}
					else
					{
						str = String.Empty;
					}
				}

				columns.Add(CreateColumn(i, str));
			}

			TreeModelRow lvi = new TreeModelRow(columns.ToArray());
			lvi.SetExtraData<TRef>("TRef", itmr);
			AddRow(lvi);
		}

		protected override void SelectRow(TreeModelRow row)
		{
			base.SelectRow(row);
			if (row == null)
			{
				SelectedObject = null;
			}
			else
			{
				SelectedObject = row.GetExtraData<TRef>("TRef")?.Create();
			}
		}
	}
}
