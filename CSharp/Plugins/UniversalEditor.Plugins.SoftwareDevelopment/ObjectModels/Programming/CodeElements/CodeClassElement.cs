using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements
{
    public class CodeClassElement : CodeElementContainerElement
    {
        private AccessModifiers mvarAccessModifiers = AccessModifiers.None;
        public AccessModifiers AccessModifiers
        {
            get { return mvarAccessModifiers; }
            set { mvarAccessModifiers = value; }
        }

        private bool mvarIsSealed = false;
        public bool IsSealed
        {
            get { return mvarIsSealed; }
            set { mvarIsSealed = value; }
        }

        private bool mvarIsAbstract = false;
        public bool IsAbstract
        {
            get { return mvarIsAbstract; }
            set { mvarIsAbstract = value; }
        }

        private bool mvarIsPartial = false;
        public bool IsPartial
        {
            get { return mvarIsPartial; }
            set { mvarIsPartial = value; }
        }

        private CodeVariableElement.CodeVariableElementCollection mvarGenericParameters = new CodeVariableElement.CodeVariableElementCollection();
        public CodeVariableElement.CodeVariableElementCollection GenericParameters
        {
            get { return mvarGenericParameters; }
        }
    }
}
