//
//  TopicLinkDisplay31Flags.cs - internal enum indicating how a topic link is intended for display on WinHelp 3.1 systems
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Internal enum indicating how a topic link is intended for display on WinHelp 3.1 systems.
	/// </summary>
	[Flags()]
	internal enum TopicLinkDisplay31Flags : ushort
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
