using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	[Flags()]
	public enum TopicLinkDisplay31Flags : ushort
	{
		None = 0x0000,
		UnknownFollows = 0x0001,
		SpacingAboveFollows = 0x0002,
		SpacingBelowFollows = 0x0004,
		SpacingLinesFollows = 0x0008,
		LeftIndentFollows = 0x0010,
		RightIndentFollows = 0x0020,
		FirstLineIndentFollows = 0x0040,
		Unused1 = 0x0080,
		BorderInfoFollows = 0x0100,
		TabInfoFollows = 0x0200,
		RightAlignedParagraph = 0x0400,
		CenterAlignedParagraph = 0x0800
	}
}
