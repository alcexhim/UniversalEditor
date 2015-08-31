using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming
{
    public class CodeElementReference
    {
        private CodeElement mvarValue = null;
        public CodeElement Value
        {
            get { return mvarValue; }
            set { mvarValue = value; }
        }
    }
}
