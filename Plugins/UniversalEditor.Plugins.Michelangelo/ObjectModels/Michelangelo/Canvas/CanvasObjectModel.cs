using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Michelangelo.Canvas
{
	public class CanvasObjectModel : ObjectModel
	{
		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Title = "Michelangelo canvas";
				_omr.Path = new string[] { "Michelangelo" };
			}
			return _omr;
		}

		private CanvasObject.CanvasObjectCollection mvarObjects = new CanvasObject.CanvasObjectCollection();
		public CanvasObject.CanvasObjectCollection Objects { get { return mvarObjects; } }

		private CanvasObjectInstance.CanvasObjectInstanceCollection mvarObjectInstances = new CanvasObjectInstance.CanvasObjectInstanceCollection();
		public CanvasObjectInstance.CanvasObjectInstanceCollection ObjectInstances { get { return mvarObjectInstances; } }

		public override void Clear()
		{

		}

		public override void CopyTo(ObjectModel where)
		{
		}
	}
}
