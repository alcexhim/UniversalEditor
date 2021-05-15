//
//  MarkupTagElement.cs - represents a tag element in a MarkupObjectModel
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
using System.Collections.Generic;

namespace UniversalEditor.ObjectModels.Markup
{
	/// <summary>
	/// Represents a tag element in a <see cref="MarkupObjectModel" />.
	/// </summary>
	public class MarkupTagElement : MarkupContainerElement
	{
		/// <summary>
		/// Combines the attributes and child elements of this <see cref="MarkupTagElement" /> with the given <see cref="MarkupElement" />.
		/// </summary>
		/// <param name="el">The <see cref="MarkupElement" /> to combine. This must be an instance of <see cref="MarkupTagElement" />.</param>
		public override void Combine(MarkupElement el)
		{
			base.Combine(el);

			MarkupTagElement tag = (el as MarkupTagElement);
			if (tag != null)
			{
				for (int i = 0; i < tag.Attributes.Count; i++)
				{
					if (!this.Attributes.Contains(tag.Attributes[i].FullName))
					{
						this.Attributes.Add(tag.Attributes[i].FullName, tag.Attributes[i].Value);
					}
				}
			}
		}

		/// <summary>
		/// Gets a collection of <see cref="MarkupAttribute" /> instances representing the attributes of this <see cref="MarkupTagElement" />.
		/// </summary>
		/// <value>The attributes of this <see cref="MarkupTagElement" />.</value>
		public MarkupAttribute.MarkupAttributeCollection Attributes { get; } = new MarkupAttribute.MarkupAttributeCollection();

		public override object Clone()
		{
			MarkupTagElement clone = new MarkupTagElement();
			foreach (MarkupElement el in base.Elements)
			{
				clone.Elements.Add(el.Clone() as MarkupElement);
			}
			foreach (MarkupAttribute att in Attributes)
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
			MarkupAttribute att = Attributes[Name];
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

		public MarkupAttribute FindAttributeUsingSchema(string schema, string name)
		{
			string tagPrefix = FindSchemaTagPrefix(schema);
			if (tagPrefix != null)
			{
				for (int i = 0; i < Attributes.Count; i++)
				{
					if (Attributes[i].Namespace == tagPrefix && Attributes[i].Name == name)
						return Attributes[i];
				}
			}
			return null;
		}
	}
}
