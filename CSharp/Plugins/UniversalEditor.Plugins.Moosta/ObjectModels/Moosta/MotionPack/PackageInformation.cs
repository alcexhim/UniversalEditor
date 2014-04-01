using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Moosta.MotionPack
{
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
