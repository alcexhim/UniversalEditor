using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UniversalEditor;

namespace Flame.ObjectModels.Programming
{
    public class CodeObjectModel : ObjectModel
    {
        private string[] mvarPath = new string[] { "Programming", "Code File" };
        public override string[] Path
        {
            get { return mvarPath; }
        }

        public override void CopyTo(ObjectModel destination)
        {
            CodeObjectModel clone = (destination as CodeObjectModel);
            if (clone == null) return;

            foreach (CodeElement element in mvarElements)
            {
                clone.Elements.Add(element.Clone() as CodeElement);
            }
        }

        private CodeElement.CodeElementCollection mvarElements = new CodeElement.CodeElementCollection();
        public CodeElement.CodeElementCollection Elements
        {
            get { return mvarElements; }
        }
    }
}
