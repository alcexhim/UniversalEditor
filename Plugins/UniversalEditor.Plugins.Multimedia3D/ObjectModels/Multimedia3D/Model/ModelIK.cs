using System;
using System.Collections.ObjectModel;
namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelIK : ICloneable
	{
		public class ModelIKCollection : Collection<ModelIK>
		{
            private ModelObjectModel mvarParent = null;
            public ModelIKCollection(ModelObjectModel parent)
            {
                mvarParent = parent;
            }

            protected override void InsertItem(int index, ModelIK item)
            {
                base.InsertItem(index, item);

                item.mvarParent = mvarParent;
                item.BoneList.mvarParent = mvarParent;
            }
            protected override void RemoveItem(int index)
            {
                base.RemoveItem(index);

                this[index].mvarParent = null;
                this[index].BoneList.mvarParent = null;
            }
		}

        public ModelIK()
        {
            mvarBoneList = new ModelBone.ModelBoneCollection(mvarParent);
        }

        private ModelObjectModel mvarParent = null;
        public ModelObjectModel Parent { get { return mvarParent; } }

		private ModelBone mvarTargetBone = null;
		private ModelBone mvarEffBone = null;
		private ModelBone.ModelBoneCollection mvarBoneList = null;
		private ushort mvarIndex = 0;
		private ushort mvarLoopCount = 0;
		private float mvarLimitOnce = 0f;
		public ModelBone TargetBone
		{
			get
			{
				return this.mvarTargetBone;
			}
			set
			{
				this.mvarTargetBone = value;
			}
		}
		public ModelBone EffBone
		{
			get
			{
				return this.mvarEffBone;
			}
			set
			{
				this.mvarEffBone = value;
			}
		}
		public ModelBone.ModelBoneCollection BoneList
		{
			get
			{
				return this.mvarBoneList;
			}
		}
		public ushort Index
		{
			get
			{
				return this.mvarIndex;
			}
			set
			{
				this.mvarIndex = value;
			}
		}
		public ushort LoopCount
		{
			get
			{
				return this.mvarLoopCount;
			}
			set
			{
				this.mvarLoopCount = value;
			}
		}
		public float LimitOnce
		{
			get
			{
				return this.mvarLimitOnce;
			}
			set
			{
				this.mvarLimitOnce = value;
			}
		}
		public object Clone()
		{
			ModelIK clone = new ModelIK();
			foreach (ModelBone bone in this.mvarBoneList)
			{
				clone.BoneList.Add(bone);
			}
			clone.EffBone = this.mvarEffBone;
			clone.Index = this.mvarIndex;
			clone.LimitOnce = this.mvarLimitOnce;
			clone.LoopCount = this.mvarLoopCount;
			clone.TargetBone = this.mvarTargetBone;
			return clone;
		}
	}
}
