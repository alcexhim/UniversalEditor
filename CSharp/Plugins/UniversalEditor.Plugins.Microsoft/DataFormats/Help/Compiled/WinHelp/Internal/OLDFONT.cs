using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct OLDFONT
	{
		public FontAttributes Attributes;
		public byte HalfPoints;
		public FontFamily FontFamily;
		public ushort FacenameIndex;
		/// <summary>
		/// RGB values of foreground
		/// </summary>
		public byte[] ForegroundColor;
		/// <summary>
		/// Unused background RGB Values
		/// </summary>
		public byte[] BackgroundColor;
	}
}
