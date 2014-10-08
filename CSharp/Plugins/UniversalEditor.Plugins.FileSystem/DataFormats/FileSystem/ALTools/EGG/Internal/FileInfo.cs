using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.FileSystem.ALTools.EGG.Internal
{
	public class FileInfo
	{
		public string name;
		public ulong length;
		public List<BlockInfo> blocks = new List<BlockInfo>();
	}
}
