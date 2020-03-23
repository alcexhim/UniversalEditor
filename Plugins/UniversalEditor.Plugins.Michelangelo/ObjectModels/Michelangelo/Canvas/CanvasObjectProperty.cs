using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Michelangelo.Canvas
{
	public class CanvasObjectProperty
	{
		public class CanvasObjectPropertyCollection
			: System.Collections.ObjectModel.Collection<CanvasObjectProperty>
		{

		}

		private Guid mvarID = Guid.Empty;
		public Guid ID { get { return mvarID; } set { mvarID = value; } }

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private Type mvarDataType = null;
		public Type DataType { get { return mvarDataType; } set { mvarDataType = value; } }

		private object mvarValue = null;
		public object Value { get { return mvarValue; } set { mvarValue = value; } }
	}
}
