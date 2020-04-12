//
//  RichHeader.cs - contains classes to read and manipulate the "Rich!" header included in some Microsoft executables
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

namespace UniversalEditor.DataFormats.Executable.Microsoft
{
	public class RichHeaderEntry
	{
		public class RichHeaderEntryCollection
			: System.Collections.ObjectModel.Collection<RichHeaderEntry>
		{

		}

		private int mvarId = 0;
		public int Id { get { return mvarId; } set { mvarId = value; } }

		private int mvarVersion = 0;
		public int Version { get { return mvarVersion; } set { mvarVersion = value; } }

		private int mvarCount = 0;
		public int Count { get { return mvarCount; } set { mvarCount = value; } }

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("Id: ");
			sb.Append(mvarId);
			sb.Append("; Version: ");
			sb.Append(mvarVersion);
			sb.Append("; Count: ");
			sb.Append(mvarCount);
			return sb.ToString();
		}
	}
	public class RichHeader
	{
		private bool mvarEnabled = false;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private RichHeaderEntry.RichHeaderEntryCollection mvarEntries = new RichHeaderEntry.RichHeaderEntryCollection();
		public RichHeaderEntry.RichHeaderEntryCollection Entries { get { return mvarEntries; } }
	}
}
