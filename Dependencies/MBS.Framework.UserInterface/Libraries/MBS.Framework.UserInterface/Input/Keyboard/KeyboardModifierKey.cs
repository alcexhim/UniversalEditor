using System;

namespace MBS.Framework.UserInterface.Input.Keyboard
{
	[Flags()]
	public enum KeyboardModifierKey
	{
		None = 0,
		Control = 1,
		Alt = 2,
		Shift = 4,
		Meta = 8,
		Super = 16,
		Hyper = 32,
		Top = 64
	}
}

