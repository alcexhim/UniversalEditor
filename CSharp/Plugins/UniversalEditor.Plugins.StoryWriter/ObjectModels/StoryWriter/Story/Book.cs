using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Book : ICloneable
	{
		public class BookCollection
			: System.Collections.ObjectModel.Collection<Book>
		{

		}

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Author.AuthorCollection mvarAuthors = new Author.AuthorCollection();
		public Author.AuthorCollection Authors { get { return mvarAuthors; } }

		private Chapter.ChapterCollection mvarChapters = new Chapter.ChapterCollection();
		public Chapter.ChapterCollection Chapters { get { return mvarChapters; } }

		public object Clone()
		{
			Book clone = new Book();
			clone.Title = (mvarTitle.Clone() as string);
			foreach (Author author in mvarAuthors)
			{
				clone.Authors.Add(author);
			}
			foreach (Chapter chapter in mvarChapters)
			{
				clone.Chapters.Add(chapter.Clone() as Chapter);
			}
			return clone;
		}
	}
}
