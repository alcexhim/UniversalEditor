using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	/// <summary>
	/// Represents a universe, a collection of related elements in a particular story.
	/// </summary>
	public class Universe
	{
		private Character.CharacterCollection mvarCharacters = new Character.CharacterCollection();
		public Character.CharacterCollection Characters { get { return mvarCharacters; } }
	}
}
