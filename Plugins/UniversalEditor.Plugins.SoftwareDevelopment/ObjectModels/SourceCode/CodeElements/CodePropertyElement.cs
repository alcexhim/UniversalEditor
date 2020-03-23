using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodePropertyElement : CodeElement, INamedCodeElement, IAccessModifiableCodeElement
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        public string GetFullName(string separator = ".")
        {
            return CodeElement.GetFullName(this, separator);
        }

        private CodeAccessModifiers mvarAccessModifiers = CodeAccessModifiers.None;
        public CodeAccessModifiers AccessModifiers { get { return mvarAccessModifiers; } set { mvarAccessModifiers = value; } }

        private CodeMethodElement mvarGetMethod = null;
        public CodeMethodElement GetMethod { get { return mvarGetMethod; } set { mvarGetMethod = value; } }
        private CodeMethodElement mvarSetMethod = null;
        public CodeMethodElement SetMethod { get { return mvarSetMethod; } set { mvarSetMethod = value; } }

        private string mvarDataType = String.Empty;
        public string DataType { get { return mvarDataType; } set { mvarDataType = value; } }

        private bool mvarIsAbstract = false;
        public bool IsAbstract { get { return mvarIsAbstract; } set { mvarIsAbstract = value; } }
        private bool mvarIsVirtual = false;
        public bool IsVirtual { get { return mvarIsVirtual; } set { mvarIsVirtual = value; } }
        private bool mvarIsOverriding = false;
        public bool IsOverriding { get { return mvarIsOverriding; } set { mvarIsOverriding = value; } }

        private CodeVariableElement.CodeVariableElementCollection mvarParameters = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection Parameters { get { return mvarParameters; } }

		private bool mvarAutoGenerateGetMethod = false;
		public bool AutoGenerateGetMethod { get { return mvarAutoGenerateGetMethod; } set { mvarAutoGenerateGetMethod = value; } }

		private bool mvarAutoGenerateSetMethod = false;
		public bool AutoGenerateSetMethod { get { return mvarAutoGenerateSetMethod; } set { mvarAutoGenerateSetMethod = value; } }

		private bool mvarIsStatic = false;
		public bool IsStatic { get { return mvarIsStatic; } set { mvarIsStatic = value; } }
	}
}
