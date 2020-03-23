//
//  KeyBinding.cs
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
using MBS.Framework.UserInterface.Input.Keyboard;

namespace MBS.Framework.UserInterface
{
	public class KeyBinding
	{
		public class KeyBindingCollection
			: System.Collections.ObjectModel.Collection<KeyBinding>
		{
		}

		public KeyboardKey Key { get; set; } = KeyboardKey.None;
		public KeyboardModifierKey ModifierKeys { get; set; } = KeyboardModifierKey.None;

		private Command _Command = null;
		public Command Command
		{
			get
			{
				if (_Command == null && _CommandID != null)
				{
					_Command = Application.Commands[_CommandID];
				}
				return _Command;
			}
			set { _Command = value; _CommandID = value.ID; }
		}

		private string _CommandID = null;
		public string CommandID
		{
			get { return _CommandID; }
			set
			{
				_CommandID = value;
				if (Application.Commands[_CommandID] != null)
				{
					_Command = Application.Commands[_CommandID];
				}
			}
		}

		public KeyBinding(string commandID, KeyboardKey key, KeyboardModifierKey modifiers = KeyboardModifierKey.None)
		{
			CommandID = commandID;
			Key = key;
			ModifierKeys = modifiers;
		}
		public KeyBinding(Command command, KeyboardKey key, KeyboardModifierKey modifiers = KeyboardModifierKey.None)
		{
			Command = command;
			Key = key;
			ModifierKeys = modifiers;
		}
	}
}
