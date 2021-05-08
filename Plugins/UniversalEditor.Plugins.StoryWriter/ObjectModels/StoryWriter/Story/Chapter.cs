//
//  Chapter.cs - represents a chapter in a Book
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
	/// Represents a chapter in a <see cref="Book" />.
	/// </summary>
	public class Chapter : ICloneable
	{

		public class ChapterCollection
			: System.Collections.ObjectModel.Collection<Chapter>
		{

		}

		/// <summary>
		/// Gets or sets the globally-unique identifier for this <see cref="Chapter" />.
		/// </summary>
		/// <value>The globally-unique identifier for this <see cref="Chapter" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="Chapter" />.
		/// </summary>
		/// <value>The title of this <see cref="Chapter" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="Section" /> instances representing the sections in this <see cref="Chapter" />.
		/// </summary>
		/// <value>The sections in this <see cref="Chapter" />.</value>
		public Section.SectionCollection Sections { get; } = new Section.SectionCollection();

		public object Clone()
		{
			Chapter clone = new Chapter();
			clone.Title = (Title.Clone() as string);
			foreach (Section section in Sections)
			{
				clone.Sections.Add(section.Clone() as Section);
			}
			return clone;
		}
	}
}
