//
//  Context.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;

namespace MBS.Framework.UserInterface
{
	public class Context
	{
		public class ContextCollection
			: System.Collections.ObjectModel.Collection<Context>
		{
			protected override void ClearItems()
			{
				for (int i = 0; i < this.Count; i++)
				{
					Application.RemoveContext(this[i]);
				}
				base.ClearItems();
			}
			protected override void InsertItem(int index, Context item)
			{
				base.InsertItem(index, item);
				Application.AddContext(item);
			}
			protected override void RemoveItem(int index)
			{
				Application.RemoveContext(this[index]);
				base.RemoveItem(index);
			}
		}

		public Guid ID { get; private set; } = Guid.Empty;
		public string Name { get; private set; } = String.Empty;

		// public MenuBar MenuBar { get; } = new MenuBar();
		public CommandItem.CommandItemCollection MenuItems { get; } = new CommandItem.CommandItemCollection();
		public Command.CommandCollection Commands { get; } = new Command.CommandCollection();
		public KeyBinding.KeyBindingCollection KeyBindings { get; } = new KeyBinding.KeyBindingCollection();

		public Context(Guid id, string name)
		{
			ID = id;
			Name = name;
		}

		public override string ToString()
		{
			return String.Format("{0}     {1}", Name, ID);
		}

		private Dictionary<string, List<EventHandler>> _CommandEventHandlers = new Dictionary<string, List<EventHandler>>();
		public bool AttachCommandEventHandler(string commandID, EventHandler handler)
		{
			// handle command event handlers attached without a Command instance
			if (!_CommandEventHandlers.ContainsKey(commandID))
			{
				_CommandEventHandlers.Add(commandID, new List<EventHandler>());
			}
			if (!_CommandEventHandlers[commandID].Contains(handler))
			{
				_CommandEventHandlers[commandID].Add(handler);
				return true;
			}
			return false;
		}
		public bool ExecuteCommand(string commandID)
		{
			if (_CommandEventHandlers.ContainsKey(commandID))
			{
				for (int i = 0; i < _CommandEventHandlers[commandID].Count; i++)
				{
					_CommandEventHandlers[commandID][i](this, EventArgs.Empty);
				}
			}
			return false;
		}
	}
}
