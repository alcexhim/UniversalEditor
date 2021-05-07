using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAABox : igBase
	{
		private PositionVector3 mvarMinimum = new PositionVector3();
		public PositionVector3 Minimum { get { return mvarMinimum; } set { mvarMinimum = value; } }

		private PositionVector3 mvarMaximum = new PositionVector3();
		public PositionVector3 Maximum { get { return mvarMaximum; } set { mvarMaximum = value; } }
	}
}
