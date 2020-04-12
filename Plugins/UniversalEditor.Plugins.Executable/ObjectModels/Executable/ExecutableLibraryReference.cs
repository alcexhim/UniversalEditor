//
//  ExecutableLibraryReference.cs - represents a reference to another library
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

namespace UniversalEditor.ObjectModels.Executable
{
	/// <summary>
	/// Represents a reference to another library.
	/// </summary>
	public class ExecutableLibraryReference : ICloneable
	{
		public class ExecutableLibraryReferenceCollection
			: System.Collections.ObjectModel.Collection<ExecutableLibraryReference>
		{
			public ExecutableLibraryReference Add(int libraryID, int minVer, int numberOfTimesIncluded)
			{
				ExecutableLibraryReference r = new ExecutableLibraryReference();
				r.ProductID = libraryID;
				r.MinimumVersion = minVer;
				r.Count = numberOfTimesIncluded;
				base.Add(r);
				return r;
			}
		}

		private int mvarProductID = 0;
		public int ProductID { get { return mvarProductID; } set { mvarProductID = value; } }

		private int mvarMinimumVersion = 0;
		public int MinimumVersion { get { return mvarMinimumVersion; } set { mvarMinimumVersion = value; } }

		private int mvarCount = 0;
		public int Count { get { return mvarCount; } set { mvarCount = value; } }

		public override string ToString()
		{
			return "Product ID: " + mvarProductID.ToString() + ", minimum version " + mvarMinimumVersion.ToString() + ", count: " + mvarCount.ToString();
		}

		public object Clone()
		{
			ExecutableLibraryReference clone = new ExecutableLibraryReference();
			clone.Count = mvarCount;
			clone.MinimumVersion = mvarMinimumVersion;
			clone.ProductID = mvarProductID;
			return clone;
		}
	}
}
