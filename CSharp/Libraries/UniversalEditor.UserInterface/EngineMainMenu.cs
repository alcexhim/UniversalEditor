using System;

using UniversalWidgetToolkit;

namespace UniversalEditor.UserInterface
{
	public class EngineMainMenu
	{
		private bool mvarEnableTearoff = false;
		/// <summary>
		/// Determines whether menus can be torn off to display their contents in a separate window.
		/// </summary>
		public bool EnableTearoff { get { return mvarEnableTearoff; } set { mvarEnableTearoff = value; } }
		
		private CommandItem.CommandItemCollection mvarItems = new CommandItem.CommandItemCollection();
		public CommandItem.CommandItemCollection Items { get { return mvarItems; } }
	}
}

