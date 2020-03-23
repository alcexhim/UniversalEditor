//
//  StackSidebarPanel.cs
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
	public class StackSidebarPanel : ISupportsExtraData
	{
		public class StackSidebarPanelCollection
			: System.Collections.ObjectModel.Collection<StackSidebarPanel>
		{
		}

		public Control Control { get; set; } = null;

		#region ISupportsExtraData members
		private System.Collections.Generic.Dictionary<string, object> _ExtraData = new System.Collections.Generic.Dictionary<string, object>();
		public T GetExtraData<T>(string key, T defaultValue = default(T))
		{
			if (_ExtraData.ContainsKey (key)) {
				try {
					return (T)_ExtraData [key];
				} catch {
					return defaultValue;
				}
			}
			return defaultValue;
		}
		public void SetExtraData<T>(string key, T value)
		{
			_ExtraData [key] = value;
		}
		public object GetExtraData(string key, object defaultValue = null)
		{
			if (_ExtraData.ContainsKey (key)) {
				return _ExtraData [key];
			} else {
				return defaultValue;
			}
		}
		public void SetExtraData(string key, object value)
		{
			_ExtraData [key] = value;
		}
		#endregion
	}
}

