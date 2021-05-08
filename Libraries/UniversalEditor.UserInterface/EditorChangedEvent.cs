//
//  EditorChangedEvent.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2021 Mike Becker's Software
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
namespace UniversalEditor.UserInterface
{
	public class EditorChangedEventArgs : EventArgs
	{
		public MainWindow Window { get; } = null;
		public Editor PreviousEditor { get; } = null;
		public Editor CurrentEditor { get; } = null;

		public EditorChangedEventArgs(MainWindow window, Editor previousEditor, Editor currentEditor)
		{
			Window = window;
			PreviousEditor = previousEditor;
			CurrentEditor = currentEditor;
		}
	}
}
