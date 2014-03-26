using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public abstract class MarkupContainerElement : MarkupElement
	{
		private MarkupElement.MarkupElementCollection mvarElements = null;
		public MarkupElement.MarkupElementCollection Elements
		{
			get
			{
				return this.mvarElements;
			}
		}
		public MarkupContainerElement()
		{
			this.mvarElements = new MarkupElement.MarkupElementCollection(this);
		}
		public MarkupElement FindElement(params string[] fullNames)
		{
			MarkupElement result;
			if (fullNames.Length == 0)
			{
				result = null;
			}
			else
			{
				MarkupElement e = this.mvarElements[fullNames[0]];
				for (int i = 1; i < fullNames.Length; i++)
				{
					if (e != null && e is MarkupContainerElement)
					{
						e = (e as MarkupContainerElement).Elements[fullNames[i]];
					}
				}
				result = e;
			}
			return result;
		}
		public MarkupTagElement CreateElement(params string[] elementNames)
		{
			MarkupTagElement basetag = null;
			MarkupTagElement tag = null;
			for (int i = elementNames.Length - 1; i > -1; i--)
			{
				if (tag == null)
				{
					tag = new MarkupTagElement();
					tag.Name = elementNames[i];
					basetag = tag;
					this.Elements.Add(tag);
				}
				else
				{
					MarkupTagElement newtag = new MarkupTagElement();
					newtag.Name = elementNames[i];
					tag.Elements.Add(newtag);
					tag = newtag;
				}
			}
			return basetag;
		}

        public override void Combine(MarkupElement el)
        {
            MarkupTagElement tag = (el as MarkupTagElement);
            if (tag == null) throw new InvalidOperationException("Cannot combine MarkupTagElement with " + el.GetType().Name);

            foreach (MarkupElement el1 in tag.Elements)
            {
                if (Elements.Contains(el1.FullName))
                {
                    Elements[el1.FullName].Combine(el1);
                }
                else
                {
                    Elements.Add(el1);
                }
            }
        }
	}
}
