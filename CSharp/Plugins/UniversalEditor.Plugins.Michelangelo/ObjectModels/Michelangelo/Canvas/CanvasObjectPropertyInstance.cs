using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Michelangelo.Canvas
{
	public class CanvasObjectPropertyInstance
	{
		public class CanvasObjectPropertyInstanceCollection
			: System.Collections.ObjectModel.Collection<CanvasObjectPropertyInstance>
		{
	
		}

		private CanvasObjectProperty mvarProperty = null;
		public CanvasObjectProperty Property { get { return mvarProperty; } set { mvarProperty = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }

	}
}
