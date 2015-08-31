using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.RichTextMarkup.RTML
{
	/// <summary>
	/// Specifies settings for the RTML parser.
	/// </summary>
	public class RTMLSettings
	{
		/// <summary>
		/// The character that defines the beginning of an RTML group.
		/// </summary>
		public char GroupBeginChar { get; set; }
		/// <summary>
		/// The character that defines the end of an RTML group.
		/// </summary>
		public char GroupEndChar { get; set; }
		/// <summary>
		/// The character that defines the beginning of an RTML tag.
		/// </summary>
		public char TagBeginChar { get; set; }
	}
}
