using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using UniversalEditor.IO;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels;
using UniversalEditor.Plugins.Multimedia3D.ObjectModels.Model;

namespace UniversalEditor.Plugins.Multimedia3D.DataFormats.PolygonMovieMaker.Model
{
	/// <summary>
	/// A data format for loading Polygon Movie Maker first generation model data.
	/// </summary>
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
                if (PMDKey != "Pmd") throw new DataFormatException(Localization.StringTable.ErrorDataFormatInvalid);

                mvarVersion = br.ReadSingle();
                ModelStringTableExtension englishInformation = new ModelStringTableExtension();
                model.StringTable.Add(1033, englishInformation);
                ModelStringTableExtension japaneseInformation = new ModelStringTableExtension();
                model.StringTable.Add(1041, japaneseInformation);
                Encoding encoding = Encoding.GetEncoding("shift_jis");

                string modelName = br.ReadFixedLengthString(20, encoding);
                if (modelName.Contains("\0"))
                {
                    modelName = modelName.Substring(0, modelName.IndexOf('\0'));
                }
                japaneseInformation.Title = modelName;

                string modelComment = br.ReadFixedLengthString(256, encoding);
                if (modelComment.Contains("\0"))
                {
                    modelComment = modelComment.Substring(0, modelComment.IndexOf('\0'));
                }
                japaneseInformation.Comments = modelComment;

                #region Vertices
                uint nVertices = br.ReadUInt32();

                ushort[] vertexBone0Indices = new ushort[nVertices];
                ushort[] vertexBone1Indices = new ushort[nVertices];

                ModelSurface surf = new ModelSurface();
                model.Surfaces.Add(surf);

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
                    vtx.Weight = (float)weight;

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
                    float diffuseR = br.ReadSingle();
                    float diffuseG = br.ReadSingle();
                    float diffuseB = br.ReadSingle();
                    float diffuseA = br.ReadSingle();

                    material.DiffuseColor = Color.FromArgb((int)Math.Min(255f, diffuseA * 255f), (int)Math.Min(255f, diffuseR * 255f), (int)Math.Min(255f, diffuseG * 255f), (int)Math.Min(255f, diffuseB * 255f));

                    // shininess
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
                    material.Textures.Add(texturePath, textureFlags);

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
                    float positionX = br.ReadSingle();
                    float positionY = br.ReadSingle();
                    float positionZ = br.ReadSingle();
                    bone2.Position = new PositionVector3(positionX, positionY, positionZ);
                    bone2.OriginalPosition = new PositionVector3(positionX, positionY, positionZ);

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
                    surf.Vertices[i].Bone0 = model.Bones[vertexBone0Indices[i]];
                    surf.Vertices[i].Bone1 = model.Bones[vertexBone1Indices[i]];
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

                        float maxPosX = br.ReadSingle();
                        float maxPosY = br.ReadSingle();
                        float maxPosZ = br.ReadSingle();

                        ModelSkinVertex svtx = new ModelSkinVertex();
						
						svtx.MaximumPosition = new PositionVector3(maxPosX, maxPosY, maxPosZ);
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
                uint nBoneGroupCount = br.ReadUInt32();

                ushort[] nBoneID = new ushort[nBoneGroupCount];
                byte[] nNodeID = new byte[nBoneGroupCount];

