using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.StoryWriter.Story
{
	/// <summary>
	/// Represents a section or a scene in a <see cref="Chapter" />.
	/// </summary>
	public class Section : ICloneable
	{
		public class SectionCollection
			: System.Collections.ObjectModel.Collection<Section>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		public object Clone()
		{
			Section clone = new Section();
			clone.Title = (mvarTitle.Clone() as string);
			return clone;
		}
	}
}
