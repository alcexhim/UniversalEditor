//
//  References.cs - defines an object that references a ReferencedBy object
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

namespace UniversalEditor
{
    /// <summary>
    /// Defines an object that references a <see cref="T:ReferencedBy`1" /> object.
    /// </summary>
    /// <typeparam name="TRef">The <see cref="T:ReferencedBy`1" /> object referenced by this <see cref="T:References`1" /> object.</typeparam>
	public interface References<TRef>
	{
        /// <summary>
		/// Creates or returns an existing <see cref="T:ReferencedBy`1" /> object referring to this <see cref="T:References`1" /> object.
        /// </summary>
		/// <returns>A <see cref="T:ReferencedBy`1" /> object that can be used to create additional instances of this <see cref="T:References`1" /> object.</returns>
		TRef MakeReference();
	}
    /// <summary>
	/// Defines an object that is referenced by the given <see cref="T:References`1" /> object.
    /// </summary>
	/// <typeparam name="TObj">The <see cref="T:References`1" /> object which references this <see cref="T:ReferencedBy`1" /> object.</typeparam>
	public interface ReferencedBy<TObj>
    {
        /// <summary>
		/// Creates an instance of this <see cref="T:ReferencedBy`1" /> object from the <see cref="Type" /> described in the associated <see cref="T:References`1" /> object.
        /// </summary>
		/// <returns>An instance of this <see cref="T:ReferencedBy`1" /> object created from the <see cref="Type" /> described in the associated <see cref="T:References`1" /> object.</returns>
		TObj Create();

        /// <summary>
		/// Gets the detail fields that are shown in lists of this <see cref="T:ReferencedBy`1" /> object in details view.
        /// </summary>
		/// <returns>An array of <see cref="String" />s that are shown in detail columns of lists of this <see cref="T:ReferencedBy`1" /> object.</returns>
        string[] GetDetails();
	}
}
