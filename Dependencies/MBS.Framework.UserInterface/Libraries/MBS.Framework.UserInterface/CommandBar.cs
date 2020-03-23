using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MBS.Framework.UserInterface;

namespace MBS.Framework.UserInterface
{
	public class CommandBar
	{
		public class CommandBarCollection 
			: System.Collections.ObjectModel.Collection<CommandBar>
		{

		}

		private string mvarID = String.Empty;
		public string ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarTitle = String.Empty;
		public string Title { get { return mvarTitle; } set { mvarTitle = value; } }

		private CommandItem.CommandItemCollection mvarItems = new CommandItem.CommandItemCollection();
		public CommandItem.CommandItemCollection Items { get { return mvarItems; } }
	}
}
