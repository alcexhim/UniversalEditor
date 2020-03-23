using System;

using MBS.Framework.UserInterface.Input.Keyboard;

namespace MBS.Framework.UserInterface
{
	public class Shortcut
	{
		private KeyboardKey mvarKey = KeyboardKey.None;
		public KeyboardKey Key { get { return mvarKey; } set { mvarKey = value; } }

		private KeyboardModifierKey mvarModifierKeys = KeyboardModifierKey.None;
		public KeyboardModifierKey ModifierKeys { get { return mvarModifierKeys; } set { mvarModifierKeys = value; } }

		public Shortcut (KeyboardKey key, KeyboardModifierKey modifierKeys = KeyboardModifierKey.None)
		{
			mvarKey = key;
			mvarModifierKeys = modifierKeys;
		}
	}
}

