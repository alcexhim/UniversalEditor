//
//  CodeAccessModifiers.cs - indicates the access modifiers for a CodeElement
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
	/// Indicates the access modifiers for a <see cref="CodeElement" />.
	/// </summary>
	public enum CodeAccessModifiers
	{
		/// <summary>
		/// No access modifiers have been declared for this <see cref="CodeElement" />.
		/// </summary>
		None,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible only to its immediate sibling <see cref="CodeElement" />s.
		/// </summary>
		Private,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible to members in a derived class.
		/// </summary>
		Family,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible only to members which exist both in a derived class and within the same assembly.
		/// </summary>
		FamilyANDAssembly,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible to members which exist either in a derived class or within the same assembly.
		/// </summary>
		FamilyORAssembly,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible to all members within the same assembly.
		/// </summary>
		Assembly,
		/// <summary>
		/// This <see cref="CodeElement" /> is visible to all members in all referenced assemblies.
		/// </summary>
		Public
	}
}
