using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct TOPICBLOCKHEADER
	{
		/// <summary>
		/// Points to last TOPICLINK in previous block
		/// </summary>
		public int LastTopicLink;
		/// <summary>
		/// Points to first TOPICLINK in this block
		/// </summary>
		public int FirstTopicLink;
		/// <summary>
		/// Points to TOPICLINK of last TOPICHEADER
		/// </summary>
		public int LastTopicHeader;
	}
}
