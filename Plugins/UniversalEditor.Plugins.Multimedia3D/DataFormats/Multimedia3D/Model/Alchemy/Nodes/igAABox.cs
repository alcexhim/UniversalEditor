using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.Alchemy.Nodes
{
	public class igAABox : igObject
	{
		public PositionVector3 Minimum { get; set; } = new PositionVector3();
		public PositionVector3 Maximum { get; set; } = new PositionVector3();

		public override string ToString()
		{
			return String.Format(" ( {0} ) - ( {1} ) ", Minimum, Maximum);
		}
	}
}
