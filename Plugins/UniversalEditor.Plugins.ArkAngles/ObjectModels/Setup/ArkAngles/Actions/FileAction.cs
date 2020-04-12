//
//  FileAction.cs - the action which copies a file to the user's computer during the installation process
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
	/// The action which copies a file to the user's computer during the installation process.
	/// </summary>
	public class FileAction : Action
	{
		private string mvarSourceFileName = String.Empty;
		/// <summary>
		/// The location of the source file to copy.
		/// </summary>
		public string SourceFileName { get { return mvarSourceFileName; } set { mvarSourceFileName = value; } }

		private string mvarDestinationFileName = String.Empty;
		/// <summary>
		/// The location in which to place the copied file. If a directory is specified, the file name from the source file will be used.
		/// </summary>
		public string DestinationFileName { get { return mvarDestinationFileName; } set { mvarDestinationFileName = value; } }

		private int mvarFileSize = 0;
		/// <summary>
		/// The size of the file on disk.
		/// </summary>
		public int FileSize { get { return mvarFileSize; } set { mvarFileSize = value; } }

		private string mvarDescription = String.Empty;
		public string Description {  get { return mvarDescription; } set { mvarDescription = value; } }

		public override object Clone()
		{
			FileAction clone = new FileAction();
			clone.Description = (mvarDescription.Clone() as string);
			clone.DestinationFileName = (mvarDestinationFileName.Clone() as string);
			clone.FileSize = mvarFileSize;
			clone.SourceFileName = (mvarSourceFileName.Clone() as string);
			return clone;
		}
	}
}