                // Names for the bone groups to display in MMD's dopesheet.
                for (ushort j = 0; j < nBoneGroupCount; j++)
                {
                    // Index in the bone list representing which bone to display
                    nBoneID[j] = br.ReadUInt16();
                    // Index in the list of bone groups representing which group to place this bone in 
                    nNodeID[j] = br.ReadByte();
                }
                for (ushort j = 0; j < nBoneGroupCount; j++)
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
                    englishInformation.Title = br.ReadNullTerminatedString(20);
                    englishInformation.Comments = br.ReadNullTerminatedString(256);
                    for (ushort j = 0; j < nBones; j += 1)
                    {
                        string boneNameInShiftJIS = br.ReadNullTerminatedString(20);
                        string boneNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(boneNameInShiftJIS));
                        englishInformation.BoneNames.Add(boneNameInUTF8.Trim());
                    }
                    for (ushort j = 0; j < nSkins; j += 1)
                    {
                        string skinNameInShiftJIS = br.ReadNullTerminatedString(20);
                        string skinNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(skinNameInShiftJIS));
                        englishInformation.SkinNames.Add(skinNameInUTF8.Trim());
                    }
                    br.BaseStream.Seek(30L, SeekOrigin.Current);
                    for (byte m = 0; m < nNodeNames - 1; m += 1)
                    {
                        string nodeNameInShiftJIS = br.ReadNullTerminatedString(50);
                        string nodeNameInUTF8 = encoding.GetString(System.Text.Encoding.Default.GetBytes(nodeNameInShiftJIS));
                        englishInformation.NodeNames.Add(nodeNameInUTF8.Trim());
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
                    #endregion
                    #region Joints
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
                    #endregion
                }
                #endregion
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

                string modelNameJ = String.Empty;
                if (model.StringTable.ContainsKey(1041))
                {
                    modelNameJ = model.StringTable[1041].Title;
                }
				bw.WriteFixedLengthString(modelNameJ, encoding, 20);

                string modelCommentJ = String.Empty;
                if (model.StringTable.ContainsKey(1041))
                {
                    modelCommentJ = model.StringTable[1041].Comments;
                }
				bw.WriteFixedLengthString(modelCommentJ, encoding, 256);

                ModelSurface surf = model.Surfaces[0];

				bw.Write((uint)surf.Vertices.Count);
				for (int i = 0; i < surf.Vertices.Count; i++)
				{
					ModelVertex vtx = surf.Vertices[i];
					
					PositionVector3 vec = vtx.Position;
					bw.Write((float)vec.X);
                    bw.Write((float)vec.Y);
                    bw.Write((float)vec.Z);

					vec = vtx.Normal;
                    bw.Write((float)vec.X);
                    bw.Write((float)vec.Y);
                    bw.Write((float)vec.Z);

					TextureVector2 texture = vtx.Texture;
                    bw.Write((float)texture.U);
                    bw.Write((float)texture.V);

                    ushort indexToBone0 = (ushort)model.Bones.IndexOf(vtx.Bone0);
                    ushort indexToBone1 = (ushort)model.Bones.IndexOf(vtx.Bone1);

					bw.Write(indexToBone0);
					bw.Write(indexToBone1);

					bw.Write((byte)vtx.Weight);
					bw.Write(vtx.EdgeFlag);
				}
				bw.Write((uint)surf.Triangles.Count);
				for (int i = 0; i < surf.Triangles.Count; i++)
				{
					ModelTriangle face = surf.Triangles[i];

					ushort vertex1Index = (ushort)(surf.Vertices.IndexOf(face.Vertex1));
					ushort vertex2Index = (ushort)(surf.Vertices.IndexOf(face.Vertex2));
					ushort vertex3Index = (ushort)(surf.Vertices.IndexOf(face.Vertex3));
					
					bw.Write(vertex1Index);
					bw.Write(vertex2Index);
					bw.Write(vertex3Index);
				}
				bw.Write((uint)model.Materials.Count);
				for (int i = 0; i < model.Materials.Count; i++)
				{
					ModelMaterial material = model.Materials[i];
					Color color = material.DiffuseColor;
					bw.Write((float)color.R / 255f);
					bw.Write((float)color.G / 255f);
					bw.Write((float)color.B / 255f);
					bw.Write((float)color.A / 255f);
					bw.Write(material.Shininess);
					
                    color = material.SpecularColor;
					bw.Write((float)color.R / 255f);
					bw.Write((float)color.G / 255f);
					bw.Write((float)color.B / 255f);
					
                    color = material.AmbientColor;
					bw.Write((float)color.R / 255f);
					bw.Write((float)color.G / 255f);
					bw.Write((float)color.B / 255f);

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
					bw.Write((byte)bone.BoneType);
					bw.Write(bone.IKNumber);

                    bw.Write((float)bone.Position.X);
                    bw.Write((float)bone.Position.Y);
                    bw.Write((float)bone.Position.Z);
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
						uint targetVertex = (uint)(surf.Vertices.IndexOf(skin.Vertices[j].TargetVertex));
						bw.Write(targetVertex);

                        bw.Write((float)skin.Vertices[j].MaximumPosition.X);
                        bw.Write((float)skin.Vertices[j].MaximumPosition.Y);
                        bw.Write((float)skin.Vertices[j].MaximumPosition.Z);
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
				bw.Write((ushort)model.BoneGroups.Count);
				foreach (ModelBoneGroup group in model.BoneGroups)
				{
					foreach (ModelBone bone in group.Bones)
					{
						ushort boneIndex = (ushort)model.Bones.IndexOf(bone);
						byte groupIndex = (byte)model.BoneGroups.IndexOf(group);
						bw.Write(boneIndex);
						bw.Write(groupIndex);
					}
				}
				bw.Write(true);
				bw.Write(true);
				if (model.StringTable.ContainsKey(1033))
				{
					bw.Write(true);
					
                    ModelStringTableExtension englishInformation = model.StringTable[1033];
					bw.WriteNullTerminatedString(englishInformation.Title, 20);
					bw.WriteNullTerminatedString(englishInformation.Comments, 256);

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
