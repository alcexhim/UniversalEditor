//
//  MarkupObjectModel.cs - provides an ObjectModel for manipulating markup documents (e.g. XML, HTML)
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.ObjectModels.Markup
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating markup documents (e.g. XML, HTML).
	/// </summary>
	public class MarkupObjectModel : ObjectModel
	{
		protected override ObjectModelReference MakeReferenceInternal()
		{
			ObjectModelReference omr = base.MakeReferenceInternal();
			omr.Path = new string[] { "General", "Markup" };
			return omr;
		}
		public override void Clear()
		{
			this.mvarElements.Clear();
		}

		public MarkupObjectModel()
		{
			mvarElements = new MarkupElement.MarkupElementCollection (null, this);
		}

		private MarkupElement.MarkupElementCollection mvarElements = null;
		public MarkupElement.MarkupElementCollection Elements { get { return this.mvarElements; } }

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

		public string DefaultSchema { get; set; } = null;

		public MarkupElement FindElementUsingSchema(string schema, string name)
		{
			string tagPrefix = null;
			string defaultSchema = null;

			if (DefaultSchema != null && DefaultSchema == schema)
			{
				return FindElement(name);
			}

			for (int i = 0; i < this.Elements.Count; i++) {
				MarkupTagElement tagTopLevel = (this.Elements [i] as MarkupTagElement);
				if (tagTopLevel != null) {
					for (int j = 0; j < tagTopLevel.Attributes.Count; j++) {
						if ("xmlns".Equals(tagTopLevel.Attributes [j].Namespace)) {

							if (tagTopLevel.Attributes [j].Value.Equals (schema)) {
								tagPrefix = tagTopLevel.Attributes [j].Name;

								MarkupAttribute attXMLNS = tagTopLevel.Attributes["xmlns"];
								if (attXMLNS != null)
								{
									defaultSchema = attXMLNS.Value;
								}
								break;
							}

						}

						// BEGIN: added 2020-08-20 by beckermj - for xmlns without namespace
						else if (tagTopLevel.Attributes[j].Name == "xmlns")
						{
							if (tagTopLevel.Attributes[j].Value.Equals(schema))
							{
								defaultSchema = schema;
								return tagTopLevel;
							}
						}
						// END: added 2020-08-20 by beckermj - for xmlns without namespace
					}
				}
			}

			if (tagPrefix == null) {
				Console.WriteLine ("ue: MarkupObjectModel: tag prefix for schema '" + schema + "' not found");
				return null;
			}

			DefaultSchema = defaultSchema;
			if (DefaultSchema != null && DefaultSchema == schema)
			{
				MarkupElement el = FindElement(name);
				if (el != null) return el;
			}

			string fullName = tagPrefix + ":" + name;
			return FindElement (fullName);
		}

	}
}
