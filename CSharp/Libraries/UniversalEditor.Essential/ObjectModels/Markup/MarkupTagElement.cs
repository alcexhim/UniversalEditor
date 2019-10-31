using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupTagElement : MarkupContainerElement
	{
		private MarkupAttribute.MarkupAttributeCollection mvarAttributes = new MarkupAttribute.MarkupAttributeCollection();
		public MarkupAttribute.MarkupAttributeCollection Attributes
		{
			get
			{
				return this.mvarAttributes;
			}
		}
		public override object Clone()
		{
			MarkupTagElement clone = new MarkupTagElement();
			foreach (MarkupElement el in base.Elements)
			{
				clone.Elements.Add(el.Clone() as MarkupElement);
			}
            foreach (MarkupAttribute att in mvarAttributes)
            {
                clone.Attributes.Add(att.Clone() as MarkupAttribute);
            }
			clone.Name = base.Name;
			clone.Namespace = base.Namespace;
			clone.Value = base.Value;
			return clone;
		}

        public MarkupAttribute FindAttribute(string Name)
        {
            return FindAttribute(Name, (MarkupAttribute)null);
        }
        public string FindAttribute(string Name, string DefaultValue)
        {
            MarkupAttribute att = FindAttribute(Name);
            if (att == null) return DefaultValue;
            return att.Value;
        }
        public MarkupAttribute FindAttribute(string Name, MarkupAttribute defaultValue)
        {
            MarkupAttribute att = mvarAttributes[Name];
            if (att == null) return defaultValue;
            return att;
        }

		public MarkupTagElement[] GetElementsByTagName(string tagName)
		{
			List<MarkupTagElement> list = new List<MarkupTagElement>();
			for (int i = 0; i < Elements.Count; i++)
			{
				if ((Elements[i] as MarkupTagElement)?.FullName == tagName)
					list.Add(Elements[i] as MarkupTagElement);
			}
			return list.ToArray();
		}
	}
}
