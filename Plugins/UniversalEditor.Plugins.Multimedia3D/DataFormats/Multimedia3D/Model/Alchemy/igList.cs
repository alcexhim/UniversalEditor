using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public class igList<T> : igBase where T : igBase
	{
		private uint mvarCapacity = 0;
		public uint Capacity { get { return mvarCapacity; } set { mvarCapacity = value; } }

		private List<T> mvarItems = new List<T>();
		public List<T> Items { get { return mvarItems; } }
	}
}
