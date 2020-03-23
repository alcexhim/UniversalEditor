using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.Apple.HFS.Internal
{
	internal struct HFSDXInfo
	{
		public HFSPoint scroll;
		public int openChain;
		public short reserved;
		public short comment;
		public int putAway;
	}
}
