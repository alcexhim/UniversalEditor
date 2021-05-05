using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model.Morphing;
namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker
{
	public class PMXModelDataFormat : DataFormat
	{
        public override DataFormatReference MakeReference()
        {
            DataFormatReference dfr = base.MakeReference();
            dfr.Filters.Add("Polygon Movie Maker enhanced model", new byte?[][] { new byte?[] { new byte?(80), new byte?(77), new byte?(88), new byte?(32) } }, new string[] { "*.pmx", "*.pma" });
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
				string PMDKey = br.ReadFixedLengthString(4);
				if (!(PMDKey != "PMX "))
				{
					float version = br.ReadSingle();
					if ((double)version == 2.0)
					{
						byte flagBytes = br.ReadByte();
						byte[] flags = br.ReadBytes((uint)flagBytes);
						byte textEncoding = 0;
						if (flags.Length >= 1)
						{
							textEncoding = flags[0];
							if (flags.Length >= 2)
							{
								byte extendedUV = flags[1];
								if (flags.Length >= 3)
								{
									model.IndexSizes.Vertex = flags[2];
									if (flags.Length >= 4)
									{
										model.IndexSizes.Texture = flags[3];
										if (flags.Length >= 5)
										{
											model.IndexSizes.Material = flags[4];
											if (flags.Length >= 6)
											{
												model.IndexSizes.Bone = flags[5];
												if (flags.Length >= 7)
												{
													model.IndexSizes.Morph = flags[6];
													if (flags.Length >= 8)
													{
														model.IndexSizes.RigidBody = flags[7];
													}
												}
											}
										}
									}
								}
							}
						}
						ModelStringTableExtension japaneseInformation = new ModelStringTableExtension();
						model.StringTable.Add(1041, japaneseInformation);
						ModelStringTableExtension englishInformation = new ModelStringTableExtension();
						model.StringTable.Add(1033, englishInformation);
						Encoding encoding = Encoding.Default;
						switch (textEncoding)
						{
							case 0:
							{
								encoding = Encoding.Unicode;
								break;
							}
							case 1:
							{
								encoding = Encoding.UTF8;
								break;
							}
						}
						model.ModelName = br.ReadInt32String(encoding);
						japaneseInformation.ModelName = model.ModelName;
						englishInformation.ModelName = br.ReadInt32String(encoding);
						model.ModelComment = br.ReadInt32String(encoding);
						japaneseInformation.ModelComment = model.ModelComment;
						englishInformation.ModelComment = br.ReadInt32String(encoding);

                        #region Vertices
                        int vertexCount = br.ReadInt32();

                        int[] vertexBone0Indices = new int[vertexCount];
                        int[] vertexBone1Indices = new int[vertexCount];

						for (int i = 0; i < vertexCount; i++)
						{
							ModelVertex vertex = new ModelVertex();
							float vertexPosX = br.ReadSingle();
							float vertexPosY = br.ReadSingle();
							float vertexPosZ = br.ReadSingle();
							vertex.Position = new PositionVector3(vertexPosX, vertexPosY, vertexPosZ);
                            vertex.OriginalPosition = new PositionVector3(vertexPosX, vertexPosY, vertexPosZ);

							float vertexNormalX = br.ReadSingle();
							float vertexNormalY = br.ReadSingle();
							float vertexNormalZ = br.ReadSingle();
							vertex.Normal = new PositionVector3(vertexNormalX, vertexNormalY, vertexNormalZ);
							float vertexTextureU = br.ReadSingle();
							float vertexTextureV = br.ReadSingle();
							vertex.Texture = new TextureVector2(vertexTextureU, vertexTextureV);
							byte vertexDeformType = br.ReadByte();


							if (vertexDeformType == 0)
							{
								switch (model.IndexSizes.Bone)
								{
									case 1:
									{
										byte vertexBoneIndex = br.ReadByte();
										vertexBone0Indices[i] = vertexBoneIndex;
										break;
									}
									case 2:
									{
										short vertexBoneIndex2 = br.ReadInt16();
                                        vertexBone0Indices[i] = vertexBoneIndex2;
										break;
									}
									case 4:
									{
										int vertexBoneIndex3 = br.ReadInt32();
                                        vertexBone0Indices[i] = vertexBoneIndex3;
										break;
									}
								}
							}
							else
							{
								if (vertexDeformType == 1)
								{
									switch (model.IndexSizes.Bone)
									{
										case 1:
										{
											byte vertexBoneIndex = br.ReadByte();
											vertexBone0Indices[i] = vertexBoneIndex;
											break;
										}
										case 2:
										{
											short vertexBoneIndex2 = br.ReadInt16();
											vertexBone0Indices[i] = vertexBoneIndex2;
											break;
										}
										case 4:
										{
											int vertexBoneIndex3 = br.ReadInt32();
											vertexBone0Indices[i] = vertexBoneIndex3;
											break;
										}
									}
									switch (model.IndexSizes.Bone)
									{
										case 1:
										{
											byte vertexBoneIndex = br.ReadByte();
											vertexBone1Indices[i] = vertexBoneIndex;
											break;
										}
										case 2:
										{
											short vertexBoneIndex2 = br.ReadInt16();
                                            vertexBone1Indices[i] = vertexBoneIndex2;
											break;
										}
										case 4:
										{
											int vertexBoneIndex3 = br.ReadInt32();
                                            vertexBone1Indices[i] = vertexBoneIndex3;
											break;
										}
									}
									float weight0 = br.ReadSingle();
									vertex.Weight = weight0;
								}
								else
								{
									if (vertexDeformType == 2)
									{
										switch (model.IndexSizes.Bone)
										{
											case 1:
											{
												byte vertexBoneIndex = br.ReadByte();
                                                vertexBone0Indices[i] = (ushort)vertexBoneIndex;
												break;
											}
											case 2:
											{
												short vertexBoneIndex2 = br.ReadInt16();
                                                vertexBone0Indices[i] = (ushort)vertexBoneIndex2;
												break;
											}
											case 4:
											{
												int vertexBoneIndex3 = br.ReadInt32();
												vertexBone0Indices[i] = (ushort)vertexBoneIndex3;
												break;
											}
										}
										switch (model.IndexSizes.Bone)
										{
											case 1:
											{
												byte vertexBoneIndex = br.ReadByte();
                                                vertexBone1Indices[i] = (ushort)vertexBoneIndex;
												break;
											}
											case 2:
											{
												short vertexBoneIndex2 = br.ReadInt16();
                                                vertexBone1Indices[i] = (ushort)vertexBoneIndex2;
												break;
											}
											case 4:
											{
												int vertexBoneIndex3 = br.ReadInt32();
                                                vertexBone1Indices[i] = (ushort)vertexBoneIndex3;
												break;
											}
										}
										switch (model.IndexSizes.Bone)
										{
											case 1:
											{
												byte vertexBoneIndex = br.ReadByte();
                                                vertexBone0Indices[i] = (ushort)vertexBoneIndex;
												break;
											}
											case 2:
											{
												short vertexBoneIndex2 = br.ReadInt16();
                                                vertexBone0Indices[i] = (ushort)vertexBoneIndex2;
												break;
											}
											case 4:
											{
												int vertexBoneIndex3 = br.ReadInt32();
                                                vertexBone0Indices[i] = (ushort)vertexBoneIndex3;
												break;
											}
										}
										switch (model.IndexSizes.Bone)
										{
											case 1:
											{
												byte vertexBoneIndex = br.ReadByte();
                                                vertexBone1Indices[i] = (ushort)vertexBoneIndex;
												break;
											}
											case 2:
											{
												short vertexBoneIndex2 = br.ReadInt16();
												vertexBone1Indices[i] = (ushort)vertexBoneIndex2;
												break;
											}
											case 4:
											{
												int vertexBoneIndex3 = br.ReadInt32();
                                                vertexBone1Indices[i] = (ushort)vertexBoneIndex3;
												break;
											}
										}
										float weight0 = br.ReadSingle();
										vertex.Weight = weight0;
										float weight = br.ReadSingle();
										vertex.Weight = weight;
										float weight2 = br.ReadSingle();
										vertex.Weight = weight2;
										float weight3 = br.ReadSingle();
										vertex.Weight = weight3;
									}
									else
									{
										if (vertexDeformType == 3)
										{
											switch (model.IndexSizes.Bone)
											{
												case 1:
												{
													byte vertexBoneIndex = br.ReadByte();
                                                    vertexBone0Indices[i] = (ushort)vertexBoneIndex;
													break;
												}
												case 2:
												{
													short vertexBoneIndex2 = br.ReadInt16();
                                                    vertexBone0Indices[i] = (ushort)vertexBoneIndex2;
													break;
												}
												case 4:
												{
													int vertexBoneIndex3 = br.ReadInt32();
                                                    vertexBone0Indices[i] = (ushort)vertexBoneIndex3;
													break;
												}
											}
											switch (model.IndexSizes.Bone)
											{
												case 1:
												{
													byte vertexBoneIndex = br.ReadByte();
                                                    vertexBone1Indices[i] = (ushort)vertexBoneIndex;
													break;
												}
												case 2:
												{
													short vertexBoneIndex2 = br.ReadInt16();
                                                    vertexBone1Indices[i] = (ushort)vertexBoneIndex2;
													break;
												}
												case 4:
												{
													int vertexBoneIndex3 = br.ReadInt32();
                                                    vertexBone1Indices[i] = (ushort)vertexBoneIndex3;
													break;
												}
											}
											float boneWeights = br.ReadSingle();
											float sdefValueCX = br.ReadSingle();
											float sdefValueCY = br.ReadSingle();
											float sdefValueCZ = br.ReadSingle();
											float sdefValueR0X = br.ReadSingle();
											float sdefValueR0Y = br.ReadSingle();
											float sdefValueR0Z = br.ReadSingle();
											float sdefValueR1X = br.ReadSingle();
											float sdefValueR1Y = br.ReadSingle();
											float sdefValueR1Z = br.ReadSingle();
										}
									}
								}
							}
							float edgeFactor = br.ReadSingle();
							model.Vertices.Add(vertex);
                        }
                        #endregion
                        #region Indices
                        int indexCount = br.ReadInt32();
						for (int i = 0; i < indexCount; i++)
						{
							ModelFace face = new ModelFace();
							byte b = model.IndexSizes.Vertex;
							switch (b)
							{
								case 1:
								{
									face.Item = (long)((ulong)br.ReadByte());
									break;
								}
								case 2:
								{
									face.Item = (long)br.ReadInt16();
									break;
								}
								case 3:
								{
									break;
								}
								case 4:
								{
									face.Item = (long)br.ReadInt32();
									break;
								}
								default:
								{
									if (b == 8)
									{
										face.Item = br.ReadInt64();
									}
									break;
								}
							}
							model.Faces.Add(face);
                        }
                        #endregion
                        #region Textures
                        int textureCount = br.ReadInt32();
						List<string> TextureNames = new List<string>();
						for (int i = 0; i < textureCount; i++)
						{
							TextureNames.Add(br.ReadInt32String(encoding));
                        }
                        #endregion
                        #region Materials
                        int materialCount = br.ReadInt32();
						for (int i = 0; i < materialCount; i++)
						{
							ModelMaterial material = new ModelMaterial();
							material.Name = br.ReadInt32String(encoding);
							string EnglishName = br.ReadInt32String(encoding);
							float diffuseR = br.ReadSingle();
							float diffuseG = br.ReadSingle();
							float diffuseB = br.ReadSingle();
							float diffuseA = br.ReadSingle();
							material.DiffuseColor = Color.FromArgb((int)(diffuseA * 255f), (int)(diffuseR * 255f), (int)(diffuseG * 255f), (int)(diffuseB * 255f));
							float specularR = br.ReadSingle();
							float specularG = br.ReadSingle();
							float specularB = br.ReadSingle();
							float specularCoefficient = br.ReadSingle();
							material.SpecularColor = Color.FromArgb((int)(specularR * 255f), (int)(specularG * 255f), (int)(specularB * 255f));
							float ambientR = br.ReadSingle();
							float ambientG = br.ReadSingle();
							float ambientB = br.ReadSingle();
							material.AmbientColor = Color.FromArgb((int)(ambientR * 255f), (int)(ambientG * 255f), (int)(ambientB * 255f));
							byte flag = br.ReadByte();
							float edgeR = br.ReadSingle();
							float edgeG = br.ReadSingle();
							float edgeB = br.ReadSingle();
							float edgeA = br.ReadSingle();
							material.EdgeColor = Color.FromArgb((int)(edgeA * 255f), (int)(edgeR * 255f), (int)(edgeG * 255f), (int)(edgeB * 255f));
							float edgeSize = br.ReadSingle();
							material.EdgeSize = edgeSize;
							byte b = model.IndexSizes.Texture;
							switch (b)
							{
								case 1:
								{
									byte textureIndex = br.ReadByte();
									break;
								}
								case 2:
								{
									short textureIndex2 = br.ReadInt16();
									break;
								}
								case 3:
								{
									break;
								}
								case 4:
								{
									int textureIndex3 = br.ReadInt32();
									break;
								}
								default:
								{
									if (b == 8)
									{
										long textureIndex4 = br.ReadInt64();
									}
									break;
								}
							}
							b = model.IndexSizes.Texture;
							switch (b)
							{
								case 1:
								{
									byte sphereTextureIndex = br.ReadByte();
									break;
								}
								case 2:
								{
									short sphereTextureIndex2 = br.ReadInt16();
									break;
								}
								case 3:
								{
									break;
								}
								case 4:
								{
									int sphereTextureIndex3 = br.ReadInt32();
									break;
								}
								default:
								{
									if (b == 8)
									{
										long sphereTextureIndex4 = br.ReadInt64();
									}
									break;
								}
							}
							switch (br.ReadByte())
							{
							}
							byte toonSharingFlag = br.ReadByte();
							if (toonSharingFlag == 0)
							{
								b = model.IndexSizes.Texture;
								switch (b)
								{
									case 1:
									{
										byte toonTextureIndex = br.ReadByte();
										break;
									}
									case 2:
									{
										short toonTextureIndex2 = br.ReadInt16();
										break;
									}
									case 3:
									{
										break;
									}
									case 4:
									{
										int toonTextureIndex3 = br.ReadInt32();
										break;
									}
									default:
									{
										if (b == 8)
										{
											long toonTextureIndex4 = br.ReadInt64();
										}
										break;
									}
								}
							}
							else
							{
								if (toonSharingFlag == 1)
								{
									byte toonTextureIndex = br.ReadByte();
								}
							}
							material.Comment = br.ReadInt32String(encoding);
							int materialVertexCount = br.ReadInt32();
							material.Textures.Add(TextureNames[i], ModelTextureFlags.Texture);
							model.Materials.Add(material);
                        }
                        #endregion
                        #region Bones
                        int boneCount = br.ReadInt32();
						for (int i = 0; i < boneCount; i++)
						{
							ModelBone bone = new ModelBone();
							bone.Name = br.ReadInt32String(encoding);
							string EnglishName = br.ReadInt32String(encoding);
							float positionX = br.ReadSingle();
							float positionY = br.ReadSingle();
							float positionZ = br.ReadSingle();
							bone.Position = new PositionVector3(positionX, positionY, positionZ);
                            bone.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

							if (model.IndexSizes.Bone == 1)
							{
								byte parentBoneIndex = br.ReadByte();
							}
							else
							{
								if (model.IndexSizes.Bone == 2)
								{
									short parentBoneIndex2 = br.ReadInt16();
								}
								else
								{
									if (model.IndexSizes.Bone == 4)
									{
										int parentBoneIndex3 = br.ReadInt32();
									}
									else
									{
										if (model.IndexSizes.Bone == 8)
										{
											long parentBoneIndex4 = br.ReadInt64();
										}
									}
								}
							}
							int transformationHierarchy = br.ReadInt32();
							short boneFlag = br.ReadInt16();
							if ((boneFlag & 1) == 1)
							{
								if (model.IndexSizes.Bone == 1)
								{
									byte parentBoneIndex = br.ReadByte();
								}
								else
								{
									if (model.IndexSizes.Bone == 2)
									{
										short parentBoneIndex2 = br.ReadInt16();
									}
									else
									{
										if (model.IndexSizes.Bone == 4)
										{
											int parentBoneIndex3 = br.ReadInt32();
										}
										else
										{
											if (model.IndexSizes.Bone == 8)
											{
												long parentBoneIndex4 = br.ReadInt64();
											}
										}
									}
								}
							}
							else
							{
								float boneOffsetX = br.ReadSingle();
								float boneOffsetY = br.ReadSingle();
								float boneOffsetZ = br.ReadSingle();
							}
							if ((boneFlag & 256) == 256)
							{
								if (model.IndexSizes.Bone == 1)
								{
									byte parentBoneIndex = br.ReadByte();
								}
								else
								{
									if (model.IndexSizes.Bone == 2)
									{
										short parentBoneIndex2 = br.ReadInt16();
									}
									else
									{
										if (model.IndexSizes.Bone == 4)
										{
											int parentBoneIndex3 = br.ReadInt32();
										}
										else
										{
											if (model.IndexSizes.Bone == 8)
											{
												long parentBoneIndex4 = br.ReadInt64();
											}
										}
									}
								}
								float grantRate = br.ReadSingle();
							}
							if ((boneFlag & 512) == 512)
							{
								if (model.IndexSizes.Bone == 1)
								{
									byte parentBoneIndex = br.ReadByte();
								}
								else
								{
									if (model.IndexSizes.Bone == 2)
									{
										short parentBoneIndex2 = br.ReadInt16();
									}
									else
									{
										if (model.IndexSizes.Bone == 4)
										{
											int parentBoneIndex3 = br.ReadInt32();
										}
										else
										{
											if (model.IndexSizes.Bone == 8)
											{
												long parentBoneIndex4 = br.ReadInt64();
											}
										}
									}
								}
								float grantRate = br.ReadSingle();
							}
							if ((boneFlag & 1024) == 1024)
							{
								float axisVectorX = br.ReadSingle();
								float axisVectorY = br.ReadSingle();
								float axisVectorZ = br.ReadSingle();
							}
							if ((boneFlag & 2048) == 2048)
							{
								float XaxisVectorX = br.ReadSingle();
								float XaxisVectorY = br.ReadSingle();
								float XaxisVectorZ = br.ReadSingle();
								float ZaxisVectorX = br.ReadSingle();
								float ZaxisVectorY = br.ReadSingle();
								float ZaxisVectorZ = br.ReadSingle();
							}
							if ((boneFlag & 8192) == 8192)
							{
								int keyValue = br.ReadInt32();
							}
							if ((boneFlag & 32) == 32)
							{
								if (model.IndexSizes.Bone == 1)
								{
									byte parentBoneIndex = br.ReadByte();
								}
								else
								{
									if (model.IndexSizes.Bone == 2)
									{
										short parentBoneIndex2 = br.ReadInt16();
									}
									else
									{
										if (model.IndexSizes.Bone == 4)
										{
											int parentBoneIndex3 = br.ReadInt32();
										}
										else
										{
											if (model.IndexSizes.Bone == 8)
											{
												long parentBoneIndex4 = br.ReadInt64();
											}
										}
									}
								}
								int numberOfIKLoops = br.ReadInt32();
								float angleLimit = br.ReadSingle();
								int numberOfIKLinks = br.ReadInt32();
								for (int j = 0; j < numberOfIKLinks; j++)
								{
									if (model.IndexSizes.Bone == 1)
									{
										byte parentBoneIndex = br.ReadByte();
									}
									else
									{
										if (model.IndexSizes.Bone == 2)
										{
											short parentBoneIndex2 = br.ReadInt16();
										}
										else
										{
											if (model.IndexSizes.Bone == 4)
											{
												int parentBoneIndex3 = br.ReadInt32();
											}
											else
											{
												if (model.IndexSizes.Bone == 8)
												{
													long parentBoneIndex4 = br.ReadInt64();
												}
											}
										}
									}
									bool angleLimitEnabled = br.ReadBoolean();
									if (angleLimitEnabled)
									{
										float angleLimitLowerX = br.ReadSingle();
										float angleLimitLowerY = br.ReadSingle();
										float angleLimitLowerZ = br.ReadSingle();
										float angleLimitUpperX = br.ReadSingle();
										float angleLimitUpperY = br.ReadSingle();
										float angleLimitUpperZ = br.ReadSingle();
									}
								}
							}
							model.Bones.Add(bone);
                        }

                        for (int i = 0; i < vertexCount; i++)
                        {
                            model.Vertices[i].Bone0 = model.Bones[vertexBone0Indices[i]];
                            model.Vertices[i].Bone1 = model.Bones[vertexBone1Indices[i]];
                        }
                        #endregion
                        #region Morphs
                        int morphCount = br.ReadInt32();
						for (int i = 0; i < morphCount; i++)
						{
							string morphName = br.ReadInt32String(encoding);
							string morphNameE = br.ReadInt32String(encoding);
							byte morphCategory = br.ReadByte();
							byte morphType = br.ReadByte();
							int offsetSize = br.ReadInt32();
							switch (morphType)
							{
								case 0:
								{
									ModelMorphGroup morph = new ModelMorphGroup();
									morph.Name = morphName;
									for (int j = 0; j < offsetSize; j++)
									{
										if (model.IndexSizes.Morph == 1)
										{
											byte morphIndex = br.ReadByte();
										}
										else
										{
											if (model.IndexSizes.Morph == 2)
											{
												short morphIndex2 = br.ReadInt16();
											}
											else
											{
												if (model.IndexSizes.Morph == 4)
												{
													int morphIndex3 = br.ReadInt32();
												}
												else
												{
													if (model.IndexSizes.Morph == 8)
													{
														long morphIndex4 = br.ReadInt64();
													}
												}
											}
										}
										morph.MorphRate = br.ReadSingle();
									}
									model.Morphs.Add(morph);
									break;
								}
								case 1:
								{
									ModelMorphVertex morph2 = new ModelMorphVertex();
									morph2.Name = morphName;
									for (int j = 0; j < offsetSize; j++)
									{
										if (model.IndexSizes.Vertex == 1)
										{
											morph2.TopIndex = (long)((ulong)br.ReadByte());
										}
										else
										{
											if (model.IndexSizes.Vertex == 2)
											{
												morph2.TopIndex = (long)br.ReadInt16();
											}
											else
											{
												if (model.IndexSizes.Vertex == 4)
												{
													morph2.TopIndex = (long)br.ReadInt32();
												}
												else
												{
													if (model.IndexSizes.Vertex == 8)
													{
														morph2.TopIndex = br.ReadInt64();
													}
												}
											}
										}
										float offsetX = br.ReadSingle();
										float offsetY = br.ReadSingle();
										float offsetZ = br.ReadSingle();
										morph2.Offset = new PositionVector3(offsetX, offsetY, offsetZ);
									}
									model.Morphs.Add(morph2);
									break;
								}
								case 2:
								{
									ModelMorphBone morph3 = new ModelMorphBone();
									morph3.Name = morphName;
									for (int j = 0; j < offsetSize; j++)
									{
										byte b = model.IndexSizes.Bone;
										switch (b)
										{
											case 1:
											{
												morph3.BoneIndex = (long)((ulong)br.ReadByte());
												break;
											}
											case 2:
											{
												morph3.BoneIndex = (long)br.ReadInt16();
												break;
											}
											case 3:
											{
												break;
											}
											case 4:
											{
												morph3.BoneIndex = (long)br.ReadInt32();
												break;
											}
											default:
											{
												if (b == 8)
												{
													morph3.BoneIndex = br.ReadInt64();
												}
												break;
											}
										}
										float travelDistanceX = br.ReadSingle();
										float travelDistanceY = br.ReadSingle();
										float travelDistanceZ = br.ReadSingle();
										morph3.TravelDistance = new PositionVector3(travelDistanceX, travelDistanceY, travelDistanceZ);
										float rotationX = br.ReadSingle();
										float rotationY = br.ReadSingle();
										float rotationZ = br.ReadSingle();
										float rotationW = br.ReadSingle();
										morph3.Rotation = new PositionVector4(rotationX, rotationY, rotationZ, rotationW);
									}
									model.Morphs.Add(morph3);
									break;
								}
								case 3:
								case 4:
								case 5:
								case 6:
								case 7:
								{
									ModelMorphUV morph4 = new ModelMorphUV();
									morph4.Name = morphName;
									for (int j = 0; j < offsetSize; j++)
									{
										byte b = model.IndexSizes.Vertex;
										switch (b)
										{
											case 1:
											{
												morph4.VertexIndex = (long)((ulong)br.ReadByte());
												break;
											}
											case 2:
											{
												morph4.VertexIndex = (long)br.ReadInt16();
												break;
											}
											case 3:
											{
												break;
											}
											case 4:
											{
												morph4.VertexIndex = (long)br.ReadInt32();
												break;
											}
											default:
											{
												if (b == 8)
												{
													morph4.VertexIndex = br.ReadInt64();
												}
												break;
											}
										}
										float uvOffsetX = br.ReadSingle();
										float uvOffsetY = br.ReadSingle();
										float uvOffsetZ = br.ReadSingle();
										float uvOffsetW = br.ReadSingle();
										morph4.UVOffset = new PositionVector4(uvOffsetX, uvOffsetY, uvOffsetZ, uvOffsetW);
									}
									model.Morphs.Add(morph4);
									break;
								}
								case 8:
								{
									ModelMorphMaterial morph5 = new ModelMorphMaterial();
									morph5.Name = morphName;
									for (int j = 0; j < offsetSize; j++)
									{
										byte b = model.IndexSizes.Material;
										switch (b)
										{
											case 1:
											{
												morph5.MaterialIndex = (long)((ulong)br.ReadByte());
												break;
											}
											case 2:
											{
												morph5.MaterialIndex = (long)br.ReadInt16();
												break;
											}
											case 3:
											{
												break;
											}
											case 4:
											{
												morph5.MaterialIndex = (long)br.ReadInt32();
												break;
											}
											default:
											{
												if (b == 8)
												{
													morph5.MaterialIndex = br.ReadInt64();
												}
												break;
											}
										}
										byte formatOffsetOperation = br.ReadByte();
										float diffuseR = br.ReadSingle();
										float diffuseG = br.ReadSingle();
										float diffuseB = br.ReadSingle();
										float diffuseA = br.ReadSingle();
										morph5.DiffuseColor = Color.FromArgb((int)(diffuseA * 255f), (int)(diffuseR * 255f), (int)(diffuseG * 255f), (int)(diffuseB * 255f));
										float specularR = br.ReadSingle();
										float specularG = br.ReadSingle();
										float specularB = br.ReadSingle();
										morph5.SpecularColor = Color.FromArgb((int)(specularR * 255f), (int)(specularG * 255f), (int)(specularB * 255f));
										float specularCoefficient = br.ReadSingle();
										morph5.SpecularCoefficient = specularCoefficient;
										float ambientR = br.ReadSingle();
										float ambientG = br.ReadSingle();
										float ambientB = br.ReadSingle();
										morph5.AmbientColor = Color.FromArgb((int)(ambientR * 255f), (int)(ambientG * 255f), (int)(ambientB * 255f));
										float edgeR = br.ReadSingle();
										float edgeG = br.ReadSingle();
										float edgeB = br.ReadSingle();
										float edgeA = br.ReadSingle();
										morph5.EdgeColor = Color.FromArgb((int)(edgeA * 255f), (int)(edgeR * 255f), (int)(edgeG * 255f), (int)(edgeB * 255f));
										float edgeSize = br.ReadSingle();
										morph5.EdgeSize = edgeSize;
										float textureCoefficientR = br.ReadSingle();
										float textureCoefficientG = br.ReadSingle();
										float textureCoefficientB = br.ReadSingle();
										float textureCoefficientA = br.ReadSingle();
										morph5.TextureCoefficient = Color.FromArgb((int)(textureCoefficientA * 255f), (int)(textureCoefficientR * 255f), (int)(textureCoefficientG * 255f), (int)(textureCoefficientB * 255f));
										float sphereTextureCoefficientR = br.ReadSingle();
										float sphereTextureCoefficientG = br.ReadSingle();
										float sphereTextureCoefficientB = br.ReadSingle();
										float sphereTextureCoefficientA = br.ReadSingle();
										morph5.SphereCoefficient = Color.FromArgb((int)(sphereTextureCoefficientA * 255f), (int)(sphereTextureCoefficientR * 255f), (int)(sphereTextureCoefficientG * 255f), (int)(sphereTextureCoefficientB * 255f));
										float toonTextureCoefficientR = br.ReadSingle();
										float toonTextureCoefficientG = br.ReadSingle();
										float toonTextureCoefficientB = br.ReadSingle();
										float toonTextureCoefficientA = br.ReadSingle();
										morph5.ToonTextureCoefficient = Color.FromArgb((int)(toonTextureCoefficientA * 255f), (int)(toonTextureCoefficientR * 255f), (int)(toonTextureCoefficientG * 255f), (int)(toonTextureCoefficientB * 255f));
									}
									model.Morphs.Add(morph5);
									break;
								}
							}
                        }
                        #endregion
                        #region PMA for PMX
                        if (!br.EndOfStream)
						{
							string PMAKey = br.ReadFixedLengthString(3);
							if (!(PMAKey != "Pma"))
							{
								int versionMajor = br.ReadInt32();
								int versionMinor = br.ReadInt32();
								int versionBuild = br.ReadInt32();
								int versionRevision = br.ReadInt32();
								model.PMAExtension.Enabled = true;
								model.PMAExtension.Version = new Version(versionMajor, versionMinor, versionBuild, versionRevision);
								model.PMAExtension.TextureFlipping.Enabled = br.ReadBoolean();
								if (model.PMAExtension.TextureFlipping.Enabled)
								{
									int pmaNumberOfAnimatedBlocks = br.ReadInt32();
									for (int i = 0; i < pmaNumberOfAnimatedBlocks; i++)
									{
										int pmaAnimatedBlockMaterialIndex = br.ReadInt32();
										int pmaAnimatedBlockNumberOfDifferentTextures = br.ReadInt32();
										ModelPMAExtension.TextureFlippingBlock tfb = new ModelPMAExtension.TextureFlippingBlock();
										tfb.Material = model.Materials[pmaAnimatedBlockMaterialIndex];
										for (int j = 0; j < pmaAnimatedBlockNumberOfDifferentTextures; j++)
										{
											ModelPMAExtension.TextureFlippingFrame tff = new ModelPMAExtension.TextureFlippingFrame();
											tff.Timestamp = br.ReadUInt64();
											tff.FileName = br.ReadNullTerminatedString();
											tfb.Frames.Add(tff);
										}
										model.PMAExtension.TextureFlipping.Blocks.Add(tfb);
									}
								}
								model.PMAExtension.EffectPresets.Enabled = br.ReadBoolean();
								if (model.PMAExtension.EffectPresets.Enabled)
								{
									int pmaNumberOfMMEffectPresets = br.ReadInt32();
									for (int i = 0; i < pmaNumberOfMMEffectPresets; i++)
									{
										string effectFileName = br.ReadNullTerminatedString();
										model.PMAExtension.EffectPresets.FileNames.Add(effectFileName);
									}
								}
							}
                        }
                        #endregion
                    }
				}
			}
		}
		protected override void SaveInternal(ObjectModel objectModel)
		{
		}
	}
}
