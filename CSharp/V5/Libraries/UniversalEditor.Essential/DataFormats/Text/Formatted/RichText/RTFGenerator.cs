using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Text.Formatted.RichText
{
	public static class RTFGenerator
	{
		private static string mvarWordPadWindows7 = "Msftedit 5.41.21.2509";
		/// <summary>
		/// Gets the RTF generator tag value for Microsoft WordPad on Windows 7.
		/// </summary>
		public static string WordPadWindows7 { get { return mvarWordPadWindows7; } }
	}
}
