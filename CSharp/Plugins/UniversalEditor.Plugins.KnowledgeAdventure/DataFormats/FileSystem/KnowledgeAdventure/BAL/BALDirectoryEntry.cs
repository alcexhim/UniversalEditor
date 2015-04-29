using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.KnowledgeAdventure.BAL
{
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
