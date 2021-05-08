//
//  TOPICLINK.cs - internal structure representing TOPICLINK for WinHelp files
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Internal structure representing TOPICLINK for WinHelp files.
	/// </summary>
	internal struct TOPICLINK
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
