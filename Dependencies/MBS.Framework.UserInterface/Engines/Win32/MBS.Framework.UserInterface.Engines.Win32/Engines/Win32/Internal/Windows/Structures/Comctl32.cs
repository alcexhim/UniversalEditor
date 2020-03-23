using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Structures
{
	internal static class Comctl32
	{

		/// <summary>
		/// Carries information used to load common control classes from the dynamic-link library (DLL). This structure is used with the
		/// <see cref="Methods.User32.InitCommonControlsEx" /> function.
		/// </summary>
		public struct INITCOMMONCONTROLSEX
		{
			/// <summary>
			/// The size of the structure, in bytes.
			/// </summary>
			public int dwSize;
			/// <summary>
			/// The set of bit flags that indicate which common control classes will be loaded from the DLL.
			/// </summary>
			public Constants.Comctl32.InitCommonControlsFlags dwICC;
		}
	}
}
