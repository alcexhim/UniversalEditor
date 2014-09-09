using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElementReferences
{
    public class CodeElementEnumerationValueReference : CodeElementReference
    {
        private string[] mvarObjectName = new string[0];
        public string[] ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        private string mvarValueName = String.Empty;
        public string ValueName { get { return mvarValueName; } set { mvarValueName = value; } }

        public CodeElementEnumerationValueReference(string[] objectName, string valueName)
        {
            mvarObjectName = objectName;
            mvarValueName = valueName;
        }
    }
}
