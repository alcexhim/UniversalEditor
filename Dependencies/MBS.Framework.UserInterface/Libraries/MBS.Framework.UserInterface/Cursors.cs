//
//  Cursors.cs
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
namespace MBS.Framework.UserInterface
{
	public class Cursors
	{
		public static Cursor Default { get; } = new Cursor();
		public static Cursor Help { get; } = new Cursor();
		public static Cursor Pointer { get; } = new Cursor();
		public static Cursor ContextMenu { get; } = new Cursor();
		public static Cursor Progress { get; } = new Cursor();
		public static Cursor Wait { get; } = new Cursor();
		public static Cursor Cell { get; } = new Cursor();
		public static Cursor Crosshair { get; } = new Cursor();
		public static Cursor Text { get; } = new Cursor();
		public static Cursor VerticalText { get; } = new Cursor();
		public static Cursor Alias { get; } = new Cursor();
		public static Cursor Copy { get; } = new Cursor();
		public static Cursor NoDrop { get; } = new Cursor();
		public static Cursor Move { get; } = new Cursor();
		public static Cursor NotAllowed { get; } = new Cursor();
		public static Cursor Grab { get; } = new Cursor();
		public static Cursor Grabbing { get; } = new Cursor();
		public static Cursor AllScroll { get; } = new Cursor();
		public static Cursor ResizeColumn { get; } = new Cursor();
		public static Cursor ResizeRow { get; } = new Cursor();
		public static Cursor ResizeN { get; } = new Cursor();
		public static Cursor ResizeE { get; } = new Cursor();
		public static Cursor ResizeS { get; } = new Cursor();
		public static Cursor ResizeW { get; } = new Cursor();
		public static Cursor ResizeNE { get; } = new Cursor();
		public static Cursor ResizeNW { get; } = new Cursor();
		public static Cursor ResizeSW { get; } = new Cursor();
		public static Cursor ResizeSE { get; } = new Cursor();
		public static Cursor ResizeEW { get; } = new Cursor();
		public static Cursor ResizeNS { get; } = new Cursor();
		public static Cursor ResizeNESW { get; } = new Cursor();
		public static Cursor ResizeNWSE { get; } = new Cursor();
		public static Cursor ZoomIn { get; } = new Cursor();
		public static Cursor ZoomOut { get; } = new Cursor();

		// stock cursors used by some editing applications, may need to be custom-drawn on many OSes
		public static Cursor Pencil { get; } = new Cursor();
		public static Cursor Eraser { get; } = new Cursor();
	}
}
