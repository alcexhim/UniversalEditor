using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Executable
{
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
