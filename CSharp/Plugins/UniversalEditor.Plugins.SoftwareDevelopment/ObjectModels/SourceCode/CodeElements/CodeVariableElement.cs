using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeVariableElement : CodeElement, INamedCodeElement, IAccessModifiableCodeElement
    {
        public class CodeVariableElementCollection
            : System.Collections.ObjectModel.Collection<CodeVariableElement>
        {
            public CodeVariableElement Add(string VariableName, CodeDataType VariableDataType)
            {
                return Add(VariableName, VariableDataType, null);
            }
            public CodeVariableElement Add(string VariableName, CodeDataType VariableDataType, CodeElementReference VariableValue)
            {
                CodeVariableElement cve = new CodeVariableElement();
                cve.Name = VariableName;
                cve.DataType = VariableDataType;
                cve.Value = VariableValue;
                base.Add(cve);
                return cve;
            }
        }

        public CodeVariableElement()
        {
        }
        public CodeVariableElement(string name) : this(name, null)
        {
        }
        public CodeVariableElement(string name, string[] datatype)
        {
            mvarName = name;
            mvarDataType = datatype;
			mvarValue = null;
        }
		public CodeVariableElement(string name, string[] datatype, CodeElementReference value)
		{
            mvarName = name;
			mvarDataType = datatype;
			mvarValue = value;
		}
        public CodeVariableElement(string name, CodeDataType datatype, CodeElementReference value)
        {
            mvarName = name;
            mvarDataType = datatype;
            mvarValue = value;
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        public string GetFullName(string separator = ".")
        {
            return CodeElement.GetFullName(this, separator);
        }

        private CodeElementReference mvarValue = null;
        public CodeElementReference Value
        {
            get { return mvarValue; }
            set { mvarValue = value; }
        }

        private CodeDataType mvarDataType = CodeDataType.Empty;
        public CodeDataType DataType
        {
            get { return mvarDataType; }
            set { mvarDataType = value; }
        }

        private CodeAccessModifiers mvarAccessModifiers = CodeAccessModifiers.None;
        public CodeAccessModifiers AccessModifiers
        {
            get { return mvarAccessModifiers; }
            set { mvarAccessModifiers = value; }
        }

        private bool mvarPassByReference = false;
        public bool PassByReference
        {
            get { return mvarPassByReference; }
            set { mvarPassByReference = value; }
        }
    }
}
