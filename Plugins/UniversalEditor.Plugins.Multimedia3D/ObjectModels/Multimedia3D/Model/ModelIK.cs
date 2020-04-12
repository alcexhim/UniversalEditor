//
//  ModelIK.cs - describes inverse kinematics information for a 3D model
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2013-2020 Mike Becker's Software
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.ObjectModel;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Describes inverse kinematics information for a 3D model.
	/// </summary>
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

				item.Parent = mvarParent;
				item.BoneList.mvarParent = mvarParent;
			}
			protected override void RemoveItem(int index)
			{
				base.RemoveItem(index);

				this[index].Parent = null;
				this[index].BoneList.mvarParent = null;
			}
		}

		public ModelIK()
		{
			BoneList = new ModelBone.ModelBoneCollection(Parent);
		}
		public ModelObjectModel Parent { get; private set; } = null;

		public ModelBone TargetBone { get; set; } = null;
		public ModelBone EffBone { get; set; } = null;
		public ModelBone.ModelBoneCollection BoneList { get; } = null;
		public ushort Index { get; set; } = 0;
		public ushort LoopCount { get; set; } = 0;
		public float LimitOnce { get; set; } = 0f;
		public object Clone()
		{
			ModelIK clone = new ModelIK();
			foreach (ModelBone bone in this.BoneList)
			{
				clone.BoneList.Add(bone);
			}
			clone.EffBone = this.EffBone;
			clone.Index = this.Index;
			clone.LimitOnce = this.LimitOnce;
			clone.LoopCount = this.LoopCount;
			clone.TargetBone = this.TargetBone;
			return clone;
		}
	}
}
