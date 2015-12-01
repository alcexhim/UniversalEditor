using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public enum TopicLinkRecordType : byte
	{
		/// <summary>
		/// Version 3.0 displayable information
		/// </summary>
		Display30 = 0x01,
		/// <summary>
		/// Topic header information
		/// </summary>
		TopicHeader = 0x02,
		/// <summary>
		/// Version 3.1 displayable information
		/// </summary>
		Display31 = 0x20,
		/// <summary>
		/// Version 3.1 table
		/// </summary>
		Table31 = 0x23
	}
}
