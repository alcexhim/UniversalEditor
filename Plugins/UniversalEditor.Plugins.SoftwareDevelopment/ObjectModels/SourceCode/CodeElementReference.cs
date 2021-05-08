//
//  CodeElementReference.cs - represents a reference to another CodeElement
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
using UniversalEditor.ObjectModels.SourceCode.CodeElements;

namespace UniversalEditor.ObjectModels.SourceCode
{
	/// <summary>
	/// Represents a reference to another <see cref="CodeElement" />.
	/// </summary>
	public class CodeElementReference
	{
		public CodeElementReference()
		{
		}
		public CodeElementReference(CodeElement value)
		{
			Value = value;
		}

		/// <summary>
		/// Gets or sets the <see cref="CodeElement" /> referenced by this <see cref="CodeElementReference" />.
		/// </summary>
		/// <value>The <see cref="CodeElement" /> referenced by this <see cref="CodeElementReference" />.</value>
		public CodeElement Value { get; set; } = null;

		public Type DataType
		{
			get
			{
				if (Value is CodeLiteralElement)
				{
					CodeLiteralElement lit = (Value as CodeLiteralElement);
					return lit.Value.GetType();
				}
				// otherwise is Boolean expression, maybe?
				return null;
			}
		}

		public object GetValue()
		{
			if (Value is CodeLiteralElement)
			{
				CodeLiteralElement lit = (Value as CodeLiteralElement);
				return lit.Value;
			}
			return null;
		}
	}
}
