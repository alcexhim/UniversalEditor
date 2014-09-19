using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Book
	{
		public class BookCollection
			: System.Collections.ObjectModel.Collection<Book>
		{

		}

		private Chapter.ChapterCollection mvarChapters = new Chapter.ChapterCollection();
		public Chapter.ChapterCollection Chapters { get { return mvarChapters; } }
	}
}
