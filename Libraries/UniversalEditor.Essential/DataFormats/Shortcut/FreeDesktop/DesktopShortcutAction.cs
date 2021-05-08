//
//  DesktopShortcutAction.cs - represents an action in a FreeDesktop shortcut file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.Shortcut.FreeDesktop
{
	/// <summary>
	/// Represents an action in a FreeDesktop shortcut file.
	/// </summary>
	public class DesktopShortcutAction
	{
		public class DesktopShortcutActionCollection
			: System.Collections.ObjectModel.Collection<DesktopShortcutAction>
		{

		}

		/// <summary>
		/// Gets or sets the unique identifying name of the action.
		/// </summary>
		/// <value>The unique identifying name of the action.</value>
		public string Name { get; set; } = String.Empty;
		/// <summary>
		/// Label that will be shown to the user. Since actions are always shown in the context of
		/// a specific application (that is, as a submenu of a launcher), this only needs to be
		/// unambiguous within one application and should not include the application name.
		/// </summary>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Icon to be shown togheter with the action. If the name is an absolute path, the given
		/// file will be used. If the name is not an absolute path, the algorithm described in the
		/// Icon Theme Specification will be used to locate the icon. Implementations may choose
		/// to ignore it.
		/// </summary>
		public string IconFileName { get; set; } = String.Empty;
		/// <summary>
		/// Program to execute for this action. See the Exec key for details on how this key
		/// works. The Exec key is required if DBusActivatable is not set to true in the main
		/// desktop entry group. Even if DBusActivatable is true, Exec should be specified for
		/// compatibility with implementations that do not understand DBusActivatable.
		/// </summary>
		public string ExecutableFileName { get; set; } = String.Empty;
		/// <summary>
		/// Arguments passed into the associated program when it is executed.
		/// </summary>
		public System.Collections.Specialized.StringCollection ExecutableArguments { get; } = new System.Collections.Specialized.StringCollection();
	}
}
