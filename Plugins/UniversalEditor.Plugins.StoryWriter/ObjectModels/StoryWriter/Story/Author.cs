//
//  Author.cs - represents the author of a particular Story
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

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	/// <summary>
	/// Represents the author of a particular <see cref="Story" />.
	/// </summary>
	public class Author : ICloneable
	{
		public class AuthorCollection
			: System.Collections.ObjectModel.Collection<Author>
		{

		}

		/// <summary>
		/// Gets or sets the personal name of the <see cref="Author" />.
		/// </summary>
		/// <value>The personal name of the <see cref="Author" />.</value>
		public PersonalName Name { get; set; } = null;

		public object Clone()
		{
			Author clone = new Author();
			if (Name != null)
			{
				clone.Name = (Name.Clone() as PersonalName);
			}
			return clone;
		}
	}
}
