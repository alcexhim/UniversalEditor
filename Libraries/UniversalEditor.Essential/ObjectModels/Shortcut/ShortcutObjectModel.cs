//
//  ShortcutObjectModel.cs - provides an ObjectModel for manipulating a shortcut or symbolic link to a file on a file system
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

namespace UniversalEditor.ObjectModels.Shortcut
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating a shortcut or symbolic link to a file on a file system.
	/// </summary>
	public class ShortcutObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Description = "Allows the user to find a file or resource located in a different directory or folder from the place where the shortcut is located.";
				_omr.Path = new string[] { "General", "Shortcut" };
			}
			return _omr;
		}

		public override void Clear()
		{
			Comment = String.Empty;
			ExecutableArguments.Clear();
			ExecutableFileName = String.Empty;
			IconFileName = String.Empty;
			RunInTerminal = false;
			WorkingDirectory = String.Empty;
		}

		public override void CopyTo(ObjectModel where)
		{
			ShortcutObjectModel shortcut = (where as ShortcutObjectModel);
			shortcut.Comment = (Comment.Clone() as string);
			foreach (string s in ExecutableArguments)
			{
				shortcut.ExecutableArguments.Add(s.Clone() as string);
			}
			shortcut.ExecutableFileName = (ExecutableFileName.Clone() as string);
			shortcut.IconFileName = (IconFileName.Clone() as string);
			shortcut.RunInTerminal = RunInTerminal;
			shortcut.WorkingDirectory = (WorkingDirectory.Clone() as string);
		}

		/// <summary>
		/// Tooltip for the entry, for example "View sites on the Internet". The value should not
		/// be redundant with the shortcut title.
		/// </summary>
		public string Comment { get; set; } = String.Empty;
		/// <summary>
		/// File name or known icon name of the icon to display in the file manager, menus, etc.
		/// </summary>
		public string IconFileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets or sets the full path to the program file pointed to by this <see cref="ShortcutObjectModel" />.
		/// </summary>
		/// <value>The full path to the program file pointed to by this <see cref="ShortcutObjectModel" />.</value>
		public string ExecutableFileName { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="string" /> instances representing the arguments passed into the command line when executing the program.
		/// </summary>
		/// <value>The arguments passed into the command line when executing the program.</value>
		public System.Collections.Specialized.StringCollection ExecutableArguments { get; } = new System.Collections.Specialized.StringCollection();
		/// <summary>
		/// The directory in which to run the program, if different than the program's location.
		/// </summary>
		public string WorkingDirectory { get; set; } = String.Empty;
		/// <summary>
		/// Whether the program runs in a terminal window.
		/// </summary>
		public bool RunInTerminal { get; set; } = false;
	}
}
