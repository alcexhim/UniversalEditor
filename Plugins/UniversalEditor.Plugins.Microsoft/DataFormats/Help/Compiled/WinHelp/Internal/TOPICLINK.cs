using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	public struct TOPICLINK
	{
		/// <summary>
		/// Size of this link + LinkData1 + LinkData2
		/// </summary>
		public int BlockSize;
		/// <summary>
		/// Length of decompressed LinkData2
		/// </summary>
		public int DataLen2;
		/// <summary>
		/// Windows 3.0 (HC30): number of bytes the TOPICLINK of the previous block is located before this TOPICLINK, that is the block size of the previous TOPICLINK plus
		/// eventually skipped TOPICBLOCKHEADER.
		/// Windows 3.1 (HC31): TOPICPOS of previous TOPICLINK
		/// </summary>
		public int PrevBlock;
		/// <summary>
		/// Windows 3.0 (HC30): number of bytes the TOPICLINK of the next block is located behind this block, including skipped TOPICBLOCKHEADER.
		/// Windows 3.1 (HC31): TOPICPOS of next TOPICLINK
		/// </summary>
		public int NextBlock;
		/// <summary>
		/// includes size of TOPICLINK
		/// </summary>
		public int DataLen1;
		public TopicLinkRecordType RecordType;
	}
}
