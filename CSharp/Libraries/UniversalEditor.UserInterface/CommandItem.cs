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
	public class CommandPlaceholderCommandItem : CommandItem
	{
		private string mvarPlaceholderID = String.Empty;
		public string PlaceholderID { get { return mvarPlaceholderID; } set { mvarPlaceholderID = value; } }

		public CommandPlaceholderCommandItem(string placeholderID)
		{
			mvarPlaceholderID = placeholderID;
		}
	}
	public class SeparatorCommandItem : CommandItem
	{
	}
}

