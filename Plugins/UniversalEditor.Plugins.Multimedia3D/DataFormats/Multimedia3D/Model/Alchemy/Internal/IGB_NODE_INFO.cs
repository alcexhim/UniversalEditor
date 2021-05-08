using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Internal
{
	internal struct IGB_NODE_INFO
	{
		public uint nameLength;
		public uint param02;
		public uint param03;
		public uint param04;
		public uint parentNode;
		public uint childCount;
		public string name;
	}
}
