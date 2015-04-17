using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.Plugins.RebelSoftware.ObjectModels.InstallationScript
{
	public abstract class Action : ICloneable
	{
		public class ActionCollection
			: System.Collections.ObjectModel.Collection<Action>
		{

		}

		public abstract object Clone();
	}
}
