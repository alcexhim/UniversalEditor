using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming
{
    public class CodeElementContainerElement : CodeElement
    {
        private CodeElement.CodeElementCollection mvarElements = new CodeElement.CodeElementCollection();
        public CodeElement.CodeElementCollection Elements
        {
            get { return mvarElements; }
        }
    }
}
