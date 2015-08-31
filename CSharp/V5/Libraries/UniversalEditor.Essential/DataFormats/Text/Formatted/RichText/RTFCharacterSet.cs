using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Text.Formatted.RichText
{
	public enum RTFCharacterSet
	{
		/// <summary>
		/// ANSI (ansi)
		/// </summary>
		ANSI,
		/// <summary>
		/// Apple Macintosh (mac)
		/// </summary>
		AppleMacintosh,
		/// <summary>
		/// IBM PC code page 437 (pc)
		/// </summary>
		IBMPC437,
		/// <summary>
		/// IBM PC code page 850, used by IBM Personal System/2 (not implemented in version 1 of
		/// Microsoft Word for OS/2) (pca)
		/// </summary>
		IBMPC850
	}
}
