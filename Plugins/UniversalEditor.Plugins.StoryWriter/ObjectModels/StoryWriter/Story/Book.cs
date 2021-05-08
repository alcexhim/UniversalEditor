//
//  Book.cs - represents a book
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
	/// Represents a <see cref="Book" />.
	/// </summary>
	public class Book : ICloneable
	{
		public class BookCollection
			: System.Collections.ObjectModel.Collection<Book>
		{

		}

		/// <summary>
		/// Gets or sets the title of the <see cref="Book" />.
		/// </summary>
		/// <value>The title of the <see cref="Book" />.</value>
		public string Title { get; set; } = String.Empty;
		/// <summary>
		/// Gets a collection of <see cref="Author" /> instances representing the authors of the <see cref="Book" />.
		/// </summary>
		/// <value>The authors of the <see cref="Book" />.</value>
		public Author.AuthorCollection Authors { get; } = new Author.AuthorCollection();
		/// <summary>
		/// Gets a collection of <see cref="Chapter" /> instances representing the chapters in this <see cref="Book" />.
		/// </summary>
		/// <value>The chapters in this <see cref="Book" />.</value>
		public Chapter.ChapterCollection Chapters { get; } = new Chapter.ChapterCollection();

		public object Clone()
		{
			Book clone = new Book();
			clone.Title = (Title.Clone() as string);
			foreach (Author author in Authors)
			{
				clone.Authors.Add(author);
			}
			foreach (Chapter chapter in Chapters)
			{
				clone.Chapters.Add(chapter.Clone() as Chapter);
			}
			return clone;
		}
	}
}
