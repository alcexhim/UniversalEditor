using System;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing
{
	public class ModelMorphBone : ModelMorph
	{
		private long mvarBoneIndex = 0L;
		private PositionVector3 mvarTravelDistance = default(PositionVector3);
		private PositionVector4 mvarRotation = default(PositionVector4);
		public long BoneIndex
		{
			get
			{
				return this.mvarBoneIndex;
			}
			set
			{
				this.mvarBoneIndex = value;
			}
		}
		public PositionVector3 TravelDistance
		{
			get
			{
				return this.mvarTravelDistance;
			}
			set
			{
				this.mvarTravelDistance = value;
			}
		}
		public PositionVector4 Rotation
		{
			get
			{
				return this.mvarRotation;
			}
			set
			{
				this.mvarRotation = value;
			}
		}
		public override object Clone()
		{
			return new ModelMorphBone
			{
				BoneIndex = this.mvarBoneIndex, 
				Name = base.Name, 
				Rotation = new PositionVector4(this.mvarRotation.X, this.mvarRotation.Y, this.mvarRotation.Z, this.mvarRotation.W), 
				TravelDistance = new PositionVector3(this.mvarTravelDistance.X, this.mvarRotation.Y, this.mvarRotation.Z)
			};
		}
	}
}
