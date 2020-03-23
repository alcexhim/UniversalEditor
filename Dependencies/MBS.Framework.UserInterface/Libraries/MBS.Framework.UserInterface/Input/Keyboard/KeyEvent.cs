//
//  KeyEvent.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 
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
using System.ComponentModel;

namespace MBS.Framework.UserInterface.Input.Keyboard
{
	public delegate void KeyEventHandler(object sender, KeyEventArgs e);
	public class KeyEventArgs : CancelEventArgs
	{
		private const ulong KEYS_MODIFIER_MASK = 0xFFFFFFFFFFFF0000;

		public KeyEventArgs()
		{

		}
		public KeyEventArgs(KeyboardKey key, KeyboardModifierKey modifierKeys, int keyCode, int hardwareKeyCode)
		{
			Key = key;
			ModifierKeys = modifierKeys;
			KeyCode = keyCode;
			HardwareKeyCode = hardwareKeyCode;
		}

		public KeyboardKey Key { get; set; }
		public KeyboardModifierKey ModifierKeys { get; set; }

		public KeyboardKey KeyData { get { return (KeyboardKey)((uint)Key & KEYS_MODIFIER_MASK); } }

		public int KeyCode { get; set; }
		public int HardwareKeyCode { get; set; }
		public bool KeyIsModifier
		{
			get
			{
				return Key == KeyboardKey.LControlKey
					|| Key == KeyboardKey.LShiftKey
					|| Key == KeyboardKey.LMenu
					|| Key == KeyboardKey.LWin
					|| Key == KeyboardKey.RControlKey
					|| Key == KeyboardKey.RShiftKey
					|| Key == KeyboardKey.RMenu
					|| Key == KeyboardKey.RWin;
			}
		}

		public KeyboardModifierKey KeyAsModifier
		{
			get
			{
				switch (Key)
				{
					case KeyboardKey.LControlKey:
					case KeyboardKey.RControlKey:
						return KeyboardModifierKey.Control;
					case KeyboardKey.LShiftKey:
					case KeyboardKey.RShiftKey:
						return KeyboardModifierKey.Shift;
					case KeyboardKey.LMenu:
					case KeyboardKey.RMenu:
						return KeyboardModifierKey.Alt;
					case KeyboardKey.LWin:
					case KeyboardKey.RWin:
						return KeyboardModifierKey.Super;
				}
				return KeyboardModifierKey.None;
			}
		}
	}
}
