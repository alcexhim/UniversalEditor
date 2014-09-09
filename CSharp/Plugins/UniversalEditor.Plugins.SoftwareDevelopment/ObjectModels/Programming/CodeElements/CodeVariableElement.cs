using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements
{
    public class CodeVariableElement : CodeElement
    {
        public class CodeVariableElementCollection
            : System.Collections.ObjectModel.Collection<CodeVariableElement>
        {
        }

        private CodeElementReference mvarValue = null;
        public CodeElementReference Value
        {
            get { return mvarValue; }
            set { mvarValue = value; }
        }

        private string mvarDataType = String.Empty;
        public string DataType
        {
            get { return mvarDataType; }
            set { mvarDataType = value; }
        }

        private AccessModifiers mvarAccessModifiers = AccessModifiers.None;
        public AccessModifiers AccessModifiers
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
