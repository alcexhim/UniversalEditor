//
//  BALDirectoryEntry.cs - represents a directory entry in a BAL archive
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

using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.KnowledgeAdventure.BAL
{
	/// <summary>
	/// Represents a directory entry in a BAL archive.
	/// </summary>
	public class BALDirectoryEntry
	{
		public int UnknownA1;
		public BALDirectoryEntryAttributes Attributes;
		public int UnknownA3;
		public int Offset;
		public int Length;

		public string Name;

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append(Name);
			sb.Append(" @ " + Offset + " : " + Length);
			sb.Append(" [ ");
			sb.Append(Attributes);
			sb.Append(" ]");
			return sb.ToString();
		}
	}
}
