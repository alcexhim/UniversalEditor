//
//  ProgramGroupAction.cs - the action which creates program groups (or Start Menu entries) on the user's computer during the installation process
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

namespace UniversalEditor.ObjectModels.Setup.ArkAngles.Actions
{
	/// <summary>
	/// The action which creates program groups (or Start Menu entries) on the user's computer during the installation process.
	/// </summary>
	public class ProgramGroupAction : Action
	{
		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the Program Group to create.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private ProgramGroupShortcut.ProgramGroupShortcutCollection mvarShortcuts = new ProgramGroupShortcut.ProgramGroupShortcutCollection();
		/// <summary>
		/// The shortcuts added to this Program Group.
		/// </summary>
		public ProgramGroupShortcut.ProgramGroupShortcutCollection Shortcuts { get { return mvarShortcuts; } }

		public override object Clone()
		{
			ProgramGroupAction clone = new ProgramGroupAction();
			foreach (ProgramGroupShortcut item in mvarShortcuts)
			{
				clone.Shortcuts.Add(item.Clone() as ProgramGroupShortcut);
			}
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
