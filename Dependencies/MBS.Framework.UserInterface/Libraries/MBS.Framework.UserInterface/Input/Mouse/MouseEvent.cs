//
//  MouseEvent.cs
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
using System.ComponentModel;
using MBS.Framework.Drawing;
using MBS.Framework.UserInterface.Input.Keyboard;

namespace MBS.Framework.UserInterface.Input.Mouse
{
	public delegate void MouseEventHandler(object sender, MouseEventArgs e);
	public class MouseEventArgs : CancelEventArgs
	{
		public double X { get; private set; }
		public double Y { get; private set; }

		public Vector2D Location { get { return new Vector2D((int)X, (int)Y); } }
		
		public MouseButtons Buttons { get; private set; }
		public KeyboardModifierKey ModifierKeys { get; private set; }

		public bool Handled { get; set; } = false;
		
		public MouseEventArgs(double x, double y, MouseButtons buttons, KeyboardModifierKey modifierKeys)
		{
			X = x;
			Y = y;
			Buttons = buttons;
			ModifierKeys = modifierKeys;
		}
	}
}
