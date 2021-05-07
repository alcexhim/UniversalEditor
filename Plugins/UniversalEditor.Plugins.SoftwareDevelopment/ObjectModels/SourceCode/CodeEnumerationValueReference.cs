using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
	public class CodeEnumerationValueReference : CodeElementReference
	{
		private string[] mvarObjectName = new string[0];
		public string[] ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

		private string mvarValueName = String.Empty;
		public string ValueName { get { return mvarValueName; } set { mvarValueName = value; } }

		public CodeEnumerationValueReference(string[] objectName, string valueName)
		{
			mvarObjectName = objectName;
			mvarValueName = valueName;
		}
	}
}
