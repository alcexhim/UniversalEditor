using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	public delegate void ObjectModelChangingEventHandler(object sender, ObjectModelChangingEventArgs e);
	public class ObjectModelChangingEventArgs : CancelEventArgs
	{
		private ObjectModel mvarOldObjectModel = null;
		public ObjectModel OldObjectModel { get { return mvarOldObjectModel; } }

		private ObjectModel mvarNewObjectModel = null;
		public ObjectModel NewObjectModel { get { return mvarNewObjectModel; } set { mvarNewObjectModel = value; } }

		public ObjectModelChangingEventArgs(ObjectModel oldObjectModel, ObjectModel newObjectModel)
		{
			mvarOldObjectModel = oldObjectModel;
			mvarNewObjectModel = newObjectModel;
		}
	}
}
