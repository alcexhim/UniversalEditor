//
//  StoryObjectModel.cs - provides an ObjectModel for manipulating collections of characters, books, and other story elements
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

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating collections of characters, books, and other story elements.
	/// </summary>
	public class StoryObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Description = "Tracks characters, locations, and other components of large fictional universes";
				_omr.Path = new string[] { "StoryWriter", "Story" };
			}
			return _omr;
		}

		/// <summary>
		/// The <see cref="Universe" /> this story is set in. A Universe is a collection of characters and
		/// other entities referenced in a story.
		/// </summary>
		public Universe Universe { get; } = new Universe();
		/// <summary>
		/// Characters introduced for this story only. Characters that can be used across stories should
		/// be added to a <see cref="Universe" />.
		/// </summary>
		public Character.CharacterCollection Characters { get; } = new Character.CharacterCollection();
		/// <summary>
		/// The books collected in this story.
		/// </summary>
		public Book.BookCollection Books { get; } = new Book.BookCollection();

		public override void Clear()
		{
			Characters.Clear();
			Books.Clear();
		}

		public override void CopyTo(ObjectModel where)
		{
			StoryObjectModel clone = (where as StoryObjectModel);
			foreach (Character chara in Characters)
			{
				clone.Characters.Add(chara.Clone() as Character);
			}
			foreach (Book book in Books)
			{
				clone.Books.Add(book.Clone() as Book);
			}
		}
	}
}
