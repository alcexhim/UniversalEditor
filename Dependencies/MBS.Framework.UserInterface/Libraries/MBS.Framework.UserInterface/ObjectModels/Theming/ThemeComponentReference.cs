using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Framework.UserInterface.ObjectModels.Theming
{
	public struct ThemeComponentReference
	{
		private Guid mvarComponentID;
		public Guid ComponentID { get { return mvarComponentID; } set { mvarComponentID = value; } }

		private Guid mvarStateID;
		public Guid StateID { get { return mvarStateID; } set { mvarStateID = value; } }

		public ThemeComponentReference(Guid componentID, Guid stateID)
		{
			mvarComponentID = componentID;
			mvarStateID = stateID;
		}
	}
}
