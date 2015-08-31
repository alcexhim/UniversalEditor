using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Author : ICloneable
	{
		public class AuthorCollection
			: System.Collections.ObjectModel.Collection<Author>
		{

		}

		private PersonalName mvarName = null;
		public PersonalName Name { get { return mvarName; } set { mvarName = value; } }

		public object Clone()
		{
			Author clone = new Author();
			if (mvarName != null)
			{
				clone.Name = (mvarName.Clone() as PersonalName);
			}
			return clone;
		}
	}
}
