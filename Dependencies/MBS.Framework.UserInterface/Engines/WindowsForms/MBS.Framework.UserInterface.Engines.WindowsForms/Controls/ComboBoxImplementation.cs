//
//  ComboBoxImplementation.cs
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
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Controls.Native;

namespace MBS.Framework.UserInterface.Engines.WindowsForms.Controls
{
	[ControlImplementation(typeof(ComboBox))]
	public class ComboBoxImplementation : WindowsFormsNativeImplementation, IComboBoxNativeImplementation
	{
		public ComboBoxImplementation (Engine engine, Control control)
			: base(engine, control)
		{
		}

		private TreeModel _TreeModel = null;
		public TreeModel GetModel()
		{
			return _TreeModel;
		}

		public void SetModel(TreeModel value)
		{
			System.Windows.Forms.ComboBox handle = ((Engine.GetHandleForControl(Control) as WindowsFormsNativeControl).Handle as System.Windows.Forms.ComboBox);
			UpdateModel(handle, value);
			_TreeModel = value;
		}

		private bool mvarReadOnly = false;
		public bool GetReadOnly()
		{
			return mvarReadOnly;
		}

		public void SetReadOnly(bool value)
		{
			mvarReadOnly = value;
		}

		private Dictionary<object, TreeModelRow> TreeModelRowForItem = new Dictionary<object, TreeModelRow>();
		public TreeModelRow GetSelectedItem()
		{
			System.Windows.Forms.ComboBox handle = ((Handle as WindowsFormsNativeControl).Handle as System.Windows.Forms.ComboBox);
			if (handle != null)
			{
				return TreeModelRowForItem[handle.SelectedItem];
			}
			return null;
		}

		public void SetSelectedItem(TreeModelRow value)
		{

		}

		protected virtual void OnChanged(EventArgs e)
		{
			InvokeMethod((Control as ComboBox), "OnChanged", e);
		}

		protected override NativeControl CreateControlInternal (Control control)
		{
			System.Windows.Forms.ComboBox handle = new System.Windows.Forms.ComboBox();
			handle.FlatStyle = System.Windows.Forms.FlatStyle.System;
			handle.SelectedIndexChanged += handle_SelectedIndexChanged;

			ComboBox ctl = (control as ComboBox);
			if (ctl.ReadOnly)
			{
				handle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			}
			else
			{
				handle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
			}

			if (ctl.Model != null)
			{
				UpdateModel(handle, ctl.Model);
			}

			return new WindowsFormsNativeControl(handle);
		}

		private void UpdateModel(System.Windows.Forms.ComboBox handle, TreeModel model)
		{

			DefaultTreeModel dtm = (model as DefaultTreeModel);
			for (int i = 0; i < dtm.Rows.Count; i++)
			{
				if (dtm.Rows[i].RowColumns.Count > 0)
				{
					TreeModelRowForItem.Add(dtm.Rows[i].RowColumns[0].Value, dtm.Rows[i]);
					handle.Items.Add(dtm.Rows[i].RowColumns[0].Value);
				}
			}
		}

		void handle_SelectedIndexChanged(object sender, EventArgs e)
		{
			OnChanged(EventArgs.Empty);
		}

	}
}

