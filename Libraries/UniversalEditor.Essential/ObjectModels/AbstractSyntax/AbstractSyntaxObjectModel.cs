//
//  AbstractSyntaxObjectModel.cs - provides an ObjectModel for manipulating data that follows the Abstract Syntax Notation One (ASN.1) specification
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

namespace UniversalEditor.ObjectModels.AbstractSyntax
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating data that follows the Abstract Syntax Notation One (ASN.1) specification.
	/// </summary>
	public class AbstractSyntaxObjectModel : ObjectModel
	{
		/// <summary>
		/// Clears all data from this <see cref="ObjectModel" /> and returns it to a pristine state.
		/// </summary>
		public override void Clear()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Copies all data from this <see cref="ObjectModel" /> to the specified <see cref="ObjectModel" />.
		/// </summary>
		/// <param name="where">The <see cref="ObjectModel" /> into which to copy the data of this <see cref="ObjectModel" />.</param>
		/// <exception cref="ObjectModelNotSupportedException">The conversion between this <see cref="ObjectModel" /> and the given <see cref="ObjectModel" /> is not supported.</exception>
		public override void CopyTo(ObjectModel where)
		{
			throw new NotImplementedException();
		}

		private static ObjectModelReference _omr = null;
		/// <summary>
		/// Creates a new <see cref="ObjectModelReference" /> containing the appropriate metadata for the <see cref="AbstractSyntaxObjectModel" /> and caches it, returning the cached instance.
		/// </summary>
		/// <returns>The <see cref="ObjectModelReference" /> that provides metadata and other information about this <see cref="AbstractSyntaxObjectModel" />.</returns>
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "General", "Abstract Syntax Notation" };
			}
			return _omr;
		}
	}
}
