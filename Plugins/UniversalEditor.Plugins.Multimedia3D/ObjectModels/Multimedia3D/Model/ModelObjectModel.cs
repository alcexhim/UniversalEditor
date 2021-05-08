//
//  ModelObjectModel.cs - provides an ObjectModel for manipulating 3D models
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
using System.Collections.Generic;
using System.Collections.Specialized;
using UniversalEditor.ObjectModels.Multimedia3D.Model.Morphing;

namespace UniversalEditor.ObjectModels.Multimedia3D.Model
{
	/// <summary>
	/// Provides an <see cref="ObjectModel" /> for manipulating 3D models.
	/// </summary>
	public class ModelObjectModel : ObjectModel
	{
		public ModelObjectModel()
		{
			mvarBones = new ModelBone.ModelBoneCollection(this);
			mvarIK = new ModelIK.ModelIKCollection(this);
			mvarBoneGroups = new ModelBoneGroup.ModelBoneGroupCollection(this);
		}

		public bool MaterialsLoading = false;
		public bool MaterialsLoaded = false;

		private static ObjectModelReference _omr = null;
		protected override ObjectModelReference MakeReferenceInternal()
		{
			if (_omr == null)
			{
				_omr = base.MakeReferenceInternal();
				_omr.Path = new string[] { "Multimedia", "3D Multimedia", "3D Model" };
				_omr.Description = "A model that can be manipulated in 3D space.";
			}
			return _omr;
		}

		private ModelIndexSizes mvarIndexSizes = new ModelIndexSizes();
		public ModelIndexSizes IndexSizes
		{
			get { return mvarIndexSizes; }
		}

		private ModelBoneGroup.ModelBoneGroupCollection mvarBoneGroups = null;
		public ModelBoneGroup.ModelBoneGroupCollection BoneGroups { get { return mvarBoneGroups; } }

		private List<ushort> mvarExpressions = new List<ushort>();
		public List<ushort> Expressions
		{
			get { return mvarExpressions; }
		}

		private StringCollection mvarNodeNames = new StringCollection();
		public StringCollection NodeNames { get { return mvarNodeNames; } }

		private ModelSkin.ModelSkinCollection mvarSkins = new ModelSkin.ModelSkinCollection();
		public ModelSkin.ModelSkinCollection Skins { get { return mvarSkins; } }

		private ModelSurface.ModelSurfaceCollection mvarSurfaces = new ModelSurface.ModelSurfaceCollection();
		public ModelSurface.ModelSurfaceCollection Surfaces { get { return mvarSurfaces; } }

		private ModelMaterial.ModelMaterialCollection mvarMaterials = new ModelMaterial.ModelMaterialCollection();
		public ModelMaterial.ModelMaterialCollection Materials { get { return mvarMaterials; } }

		private ModelBone.ModelBoneCollection mvarBones = null;
		public ModelBone.ModelBoneCollection Bones { get { return mvarBones; } }

		private ModelIK.ModelIKCollection mvarIK = null;
		public ModelIK.ModelIKCollection IK { get { return mvarIK; } }

		private ModelRigidBody.ModelRigidBodyCollection mvarRigidBodies = new ModelRigidBody.ModelRigidBodyCollection();
		public ModelRigidBody.ModelRigidBodyCollection RigidBodies { get { return mvarRigidBodies; } }

		private ModelJoint.ModelJointCollection mvarJoints = new ModelJoint.ModelJointCollection();
		public ModelJoint.ModelJointCollection Joints { get { return mvarJoints; } }

		private ModelTexture.ModelTextureCollection mvarTextures = new ModelTexture.ModelTextureCollection();
		public ModelTexture.ModelTextureCollection Textures { get { return mvarTextures; } }

		private ModelMorph.ModelMorphCollection mvarMorphs = new ModelMorph.ModelMorphCollection();
		public ModelMorph.ModelMorphCollection Morphs { get { return mvarMorphs; } }

		private Dictionary<int, ModelStringTableExtension> mvarStringTable = new Dictionary<int, ModelStringTableExtension>();
		public Dictionary<int, ModelStringTableExtension> StringTable { get { return mvarStringTable; } }

		private StringCollection mvarModelEffectScriptFileNames = new StringCollection();
		public StringCollection ModelEffectScriptFileNames { get { return mvarModelEffectScriptFileNames; } }

		private StringCollection mvarToonNames = new StringCollection();
		public StringCollection ToonNames { get { return mvarToonNames; } }

