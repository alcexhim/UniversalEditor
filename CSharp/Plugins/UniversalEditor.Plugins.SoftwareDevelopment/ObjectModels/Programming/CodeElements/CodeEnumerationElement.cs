using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming.CodeElements
{
    public class CodeEnumerationElement : CodeElement
    {
        private Dictionary<string, int> mvarValues = new Dictionary<string, int>();
        public Dictionary<string, int> Values
        {
            get { return mvarValues; }
        }
    }
}
