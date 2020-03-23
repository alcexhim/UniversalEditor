//
//  ComboBox.cs
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

namespace MBS.Framework.UserInterface.Controls
{
	namespace Native
	{
		public interface IComboBoxNativeImplementation
		{
			bool GetReadOnly();
			void SetReadOnly(bool value);

			TreeModel GetModel();
			void SetModel(TreeModel value);

			TreeModelRow GetSelectedItem();
			void SetSelectedItem(TreeModelRow value);
		}
	}
	public class ComboBox : Control
	{
		private bool mvarReadOnly = false;
		/// <summary>
		/// Determines if the current value in this <see cref="ComboBox" /> can be edited in a text box.
		/// </summary>
		/// <value><c>true</c> if values must be chosen from a list; <c>false</c> to enable user to type in custom values.</value>
		public bool ReadOnly
		{
			get
			{
				return mvarReadOnly;
			}
			set
			{
				Native.IComboBoxNativeImplementation impl = (this.ControlImplementation as Native.IComboBoxNativeImplementation);
				impl?.SetReadOnly(value);
				mvarReadOnly = value;
			}
		}

		private TreeModel mvarModel = null;
		public TreeModel Model
		{
			get
			{
				if (IsCreated)
				{
					Native.IComboBoxNativeImplementation impl = (this.ControlImplementation as Native.IComboBoxNativeImplementation);
					if (impl != null)
						mvarModel = impl.GetModel();
				}

				return mvarModel;
			}
			set
			{
				if (IsCreated)
				{
					Native.IComboBoxNativeImplementation impl = (this.ControlImplementation as Native.IComboBoxNativeImplementation);
					impl?.SetModel(value);
				}
				mvarModel = value;
			}
		}

		private TreeModelRow mvarSelectedItem = null;
		public TreeModelRow SelectedItem
		{
			get
			{
				Native.IComboBoxNativeImplementation impl = (this.ControlImplementation as Native.IComboBoxNativeImplementation);
				if (impl != null)
					mvarSelectedItem = impl.GetSelectedItem();

				return mvarSelectedItem;
			}
			set
			{
				bool changed = (mvarSelectedItem != value);

				Native.IComboBoxNativeImplementation impl = (this.ControlImplementation as Native.IComboBoxNativeImplementation);
				impl?.SetSelectedItem(value);
				mvarSelectedItem = value;

				if (changed && IsCreated)
					OnChanged(EventArgs.Empty);
			}
		}

		public event EventHandler Changed;
		protected virtual void OnChanged(EventArgs e)
		{
			Changed?.Invoke(this, e);
		}
	}
}

