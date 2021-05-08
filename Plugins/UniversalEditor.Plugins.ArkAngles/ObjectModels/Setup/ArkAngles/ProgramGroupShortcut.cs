//
//  ProgramGroupShortcut.cs - represents a program group (or Start Menu shortcut) to install to the user's computer during the installation process
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

namespace UniversalEditor.ObjectModels.Setup.ArkAngles
{
	/// <summary>
	/// Represents a program group (or Start Menu shortcut) to install to the
	/// user's computer during the installation process as part of a
	/// <see cref="Actions.ProgramGroupAction" /> command.
	/// </summary>
	public class ProgramGroupShortcut : ICloneable
	{
		public class ProgramGroupShortcutCollection
			: System.Collections.ObjectModel.Collection<ProgramGroupShortcut>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private string mvarFileName = String.Empty;
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private string mvarIconFileName = null;
		/// <summary>
		/// The file name of the icon to display in the shortcut.
		/// </summary>
		public string IconFileName { get { return mvarIconFileName; } set { mvarIconFileName = value; } }

		private int? mvarIconIndex = null;
		/// <summary>
		/// The index of the icon to display in the shortcut.
		/// </summary>
		public int? IconIndex { get { return mvarIconIndex; } set { mvarIconIndex = value; } }

		public object Clone()
		{
			ProgramGroupShortcut clone = new ProgramGroupShortcut();
			clone.FileName = (mvarFileName.Clone() as string);
			clone.IconFileName = (mvarIconFileName.Clone() as string);
			clone.IconIndex = mvarIconIndex;
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
