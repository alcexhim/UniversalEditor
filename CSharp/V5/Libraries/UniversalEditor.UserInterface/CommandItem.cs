using System;
namespace UniversalEditor.UserInterface
{
	public abstract class CommandItem
	{
		public class CommandItemCollection
			: System.Collections.ObjectModel.Collection<CommandItem>
		{
		}
	}
	public class CommandReferenceCommandItem : CommandItem
	{
		private string mvarCommandID = String.Empty;
		public string CommandID { get { return mvarCommandID; } set { mvarCommandID = value; } }
		
		public CommandReferenceCommandItem(string commandID)
		{
			mvarCommandID = commandID;
		}
	}
	public class SeparatorCommandItem : CommandItem
	{
	}
}

