using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Internal
{
	public struct IGB_ENTRY
	{
		public IGBNodeEntryType type;
		public uint[] entries;
		public override string ToString()
		{
			return String.Format("{0}: {1}", type, String.Join<uint>(", ", entries));
		}
	}
}
