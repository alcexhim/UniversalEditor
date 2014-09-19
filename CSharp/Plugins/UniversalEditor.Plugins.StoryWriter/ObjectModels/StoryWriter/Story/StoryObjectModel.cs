using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class StoryObjectModel
	{
		private Character.CharacterCollection mvarCharacters = new Character.CharacterCollection();
		/// <summary>
		/// All the characters available to draw from within this story.
		/// </summary>
		public Character.CharacterCollection Characters { get { return mvarCharacters; } }

		private Book.BookCollection mvarBooks = new Book.BookCollection();
		/// <summary>
		/// The books collected in this story.
		/// </summary>
		public Book.BookCollection Books { get { return mvarBooks; } }
	}
}
