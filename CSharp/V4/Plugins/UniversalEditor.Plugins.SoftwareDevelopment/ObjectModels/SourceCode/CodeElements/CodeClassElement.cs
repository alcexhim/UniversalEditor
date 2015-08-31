using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode.CodeElements
{
    public class CodeClassElement : CodeElementContainerElement, INamedCodeElement, IAccessModifiableCodeElement
    {
        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } set { mvarName = value; } }

        public string GetFullName(string separator = ".")
        {
            CodeElementContainerElement parent = base.Parent;
            StringBuilder sb = new StringBuilder();
            while (parent != null)
            {
                if (parent is INamedCodeElement)
                {
                    sb.Append((parent as INamedCodeElement).Name);
                    sb.Append(separator);
                }
                parent = parent.Parent;
            }
            sb.Append(mvarName);
            return sb.ToString();
        }

        private CodeAccessModifiers mvarAccessModifiers = CodeAccessModifiers.None;
        public CodeAccessModifiers AccessModifiers { get { return mvarAccessModifiers; } set { mvarAccessModifiers = value; } }

        private bool mvarIsStatic = false;
        public bool IsStatic { get { return mvarIsStatic; } set { mvarIsStatic = value; } }

        private bool mvarIsSealed = false;
        public bool IsSealed { get { return mvarIsSealed; } set { mvarIsSealed = value; } }

        private bool mvarIsAbstract = false;
        public bool IsAbstract { get { return mvarIsAbstract; } set { mvarIsAbstract = value; } }

        private bool mvarIsPartial = false;
        public bool IsPartial { get { return mvarIsPartial; } set { mvarIsPartial = value; } }

        private CodeVariableElement.CodeVariableElementCollection mvarGenericParameters = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection GenericParameters
        {
            get { return mvarGenericParameters; }
        }

		public override string ToString()
		{
			return "Class: " + mvarName + " (" + mvarAccessModifiers.ToString() + ")";
		}

        private string[] mvarBaseClassName = new string[0];
        public string[] BaseClassName { get { return mvarBaseClassName; } set { mvarBaseClassName = value; } }
    }
}
