using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeComponentStateReference : ICloneable
	{
		public class ThemeComponentStateReferenceCollection
			: System.Collections.ObjectModel.Collection<ThemeComponentStateReference>
		{
			public ThemeComponentStateReference this[Guid id]
			{
				get
				{
					foreach (ThemeComponentStateReference item in this)
					{
						if (item.StateID == id) return item;
					}
					return null;
				}
			}
			public bool Contains(Guid id)
			{
				return (this[id] != null);
			}
		}

		private Guid mvarStateID = Guid.Empty;
		public Guid StateID { get { return mvarStateID; } set { mvarStateID = value; } }

		public ThemeComponentStateReference()
		{
		}
		public ThemeComponentStateReference(Guid stateID)
		{
			mvarStateID = stateID;
		}

		public object Clone()
		{
			ThemeComponentStateReference clone = new ThemeComponentStateReference();
			clone.StateID = mvarStateID;
			return clone;
		}
	}
}
