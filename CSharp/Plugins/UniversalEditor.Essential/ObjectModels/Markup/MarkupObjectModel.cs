using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.Markup
{
	public class MarkupObjectModel : ObjectModel
	{
		public override ObjectModelReference MakeReference()
		{
			ObjectModelReference omr = base.MakeReference();
			omr.Title = "Markup";
			omr.Path = new string[] { "General", "Markup" };
			return omr;
		}
		public override void Clear()
		{
			this.mvarElements.Clear();
		}

		private MarkupElement.MarkupElementCollection mvarElements = new MarkupElement.MarkupElementCollection();
		public MarkupElement.MarkupElementCollection Elements
		{
			get
			{
				return this.mvarElements;
			}
		}
		public override void CopyTo(ObjectModel destination)
		{
			MarkupObjectModel dest = destination as MarkupObjectModel;
			if (dest != null)
			{
				foreach (MarkupElement el in this.mvarElements)
				{
					if (dest.Elements.Contains(el.FullName, el.GetType()))
					{
						dest.Elements[el.FullName].Combine(el);
					}
					else
					{
						dest.Elements.Add(el.Clone() as MarkupElement);
					}
				}
			}
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

		public MarkupTagElement FindElementByPath(string xpath)
		{
			string[] XPATHParts = xpath.Split(new char[] { '/' });
			MarkupTagElement tagParent = null;
			foreach (string part in XPATHParts)
			{
				if (String.IsNullOrEmpty(part)) continue;
				string tagName = part;
				int index = -1;
				if (part.Contains("[") && part.EndsWith("]"))
				{
					string indexStr = part.Substring(part.IndexOf("[") + 1, part.Length - part.IndexOf("]"));
					index = Int32.Parse(indexStr) - 1;
					tagName = part.Substring(0, part.IndexOf("["));
				}
				if (index > -1)
				{
					if (tagParent == null)
					{
						tagParent = (mvarElements[tagName, index] as MarkupTagElement);
						if (tagParent == null) return null;
					}
					else
					{
						tagParent = (tagParent.Elements[tagName, index] as MarkupTagElement);
						if (tagParent == null) return null;
					}
				}
				else
				{
					if (tagParent == null)
					{
						tagParent = (mvarElements[part] as MarkupTagElement);
						if (tagParent == null) return null;
					}
					else
					{
						tagParent = (tagParent.Elements[part] as MarkupTagElement);
						if (tagParent == null) return null;
					}
				}
			}
			return tagParent;
		}

		public string GetElementValue(string[] path, string defaultValue = null)
		{
			MarkupTagElement tag = (FindElement(path) as MarkupTagElement);
			if (tag == null) return defaultValue;
			return tag.Value;
		}
	}
}
