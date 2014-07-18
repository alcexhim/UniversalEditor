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
		Super = 16
	}
	public enum CommandShortcutKeyValue
	{
		None = 0,
		TopRow0,
		TopRow1,
		TopRow2,
		TopRow3,
		TopRow4,
		TopRow5,
		TopRow6,
		TopRow7,
		TopRow8,
		TopRow9,
		A,
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
		NumPad0,
		NumPad1,
		NumPad2,
		NumPad3,
		NumPad4,
		NumPad5,
		NumPad6,
		NumPad7,
		NumPad8,
		NumPad9
	}
	public class CommandShortcutKey
	{
		private CommandShortcutKeyModifiers mvarModifiers = CommandShortcutKeyModifiers.None;
		public CommandShortcutKeyModifiers Modifiers { get { return mvarModifiers; } set { mvarModifiers = value; } }

		private CommandShortcutKeyValue mvarValue = CommandShortcutKeyValue.None;
		public CommandShortcutKeyValue Value { get { return mvarValue; } set { mvarValue = value; } }

		public CommandShortcutKey(CommandShortcutKeyValue value, CommandShortcutKeyModifiers modifiers = CommandShortcutKeyModifiers.None)
		{
			mvarValue = value;
			mvarModifiers = modifiers;
		}
	}
}
