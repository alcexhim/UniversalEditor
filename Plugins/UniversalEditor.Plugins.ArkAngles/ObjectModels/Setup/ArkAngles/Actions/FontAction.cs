//
//  FontAction.cs - the action which installs a font to the user's computer during the installation process
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
	/// The action which installs a font to the user's computer during the installation process.
	/// </summary>
	public class FontAction : Action
	{
		private string mvarFileName = String.Empty;
		/// <summary>
		/// The file name of the font to install.
		/// </summary>
		public string FileName { get { return mvarFileName; } set { mvarFileName = value; } }

		private string mvarTitle = String.Empty;
		/// <summary>
		/// The title of the font to install.
		/// </summary>
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public override object Clone()
		{
			FontAction clone = new FontAction();
			clone.FileName = (mvarFileName.Clone() as string);
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
