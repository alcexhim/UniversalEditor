//
//  MicrosoftRegistryKeyNode.cs - describes a registry key node in a Microsoft registry file
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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

namespace UniversalEditor.DataFormats.PropertyList.Registry
{
	/// <summary>
	/// Describes a registry key node in a Microsoft registry file.
	/// </summary>
	public struct MicrosoftRegistryKeyNode
	{
		public long LocalOffset;

		public MicrosoftRegistryKeyNodeFlags Flags;
		public DateTime LastModifiedTimestamp;
		public MicrosoftRegistryKeyNodeAccess Access;
		public int ParentKeyNodeOffset;
		public int SubkeyCount;
		public int VolatileSubkeyCount;
		public int SubkeysListOffset;
		public int VolatileSubkeysListOffset;
		public int KeyValueCount;
		public int KeyValueListOffset;
		public int KeySecurityOffset;
		public int ClassNameOffset;
		public int LargestSubkeyNameLength;
		public int LargestSubkeyClassNameLength;
		public int LargestValueNameLength;
		public int LargestValueDataSize;
		public int Workvar;
		public int ClassNameLength;
		public string KeyName;

		public override string ToString()
		{
			return "[" + LocalOffset.ToString().PadLeft(12, ' ') + "] " + KeyName;
		}
	}
}
