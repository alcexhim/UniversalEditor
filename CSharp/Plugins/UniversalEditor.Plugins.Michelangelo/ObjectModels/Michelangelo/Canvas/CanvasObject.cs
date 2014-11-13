using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Michelangelo.Canvas
{
	public class CanvasObject
	{
		public class CanvasObjectCollection
			: System.Collections.ObjectModel.Collection<CanvasObject>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private CanvasObjectProperty.CanvasObjectPropertyCollection mvarProperties = new CanvasObjectProperty.CanvasObjectPropertyCollection();
		public CanvasObjectProperty.CanvasObjectPropertyCollection Properties { get { return mvarProperties; } }
		
	}
}
