//
//  ResizeEvent.cs
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
using MBS.Framework.Drawing;

namespace MBS.Framework.UserInterface
{
	public delegate void ResizingEventHandler(object sender, ResizingEventArgs e);
	public class ResizingEventArgs : System.ComponentModel.CancelEventArgs
	{
		public Dimension2D OldSize { get; private set; } = new Dimension2D();
		public Dimension2D NewSize { get; set; } = new Dimension2D();

		public ResizingEventArgs(Dimension2D oldSize, Dimension2D newSize)
		{
			OldSize = oldSize;
			NewSize = newSize;
		}
	}
	public delegate void ResizedEventHandler(object sender, ResizedEventArgs e);
	public class ResizedEventArgs : EventArgs
	{
		public Dimension2D OldSize { get; private set; } = new Dimension2D();
		public Dimension2D NewSize { get; private set; } = new Dimension2D();

		public ResizedEventArgs(Dimension2D oldSize, Dimension2D newSize)
		{
			OldSize = oldSize;
			NewSize = newSize;
		}
	}
}
