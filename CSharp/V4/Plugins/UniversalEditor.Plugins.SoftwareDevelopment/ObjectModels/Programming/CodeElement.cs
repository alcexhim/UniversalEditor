using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flame.ObjectModels.Programming
{
    public class CodeElement : ICloneable
    {
        public class CodeElementCollection
            : System.Collections.ObjectModel.Collection<CodeElement>
        {
        }

        private string mvarName = String.Empty;
        public string Name
        {
            get { return mvarName; }
            set { mvarName = value; }
        }

        public virtual object Clone()
        {
            return MemberwiseClone();
        }
    }
}
