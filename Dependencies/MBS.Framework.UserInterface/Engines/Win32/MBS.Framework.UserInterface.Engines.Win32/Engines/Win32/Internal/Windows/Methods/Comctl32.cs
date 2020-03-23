using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Methods
{
	internal static class Comctl32
	{
		/// <summary>
		/// Ensures that the common control DLL (Comctl32.dll) is loaded, and registers specific common control classes from the DLL. An application must call this function
		/// before creating a common control.
		/// </summary>
		/// <param name="lpInitCtrls">
		/// A pointer to an <see cref="Structures.User32.INITCOMMONCONTROLSEX" /> structure that contains information specifying which control classes will be registered.
		/// </param>
		/// <returns>Returns TRUE if successful, or FALSE otherwise.</returns>
		/// <remarks>
		/// The effect of each call to InitCommonControlsEx is cumulative. For example, if InitCommonControlsEx is called with the ICC_UPDOWN_CLASS flag, then is later called
		/// with the ICC_HOTKEY_CLASS flag, the result is that both the up-down and hot key common control classes are registered and available to the application.
		/// </remarks>
		[DllImport("comctl32.dll")]
		public static extern bool InitCommonControlsEx(ref Structures.Comctl32.INITCOMMONCONTROLSEX lpInitCtrls);
	}
}
