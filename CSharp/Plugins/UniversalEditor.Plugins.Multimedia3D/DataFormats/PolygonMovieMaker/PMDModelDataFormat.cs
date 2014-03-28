using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker
{
	public class PMDModelDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Polygon Movie Maker model", new byte?[][] { new byte?[] { new byte?(80), new byte?(109), new byte?(100) } }, new string[] { "*.pmd", "*.pma" });
            dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
			return dfr;
        }

		private float mvarVersion = 0f;
		public float Version { get { return mvarVersion; } set { mvarVersion = value; } }

		protected override void LoadInternal(ref ObjectModel objectModel)
		{
			ModelObjectModel model = objectModel as ModelObjectModel;
			if (model != null)
			{
                IO.BinaryReader br = base.Stream.BinaryReader;
				string PMDKey = br.ReadFixedLengthString(3);
				if (!(PMDKey != "Pmd"))
				{
					mvarVersion = br.ReadSingle();
					ModelStringTableExtension englishInformation = new ModelStringTableExtension();
					model.StringTable.Add(1033, englishInformation);
					ModelStringTableExtension japaneseInformation = new ModelStringTableExtension();
					model.StringTable.Add(1041, japaneseInformation);
					Encoding encoding = Encoding.GetEncoding("shift_jis");
					model.ModelName = br.ReadFixedLengthString(20, encoding);
                    model.ModelName = model.ModelName.Substring(0, model.ModelName.IndexOf('\0'));
					japaneseInformation.ModelName = model.ModelName;
                    model.ModelComment = br.ReadFixedLengthString(256, encoding);
                    model.ModelComment = model.ModelComment.Substring(0, model.ModelComment.IndexOf('\0'));
					japaneseInformation.ModelComment = model.ModelComment;
					uint nVertices = br.ReadUInt32();
					model.Vertices.Clear();

                    ushort[] vertexBone0Indices = new ushort[nVertices];
                    ushort[] vertexBone1Indices = new ushort[nVertices];

					for (uint i = 0u; i < nVertices; i += 1u)
					{
						ModelVertex vtx = new ModelVertex();
						float posX = br.ReadSingle();
						float posY = br.ReadSingle();
						float posZ = br.ReadSingle();
						vtx.Position = new PositionVector3(posX, posY, posZ);
                        vtx.OriginalPosition = new PositionVector3(posX, posY, posZ);
						
                        float normalX = br.ReadSingle();
						float normalY = br.ReadSingle();
						float normalZ = br.ReadSingle();
						vtx.Normal = new PositionVector3(normalX, normalY, normalZ);
						
                        float textureU = br.ReadSingle();
						float textureV = br.ReadSingle();
						vtx.Texture = new TextureVector2(textureU, textureV);
						
                        ushort bone0 = br.ReadUInt16();
						ushort bone1 = br.ReadUInt16();
                        vertexBone0Indices[i] = bone0;
                        vertexBone1Indices[i] = bone1;

						byte weight = br.ReadByte();
						vtx.Weight = (float)weight;
						byte flag = br.ReadByte();
						vtx.Flags = flag;
						model.Vertices.Add(vtx);
					}
					uint nFaces = br.ReadUInt32();
					for (uint i = 0u; i < nFaces; i += 1u)
					{
						ModelFace face = new ModelFace();
						ushort item = br.ReadUInt16();
						face.Item = (long)((ulong)item);
						model.Faces.Add(face);
					}
					uint nMaterials = br.ReadUInt32();
					model.Materials.Clear();
					for (uint i = 0u; i < nMaterials; i += 1u)
					{
						ModelMaterial material = new ModelMaterial();
						material.Name = "MA" + i.ToString();
						float diffuseR = br.ReadSingle();
						float diffuseG = br.ReadSingle();
						float diffuseB = br.ReadSingle();
						float diffuseA = br.ReadSingle();
						material.DiffuseColor = Color.FromArgb((int)(diffuseA * 255f), (int)(diffuseR * 255f), (int)(diffuseG * 255f), (int)(diffuseB * 255f));
						float shininess = br.ReadSingle();
						material.Shininess = shininess;
						float specularR = br.ReadSingle();
						float specularG = br.ReadSingle();
						float specularB = br.ReadSingle();
						material.SpecularColor = Color.FromArgb((int)Math.Min(255f, specularR * 255f), (int)Math.Min(255f, specularG * 255f), (int)Math.Min(255f, specularB * 255f));
						float ambientR = br.ReadSingle();
						float ambientG = br.ReadSingle();
						float ambientB = br.ReadSingle();
						material.AmbientColor = Color.FromArgb((int)Math.Min(255f, ambientR * 255f), (int)Math.Min(255f, ambientG * 255f), (int)Math.Min(255f, ambientB * 255f));
						sbyte toonNo = br.ReadSByte();
						material.ToonNumber = toonNo;
						bool edge = br.ReadBoolean();
						material.EdgeFlag = edge;
						uint faceVertexCount = br.ReadUInt32();
						material.IndexCount = faceVertexCount;
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
                        material.Textures.Add(texturePath, textureFlags);

						model.Materials.Add(material);
					}
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
						bone2.Kind = kind;
						short ikNum = br.ReadInt16();
						bone2.IKNumber = ikNum;
						float positionX = br.ReadSingle();
						float positionY = br.ReadSingle();
						float positionZ = br.ReadSingle();
                        bone2.Position = new PositionVector3(positionX, positionY, positionZ);
                        bone2.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);
						model.Bones.Add(bone2);
					}
					foreach (KeyValuePair<ModelBone, short> bonePair in parentBonesList)
					{
						bonePair.Key.ParentBone = model.Bones[(int)bonePair.Value];
					}
					foreach (KeyValuePair<ModelBone, short> bonePair in childBonesList)
					{
						bonePair.Key.ChildBone = model.Bones[(int)bonePair.Value];
					}

                    for (int i = 0; i < nVertices; i++)
                    {
                        model.Vertices[i].Bone0 = model.Bones[vertexBone0Indices[i]];
                        model.Vertices[i].Bone1 = model.Bones[vertexBone1Indices[i]];
                    }

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
							float skinVertexPointX = br.ReadSingle();
							float skinVertexPointY = br.ReadSingle();
							float skinVertexPointZ = br.ReadSingle();
							ModelSkinVertex svtx = new ModelSkinVertex();
							svtx.Index = nSkinVertexIndex;
							svtx.X = skinVertexPointX;
							svtx.Y = skinVertexPointY;
							svtx.Z = skinVertexPointZ;
							skin.Vertices.Add(svtx);
						}
						model.Skins.Add(skin);
					}
					if (!br.EndOfStream)
					{
						byte nExpressionList = br.ReadByte();
						for (byte m = 0; m < nExpressionList; m += 1)
						{
							ushort n = br.ReadUInt16();
							model.Expressions.Add(n);
						}
						byte nNodeNames = br.ReadByte();
						List<string> nodeNames = new List<string>();
						for (byte m = 0; m < nNodeNames; m += 1)
						{
							string nodeName = br.ReadNullTerminatedString(50);
							nodeNames.Add(nodeName);
						}
						ushort nBoneToNodes = br.ReadUInt16();
						for (ushort j = 0; j < nBoneToNodes; j += 1)
						{
							ushort nBoneID = br.ReadUInt16();
							byte nNodeID = br.ReadByte();
						}
						if (!br.EndOfStream)
						{
							bool h0 = br.ReadBoolean();
							bool h = br.ReadBoolean();
							bool hasEnglishInformation = br.ReadBoolean();
							if (hasEnglishInformation)
							{
								englishInformation.ModelName = br.ReadNullTerminatedString(20);
								englishInformation.ModelComment = br.ReadNullTerminatedString(256);
								for (ushort j = 0; j < nBones; j += 1)
								{
									string boneName = br.ReadNullTerminatedString(20);
									englishInformation.BoneNames.Add(boneName);
								}
								for (ushort j = 0; j < nSkins; j += 1)
								{
									string skinName = br.ReadNullTerminatedString(20);
									englishInformation.SkinNames.Add(skinName);
								}
								br.BaseStream.Seek(30L, SeekOrigin.Current);
								for (byte m = 0; m < nNodeNames - 1; m += 1)
								{
									string nodeName = br.ReadNullTerminatedString(50);
									englishInformation.NodeNames.Add(nodeName);
								}
							}
							for (int i2 = 0; i2 < 10; i2++)
							{
								string toonName = br.ReadFixedLengthString(100, encoding);
								model.ToonNames.Add(toonName);
							}
							if (!br.EndOfStream)
							{
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
									float boxSizeX = br.ReadSingle();
									float boxSizeY = br.ReadSingle();
									float boxSizeZ = br.ReadSingle();
                                    body.BoxSize = new PositionVector3(boxSizeX, boxSizeY, boxSizeZ);
									float positionX = br.ReadSingle();
									float positionY = br.ReadSingle();
									float positionZ = br.ReadSingle();
                                    body.Position = new PositionVector3(positionX, positionY, positionZ);
									float rotationX = br.ReadSingle();
									float rotationY = br.ReadSingle();
									float rotationZ = br.ReadSingle();
                                    body.Rotation = new PositionVector3(rotationX, rotationY, rotationZ);

									body.Mass = br.ReadSingle();
									body.PositionDamping = br.ReadSingle();
									body.RotationDamping = br.ReadSingle();
									body.Restitution = br.ReadSingle();
									body.Friction = br.ReadSingle();
									body.Mode = br.ReadByte();
                                    model.RigidBodies.Add(body);
								}
								uint nJoints = br.ReadUInt32();
								for (uint i = 0u; i < nJoints; i += 1u)
								{
                                    ModelJoint joint = new ModelJoint();
                                    joint.Name = br.ReadFixedLengthString(20, encoding);
									int bodyA = br.ReadInt32();
									int bodyB = br.ReadInt32();

									float positionX = br.ReadSingle();
									float positionY = br.ReadSingle();
									float positionZ = br.ReadSingle();
                                    joint.Position = new PositionVector3(positionX, positionY, positionZ);

									float rotationX = br.ReadSingle();
									float rotationY = br.ReadSingle();
									float rotationZ = br.ReadSingle();
                                    joint.Rotation = new PositionVector3(rotationX, rotationY, rotationZ);

									float limitMoveLowX = br.ReadSingle();
									float limitMoveLowY = br.ReadSingle();
									float limitMoveLowZ = br.ReadSingle();
                                    joint.LimitMoveLow = new PositionVector3(limitMoveLowX, limitMoveLowY, limitMoveLowZ);

									float limitMoveHighX = br.ReadSingle();
									float limitMoveHighY = br.ReadSingle();
									float limitMoveHighZ = br.ReadSingle();
                                    joint.LimitMoveHigh = new PositionVector3(limitMoveHighX, limitMoveHighY, limitMoveHighZ);

									float limitAngleLowX = br.ReadSingle();
									float limitAngleLowY = br.ReadSingle();
									float limitAngleLowZ = br.ReadSingle();
                                    joint.LimitAngleLow = new PositionVector3(limitAngleLowX, limitAngleLowY, limitAngleLowZ);

									float limitAngleHighX = br.ReadSingle();
									float limitAngleHighY = br.ReadSingle();
									float limitAngleHighZ = br.ReadSingle();
                                    joint.LimitAngleHigh = new PositionVector3(limitAngleHighX, limitAngleHighY, limitAngleHighZ);

									float spConstMoveX = br.ReadSingle();
									float spConstMoveY = br.ReadSingle();
									float spConstMoveZ = br.ReadSingle();
                                    joint.SPConstMove = new PositionVector3(spConstMoveX, spConstMoveY, spConstMoveZ);

									float spConstRotateX = br.ReadSingle();
									float spConstRotateY = br.ReadSingle();
									float spConstRotateZ = br.ReadSingle();
                                    joint.SPConstRotate = new PositionVector3(spConstRotateX, spConstRotateY, spConstRotateZ);
                                    model.Joints.Add(joint);
								}
								if (!br.EndOfStream)
								{
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
                                                    ModelPMAExtension.TextureFlippingFrame tff = new ModelPMAExtension.TextureFlippingFrame();
                                                    long timestamp = image.Timestamp;
                                                    material.Textures.Add(image.FileName, image.TextureFlags);
                                                }
                                            }
                                        }
                                        else if (chunk.Name == "EFXS")
                                        {
                                            // Effects preset information
                                            UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk EFXS = (chunk as UniversalEditor.ObjectModels.PMAXPatch.Chunks.PMAXEffectsScriptChunk);
                                            model.PMAExtension.EffectPresets.Enabled = true;
                                            foreach (string effectFileName in EFXS.EffectScriptFileNames)
                                            {
                                                model.PMAExtension.EffectPresets.FileNames.Add(effectFileName);
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
							}
						}
					}
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
			ModelObjectModel model = objectModel as ModelObjectModel;
			if (model != null)
			{
				IO.BinaryWriter bw = base.Stream.BinaryWriter;
				bw.WriteFixedLengthString("Pmd");
				bw.Write(this.mvarVersion);
				Encoding encoding = Encoding.GetEncoding("shift-jis");
				bw.WriteFixedLengthString(model.ModelName, encoding, 20);
				bw.WriteFixedLengthString(model.ModelComment, encoding, 256);
				bw.Write((uint)model.Vertices.Count);
				for (int i = 0; i < model.Vertices.Count; i++)
				{
					ModelVertex vtx = model.Vertices[i];
					System.IO.BinaryWriter arg_A6_0 = bw;
					PositionVector3 positionVector = vtx.Position;
					arg_A6_0.Write(positionVector.X);
					System.IO.BinaryWriter arg_BD_0 = bw;
					positionVector = vtx.Position;
					arg_BD_0.Write(positionVector.Y);
					System.IO.BinaryWriter arg_D4_0 = bw;
					positionVector = vtx.Position;
					arg_D4_0.Write(positionVector.Z);
					System.IO.BinaryWriter arg_EB_0 = bw;
					positionVector = vtx.Normal;
					arg_EB_0.Write(positionVector.X);
					System.IO.BinaryWriter arg_102_0 = bw;
					positionVector = vtx.Normal;
					arg_102_0.Write(positionVector.Y);
					System.IO.BinaryWriter arg_119_0 = bw;
					positionVector = vtx.Normal;
					arg_119_0.Write(positionVector.Z);
					System.IO.BinaryWriter arg_130_0 = bw;
					TextureVector2 texture = vtx.Texture;
					arg_130_0.Write(texture.U);
					System.IO.BinaryWriter arg_147_0 = bw;
					texture = vtx.Texture;
					arg_147_0.Write(texture.V);

                    ushort indexToBone0 = (ushort)model.Bones.IndexOf(vtx.Bone0);
                    ushort indexToBone1 = (ushort)model.Bones.IndexOf(vtx.Bone1);

					bw.Write(indexToBone0);
					bw.Write(indexToBone1);

					bw.Write((byte)vtx.Weight);
					bw.Write(vtx.Flags);
				}
				bw.Write((uint)model.Faces.Count);
				for (int i = 0; i < model.Faces.Count; i++)
				{
					ModelFace face = model.Faces[i];
					bw.Write((ushort)face.Item);
				}
				bw.Write((uint)model.Materials.Count);
				for (int i = 0; i < model.Materials.Count; i++)
				{
					ModelMaterial material = model.Materials[i];
					System.IO.BinaryWriter arg_230_0 = bw;
					Color color = material.DiffuseColor;
					arg_230_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_24F_0 = bw;
					color = material.DiffuseColor;
					arg_24F_0.Write((float)color.G / 255f);
					System.IO.BinaryWriter arg_26E_0 = bw;
					color = material.DiffuseColor;
					arg_26E_0.Write((float)color.B / 255f);
					System.IO.BinaryWriter arg_28D_0 = bw;
					color = material.DiffuseColor;
					arg_28D_0.Write((float)color.A / 255f);
					bw.Write(material.Shininess);
					System.IO.BinaryWriter arg_2BA_0 = bw;
					color = material.SpecularColor;
					arg_2BA_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_2D9_0 = bw;
					color = material.SpecularColor;
					arg_2D9_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_2F8_0 = bw;
					color = material.SpecularColor;
					arg_2F8_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_317_0 = bw;
					color = material.AmbientColor;
					arg_317_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_336_0 = bw;
					color = material.AmbientColor;
					arg_336_0.Write((float)color.R / 255f);
					System.IO.BinaryWriter arg_355_0 = bw;
					color = material.AmbientColor;
					arg_355_0.Write((float)color.R / 255f);
					bw.Write(material.ToonNumber);
					bw.Write(material.EdgeFlag);
					bw.Write(material.IndexCount);
					bw.WriteNullTerminatedString(material.Textures[0].FileName, 20);
				}
				bw.Write((ushort)model.Bones.Count);
				for (int i = 0; i < model.Bones.Count; i++)
				{
					ModelBone bone = model.Bones[i];
					bw.WriteFixedLengthString(bone.Name, encoding, 20);
					if (bone.ParentBone != null && model.Bones.Contains(bone.ParentBone))
					{
						bw.Write((short)model.Bones.IndexOf(bone.ParentBone));
					}
					else
					{
						bw.Write(-1);
					}
					if (bone.ChildBone != null && model.Bones.Contains(bone.ChildBone))
					{
						bw.Write((short)model.Bones.IndexOf(bone.ChildBone));
					}
					else
					{
						bw.Write(-1);
					}
					bw.Write(bone.Kind);
					bw.Write(bone.IKNumber);

					bw.Write(bone.Position.X);
                    bw.Write(bone.Position.Y);
					bw.Write(bone.Position.Z);
				}
				bw.Write((ushort)model.IK.Count);
				for (int i = 0; i < model.IK.Count; i++)
				{
					ModelIK ik = model.IK[i];
					bw.Write(ik.Index);
					if (model.Bones.Contains(ik.TargetBone))
					{
						bw.Write(model.Bones.IndexOf(ik.TargetBone));
					}
					else
					{
						bw.Write(65535);
					}
					bw.Write((byte)ik.BoneList.Count);
					bw.Write(ik.LoopCount);
					bw.Write(ik.LimitOnce);
					for (int j = 0; j < ik.BoneList.Count; j++)
					{
						bw.Write((ushort)model.Bones.IndexOf(ik.BoneList[j]));
					}
				}
				bw.Write((ushort)model.Skins.Count);
				for (int i = 0; i < model.Skins.Count; i++)
				{
					ModelSkin skin = model.Skins[i];
					bw.WriteFixedLengthString(skin.Name, encoding, 20);
					bw.Write((uint)skin.Vertices.Count);
					bw.Write(skin.Category);
					for (int j = 0; j < skin.Vertices.Count; j++)
					{
						bw.Write(skin.Vertices[j].Index);
						bw.Write(skin.Vertices[j].X);
						bw.Write(skin.Vertices[j].Y);
						bw.Write(skin.Vertices[j].Z);
					}
				}
				bw.Write((byte)model.Expressions.Count);
				for (int i = 0; i < model.Expressions.Count; i++)
				{
					bw.Write(model.Expressions[i]);
				}
				bw.Write((byte)model.NodeNames.Count);
				for (int i = 0; i < model.NodeNames.Count; i++)
				{
					bw.WriteNullTerminatedString(model.NodeNames[i], 50);
				}
				bw.Write((ushort)model.BoneNodeMappings.Count);
				foreach (KeyValuePair<ushort, byte> kvp in model.BoneNodeMappings)
				{
					bw.Write(kvp.Key);
					bw.Write(kvp.Value);
				}
				bw.Write(true);
				bw.Write(true);
				if (model.StringTable.ContainsKey(1033))
				{
					bw.Write(true);
					ModelStringTableExtension englishInformation = model.StringTable[1033];
					bw.WriteNullTerminatedString(englishInformation.ModelName, 20);
					bw.WriteNullTerminatedString(englishInformation.ModelComment, 256);
					for (int i = 0; i < model.Bones.Count; i++)
					{
						bw.WriteNullTerminatedString(englishInformation.BoneNames[i], 20);
					}
					for (int i = 0; i < model.Skins.Count; i++)
					{
						bw.WriteNullTerminatedString(englishInformation.SkinNames[i], 20);
					}
					for (int i = 0; i < model.NodeNames.Count; i++)
					{
						bw.WriteNullTerminatedString(englishInformation.NodeNames[i], 50);
					}
				}
				else
				{
					bw.Write(false);
				}
				List<string> toonNames = new List<string>();
				for (int i = 0; i < 10; i++)
				{
					bw.WriteFixedLengthString("toon" + i.ToString().PadLeft(2, '0'), 100);
				}
			}
		}
	}
}
