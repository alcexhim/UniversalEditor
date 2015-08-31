using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public static class ExtensionMethods
	{
		public static bool CompareTo(this CommandShortcutKey value, System.Windows.Forms.Keys keyData)
		{
			// first look at modifier keys
			if (!(((value.Modifiers & CommandShortcutKeyModifiers.Alt) == CommandShortcutKeyModifiers.Alt)
				&& ((keyData & System.Windows.Forms.Keys.Alt) == System.Windows.Forms.Keys.Alt))) return false;
			
			return true;
		}
	}
}
