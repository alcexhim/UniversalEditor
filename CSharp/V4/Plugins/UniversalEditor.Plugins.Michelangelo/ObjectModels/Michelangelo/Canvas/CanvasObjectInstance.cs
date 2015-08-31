using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Michelangelo.Canvas
{
	public class CanvasObjectInstance
	{
		public class CanvasObjectInstanceCollection
			: System.Collections.ObjectModel.Collection<CanvasObjectInstance>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private CanvasObject mvarObject = null;
		public CanvasObject Object { get { return mvarObject; } set { mvarObject = value; } }

		private CanvasObjectPropertyInstance.CanvasObjectPropertyInstanceCollection mvarProperties = new CanvasObjectPropertyInstance.CanvasObjectPropertyInstanceCollection();
		public CanvasObjectPropertyInstance.CanvasObjectPropertyInstanceCollection Properties { get { return mvarProperties; } }

		private double mvarX = 0.0;
		public double X { get { return mvarX; } set { mvarX = value; } }
		private double mvarY = 0.0;
		public double Y { get { return mvarY; } set { mvarY = value; } }
		private double mvarZ = 0.0;
		public double Z { get { return mvarZ; } set { mvarZ = value; } }


		private double mvarWidth = 0.0;
		public double Width { get { return mvarWidth; } set { mvarWidth = value; } }
		private double mvarHeight = 0.0;
		public double Height { get { return mvarHeight; } set { mvarHeight = value; } }
		private double mvarDepth = 0.0;
		public double Depth { get { return mvarDepth; } set { mvarDepth = value; } }

	}
}
