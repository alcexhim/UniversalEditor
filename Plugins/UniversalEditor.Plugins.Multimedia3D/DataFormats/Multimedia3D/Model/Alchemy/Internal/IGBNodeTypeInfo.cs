using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Internal
{
	internal class IGBNodeTypeInfo
	{
		public uint nameLength;
		public uint param02;
		public uint param03;
		public uint fieldCount;
		public uint parentNodeindex;
		public uint childCount;
		public string name;
		public IGBNodeTypeInfo parentNode = null;

		public override string ToString()
		{
			return String.Format("{0} : {1}", name, parentNode?.name);
		}
	}
}