		public override void Clear()
		{
			mvarBoneGroups.Clear();
			mvarBones.Clear();
			mvarExpressions.Clear();
			mvarIgnoreEdgeFlag = false;
			mvarIK.Clear();
			mvarIndexSizes.Clear();
			mvarMaterials.Clear();
			mvarMorphs.Clear();
			mvarNodeNames.Clear();
			mvarRigidBodies.Clear();
			mvarSkins.Clear();
			mvarStringTable.Clear();
			mvarSurfaces.Clear();
			mvarToonNames.Clear();
			mvarJoints.Clear();
		}
		public override void CopyTo(ObjectModel destination)
		{
			ModelObjectModel clone = destination as ModelObjectModel;
			if (clone == null) return;

			foreach (ModelBoneGroup group in mvarBoneGroups)
			{
				clone.BoneGroups.Add(group.Clone() as ModelBoneGroup);
			}
			foreach (ModelBone bone in this.mvarBones)
			{
				clone.Bones.Add(bone.Clone() as ModelBone);
			}
			foreach (ushort expression in this.mvarExpressions)
			{
				clone.Expressions.Add(expression);
			}
			foreach (ModelIK ik in this.mvarIK)
			{
				clone.IK.Add(ik.Clone() as ModelIK);
			}
			foreach (ModelMaterial mat in this.mvarMaterials)
			{
				clone.Materials.Add(mat.Clone() as ModelMaterial);
			}
			foreach (ModelRigidBody rigidBody in this.mvarRigidBodies)
			{
				clone.RigidBodies.Add(rigidBody.Clone() as ModelRigidBody);
			}
			foreach (ModelMorph morph in this.mvarMorphs)
			{
				clone.Morphs.Add(morph.Clone() as ModelMorph);
			}
			foreach (string nodeName in this.mvarNodeNames)
			{
				clone.NodeNames.Add(nodeName);
			}
			foreach (ModelSkin skin in this.mvarSkins)
			{
				clone.Skins.Add(skin.Clone() as ModelSkin);
			}
			foreach (ModelSurface surf in mvarSurfaces)
			{
				clone.Surfaces.Add(surf.Clone() as ModelSurface);
			}
			foreach (string toonName in mvarToonNames)
			{
				clone.ToonNames.Add(toonName.Clone() as string);
			}
			foreach (ModelJoint joint in mvarJoints)
			{
				clone.Joints.Add(joint.Clone() as ModelJoint);
			}
			foreach (KeyValuePair<int, ModelStringTableExtension> kvp in mvarStringTable)
			{
				clone.StringTable.Add(kvp.Key, kvp.Value.Clone() as ModelStringTableExtension);
			}
		}

		private bool mvarIgnoreEdgeFlag = false;
		public bool IgnoreEdgeFlag { get { return mvarIgnoreEdgeFlag; } set { mvarIgnoreEdgeFlag = value; } }

		public string Name
		{
			get
			{
				if (mvarStringTable.ContainsKey(1033))
				{
					return mvarStringTable[1033].Title;
				}
				else
				{
					foreach (KeyValuePair<int, ModelStringTableExtension> kvp in mvarStringTable)
					{
						return kvp.Value.Title;
					}
					return String.Empty;
				}
			}
			set
			{
				if (mvarStringTable.ContainsKey(1033))
				{
					mvarStringTable[1033].Title = value;
				}
				else
				{
					ModelStringTableExtension strtbl = new ModelStringTableExtension();
					strtbl.Title = value;
					mvarStringTable.Add(1033, strtbl);
				}
			}
		}

		public void ResetPose()
		{
			foreach (Model.ModelBone bone in mvarBones)
			{
				bone.Reset();
			}
		}
		public void ApplyPose(Pose.PoseObjectModel pose)
		{
			if (pose.ModelName != Name)
			{
				Console.WriteLine("poser warning: model name mismatch");
			}

			BeginUpdate();
			foreach (Pose.PoseBone bone in pose.Bones)
			{
				Model.ModelBone affBone = mvarBones[bone.BoneName];
				if (affBone != null)
				{
					affBone.Position = bone.Position;
					affBone.Rotation = bone.Quaternion;
				}
			}
			EndUpdate();
		}

		private bool mvarUpdating = false;
		public void BeginUpdate()
		{
			if (!mvarUpdating)
			{
				mvarUpdating = true;
			}
		}
		public void EndUpdate()
		{
			if (mvarUpdating)
			{
				mvarUpdating = false;
				Update();
			}
		}

		internal void Update()
		{
			if (mvarUpdating) return;
			mvarUpdating = true;

			//SkinningMatrix = -BoneInitialPosition * LocalRotationMatrix * (BoneInitialPosition + UserMove) * ParentMatrix

			// 物理演算反映
			// Physics reflect
			#if ENABLE_PHYSICS
			for( unsigned int i = 0 ; i < m_ulRigidBodyNum ; i++ )
			{
				m_pRigidBodyArray[i].updateBoneTransform();
			}
			#endif

			// スキニング用行列の更新
			// Update of skinning matrix
			// 2013-07-20 Michael Becker: removed because UpdateSkinningMatrix is called during GetSkinningMatrix anyway
			/*
			for(ushort i = 0; i < mvarBones.Count; i++)
			{
				mvarBones[i].UpdateSkinningMatrix();
			}
			*/

			// 頂点スキニング
			// Vertex skinning
			for (int i = 0; i < mvarSurfaces.Count; i++)
			{
				mvarSurfaces[i].Update();
			}

			mvarUpdating = false;
		}

		public void Reset()
		{
			foreach (ModelBone bone in mvarBones)
			{
				bone.Reset();
			}
			foreach (ModelSurface surf in mvarSurfaces)
			{
				surf.Reset();
			}
		}
	}
}
