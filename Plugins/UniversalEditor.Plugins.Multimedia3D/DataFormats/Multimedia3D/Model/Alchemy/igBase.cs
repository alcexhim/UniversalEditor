using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy
{
	public abstract class igBase
	{
		public class IGBNodeCollection
			: System.Collections.ObjectModel.Collection<igBase>
		{
		}

		private string mvarName = String.Empty;
		public string TypeName { get { return mvarName; } set { mvarName = value; } }

		public override string ToString()
		{
			return mvarName;
		}

		private ushort[] mvarData = new ushort[0];
		public ushort[] Data { get { return mvarData; } set { mvarData = value; } }
	}
}
