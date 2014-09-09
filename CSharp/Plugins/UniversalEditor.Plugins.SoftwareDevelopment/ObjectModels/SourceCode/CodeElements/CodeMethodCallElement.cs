using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeMethodCallElement : CodeElementContainerElement
    {
		public CodeMethodCallElement()
		{
		}
		public CodeMethodCallElement(string objectName, string methodName, params CodeVariableElement[] variables) : this(objectName.Split(new char[] { '.' }), methodName, variables) { }
		public CodeMethodCallElement(string methodName, params CodeVariableElement[] variables) : this((string[])null, methodName, variables) { }
		public CodeMethodCallElement(string[] objectName, string methodName, params CodeVariableElement[] variables)
		{
			mvarObjectName = objectName;
			mvarMethodName = methodName;
			foreach (CodeVariableElement cve in variables)
			{
				mvarVariables.Add(cve);
			}
		}

		private string[] mvarObjectName = new string[0];
        public string[] ObjectName
        {
            get { return mvarObjectName; }
            set { mvarObjectName = value; }
        }
        private string mvarMethodName = String.Empty;
        public string MethodName
        {
            get { return mvarMethodName; }
            set { mvarMethodName = value; }
        }
        private CodeVariableElement.CodeVariableElementCollection mvarVariables = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection Parameters
        {
            get { return mvarVariables; }
        }
    }
}
