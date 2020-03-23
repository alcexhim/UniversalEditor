using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public class ThemeComponentState : ICloneable
	{
		public class ThemeComponentStateCollection
			: System.Collections.ObjectModel.Collection<ThemeComponentState>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public object Clone()
		{
			ThemeComponentState clone = new ThemeComponentState();
			clone.ID = mvarID;
			return clone;
		}

		public override string ToString()
		{
			return mvarName;
		}
	}
}
