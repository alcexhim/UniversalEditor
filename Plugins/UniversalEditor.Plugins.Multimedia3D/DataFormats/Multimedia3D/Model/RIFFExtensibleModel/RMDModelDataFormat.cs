using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.DataFormats.Chunked;
using UniversalEditor.ObjectModels.Chunked;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

#if ZZYZX
namespace UniversalEditor.DataFormats.Multimedia3D.Model.RIFFExtensibleModel
{
	public class RMDModelDataFormat : RIFFDataFormat
	{
		protected override bool IsObjectModelSupported(ObjectModel omb)
		{
			RIFFObjectModel riff = (omb as RIFFObjectModel);
			if (riff != null)
			{
				if (riff.Chunks["MODL"] != null)
				{
					return true;
				}
			}
			return false;
		}
		private DataFormatReference _dfr = null;
		public override DataFormatReference MakeReference()
		{
			if (_dfr == null)
			{
				_dfr = base.MakeReference();
				_dfr.Clear();
				_dfr.Capabilities.Add(typeof(ModelObjectModel), DataFormatCapabilities.All);
				_dfr.Filters.Add("RIFF extensible model data", new byte?[][] { new byte?[] { (byte)'R', (byte)'I', (byte)'F', (byte)'F', null, null, null, null, (byte)'M', (byte)'O', (byte)'D', (byte)'L' } }, new string[] { "*.rmd" });
			}
			return _dfr;
		}
		protected override void BeforeLoadInternal(Stack<ObjectModel> objectModels)
		{
			base.BeforeLoadInternal(objectModels);
			objectModels.Push(new RIFFObjectModel());
		}
		protected override void BeforeSaveInternal(Stack<ObjectModel> objectModels)
        {
            ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);
            string basePath = System.IO.Path.GetDirectoryName(model.FileName);

            RIFFObjectModel riff = new RIFFObjectModel();

            RIFFGroupChunk chunkRIFF = new RIFFGroupChunk();
            chunkRIFF.TypeID = "RIFF";
            chunkRIFF.ID = "MODL";

            // http://owl.phy.queensu.ca/~phil/exiftool/TagNames/RIFF.html

#region Information
            {
                if (model.StringTable.ContainsKey(1033))
                {
                    riff.Information.Add(CommonMetadataType.TitleINAM, model.StringTable[1033].Title);
                    riff.Information.Add(CommonMetadataType.Artist, model.StringTable[1033].Author);
                }
                riff.Information.Add(CommonMetadataType.Software, "Mike Becker's Software - Universal Editor");
            }
			#endregion
#region Vertex Data
            {
                RIFFDataChunk chunkMVTX = new RIFFDataChunk();
                chunkMVTX.ID = "MVTX";
                System.IO.MemoryStream msMVTX = new System.IO.MemoryStream();
                IO.Writer bwMVTX = new IO.Writer(msMVTX);
                foreach (ModelSurface surf in model.Surfaces)
                {
                    foreach (ModelVertex vtx in surf.Vertices)
                    {
                        bwMVTX.Write(vtx.Position.X);
                        bwMVTX.Write(vtx.Position.Y);
                        bwMVTX.Write(vtx.Position.Z);
                        bwMVTX.Write(vtx.Normal.X);
                        bwMVTX.Write(vtx.Normal.Y);
                        bwMVTX.Write(vtx.Normal.Z);
                        bwMVTX.Write(vtx.Texture.U);
                        bwMVTX.Write(vtx.Texture.V);
                        bwMVTX.Write(vtx.Weight);
                        bwMVTX.Write(vtx.EdgeFlag);
                    }
                }
                bwMVTX.Close();
                chunkMVTX.Data = msMVTX.ToArray();
                chunkRIFF.Chunks.Add(chunkMVTX);
            }
#endregion
#region Bone Data
            {
                RIFFDataChunk chunkMBON = new RIFFDataChunk();
                chunkMBON.ID = "MBON";

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelBone bone in model.Bones)
                {
                    bw.WriteNullTerminatedString(bone.Name);
                    bw.Write(bone.Position.X);
                    bw.Write(bone.Position.Y);
                    bw.Write(bone.Position.Z);
                    bw.Write(bone.Rotation.X);
                    bw.Write(bone.Rotation.Y);
                    bw.Write(bone.Rotation.Z);
                    bw.Write(bone.Rotation.W);
                    bw.Write((int)bone.BoneType);
                    bw.Write(model.Bones.IndexOf(bone.ChildBone));
                    bw.Write(bone.IKLimitAngle);
                    bw.Write(bone.IKNumber);
                }
                bw.Close();

