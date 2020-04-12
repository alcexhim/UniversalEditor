//
//  MotionPackObjectModel.cs - provides an ObjectModel for manipulating Moosta OMP motion packages
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

using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Moosta.MotionPack
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating Moosta OMP motion packages.
	/// </summary>
	public class MotionPackObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Motion Pack";
			}
			return _omr;
		}

		private PackageInformation.PackageInformationCollection mvarPackageInformation = new PackageInformation.PackageInformationCollection();
		public PackageInformation.PackageInformationCollection PackageInformation { get { return mvarPackageInformation; } set { mvarPackageInformation = value; } }

		private List<string> mvarCopyrights = new List<string>();
		public List<string> Copyrights { get { return mvarCopyrights; } }

		public override void Clear()
		{

		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
