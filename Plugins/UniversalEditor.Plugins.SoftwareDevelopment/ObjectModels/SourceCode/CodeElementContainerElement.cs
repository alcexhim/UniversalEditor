//
//  CodeElementContainerElement.cs - the abstract base class from which all code elements which can contain other code elements are derived
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

namespace UniversalEditor.ObjectModels.SourceCode
{
	/// <summary>
	/// The abstract base class from which all code elements which can contain other code elements are derived.
	/// </summary>
	public abstract class CodeElementContainerElement : CodeElement
	{
		public CodeElementContainerElement()
		{
			Elements = new CodeElement.CodeElementCollection(this);
		}
		/// <summary>
		/// Gets a collection of <see cref="CodeElement" /> instances representing the code elements contained by this <see cref="CodeElementContainerElement" />.
		/// </summary>
		/// <value>The code elements contained by this code element.</value>
		public CodeElement.CodeElementCollection Elements { get; private set; } = null;

		public T FindElement<T>(string Name) where T : CodeElement, INamedCodeElement
		{
			foreach (CodeElement e in Elements)
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