                chunkMBON.Data = ms.ToArray();
            }
#endregion
#region Material Data
            {
                RIFFDataChunk chunkMMAT = new RIFFDataChunk();
                chunkMMAT.ID = "MMAT";

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelMaterial mat in model.Materials)
                {
                    bw.WriteNullTerminatedString(mat.Name);
                    bw.WriteNullTerminatedString(mat.Comment);

                    bw.Write(mat.AlwaysLight);
                    bw.Write(mat.EdgeFlag);
                    bw.Write(mat.EdgeSize);
                    bw.Write(mat.Shininess);
                    bw.Write(mat.TextureIndex);

                    bw.Write((byte)mat.AmbientColor.R);
                    bw.Write((byte)mat.AmbientColor.G);
                    bw.Write((byte)mat.AmbientColor.B);
                    bw.Write((byte)mat.AmbientColor.A);

                    bw.Write((byte)mat.DiffuseColor.R);
                    bw.Write((byte)mat.DiffuseColor.G);
                    bw.Write((byte)mat.DiffuseColor.B);
                    bw.Write((byte)mat.DiffuseColor.A);

                    bw.Write((byte)mat.EmissiveColor.R);
                    bw.Write((byte)mat.EmissiveColor.G);
                    bw.Write((byte)mat.EmissiveColor.B);
                    bw.Write((byte)mat.EmissiveColor.A);

                    bw.Write((byte)mat.SpecularColor.R);
                    bw.Write((byte)mat.SpecularColor.G);
                    bw.Write((byte)mat.SpecularColor.B);
                    bw.Write((byte)mat.SpecularColor.A);

                    bw.Write((byte)mat.EdgeColor.R);
                    bw.Write((byte)mat.EdgeColor.G);
                    bw.Write((byte)mat.EdgeColor.B);
                    bw.Write((byte)mat.EdgeColor.A);

                    // Number of triangles affected by this material
                    bw.Write(mat.Triangles.Count);

                    foreach (ModelTriangle tri in mat.Triangles)
                    {
                        // Pointer to the triangle for this material
                        bw.Write(model.Triangles.IndexOf(tri));
                    }
                }
                bw.Close();

                chunkMMAT.Data = ms.ToArray();
            }
#endregion
#region Texture Images
            {
                RIFFDataChunk chunkMTEX = new RIFFDataChunk();
                chunkMTEX.ID = "MTEX";

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelMaterial mat in model.Materials)
                {
                    bw.Write(model.Materials.IndexOf(mat));

                    foreach (ModelTexture tex in mat.Textures)
                    {
                        bw.Write((int)tex.Flags);

                        if (String.IsNullOrEmpty(tex.FileName))
                        {
                            bw.Write((int)0);
                            bw.Write((int)0);
                        }
                        else
                        {
                            string textureFileName = String.Empty;
                            string sphereFileName = String.Empty;

                            if (tex.FileName.Contains("*"))
                            {
                                string[] texFileName = tex.FileName.Split(new char[] { '*' });
                                textureFileName = basePath + System.IO.Path.DirectorySeparatorChar.ToString() + texFileName[0];
                                sphereFileName = basePath + System.IO.Path.DirectorySeparatorChar.ToString() + texFileName[1];
                            }
                            else if (tex.FileName.EndsWith(".sph") || tex.FileName.EndsWith(".spa"))
                            {
                                sphereFileName = basePath + System.IO.Path.DirectorySeparatorChar.ToString() + tex.FileName;
                            }
                            else
                            {
                                textureFileName = basePath + System.IO.Path.DirectorySeparatorChar.ToString() + tex.FileName;
                            }

                            if (!String.IsNullOrEmpty(textureFileName))
                            {
                                byte[] textureFileData = System.IO.File.ReadAllBytes(textureFileName);

                                bw.Write(textureFileData.Length);
                                bw.Write(textureFileData);
                            }
                            else
                            {
                                bw.Write((int)0);
                            }
                            if (!String.IsNullOrEmpty(sphereFileName))
                            {
                                byte[] sphereFileData = System.IO.File.ReadAllBytes(sphereFileName);

                                bw.Write(sphereFileData.Length);
                                bw.Write(sphereFileData);
                            }
                            else
                            {
                                bw.Write((int)0);
                            }
                        }
                    }
                }

                chunkMTEX.Data = ms.ToArray();
                chunkRIFF.Chunks.Add(chunkMTEX);
            }
#endregion
#region Group Mapping to Bones
            {
                RIFFDataChunk chunk = new RIFFDataChunk();
                chunk.ID = "MGRP";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelBoneGroup group in model.BoneGroups)
                {
                    bw.WriteNullTerminatedString(group.Name, Encoding.UTF8);
                    bw.Write(group.Bones.Count);
                    foreach (ModelBone bone in group.Bones)
                    {
                        bw.Write(model.Bones.IndexOf(bone));
                    }
                }
                bw.Close();
                chunk.Data = ms.ToArray();
                chunkRIFF.Chunks.Add(chunk);
            }
#endregion
#region Vertex Mapping to Bones
            {
                RIFFDataChunk chunk = new RIFFDataChunk();
                chunk.ID = "BMAP";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelVertex vtx in model.Vertices)
                {
                    bw.Write(model.Vertices.IndexOf(vtx));
                    bw.Write(model.Bones.IndexOf(vtx.Bone0));
                    bw.Write(model.Bones.IndexOf(vtx.Bone1));
                }
                bw.Close();
                chunk.Data = ms.ToArray();
                chunkRIFF.Chunks.Add(chunk);
            }
