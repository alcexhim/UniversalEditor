using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.ObjectModels.SourceCode.SearchExpressions
{
    public class CodeMethodCallActionSearch
    {
        private string[] mvarObjectName = new string[0];
        public string[] ObjectName { get { return mvarObjectName; } set { mvarObjectName = value; } }

        private string mvarMethodName = String.Empty;
        public string MethodName { get { return mvarMethodName; } set { mvarMethodName = value; } }

        private CodeMethodSearchParameter.CodeMethodSearchParameterCollection mvarParameters = new CodeMethodSearchParameter.CodeMethodSearchParameterCollection();
        public CodeMethodSearchParameter.CodeMethodSearchParameterCollection Parameters { get { return mvarParameters; } }

        public CodeMethodCallActionSearch(string[] objectName, string methodName)
        {
            mvarObjectName = objectName;
            mvarMethodName = methodName;
        }
    }
    public class CodeMethodSearchParameter
    {
        public class CodeMethodSearchParameterCollection
            : System.Collections.ObjectModel.Collection<CodeMethodSearchParameter>
        {
        }
    }
    public class CodeMethodSearchParameterValue : CodeMethodSearchParameter
    {
        private CodeLiteralElement mvarValue = new CodeLiteralElement(null);
        public CodeLiteralElement Value { get { return mvarValue; } set { mvarValue = value; } }

        public CodeMethodSearchParameterValue(CodeLiteralElement value)
        {
            mvarValue = value;
        }
    }
    public class CodeMethodSearchParameterReference : CodeMethodSearchParameter
    {

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        private CodeLiteralElement mvarDefaultValue = new CodeLiteralElement(null);
        public CodeLiteralElement DefaultValue { get { return mvarDefaultValue; } set { mvarDefaultValue = value; } }

        public CodeMethodSearchParameterReference(string name, CodeLiteralElement defaultValue = null)
        {
            mvarName = name;
            if (defaultValue != null) mvarDefaultValue = defaultValue;
        }
    }
}
