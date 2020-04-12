//
//  Character.cs - represents a character in a Universe
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
	/// Represents a character in a <see cref="Universe" />.
	/// </summary>
	public class Character : ICloneable
	{
		public class CharacterCollection
			: System.Collections.ObjectModel.Collection<Character>
		{

		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="Character" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="Character" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the personal name of this <see cref="Character" />.
		/// </summary>
		/// <value>The personal name of this <see cref="Character" />.</value>
		public PersonalName Name { get; set; } = null;
		/// <summary>
		/// Gets or sets the gender of this <see cref="Character" />.
		/// </summary>
		/// <value>The gender of this <see cref="Character" />.</value>
		public Gender Gender { get; set; } = null;

		public object Clone()
		{
			Character clone = new Character();
			if (Name != null)
			{
				clone.Name = (Name.Clone() as PersonalName);
			}
			clone.Gender = Gender;
			return clone;
		}
	}
}