#endregion
#region Expressions
            {
                RIFFDataChunk chunk = new RIFFDataChunk();
                chunk.ID = "MEXP";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ushort u in model.Expressions)
                {
                    bw.Write(u);
                }
                bw.Close();
                chunk.Data = ms.ToArray();
            }
#endregion
#region Skins
            {
                RIFFDataChunk chunk = new RIFFDataChunk();
                chunk.ID = "MSKN";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelSkin skin in model.Skins)
                {
                    bw.WriteNullTerminatedString(skin.Name);
                    bw.Write(skin.Category);
                    bw.Write(skin.Vertices.Count);
                    foreach (ModelSkinVertex vtx in skin.Vertices)
                    {
                        bw.Write(model.Vertices.IndexOf(vtx.TargetVertex));
                        bw.Write(vtx.MaximumPosition.X);
						bw.Write(vtx.MaximumPosition.Y);
						bw.Write(vtx.MaximumPosition.Z);
                    }
                }
                bw.Close();
                chunk.Data = ms.ToArray();
            }
#endregion
#region IK
            {
                RIFFDataChunk chunk = new RIFFDataChunk();
                chunk.ID = "IK  ";
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                IO.Writer bw = new IO.Writer(ms);
                foreach (ModelIK ik in model.IK)
                {
                    bw.Write(ik.Index);
                    bw.Write(ik.LimitOnce);
                    bw.Write(ik.LoopCount);
                    bw.Write(ik.BoneList.Count);
                    foreach (ModelBone bone in ik.BoneList)
                    {
                        bw.Write(model.Bones.IndexOf(bone));
                    }
                    bw.Write(model.Bones.IndexOf(ik.EffBone));
                    bw.Write(model.Bones.IndexOf(ik.TargetBone));
                }
                bw.Close();
                chunk.Data = ms.ToArray();
            }
#endregion
#region PMA Extensions
            {
                RIFFGroupChunk chunkPMAX = new RIFFGroupChunk();
                chunkPMAX.TypeID = "LIST";
                chunkPMAX.ID = "PMAX";

                RIFFDataChunk chunkPMAV = new RIFFDataChunk();
                chunkPMAV.ID = "PMAV";
                System.IO.MemoryStream msPMAV = new System.IO.MemoryStream();
                IO.Writer bwPMAV = new IO.Writer(msPMAV);
                bwPMAV.Write(model.PMAExtension.Version.Major);
                bwPMAV.Write(model.PMAExtension.Version.Minor);
                bwPMAV.Write(model.PMAExtension.Version.Build);
                bwPMAV.Write(model.PMAExtension.Version.Revision);
                bwPMAV.Close();
                chunkPMAV.Data = msPMAV.ToArray();
                chunkPMAX.Chunks.Add(chunkPMAV);

                if (model.PMAExtension.TextureFlipping.Enabled)
                {
                    RIFFDataChunk chunkTEXI = new RIFFDataChunk();
                    chunkTEXI.ID = "TEXI";

                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    IO.Writer bw = new IO.Writer(ms);

                    foreach (ModelPMAExtension.TextureFlippingBlock block in model.PMAExtension.TextureFlipping.Blocks)
                    {
                        bw.Write(block.Frames.Count);
                        foreach (ModelPMAExtension.TextureFlippingFrame frame in block.Frames)
                        {
                            bw.Write(frame.Timestamp);
                            bw.WriteNullTerminatedString(frame.FileName);
                        }
                    }

                    bw.Close();
                    chunkTEXI.Data = ms.ToArray();
                    chunkPMAX.Chunks.Add(chunkTEXI);
                }
                if (model.ModelEffectScriptFileNames.Count > 0)
                {
                    RIFFDataChunk chunkEFXS = new RIFFDataChunk();
                    chunkEFXS.ID = "EFXS";

                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    IO.Writer bw = new IO.Writer(ms);
					foreach (string s in model.ModelEffectScriptFileNames)
                    {
                        bw.WriteNullTerminatedString(s);
                    }
                    bw.Close();

                    chunkEFXS.Data = ms.ToArray();
                    chunkPMAX.Chunks.Add(chunkEFXS);
                }

                chunkRIFF.Chunks.Add(chunkPMAX);
            }
#endregion

            riff.Chunks.Add(chunkRIFF);

            objectModels.Push(riff);

            base.BeforeSaveInternal(objectModels);
        }
        public override bool IsBootstrappable(UniversalEditor.ObjectModel objectModel)
        {
            if (objectModel is RIFFObjectModel)
            {
                RIFFObjectModel riff = (objectModel as RIFFObjectModel);
                RIFFGroupChunk chunkRIFF = (riff.Chunks["RIFF"] as RIFFGroupChunk);
                if (chunkRIFF != null)
                {
                    if (chunkRIFF.ID == "MODL")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        protected override void AfterLoadInternal(Stack<ObjectModel> objectModels)
        {
            base.AfterLoadInternal(objectModels);

            RIFFObjectModel riff = (objectModels.Pop() as RIFFObjectModel);
            ModelObjectModel model = (objectModels.Pop() as ModelObjectModel);

        }
	}
}
#endif
