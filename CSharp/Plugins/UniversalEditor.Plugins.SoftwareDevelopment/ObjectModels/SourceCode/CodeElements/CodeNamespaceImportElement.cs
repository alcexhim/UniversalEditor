using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    /// <summary>
    /// Represents an import of a namespace
    /// </summary>
    public class CodeNamespaceImportElement : CodeElement
    {
        public CodeNamespaceImportElement()
        {
        }
        public CodeNamespaceImportElement(string[] namespaceName, string objectName = null)
        {
            mvarNamespaceName = namespaceName;
            mvarObjectName = objectName;
        }

        private string[] mvarNamespaceName = new string[0];
        public string[] NamespaceName { get { return mvarNamespaceName; } set { mvarNamespaceName = value; } }

        private string mvarObjectName = null;
        public string ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }
    }
}
