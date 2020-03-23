using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
    public class CodeElement : ICloneable
    {
        public class CodeElementCollection
            : System.Collections.ObjectModel.Collection<CodeElement>
        {
            private CodeElementContainerElement mvarParent = null;
            public CodeElementCollection(CodeElementContainerElement parent = null)
            {
                mvarParent = parent;
            }

            protected override void InsertItem(int index, CodeElement item)
            {
                base.InsertItem(index, item);
                if (item != null) item.mvarParent = mvarParent;
            }
            protected override void RemoveItem(int index)
            {
                this[index].mvarParent = null;
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                foreach (CodeElement ce in this) ce.mvarParent = null;
                base.ClearItems();
            }

			public bool ContainsMethod(string name)
			{
				foreach (CodeElement ce in this)
				{
                    if ((ce is CodeElements.CodeMethodElement) && (ce as CodeElements.CodeMethodElement).Name == name)
					{
						return true;
					}
				}
				return false;
			}
		}

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        private CodeElementContainerElement mvarParent = null;
        public CodeElementContainerElement Parent { get { return mvarParent; } internal set { mvarParent = value; } }

        public static string GetFullName(CodeElement codeElement, string separator)
        {
            CodeElementContainerElement parent = codeElement.Parent;
            StringBuilder sb = new StringBuilder();
            while (parent != null)
            {
                if (parent is INamedCodeElement)
                {
                    sb.Append((parent as INamedCodeElement).Name);
                    sb.Append(separator);
                }
                else if (parent is IMultipleNamedCodeElement)
                {
                    sb.Append(String.Join(separator, (parent as IMultipleNamedCodeElement).Name));
                    sb.Append(separator);
                }
                parent = parent.Parent;
            }
			if (sb.Length > 0)
			{
				sb.Append(separator);
			}

			if (codeElement is INamedCodeElement)
			{
				sb.Append((codeElement as INamedCodeElement).Name);
			}
			else if (codeElement is IMultipleNamedCodeElement)
			{
				sb.Append(String.Join(separator, (codeElement as IMultipleNamedCodeElement).Name));
			}
            return sb.ToString();
        }
    }
}
