using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class StoryObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		public override ObjectModelReference MakeReference()
		{
			if (_omr == null)
			{
				_omr = base.MakeReference();
				_omr.Title = "StoryWriter story";
				_omr.Description = "Tracks characters, locations, and other components of large fictional universes";
				_omr.Path = new string[] { "StoryWriter" };
			}
			return _omr;
		}

		private Universe mvarUniverse = new Universe();
		/// <summary>
		/// The <see cref="Universe" /> this story is set in. A Universe is a collection of characters and
		/// other entities referenced in a story.
		/// </summary>
		public Universe Universe { get { return mvarUniverse; } }

		private Character.CharacterCollection mvarCharacters = new Character.CharacterCollection();
		/// <summary>
		/// Characters introduced for this story only. Characters that can be used across stories should
		/// be added to a <see cref="Universe" />.
		/// </summary>
		public Character.CharacterCollection Characters { get { return mvarCharacters; } }

		private Book.BookCollection mvarBooks = new Book.BookCollection();
		/// <summary>
		/// The books collected in this story.
		/// </summary>
		public Book.BookCollection Books { get { return mvarBooks; } }

		public override void Clear()
		{
			mvarCharacters.Clear();
			mvarBooks.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			StoryObjectModel clone = (where as StoryObjectModel);
			foreach (Character chara in mvarCharacters)
			{
				clone.Characters.Add(chara.Clone() as Character);
			}
			foreach (Book book in mvarBooks)
			{
				clone.Books.Add(book.Clone() as Book);
			}
		}
	}
}
