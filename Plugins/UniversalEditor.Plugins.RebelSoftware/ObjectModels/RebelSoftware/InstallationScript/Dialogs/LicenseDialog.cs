//
//  LicenseDialog.cs - describes the dialog containing the license agreement that must be accepted before the installation process can begin
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
	/// Describes the dialog containing the license agreement that must be accepted before the installation process can begin.
	/// </summary>
	public class LicenseDialog : Dialog
	{
		public string LicenseFileName { get; set; } = String.Empty;

		public override object Clone()
		{
			LicenseDialog clone = new LicenseDialog();
			clone.LicenseFileName = (LicenseFileName.Clone() as string);
			return clone;
		}
	}
}
