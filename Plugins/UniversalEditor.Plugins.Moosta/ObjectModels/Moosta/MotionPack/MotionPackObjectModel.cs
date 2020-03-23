using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Moosta.MotionPack
{
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
