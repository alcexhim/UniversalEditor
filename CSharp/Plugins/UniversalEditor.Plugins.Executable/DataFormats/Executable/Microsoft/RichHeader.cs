using System;
using System.Collections.Generic;
using System.Linq;
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
