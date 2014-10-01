using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	public class Chapter : ICloneable
	{

		public class ChapterCollection
			: System.Collections.ObjectModel.Collection<Chapter>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private Section.SectionCollection mvarSections = new Section.SectionCollection();
		public Section.SectionCollection Sections { get { return mvarSections; } }

		public object Clone()
		{
			Chapter clone = new Chapter();
			clone.Title = (mvarTitle.Clone() as string);
			foreach (Section section in mvarSections)
			{
				clone.Sections.Add(section.Clone() as Section);
			}
			return clone;
		}
	}
}
