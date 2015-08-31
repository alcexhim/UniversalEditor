using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Text.Formatted
{
	public enum FormattedTextFontFamily
	{
		/// <summary>
		/// Unknown or default fonts
		/// </summary>
		None = 0,
		/// <summary>
		/// Roman, proportionally spaced serif fonts (e.g. Times New Roman, Palatino)
		/// </summary>
		Roman = 1,
		/// <summary>
		/// Swiss, proportionally spaced sans serif fonts (e.g. Arial)
		/// </summary>
		Swiss = 2,
		/// <summary>
		/// Fixed-pitch serif and sans serif fonts (e.g. Courier New, Pica)
		/// </summary>
		Modern = 3,
		/// <summary>
		/// Script fonts (e.g. Cursive)
		/// </summary>
		Script = 4,
		/// <summary>
		/// Decorative fonts (e.g. Old English, ITC Zapf Chancery)
		/// </summary>
		Decor = 5,
		/// <summary>
		/// Technical, symbol, and mathematical fonts (e.g. Symbol)
		/// </summary>
		Tech = 6,
		/// <summary>
		/// Arabic, Hebrew, or other bidirectional font (e.g. Miriam)
		/// </summary>
		Bidi = 7
	}
}
