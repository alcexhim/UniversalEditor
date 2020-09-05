//
//  PMDModelDataFormat.cs - provides a DataFormat for manipulating 3D models in Polygon Movie Maker/MikuMikuDance PMD format
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

using UniversalEditor.IO;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace UniversalEditor.DataFormats.Multimedia3D.Model.PolygonMovieMaker
{
	/// <summary>
	/// Provides a <see cref="DataFormat" /> for manipulating 3D models in Polygon Movie Maker/MikuMikuDance PMD format.
	/// </summary>
	public class PMDModelDataFormat : DataFormat
	{

		// TODO: figure out why PMD Save still generates an incompatible PMD file...

		protected override DataFormatReference MakeReferenceInternal()
		{
			DataFormatReference dfr = base.MakeReferenceInternal();
			dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			dfr.ExportOptions.Add(new CustomOptionText(nameof(ModelTitleJapanese), "_Japanese model title"));
			dfr.ExportOptions.Add(new CustomOptionText(nameof(ModelTitleEnglish), "_English model title"));
			dfr.ExportOptions.Add(new CustomOptionText(nameof(ModelCommentJapanese), "Ja_panese model comment"));
			dfr.ExportOptions.Add(new CustomOptionText(nameof(ModelCommentEnglish), "En_glish model comment"));
			return dfr;
		}

		private string mvarModelTitleJapanese = String.Empty;
		public string ModelTitleJapanese { get { return mvarModelTitleJapanese; } set { mvarModelTitleJapanese = value; } }

		private string mvarModelCommentJapanese = String.Empty;
		public string ModelCommentJapanese { get { return mvarModelCommentJapanese; } set { mvarModelCommentJapanese = value; } }

		private string mvarModelTitleEnglish = String.Empty;
		public string ModelTitleEnglish { get { return mvarModelTitleEnglish; } set { mvarModelTitleEnglish = value; } }

		private string mvarModelCommentEnglish = String.Empty;
		public string ModelCommentEnglish { get { return mvarModelCommentEnglish; } set { mvarModelCommentEnglish = value; } }

		private float mvarVersion = 1.0f;
		public float Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = objectModel as ModelObjectModel;
			if (model == null) throw new ObjectModelNotSupportedException();

			IO.Reader br = base.Accessor.Reader;
			string PMDKey = br.ReadFixedLengthString(3);
			if (PMDKey != "Pmd") throw new InvalidDataFormatException("File does not begin with \"Pmd\"");

			mvarVersion = br.ReadSingle();
			ModelStringTableExtension englishInformation = new ModelStringTableExtension();
			model.StringTable[1033] = englishInformation;
			ModelStringTableExtension japaneseInformation = new ModelStringTableExtension();
			model.StringTable[1041] = japaneseInformation;
			Encoding encoding = Encoding.ShiftJIS;

			string modelName = br.ReadFixedLengthString(20, encoding);
			if (modelName.Contains("\0"))
			{
				modelName = modelName.Substring(0, modelName.IndexOf('\0'));
			}
			japaneseInformation.Title = modelName;
			mvarModelTitleJapanese = modelName;

			string modelComment = br.ReadFixedLengthString(256, encoding);
			if (modelComment.Contains("\0"))
			{
				modelComment = modelComment.Substring(0, modelComment.IndexOf('\0'));
			}
			japaneseInformation.Comments = modelComment;
			mvarModelCommentJapanese = modelComment;

			#region Vertices
			uint nVertices = br.ReadUInt32();

			ushort[] vertexBone0Indices = new ushort[nVertices];
			ushort[] vertexBone1Indices = new ushort[nVertices];

			ModelSurface surf = new ModelSurface();
			model.Surfaces.Add(surf);

			long assertCount = nVertices * 38;
			if (br.Remaining <= assertCount) throw new DataCorruptedException();

			for (uint i = 0u; i < nVertices; i += 1u)
			{
				ModelVertex vtx = new ModelVertex();

				// Vertex position
				float posX = br.ReadSingle();
				float posY = br.ReadSingle();
				float posZ = br.ReadSingle();
				vtx.Position = new PositionVector3(posX, posY, posZ);
				vtx.OriginalPosition = new PositionVector3(posX, posY, posZ);

				// Vertex normal
				float normalX = br.ReadSingle();
				float normalY = br.ReadSingle();
				float normalZ = br.ReadSingle();
				vtx.Normal = new PositionVector3(normalX, normalY, normalZ);
				vtx.OriginalNormal = new PositionVector3(normalX, normalY, normalZ);

				// Vertex texture coordinates
				float textureU = br.ReadSingle();
				float textureV = br.ReadSingle();
				vtx.Texture = new TextureVector2(textureU, textureV);

				// First bone to effect the position of this vertex in vertex/geometry skinning
				ushort bone0 = br.ReadUInt16();
				vertexBone0Indices[i] = bone0;

				// Second bone to effect the position of this verted in vertex/geometry skinning
				ushort bone1 = br.ReadUInt16();
				vertexBone1Indices[i] = bone1;

				// Bone 0 weight (Weight of the first bone for vertex/geometry skinning. Weight
				// of second bone calculated by 1.0 - bone0Weight)
				byte weight = br.ReadByte();
				vtx.Bone0Weight = (float)((float)weight / 100);

				// Edge Flag (Determines if this vertex should be used to draw the edge line
				// around the model) 
				vtx.EdgeFlag = br.ReadBoolean();

				surf.Vertices.Add(vtx);
			}
			#endregion
			#region Faces

			// This section describes the indices of the model. The section starts out with a
			// UInt32 describing how many indices are in the model (MUST be a multiple of 3). These
			// are used for connecting the vertices together and drawing the triangles that are used
			// to form the 3D object.
			//
			// Here is a quick explanation: The indices are stored in pairs of 3. Each of these 3
			// indices represents a corner of a triangle, and points to a vertex in the vertex list.
			// In most 3D APIs (like Direct3D or OpenGL) the API handles drawing these for you with
			// draw functions called "Indexed Drawing" functions. With these, the API will
			// automatically look into the list of indices (called an "Index Buffer") and use each
			// group of three to build a triangle using the index into the list of vertices (called
			// "Vertex Buffer") for each corner of the triangle. This saves processing power and
			// makes the models appear as they should because the GPU/CPU knows what vertices make
			// up which triangles, so it doesn't have to estimate which vertices build which
			// triangle, which usually causes vertices to randomly be connected to each other.
			uint nFacesOrig = br.ReadUInt32();
			uint nFaces = (uint)(nFacesOrig / 3);

			for (uint i = 0; i < nFaces; i++)
			{
				ModelTriangle face = new ModelTriangle();

				ushort vertex1Index = br.ReadUInt16();
				ushort vertex2Index = br.ReadUInt16();
				ushort vertex3Index = br.ReadUInt16();

				face.Vertex1 = surf.Vertices[vertex1Index];
				face.Vertex2 = surf.Vertices[vertex2Index];
				face.Vertex3 = surf.Vertices[vertex3Index];

				surf.Triangles.Add(face);
			}
			#endregion
			#region Materials
			uint nMaterials = br.ReadUInt32();
			List<uint> IndexCounts = new List<uint>();
			for (uint i = 0u; i < nMaterials; i += 1u)
			{
				ModelMaterial material = new ModelMaterial();
				material.Name = "MA" + i.ToString();

				// Diffuse color, in float (0.0-1.0f) format instead of argb
				material.DiffuseColor = br.ReadColorRGBASingle();

				// shininess
				float shininess = br.ReadSingle();
				material.Shininess = shininess;

				material.SpecularColor = br.ReadColorRGBSingle();
				material.AmbientColor = br.ReadColorRGBSingle();

				// Toon number. if the toon number is 255 (sbyte -1) means no toon texture.
				// Otherwise it is a value of 0-9 reprsenting an index into the array of
				// toons (MMD uses 10 total toon textures)
				sbyte toonNo = br.ReadSByte();
				material.ToonNumber = toonNo;

				// Edge Flag (determines if the edge line should be drawn around this material)
				bool edge = br.ReadBoolean();
				material.EdgeFlag = edge;

				// Number of indices into the vertex list that are effected by this material.
				// (Divide by 3 to get the number of triangle surfaces that are colored by this
				// material)
				uint faceVertexCount = br.ReadUInt32();
				IndexCounts.Add(faceVertexCount);

				// The file name corresponding to the texture applied to this material. May be
				// left blank. If material has a sphere map, the texture file and sphere map
				// file are separated by a "*". Example: "tex0.bmp*sphere01.spa"
				string texturePath = br.ReadNullTerminatedString(20);

				ModelTextureFlags textureFlags = ModelTextureFlags.None;
				if (texturePath.EndsWith(".sph"))
				{
					textureFlags |= ModelTextureFlags.Map;
					if (texturePath.Contains("*"))
					{
						textureFlags |= ModelTextureFlags.Texture;
					}
				}
				else if (texturePath.EndsWith(".spa"))
				{
					textureFlags |= ModelTextureFlags.AddMap;
					if (texturePath.Contains("*"))
					{
						textureFlags |= ModelTextureFlags.Texture;
					}
				}
				else
				{
					textureFlags |= ModelTextureFlags.Texture;
				}

				string textureFileName = null;
				string mapFileName = null;
				if (texturePath.Contains("*"))
				{
					string[] FileNames = texturePath.Split(new char[] { '*' }, 2);
					if (FileNames.Length == 2)
					{
						textureFileName = FileNames[0];
						mapFileName = FileNames[1];
					}
				}
				else if (texturePath.EndsWith(".spa") || texturePath.EndsWith(".sph"))
				{
					textureFileName = null;
					mapFileName = texturePath;
				}
				else
				{
					textureFileName = texturePath;
					mapFileName = null;
				}

				material.Textures.Add(textureFileName, mapFileName, textureFlags);

				model.Materials.Add(material);
			}
			#region Associate materials with triangles
			{
				int start = 0;
				for (int i = 0; i < model.Materials.Count; i++)
				{
					int indexCount = (int)(IndexCounts[i] / 3);
					int end = (int)(indexCount / 3);
					for (int j = start; j < (indexCount + start); j++)
					{
						model.Materials[i].Triangles.Add(surf.Triangles[j]);
					}
					start += indexCount;
				}
			}
			#endregion
			#endregion


			#region Bones
			Dictionary<ModelBone, short> parentBonesList = new Dictionary<ModelBone, short>();
			Dictionary<ModelBone, short> childBonesList = new Dictionary<ModelBone, short>();
			ushort nBones = br.ReadUInt16();
			for (ushort j = 0; j < nBones; j += 1)
			{
				ModelBone bone2 = new ModelBone();
				string boneName = br.ReadFixedLengthString(20, encoding);
				bone2.Name = boneName.TrimNull();

				short parent = br.ReadInt16();
				if (parent > -1)
				{
					parentBonesList.Add(bone2, parent);
				}
				short child = br.ReadInt16();
				if (child > -1)
				{
					childBonesList.Add(bone2, child);
				}
				byte kind = br.ReadByte();
				bone2.BoneType = (ModelBoneType)kind;

				short ikNum = br.ReadInt16();
				bone2.IKNumber = ikNum;

				// Bone position as a float[3] array
				bone2.Position = br.ReadPositionVector3x32();
				bone2.OriginalPosition = (PositionVector3)bone2.Position.Clone();

				bone2.OriginalRotation = new PositionVector4(0, 0, 0, 1);
				model.Bones.Add(bone2);
			}

			// Associate child bones with their parents
			foreach (KeyValuePair<ModelBone, short> bonePair in parentBonesList)
			{
				bonePair.Key.ParentBone = model.Bones[(int)bonePair.Value];
			}
			// Associate parent bones with their children
			foreach (KeyValuePair<ModelBone, short> bonePair in childBonesList)
			{
				bonePair.Key.ChildBone = model.Bones[(int)bonePair.Value];
			}

			// Associate every vertex with its respective bones
			for (int i = 0; i < nVertices; i++)
			{
				if (vertexBone0Indices[i] < 65535) surf.Vertices[i].Bone0 = model.Bones[vertexBone0Indices[i]];
				if (vertexBone1Indices[i] < 65535) surf.Vertices[i].Bone1 = model.Bones[vertexBone1Indices[i]];
			}
			#endregion
			#region Inverse Kinematics
			ushort nIKs = br.ReadUInt16();
			for (uint i = 0u; i < (uint)nIKs; i += 1u)
			{
				ModelIK ik = new ModelIK();
				ik.Index = br.ReadUInt16();
				ushort targetBoneIndex = br.ReadUInt16();
				if ((int)targetBoneIndex < model.Bones.Count)
				{
					ik.TargetBone = model.Bones[(int)targetBoneIndex];
				}
				byte nLinksInList = br.ReadByte();
				ik.LoopCount = br.ReadUInt16();
				ik.LimitOnce = br.ReadSingle();
				for (byte k = 0; k < nLinksInList; k += 1)
				{
					ushort link = br.ReadUInt16();
					if ((int)link < model.Bones.Count)
					{
						ik.BoneList.Add(model.Bones[(int)link]);
					}
				}
				model.IK.Add(ik);
			}
			#endregion
			#region Skins
			ushort nSkins = br.ReadUInt16();
			for (ushort j = 0; j < nSkins; j += 1)
			{
				ModelSkin skin = new ModelSkin();
				skin.Name = br.ReadFixedLengthString(20, encoding);
				uint nSkinVertices = br.ReadUInt32();
				skin.Category = br.ReadByte();
				for (uint l = 0u; l < nSkinVertices; l += 1u)
				{
					uint nSkinVertexIndex = br.ReadUInt32();
					ModelVertex targetVertex = surf.Vertices[(int)nSkinVertexIndex];
					ModelSkinVertex svtx = new ModelSkinVertex();

					svtx.MaximumPosition = br.ReadPositionVector3x32();
					svtx.TargetVertex = targetVertex;

					skin.Vertices.Add(svtx);
				}
				model.Skins.Add(skin);
			}
			#endregion
			if (br.EndOfStream) return;
			#region Expression list
			// Indices for the face morphs which should be displayed in the "Facial" section in MMD.
			// This section starts with a byte[1] (unsigned char in C++) representing the number of
			// face morphs to display in the "Facial" section of MMD's dopesheet.
			byte nExpressionList = br.ReadByte();
			for (byte m = 0; m < nExpressionList; m += 1)
			{
				// Index in the face morph list of the face morph to display
				ushort n = br.ReadUInt16();
				model.Expressions.Add(n);
			}
			#endregion
			#region Names for the bone groups to display in MMD dopesheet
			byte nNodeNames = br.ReadByte();
			for (byte m = 0; m < nNodeNames; m += 1)
			{
				ModelBoneGroup group = new ModelBoneGroup();
				string nodeNameInShiftJIS = br.ReadNullTerminatedString(50);
				string nodeNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(nodeNameInShiftJIS));

				group.Name = nodeNameInUTF8.Trim();
				model.BoneGroups.Add(group);
			}
			#endregion
			#region Bone to Node Mappings for Dopesheet
			// This section starts with a byte[4] (unsigned long in C++) representing the
			// total number of bones which should be displayed in all groups
			uint totalBoneCount = br.ReadUInt32();

			ushort[] nBoneID = new ushort[totalBoneCount];
			byte[] nNodeID = new byte[totalBoneCount];

			// Indices in the list of bones for which bones should be placed within which groups in MMD's dopesheet.
			for (ushort j = 0; j < totalBoneCount; j++)
			{
				// Index in the bone list representing which bone to display
				nBoneID[j] = br.ReadUInt16();
				// Index in the list of bone groups representing which group to place this bone in 
				nNodeID[j] = br.ReadByte();
			}

			for (ushort j = 0; j < totalBoneCount; j++)
			{
				ushort boneID = nBoneID[j];
				ModelBone bone = model.Bones[boneID];
				byte nodeID = nNodeID[j];

				ModelBoneGroup group = null;
				if (nodeID < model.BoneGroups.Count)
				{
					group = model.BoneGroups[nodeID];
					group.Bones.Add(bone);
				}
			}
			#endregion
			if (br.EndOfStream) return;
			bool hasEnglishInformation = br.ReadBoolean();
			#region English Information
			if (hasEnglishInformation)
			{
				englishInformation.Title = br.ReadFixedLengthString(20);
				mvarModelTitleEnglish = englishInformation.Title;
				englishInformation.Comments = br.ReadFixedLengthString(256);
				mvarModelCommentEnglish = englishInformation.Comments;

				for (ushort j = 0; j < nBones; j++)
				{
					string boneNameInShiftJIS = br.ReadFixedLengthString(20);
					string boneNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(boneNameInShiftJIS));
					englishInformation.BoneNames.Add(boneNameInUTF8.TrimNull());
				}
				for (ushort j = 0; j < nSkins; j++)
				{
					string skinNameInShiftJIS = br.ReadFixedLengthString(20);
					string skinNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(skinNameInShiftJIS));
					englishInformation.SkinNames.Add(skinNameInUTF8.TrimNull());
				}
				br.Accessor.Seek(30L, SeekOrigin.Current);
				for (byte m = 0; m < nNodeNames - 1; m++)
				{
					string nodeNameInShiftJIS = br.ReadFixedLengthString(50);
					string nodeNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(nodeNameInShiftJIS));
					englishInformation.NodeNames.Add(nodeNameInUTF8.TrimNull());
				}
			}
			#endregion
			#region Toon Information
			for (int i2 = 0; i2 < 10; i2++)
			{
				string toonName = br.ReadFixedLengthString(100, encoding);

				int trimIndex = toonName.IndexOf('\0');
				if (trimIndex > -1) toonName = toonName.Substring(0, trimIndex);

				model.ToonNames.Add(toonName);
			}
			#endregion

			if (br.EndOfStream) return;
			#region Physics Information
			{
				#region Rigid Bodies
				uint nBodies = br.ReadUInt32();
				for (uint i = 0u; i < nBodies; i += 1u)
				{
					ModelRigidBody body = new ModelRigidBody();
					body.Name = br.ReadFixedLengthString(20, encoding);
					short boneID = br.ReadInt16();
					if (boneID > -1)
					{
						body.Bone = model.Bones[boneID];
					}
					byte groupID = br.ReadByte();
					body.GroupID = groupID;
					ushort passGroupFlag = br.ReadUInt16();
					bool[] passGroupFlags = new bool[16];
					for (int j2 = 0; j2 < 16; j2++)
					{
						passGroupFlags[j2] = (((int)passGroupFlag & 1 << j2) <= 0);
					}
					body.PassGroupFlags = passGroupFlags;
					body.BoxType = br.ReadByte();
					body.BoxSize = br.ReadPositionVector3x32();
					body.Position = br.ReadPositionVector3x32();
					body.Rotation = br.ReadPositionVector3x32();

					body.Mass = br.ReadSingle();
					body.PositionDamping = br.ReadSingle();
					body.RotationDamping = br.ReadSingle();
					body.Restitution = br.ReadSingle();
					body.Friction = br.ReadSingle();
					body.Mode = br.ReadByte();
					model.RigidBodies.Add(body);
				}
				#endregion
				#region Joints
				uint nJoints = br.ReadUInt32();
				for (uint i = 0u; i < nJoints; i += 1u)
				{
					ModelJoint joint = new ModelJoint();
					joint.Name = br.ReadFixedLengthString(20, encoding);

					// Index in the list of rigid bodies of the first body affected by this constraint 
					int bodyA = br.ReadInt32();
					// Index in the list of rigid bodies of the second body affected by this constraint 
					int bodyB = br.ReadInt32();

					joint.Position = br.ReadPositionVector3x32();
					joint.Rotation = br.ReadPositionVector3x32();
					joint.LimitMoveLow = br.ReadPositionVector3x32();
					joint.LimitMoveHigh = br.ReadPositionVector3x32();
					joint.LimitAngleLow = br.ReadPositionVector3x32();
					joint.LimitAngleHigh = br.ReadPositionVector3x32();
					joint.SpringConstraintMovementStiffness = br.ReadPositionVector3x32();
					joint.SpringConstraintRotationStiffness = br.ReadPositionVector3x32();

					model.Joints.Add(joint);
				}
				#endregion
			}
			#endregion

			/*
            #region PMAX patch
            if (!br.EndOfStream)
            {
                if (br.PeekFixedLengthString(4) != "PMAX") return;

                UniversalEditor.ObjectModels.PMAXPatch.PMAXPatchObjectModel pmax = new UniversalEditor.ObjectModels.PMAXPatch.PMAXPatchObjectModel();
                UniversalEditor.DataFormats.PMAXPatch.PMAXPatchBinaryDataFormat pmaxbin = new UniversalEditor.DataFormats.PMAXPatch.PMAXPatchBinaryDataFormat();

                pmaxbin.Open(br.BaseStream);
                ObjectModel om = pmax;
                pmaxbin.Load(ref om);

                foreach (UniversalEditor.ObjectModels.PMAXPatch.PMAXPatchChunk chunk in pmax.Patches[0].Chunks)
                {
                    if (chunk.Name == "TEXA")
                    {
                        // Texture animation information
                        UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXAdvancedTextureBlockChunk TEXA = (chunk as UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXAdvancedTextureBlockChunk);
                        model.PMAExtension.TextureFlipping.Enabled = true;
                        foreach (UniversalEditor.ObjectModels.PMAXPatch.PMAXAdvancedTextureBlock block in TEXA.AdvancedTextureBlocks)
                        {
                            ModelMaterial material = model.Materials[block.MaterialID];
                            material.AlwaysLight = block.AlwaysLight;

                            foreach (UniversalEditor.ObjectModels.PMAXPatch.PMAXAdvancedTextureBlockImage image in block.Images)
                            {
                                TextureFlippingFrame tff = new TextureFlippingFrame();
                                long timestamp = image.Timestamp;

                                string texturePath = image.FileName;
                                string textureImageFileName = null;
                                string mapImageFileName = null;

                                if (texturePath.Contains("*"))
                                {
                                    string[] texturePaths = texturePath.Split(new char[] { '*' });
                                    textureImageFileName = texturePaths[0];
                                    mapImageFileName = texturePaths[1];
                                }
                                else if (texturePath.EndsWith(".sph") || texturePath.EndsWith(".spa"))
                                {
                                    textureImageFileName = null;
                                    mapImageFileName = texturePath;
                                }
                                else
                                {
                                    textureImageFileName = texturePath;
                                    mapImageFileName = null;
                                }
                                material.Textures.Add(textureImageFileName, mapImageFileName, image.TextureFlags);
                            }
                        }
                    }
                    else if (chunk.Name == "EFXS")
                    {
                        // Effects preset information
                        UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk EFXS = (chunk as UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk);
                        foreach (string effectFileName in EFXS.EffectScriptFileNames)
                        {
                            model.ModelEffectScriptFileNames.Add(effectFileName);
                        }
                    }
                    else if (chunk.Name == "MTLN")
                    {
                        // Material name information
                        UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXMaterialNamesChunk MTLN = (chunk as UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXMaterialNamesChunk);
                        for (int i = 0; i < model.Materials.Count; i++)
                        {
                            model.Materials[i].Name = MTLN.MaterialNames[i];
                        }
                    }
                }
            }
            #endregion
            */
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = objectModel as ModelObjectModel;
			if (model == null) throw new ObjectModelNotSupportedException();

			base.Accessor.DefaultEncoding = Encoding.ShiftJIS;

			IO.Writer bw = base.Accessor.Writer;
			bw.WriteFixedLengthString("Pmd");
			bw.WriteSingle(mvarVersion);

			/*
            string modelNameJ = String.Empty;
            if (model.StringTable.ContainsKey(1041))
            {
                modelNameJ = model.StringTable[1041].Title;
            }
            else
            {
                modelNameJ = model.Name;
            }
            bw.WriteFixedLengthString(modelNameJ, encoding, 20);
            */

			bw.WriteFixedLengthString(mvarModelTitleJapanese, 20);

			/*
            string modelCommentJ = String.Empty;
            if (model.StringTable.ContainsKey(1041))
            {
                modelCommentJ = model.StringTable[1041].Comments;
            }
            bw.WriteFixedLengthString(modelCommentJ, encoding, 256);
            */

			bw.WriteFixedLengthString(mvarModelCommentJapanese, 256);

			ModelSurface surf = model.Surfaces[0];

			bw.WriteUInt32((uint)surf.Vertices.Count);
			for (int i = 0; i < surf.Vertices.Count; i++)
			{
				ModelVertex vtx = surf.Vertices[i];

				PositionVector3 vec = vtx.Position;
				bw.WritePositionVector3x32(vtx.Position);
				bw.WritePositionVector3x32(vtx.Normal);

				TextureVector2 texture = vtx.Texture;
				bw.WriteTextureVector2x32(vtx.Texture);

				ushort indexToBone0 = (ushort)model.Bones.IndexOf(vtx.Bone0);
				ushort indexToBone1 = (ushort)model.Bones.IndexOf(vtx.Bone1);

				bw.WriteUInt16(indexToBone0);
				bw.WriteUInt16(indexToBone1);

				bw.WriteByte((byte)vtx.Bone0Weight);
				bw.WriteBoolean(vtx.EdgeFlag);
			}
			bw.WriteUInt32((uint)(surf.Triangles.Count * 3));
			for (int i = 0; i < surf.Triangles.Count; i++)
			{
				ModelTriangle face = surf.Triangles[i];

				ushort vertex1Index = (ushort)(surf.Vertices.IndexOf(face.Vertex1));
				ushort vertex2Index = (ushort)(surf.Vertices.IndexOf(face.Vertex2));
				ushort vertex3Index = (ushort)(surf.Vertices.IndexOf(face.Vertex3));

				bw.WriteUInt16(vertex1Index);
				bw.WriteUInt16(vertex2Index);
				bw.WriteUInt16(vertex3Index);
			}
			bw.WriteUInt32((uint)model.Materials.Count);
			for (int i = 0; i < model.Materials.Count; i++)
			{
				ModelMaterial material = model.Materials[i];
				bw.WriteColorRGBASingle(material.DiffuseColor);
				bw.WriteSingle((float)material.Shininess);

				bw.WriteColorRGBSingle(material.SpecularColor);
				bw.WriteColorRGBSingle(material.AmbientColor);

				bw.WriteSByte(material.ToonNumber);
				bw.WriteBoolean(material.EdgeFlag);

				uint indexCount = (uint)(material.Triangles.Count * 3);
				bw.WriteUInt32(indexCount);

				System.Text.StringBuilder fileName = new System.Text.StringBuilder();
				if (!String.IsNullOrEmpty(material.Textures[0].TextureFileName))
				{
					fileName.Append(material.Textures[0].TextureFileName);
				}
				if (!String.IsNullOrEmpty(material.Textures[0].MapFileName))
				{
					if (!String.IsNullOrEmpty(material.Textures[0].TextureFileName))
					{
						fileName.Append('*');
					}
					fileName.Append(material.Textures[0].MapFileName);
				}
				bw.WriteFixedLengthString(fileName.ToString(), 20);
			}

			#region Bones
			{
				bw.WriteUInt16((ushort)model.Bones.Count);
				for (int i = 0; i < model.Bones.Count; i++)
				{
					ModelBone bone = model.Bones[i];
					bw.WriteFixedLengthString(bone.Name, 20);
					if (bone.ParentBone != null && model.Bones.Contains(bone.ParentBone))
					{
						bw.WriteInt16((short)model.Bones.IndexOf(bone.ParentBone));
					}
					else
					{
						bw.WriteInt16(-1);
					}
					if (bone.ChildBone != null && model.Bones.Contains(bone.ChildBone))
					{
						bw.WriteInt16((short)model.Bones.IndexOf(bone.ChildBone));
					}
					else
					{
						bw.WriteInt16(-1);
					}
					bw.WriteByte((byte)bone.BoneType);
					bw.WriteInt16(bone.IKNumber);

					bw.WritePositionVector3x32(bone.Position);
				}
			}
			#endregion

			bw.WriteUInt16((ushort)model.IK.Count);
			for (int i = 0; i < model.IK.Count; i++)
			{
				ModelIK ik = model.IK[i];
				bw.WriteUInt16(ik.Index);
				if (model.Bones.Contains(ik.TargetBone))
				{
					bw.WriteUInt16((ushort)(model.Bones.IndexOf(ik.TargetBone)));
				}
				else
				{
					bw.WriteUInt16(65535);
				}
				bw.WriteByte((byte)ik.BoneList.Count);
				bw.WriteUInt16((ushort)ik.LoopCount);
				bw.WriteSingle((float)ik.LimitOnce);
				for (int j = 0; j < ik.BoneList.Count; j++)
				{
					bw.WriteUInt16((ushort)model.Bones.IndexOf(ik.BoneList[j]));
				}
			}
			bw.WriteUInt16((ushort)model.Skins.Count);
			for (int i = 0; i < model.Skins.Count; i++)
			{
				ModelSkin skin = model.Skins[i];
				bw.WriteFixedLengthString(skin.Name, 20);
				bw.WriteUInt32((uint)skin.Vertices.Count);
				bw.WriteByte(skin.Category);
				for (int j = 0; j < skin.Vertices.Count; j++)
				{
					uint targetVertex = (uint)(surf.Vertices.IndexOf(skin.Vertices[j].TargetVertex));
					bw.WriteUInt32(targetVertex);

					bw.WritePositionVector3x32(skin.Vertices[j].MaximumPosition);
				}
			}
			bw.WriteByte((byte)model.Expressions.Count);
			for (int i = 0; i < model.Expressions.Count; i++)
			{
				bw.WriteUInt16(model.Expressions[i]);
			}
			bw.WriteByte((byte)model.BoneGroups.Count);
			foreach (ModelBoneGroup group in model.BoneGroups)
			{
				bw.WriteFixedLengthString(group.Name, 50);
			}

			uint totalBoneCount = 0;
			foreach (ModelBoneGroup group in model.BoneGroups)
			{
				foreach (ModelBone bone in group.Bones)
				{
					totalBoneCount++;
				}
			}
			bw.WriteUInt32(totalBoneCount);
			foreach (ModelBoneGroup group in model.BoneGroups)
			{
				foreach (ModelBone bone in group.Bones)
				{
					ushort boneIndex = (ushort)model.Bones.IndexOf(bone);
					byte groupIndex = (byte)model.BoneGroups.IndexOf(group);
					bw.WriteUInt16(boneIndex);
					bw.WriteByte(groupIndex);
				}
			}

			if (model.StringTable.ContainsKey(1033))
			{
				bw.WriteBoolean(true);

				ModelStringTableExtension englishInformation = model.StringTable[1033];
				/*
                bw.WriteFixedLengthString(englishInformation.Title, 20);
                bw.WriteFixedLengthString(englishInformation.Comments, 256);
                */

				bw.WriteFixedLengthString(mvarModelTitleEnglish, 20);
				bw.WriteFixedLengthString(mvarModelCommentEnglish, 256);

				for (int i = 0; i < model.Bones.Count; i++)
				{
					string boneName = String.Empty;
					if (i < englishInformation.BoneNames.Count)
					{
						boneName = englishInformation.BoneNames[i];
					}
					else
					{
						boneName = model.Bones[i].Name;
					}
					bw.WriteFixedLengthString(boneName, 20);
				}
				for (int i = 0; i < model.Skins.Count; i++)
				{
					string skinName = String.Empty;
					if (i < englishInformation.SkinNames.Count)
					{
						skinName = englishInformation.SkinNames[i];
					}
					else
					{
						skinName = model.Skins[i].Name;
					}
					bw.WriteFixedLengthString(skinName, 20);
				}
				bw.Accessor.Seek(30L, SeekOrigin.Current);
				for (int i = 0; i < model.BoneGroups.Count; i++)
				{
					string nodeName = String.Empty;
					if (i < englishInformation.NodeNames.Count)
					{
						nodeName = englishInformation.NodeNames[i];
					}
					else
					{
						nodeName = model.BoneGroups[i].Name;
					}
					bw.WriteFixedLengthString(nodeName, 50);
				}
			}
			else
			{
				bw.WriteBoolean(false);
			}
			List<string> toonNames = new List<string>();
			for (int i = 0; i < 10; i++)
			{
				bw.WriteFixedLengthString("toon" + (i + 1).ToString().PadLeft(2, '0') + ".bmp", 100);
			}
		}
	}
}
