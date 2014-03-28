using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelSkinVertex : ICloneable
	{
		public class ModelSkinVertexCollection : Collection<ModelSkinVertex>
		{
		}

		private ModelVertex mvarTargetVertex = null;
		public ModelVertex TargetVertex { get { return mvarTargetVertex; } set { mvarTargetVertex = value; } }

		private PositionVector3 mvarMaximumPosition = new PositionVector3();
		public PositionVector3 MaximumPosition { get { return mvarMaximumPosition; } set { mvarMaximumPosition = value; } }

		public object Clone()
		{
			ModelSkinVertex clone = new ModelSkinVertex();
			clone.TargetVertex = (mvarTargetVertex.Clone() as ModelVertex);
			clone.MaximumPosition = (PositionVector3)mvarMaximumPosition.Clone();
			return clone;
		}
	}
}
