//
//  Section.cs - represents a section or a scene in a Chapter
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
	/// Represents a section or a scene in a <see cref="Chapter" />.
	/// </summary>
	public class Section : ICloneable
	{
		public class SectionCollection
			: System.Collections.ObjectModel.Collection<Section>
		{

		}

		/// <summary>
		/// Gets or sets the globally-unique identifier associated with this <see cref="Section" />.
		/// </summary>
		/// <value>The globally-unique identifier associated with this <see cref="Section" />.</value>
		public Guid ID { get; set; } = Guid.Empty;
		/// <summary>
		/// Gets or sets the title of this <see cref="Section" />.
		/// </summary>
		/// <value>The title of this <see cref="Section" />.</value>
		public string Title { get; set; } = String.Empty;

		public object Clone()
		{
			Section clone = new Section();
			clone.Title = (Title.Clone() as string);
			return clone;
		}
	}
}
