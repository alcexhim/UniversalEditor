using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelRigidBodyCollisionShape : ICloneable
	{
		public object Clone()
		{
			return new ModelRigidBodyCollisionShape();
		}
	}
}
