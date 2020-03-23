using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	public class ModelBoneGroup : ICloneable
	{
		public class ModelBoneGroupCollection
			: System.Collections.ObjectModel.Collection<ModelBoneGroup>
		{
			private ModelObjectModel mvarParent = null;
			public ModelBoneGroupCollection(ModelObjectModel parent)
			{
				mvarParent = parent;
			}

			protected override void InsertItem(int index, ModelBoneGroup item)
			{
				base.InsertItem(index, item);
				item.ParentModel = mvarParent;
			}
			protected override void RemoveItem(int index)
			{
				this[index].ParentModel = null;
				base.RemoveItem(index);
			}
		}

		private ModelObjectModel mvarParentModel = null;
		private ModelObjectModel ParentModel
		{
			get { return mvarParentModel; }
			set
			{
				mvarParentModel = value;
				mvarBones.mvarParent = value;
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private ModelBone.ModelBoneCollection mvarBones = new ModelBone.ModelBoneCollection(null);
		public ModelBone.ModelBoneCollection Bones { get { return mvarBones; } }

		public object Clone()
		{
			ModelBoneGroup clone = new ModelBoneGroup();
			foreach (ModelBone bone in mvarBones)
			{
				clone.Bones.Add(bone.Clone() as ModelBone);
			}
			clone.Name = mvarName;
			return clone;
		}
	}
}
