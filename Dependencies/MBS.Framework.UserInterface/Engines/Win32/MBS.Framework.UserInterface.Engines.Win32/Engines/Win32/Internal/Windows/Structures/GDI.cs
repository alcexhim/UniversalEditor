using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace UniversalWidgetToolkit.Engines.Win32.Internal.Windows.Structures
{
	internal static class GDI
	{
		public struct LOGFONT
		{
			public int lfHeight;
			public int lfWidth;
			public int lfEscapement;
			public int lfOrientation;
			public int lfWeight;
			public byte lfItalic;
			public byte lfUnderline;
			public byte lfStrikeOut;
			public Constants.GDI.LogFontCharSet lfCharSet;
			public byte lfOutPrecision;
			public byte lfClipPrecision;
			public Constants.GDI.LogFontQuality lfQuality;
			public Constants.GDI.LogFontPitchAndFamily lfPitchAndFamily;
			public string lfFaceName /* [LF_FACESIZE] */;
		}
	}
}
