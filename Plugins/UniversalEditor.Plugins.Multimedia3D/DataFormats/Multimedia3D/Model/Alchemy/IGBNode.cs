using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public class IGBNode
	{
		public class IGBNodeCollection
			: System.Collections.ObjectModel.Collection<IGBNode>
		{
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		public override string ToString()
		{
			return mvarName;
		}

		private ushort[] mvarData = new ushort[0];
		public ushort[] Data { get { return mvarData; } set { mvarData = value; } }
	}
}
