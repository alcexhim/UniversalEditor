using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements
{
    public class CodeMethodElement : CodeElement
    {
        private AccessModifiers mvarAccessModifiers = AccessModifiers.None;
        public AccessModifiers AccessModifiers
        {
            get { return mvarAccessModifiers; }
            set { mvarAccessModifiers = value; }
        }

        private string mvarDataType = String.Empty;
        public string DataType
        {
            get { return mvarDataType; }
            set { mvarDataType = value; }
        }

        private bool mvarIsAbstract = false;
        public bool IsAbstract
        {
            get { return mvarIsAbstract; }
            set { mvarIsAbstract = value; }
        }
        private bool mvarIsVirtual = false;
        public bool IsVirtual
        {
            get { return mvarIsVirtual; }
            set { mvarIsVirtual = value; }
        }
        private bool mvarIsOverriding = false;
        public bool IsOverriding
        {
            get { return mvarIsOverriding; }
            set { mvarIsOverriding = value; }
        }

        private CodeActionElement.CodeActionElementCollection mvarActions = new CodeActionElement.CodeActionElementCollection();
        public CodeActionElement.CodeActionElementCollection Actions
        {
            get { return mvarActions; }
        }
        private CodeVariableElement.CodeVariableElementCollection mvarParameters = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection Parameters
        {
            get { return mvarParameters; }
        }

        private CodeVariableElement.CodeVariableElementCollection mvarGenericParameters = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection GenericParameters
        {
            get { return mvarGenericParameters; }
        }
    }
}
