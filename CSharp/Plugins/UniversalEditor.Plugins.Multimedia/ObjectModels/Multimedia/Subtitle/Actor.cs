using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia.Subtitle
{
	public class Actor : ICloneable
	{
		public class ActorCollection
			: System.Collections.ObjectModel.Collection<Actor>
		{

		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }
		
		public object Clone()
		{
			Actor clone = new Actor();
			clone.Name = (mvarName.Clone() as string);
			return clone;
		}
	}
}
