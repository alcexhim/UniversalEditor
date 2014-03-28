using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelTriangle : ICloneable
	{
		public class ModelTriangleCollection : Collection<ModelTriangle>
		{
		}

		private ModelVertex mvarVertex1 = null;
		public ModelVertex Vertex1 { get { return mvarVertex1; } set { mvarVertex1 = value; } }
		private ModelVertex mvarVertex2 = null;
		public ModelVertex Vertex2 { get { return mvarVertex2; } set { mvarVertex2 = value; } }
		private ModelVertex mvarVertex3 = null;
		public ModelVertex Vertex3 { get { return mvarVertex3; } set { mvarVertex3 = value; } }

		public object Clone()
		{
			return new ModelTriangle
			{
				Vertex1 = (mvarVertex1.Clone() as ModelVertex),
				Vertex2 = (mvarVertex2.Clone() as ModelVertex),
				Vertex3 = (mvarVertex3.Clone() as ModelVertex)
			};
		}
	}
}
