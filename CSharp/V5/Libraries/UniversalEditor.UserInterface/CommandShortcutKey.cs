using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface
{
	[Flags()]
	public enum CommandShortcutKeyModifiers
	{
		None = 0,
		Control = 1,
		Shift = 2,
		Alt = 4,
		Meta = 8,
		Super = 16,
		Hyper = 32
	}
	public enum CommandShortcutKeyValue
	{
		None = 0,

		//
		// Summary:
		//     The SPACEBAR key.
		Space = 32,
		//
		// Summary:
		//     The PAGE UP key.
		Prior = 33,
		//
		// Summary:
		//     The PAGE UP key.
		PageUp = 33,
		//
		// Summary:
		//     The PAGE DOWN key.
		Next = 34,
		//
		// Summary:
		//     The PAGE DOWN key.
		PageDown = 34,
		//
		// Summary:
		//     The END key.
		End = 35,
		//
		// Summary:
		//     The HOME key.
		Home = 36,
		//
		// Summary:
		//     The LEFT ARROW key.
		Left = 37,
		//
		// Summary:
		//     The UP ARROW key.
		Up = 38,
		//
		// Summary:
		//     The RIGHT ARROW key.
		Right = 39,
		//
		// Summary:
		//     The DOWN ARROW key.
		Down = 40,
		//
		// Summary:
		//     The SELECT key.
		Select = 41,
		//
		// Summary:
		//     The PRINT key.
		Print = 42,
		//
		// Summary:
		//     The EXECUTE key.
		Execute = 43,
		//
		// Summary:
		//     The PRINT SCREEN key.
		PrintScreen = 44,
		//
		// Summary:
		//     The INS key.
		Insert = 45,
		//
		// Summary:
		//     The DEL key.
		Delete = 46,
		//
		// Summary:
		//     The HELP key.
		Help = 47,

		TopRow0 = 48,
		TopRow1,
		TopRow2,
		TopRow3,
		TopRow4,
		TopRow5,
		TopRow6,
		TopRow7,
		TopRow8,
		TopRow9,

		A = 65,
		B,
		C,
		D,
		E,
		F,
		G,
		H,
		I,
		J,
		K,
		L,
		M,
		N,
		O,
		P,
		Q,
		R,
		S,
		T,
		U,
		V,
		W,
		X,
		Y,
		Z,

		NumPad0 = 96,
		NumPad1,
		NumPad2,
		NumPad3,
		NumPad4,
		NumPad5,
		NumPad6,
		NumPad7,
		NumPad8,
		NumPad9,

		F1 = 112,
		F2,
		F3,
		F4,
		F5,
		F6,
		F7,
		F8,
		F9,
		F10,
		F11,
		F12
	}
	public class CommandShortcutKey
	{
		private CommandShortcutKeyModifiers mvarModifiers = CommandShortcutKeyModifiers.None;
		public CommandShortcutKeyModifiers Modifiers { get { return mvarModifiers; } set { mvarModifiers = value; } }

		private CommandShortcutKeyValue mvarValue = CommandShortcutKeyValue.None;
		public CommandShortcutKeyValue Value { get { return mvarValue; } set { mvarValue = value; } }

		public CommandShortcutKey()
			: this(CommandShortcutKeyValue.None, CommandShortcutKeyModifiers.None)
		{
		}
		public CommandShortcutKey(CommandShortcutKeyValue value, CommandShortcutKeyModifiers modifiers = CommandShortcutKeyModifiers.None)
		{
			mvarValue = value;
			mvarModifiers = modifiers;
		}
	}
}
