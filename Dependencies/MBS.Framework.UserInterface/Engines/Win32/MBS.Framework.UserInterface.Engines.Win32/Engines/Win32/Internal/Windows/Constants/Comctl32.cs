using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Constants
{
	internal static class Comctl32
	{

		[Flags()]
		public enum InitCommonControlsFlags : uint
		{
			/// <summary>
			/// Load list-view and header control classes.
			/// </summary>
			ListView = 0x00000001,
			/// <summary>
			/// Load tree-view and tooltip control classes.
			/// </summary>
			TreeView = 0x00000002,
			/// <summary>
			/// Load toolbar, status bar, trackbar, and tooltip control classes.
			/// </summary>
			Bar = 0x00000004,
			/// <summary>
			/// Load tab and tooltip control classes.
			/// </summary>
			Tab = 0x00000008,
			/// <summary>
			/// Load up-down control class.
			/// </summary>
			UpDown = 0x00000010,
			/// <summary>
			/// Load progress bar control class.
			/// </summary>
			Progress = 0x00000020,
			/// <summary>
			/// Load hot key control class.
			/// </summary>
			HotKey = 0x00000040,
			/// <summary>
			/// Load animate control class.
			/// </summary>
			Animate = 0x00000080,
			/// <summary>
			/// Load date and time picker control class.
			/// </summary>
			Date = 0x00000100,
			/// <summary>
			/// Load ComboBoxEx class.
			/// </summary>
			UserEx = 0x00000200,
			/// <summary>
			/// Load rebar (coolbar) control class.
			/// </summary>
			Cool = 0x00000400,
			/// <summary>
			/// Load IP address class.
			/// </summary>
			Internet = 0x00000800,
			/// <summary>
			/// Load pager control class.
			/// </summary>
			Pager = 0x00001000,
			/// <summary>
			/// Load a native font control class.
			/// </summary>
			NativeFontControl = 0x00002000,
			/// <summary>
			/// Load one of the intrinsic User32 control classes. The user controls include button, edit, static, listbox, combobox, and scroll bar.
			/// </summary>
			Standard = 0x00004000,
			/// <summary>
			/// Load a hyperlink control class.
			/// </summary>
			Link = 0x00008000,
			/// <summary>
			/// Load animate control, header, hot key, list-view, progress bar, status bar, tab, tooltip, toolbar, trackbar, tree-view, and up-down control classes. 
			/// </summary>
			Win95 = 0x000000FF,

			All = 0xFFFFFFFF
		}
	}
}
