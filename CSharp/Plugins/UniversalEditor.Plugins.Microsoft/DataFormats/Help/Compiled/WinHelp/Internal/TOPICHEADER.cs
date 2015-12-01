using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct TOPICHEADER
	{
		/// <summary>
		/// Size of topic, including internal topic links
		/// </summary>
		public int BlockSize;
		/// <summary>
		/// Topic offset for prev topic in browse sequence
		/// </summary>
		public int BrowseSequencePreviousTopic;
		/// <summary>
		/// Topic offset for next topic in browse sequence
		/// </summary>
		public int BrowseSequenceNextTopic;
		/// <summary>
		/// Topic number
		/// </summary>
		public int TopicNum;
		/// <summary>
		/// Start of non-scrolling region (topic offset) or -1
		/// </summary>
		public int NonScrollingRegionOffset;
		/// <summary>
		/// Start of scrolling region (topic offset)
		/// </summary>
		public int ScrollingRegionOffset;
		/// <summary>
		/// Start of next type 2 record
		/// </summary>
		public int NextTopic;
	}
}
