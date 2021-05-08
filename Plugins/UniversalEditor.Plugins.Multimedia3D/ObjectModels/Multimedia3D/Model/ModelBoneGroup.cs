//
//  ModelBoneGroup.cs - represents a bone group in a 3D model
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

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Represents a bone group in a 3D model.
	/// </summary>
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
				Bones.mvarParent = value;
			}
		}

		public string Name { get; set; } = String.Empty;
		public ModelBone.ModelBoneCollection Bones { get; } = new ModelBone.ModelBoneCollection(null);

		public object Clone()
		{
			ModelBoneGroup clone = new ModelBoneGroup();
			foreach (ModelBone bone in Bones)
			{
				clone.Bones.Add(bone.Clone() as ModelBone);
			}
			clone.Name = Name;
			return clone;
		}
	}
}
