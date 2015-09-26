using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal struct HFSFXInfo
	{
		public short iconID;
		public byte[] reserved;
		public short comment;
		public int putAway;
	}
}
