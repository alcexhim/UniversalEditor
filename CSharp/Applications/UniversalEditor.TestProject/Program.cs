using System;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Package.OpenPackagingConvention;
using UniversalEditor.ObjectModels.Package;

namespace UniversalEditor.TestProject
{
	class MainClass
	{
		public static void Main (string [] args)
		{
			string fileName = "/tmp/UETest/test.opc";

			PackageObjectModel om = new PackageObjectModel ();
			OPCDataFormat df = new OPCDataFormat ();

			Document.Save (om, df, new FileAccessor (fileName, true, true));
		}
	}
}
