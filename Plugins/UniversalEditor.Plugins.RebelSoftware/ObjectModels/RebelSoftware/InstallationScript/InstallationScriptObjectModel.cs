//
//  InstallationScriptObjectModel.cs - provides an ObjectModel for manipulating Rebel Software installation scripts
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

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Rebel Software installation scripts.
	/// </summary>
	public class InstallationScriptObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Setup", "Rebel Software", "Installation Script" };
			}
			return _omr;
		}

		private string mvarProductName = String.Empty;
		public string ProductName { get { return mvarProductName; } set { mvarProductName = value; } }

		private Version mvarProductVersion = new Version(1, 0);
		public Version ProductVersion { get { return mvarProductVersion; } set { mvarProductVersion = value; } }

		private string mvarBackgroundImageFileName = String.Empty;
		public string BackgroundImageFileName { get { return mvarBackgroundImageFileName; } set { mvarBackgroundImageFileName = value; } }

		private Dialog.DialogCollection mvarDialogs = new Dialog.DialogCollection();
		public Dialog.DialogCollection Dialogs { get { return mvarDialogs; } }

		private string mvarStartMenuDirectoryName = String.Empty;
		public string StartMenuDirectoryName { get { return mvarStartMenuDirectoryName; } set { mvarStartMenuDirectoryName = value; } }

		public override void Clear()
		{
			mvarProductName = String.Empty;
			mvarProductVersion = new Version(1, 0);
			mvarBackgroundImageFileName = String.Empty;
			mvarDialogs.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			InstallationScriptObjectModel clone = (where as InstallationScriptObjectModel);
			if (clone == null) throw new InvalidOperationException();

			clone.ProductName = (mvarProductName.Clone() as string);
			clone.ProductVersion = (mvarProductVersion.Clone() as Version);
			clone.BackgroundImageFileName = (mvarBackgroundImageFileName.Clone() as string);
			foreach (Dialog dialog in mvarDialogs)
			{
				clone.Dialogs.Add(dialog.Clone() as Dialog);
			}
		}
	}
}
