//
//  FinishDialog.cs - describes the dialog displayed when the installation process has finished
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

namespace UniversalEditor.ObjectModels.RebelSoftware.InstallationScript.Dialogs
{
	/// <summary>
	/// Describes the dialog displayed when the installation process has finished.
	/// </summary>
	public class FinishDialog : Dialog
	{
		/// <summary>
		/// The file name of the Readme file to offer upon installation completion, relative to the installation directory.
		/// </summary>
		public string ReadmeFileName { get; set; } = String.Empty;

		/// <summary>
		/// True if a reboot is required after installation.
		/// </summary>
		public bool RequireReboot { get; set; } = false;

		/// <summary>
		/// The file name of the executable file to launch upon installation completion, relative to the installation directory.
		/// </summary>
		public string ExecutableFileName { get; set; } = String.Empty;

		/// <summary>
		/// The file name of the executable file to launch upon installation completion, relative to the installation directory.
		/// </summary>
		public string ExecutableWorkingDirectory { get; set; } = String.Empty;

		public override object Clone()
		{
			FinishDialog clone = new FinishDialog();
			clone.ReadmeFileName = (ReadmeFileName.Clone() as string);
			clone.RequireReboot = RequireReboot;
			clone.ExecutableFileName = (ExecutableFileName.Clone() as string);
			clone.ExecutableWorkingDirectory = (ExecutableWorkingDirectory.Clone() as string);
			return clone;
		}
	}
}
