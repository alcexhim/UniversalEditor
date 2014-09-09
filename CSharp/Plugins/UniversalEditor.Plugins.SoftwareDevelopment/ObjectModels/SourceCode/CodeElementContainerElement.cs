using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
    public class CodeElementContainerElement : CodeElement
    {
        public CodeElementContainerElement()
        {
            mvarElements = new CodeElement.CodeElementCollection(this);
        }

        private CodeElement.CodeElementCollection mvarElements = null;
        public CodeElement.CodeElementCollection Elements
        {
            get { return mvarElements; }
        }

        public T FindElement<T>(string Name) where T : CodeElement, INamedCodeElement
        {
            foreach (CodeElement e in mvarElements)
            {
                if (!(e is INamedCodeElement)) continue;

                INamedCodeElement nce = (e as INamedCodeElement);
                if (nce is T && nce.Name == Name)
                {
                    return (nce as T);
                }
                else if (nce is CodeElementContainerElement)
                {
                    T ce = (nce as CodeElementContainerElement).FindElement<T>(Name);
                    if (ce != null) return ce;
                }
            }
            return null;
        }
    }
}
