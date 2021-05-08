//
//  TOPICBLOCKHEADER.cs - internal structure representing TOPICBLOCKHEADER for WinHelp files
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

namespace UniversalEditor.DataFormats.Help.Compiled.WinHelp.Internal
{
	/// <summary>
	/// Internal structure representing TOPICBLOCKHEADER for WinHelp files.
	/// </summary>
	internal struct TOPICBLOCKHEADER
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
