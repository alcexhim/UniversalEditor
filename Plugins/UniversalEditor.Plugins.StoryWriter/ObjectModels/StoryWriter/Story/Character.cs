using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Character : ICloneable
	{
		public class CharacterCollection
			: System.Collections.ObjectModel.Collection<Character>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private PersonalName mvarName = null;
		public PersonalName Name { get { return mvarName; } set { mvarName = value; } }

		private Gender mvarGender = null;
		public Gender Gender { get { return mvarGender; } set { mvarGender = value; } }

		public object Clone()
		{
			Character clone = new Character();
			if (mvarName != null)
			{
				clone.Name = (mvarName.Clone() as PersonalName);
			}
			clone.Gender = mvarGender;
			return clone;
		}
	}
}
