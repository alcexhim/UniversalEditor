//
//  CodeElement.cs - the abstract base class from which all code elements derive
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
using System.Text;

namespace UniversalEditor.ObjectModels.SourceCode
{
	/// <summary>
	/// The abstract base class from which all code elements derive.
	/// </summary>
	public abstract class CodeElement : ICloneable
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
