//
//  RibbonControl.cs
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

namespace MBS.Framework.UserInterface.Controls.Ribbon
{
	namespace Native
	{
		public interface IRibbonControlImplementation
		{
			bool GetExpanded();
			void SetExpanded(bool value);
		}
	}
	public class RibbonControl : SystemControl
	{
		public RibbonTab.RibbonTabCollection Tabs { get; } = new RibbonTab.RibbonTabCollection();

		private bool mvarExpanded = true;
		public bool Expanded {
			get { return mvarExpanded; }
			set {
				mvarExpanded = value;
				(this.ControlImplementation as Native.IRibbonControlImplementation).SetExpanded(value);
			}
		}

		internal protected override void OnMouseDoubleClick (MBS.Framework.UserInterface.Input.Mouse.MouseEventArgs e)
		{
			base.OnMouseDoubleClick (e);

			this.Expanded = !this.Expanded;
		}
	}
}

