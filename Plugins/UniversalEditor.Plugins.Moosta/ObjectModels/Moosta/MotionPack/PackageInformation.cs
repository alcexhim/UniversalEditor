//
//  PackageInformation.cs - provides metadata information for a Moosta OMP package
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

namespace UniversalEditor.ObjectModels.Moosta.MotionPack
{
	/// <summary>
	/// Provides metadata information for a Moosta OMP package.
	/// </summary>
	public class PackageInformation
	{
		public class PackageInformationCollection
			: System.Collections.ObjectModel.Collection<PackageInformation>
		{

		}

		private int mvarLanguageID = 0;
		public int LanguageID { get { return mvarLanguageID; } set { mvarLanguageID = value; } }

		private string mvarPackageName = String.Empty;
		public string PackageName { get { return mvarPackageName; } set { mvarPackageName = value; } }

		private string mvarPackageDescription = String.Empty;
		public string PackageDescription { get { return mvarPackageDescription; } set { mvarPackageDescription = value; } }
	}
}
