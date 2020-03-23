//
//  MyClass.cs
//
//  Author:
//       Mike Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019 Mike Becker
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
using MBS.Framework.Drawing;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Multimedia.Picture;
using UniversalEditor.ObjectModels.Multimedia3D.Model;

namespace MBS.Framework.Rendering
{
	public abstract class Canvas
	{
		public Engine Engine { get; private set; } = null;
		protected internal Canvas(Engine engine)
		{
			Engine = engine;
		}

		protected abstract Matrix GetMatrixInternal();

		private Matrix _Matrix = null;
		public Matrix Matrix
		{
			get
			{
				if (_Matrix == null)
				{
					_Matrix = GetMatrixInternal();
				}
				return _Matrix;
			}
		}

		protected abstract void ClearInternal(Color black);
		public void Clear(Color color)
		{
			ClearInternal(color);
		}

		public bool RenderModels { get; set; } = true;
		public bool RenderBones { get; set; } = false;

		protected abstract bool EnableBlendingInternal { get; set; }
		public bool EnableBlending { get { return EnableBlendingInternal; } set { EnableBlendingInternal = value; } }
		protected abstract bool EnableTexturingInternal { get; set; }
		public bool EnableTexturing { get { return EnableTexturingInternal; } set { EnableTexturingInternal = value; } }
		protected abstract bool EnableLightingInternal { get; set; }
		public bool EnableLighting { get { return EnableLightingInternal; } set { EnableLightingInternal = value; } }
		protected abstract bool EnableCullingInternal { get; set; }
		public bool EnableCulling { get { return EnableCullingInternal; } set { EnableCullingInternal = value; } }
		protected abstract bool EnableTextureGenerationSInternal { get; set; }
		public bool EnableTextureGenerationS { get { return EnableTextureGenerationSInternal; } set { EnableTextureGenerationSInternal = value; } }
		protected abstract bool EnableTextureGenerationTInternal { get; set; }
		public bool EnableTextureGenerationT { get { return EnableTextureGenerationTInternal; } set { EnableTextureGenerationTInternal = value; } }
		protected abstract bool EnableTextureGenerationRInternal { get; set; }
		public bool EnableTextureGenerationR { get { return EnableTextureGenerationRInternal; } set { EnableTextureGenerationRInternal = value; } }
		protected abstract bool EnableTextureGenerationQInternal { get; set; }

		protected abstract void DrawArraysInternal(RenderMode mode, int start, int count);
		public void DrawArrays(RenderMode mode, int start, int count)
		{
			DrawArraysInternal(mode, start, count);
		}

		public bool EnableTextureGenerationQ { get { return EnableTextureGenerationQInternal; } set { EnableTextureGenerationQInternal = value; } }

		protected abstract Color ColorInternal { get; set; }
		public Color Color { get { return ColorInternal; } set { ColorInternal = value; } }

		private TextureTarget mvarLastTextureTarget = TextureTarget.Texture2D;
		private Texture mvarTexture = null;
		public Texture Texture
		{
			get { return mvarTexture; }
			set
			{
				mvarTexture = value;
				if (mvarTexture == null)
				{
					// Internal.OpenGL.Methods.glBindTexture(mvarLastTextureTarget, UInt32.MaxValue);
				}
				else
				{
					Engine.BindTexture(mvarTexture.Target, mvarTexture.ID);
					mvarLastTextureTarget = mvarTexture.Target;
				}
			}
		}

		private ShaderProgram _Program = null;
		public ShaderProgram Program { get { return _Program; } set { _Program = value; Engine.UseProgram(value); } }

		private static Dictionary<ModelObjectModel, int> modelDisplayLists = new Dictionary<ModelObjectModel, int>();
		public void DrawModel(ModelObjectModel model)
		{
			if (model == null) return;

			string basedir = Environment.CurrentDirectory;
			if (model.Accessor is FileAccessor)
			{
				basedir = System.IO.Path.GetDirectoryName((model.Accessor as FileAccessor).FileName);
			}

			if (RenderModels)
			{
				#region Load Materials
				if (!model.MaterialsLoaded && !model.MaterialsLoading)
				{
					model.MaterialsLoading = true;

					if (model.Accessor is FileAccessor)
					{
						Console.WriteLine("Loading materials for model: \"" + (model.Accessor as FileAccessor).FileName + "\"");
					}
					else
					{
						Console.WriteLine("Loading materials for model");
					}

					foreach (ModelMaterial mat1 in model.Materials)
					{
						Console.Write("Loading material " + model.Materials.IndexOf(mat1).ToString() + " / " + model.Materials.Count.ToString() + "... ");

						foreach (ModelTexture texture in mat1.Textures)
						{
							if (texture.TexturePicture != null)
							{
								// Image has already been preloaded
								if (texture.TextureID == null)
								{
									// Store texture ID for this texture
									Texture t1 = Texture.FromPicture(texture.TexturePicture);
									texture.TextureID = t1.ID;
								}
							}
							else
							{
								if (!String.IsNullOrEmpty(texture.TextureFileName))
								{
									if (texture.Flags != ModelTextureFlags.None)
									{
										if (texture.TextureID == null)
										{
											// Store texture ID for this texture
											string textureImageFullFileName = UniversalEditor.Common.Path.MakeAbsolutePath(texture.TextureFileName, basedir);
											if (!System.IO.File.Exists(textureImageFullFileName))
											{
												Console.WriteLine("texture image not found: " + textureImageFullFileName);
												continue;
											}

											Texture t1 = CreateTextureFromFile(textureImageFullFileName);
											texture.TextureID = t1.ID;
										}
									}
								}
							}

							if ((texture.Flags & (ModelTextureFlags.Map | ModelTextureFlags.AddMap)) != ModelTextureFlags.None)
							{
								if (texture.TexturePicture != null)
								{
									// Image has already been preloaded
									if (texture.MapID == null)
									{
										// Store texture ID for this texture
										Texture t1 = Texture.FromPicture(texture.TexturePicture);
										texture.MapID = t1.ID;
									}
								}
								else
								{
									if (!String.IsNullOrEmpty(texture.MapFileName))
									{
										if (texture.Flags != ModelTextureFlags.None)
										{
											if (texture.TextureID == null)
											{
												// Store texture ID for this texture
												string textureImageFullFileName = UniversalEditor.Common.Path.MakeAbsolutePath(texture.MapFileName, basedir);
												if (!System.IO.File.Exists(textureImageFullFileName))
												{
													Console.WriteLine("texture image not found: " + textureImageFullFileName);
													continue;
												}

												Texture t1 = CreateTextureFromFile(textureImageFullFileName);
												texture.MapID = t1.ID;
											}
										}
									}
								}
							}
						}

						Console.WriteLine("done!");
					}
					model.MaterialsLoading = false;
					model.MaterialsLoaded = true;
				}
				#endregion

				int vertexIndex = 0;
				if (model.Materials.Count > 0)
				{
					foreach (ModelMaterial mat in model.Materials)
					{
						// update the texture index
						if (mat.TextureIndex < (mat.Textures.Count - 1))
						{
							mat.TextureIndex++;
						}
						else
						{
							mat.TextureIndex = 0;
						}

						// 輪郭・影有無で色指定方法を変える
						// Contour - How to specify color change with or without shadow

						// 半透明でなければポリゴン裏面を無効にする
						// To disable the reverse side must be semi-transparent polygons
						/*
						if (mat.DiffuseColor.A >= 255)
						{
							CullingMode = Caltron.CullingMode.Disabled;
						}
						else
						{
							CullingMode = Caltron.CullingMode.Front;
						}
						*/

						// テクスチャ・スフィアマップの処理
						// Processing of the texture map sphere
						ModelTextureFlags fTexFlag = ModelTextureFlags.None;
						if (mat.TextureIndex > -1 && mat.Textures.Count > 0)
						{
							fTexFlag = mat.Textures[mat.TextureIndex].Flags;
						}

						if (((fTexFlag & ModelTextureFlags.Texture) == ModelTextureFlags.Texture) && mat.Textures[mat.TextureIndex].TextureID != null)
						{
							// テクスチャありならBindする
							// Bind the texture to be there
							Texture = Texture.FromID(mat.Textures[mat.TextureIndex].TextureID.Value);

							EnableTexturing = true;
							EnableTextureGenerationS = false;
							EnableTextureGenerationT = false;
						}
						else if ((((fTexFlag & ModelTextureFlags.Map) == ModelTextureFlags.Map) || ((fTexFlag & ModelTextureFlags.AddMap) == ModelTextureFlags.AddMap)) && (mat.Textures[mat.TextureIndex].MapID != null))
						{
							// スフィアマップありならBindする
							// Bind sphere map, if it exists
							// Texture = Texture.FromID(mat.MapID.Value);
							Texture = Texture.FromID(mat.Textures[mat.TextureIndex].MapID.Value);

							EnableTexturing = false;
							EnableTextureGenerationS = true;
							EnableTextureGenerationT = true;
						}
						else
						{
							// テクスチャもスフィアマップもなし
							// A texture map sphere without any

							EnableTexturing = false;
							EnableTextureGenerationS = false;
							EnableTextureGenerationT = false;
						}

						if (!mat.AlwaysLight && (mat.EdgeFlag || model.IgnoreEdgeFlag))
						{
							// 輪郭・影有りのときは照明を有効にする
							// Contour - When the shadow is there to enable the lighting
							SetMaterialParameter(FaceName.Both, MaterialParameterName.Diffuse, new float[] { (float)mat.DiffuseColor.R, (float)mat.DiffuseColor.G, (float)mat.DiffuseColor.B, (float)mat.DiffuseColor.A });
							SetMaterialParameter(FaceName.Both, MaterialParameterName.Ambient, new float[] { (float)mat.AmbientColor.R, (float)mat.AmbientColor.G, (float)mat.AmbientColor.B, (float)mat.AmbientColor.A });
							SetMaterialParameter(FaceName.Both, MaterialParameterName.Specular, new float[] { (float)mat.SpecularColor.R, (float)mat.SpecularColor.G, (float)mat.SpecularColor.B, (float)mat.SpecularColor.A });
							SetMaterialParameter(FaceName.Both, MaterialParameterName.Shininess, (float)mat.Shininess);
							EnableLighting = true;
						}
						else
						{
							// 輪郭・影無しのときは照明を無効にする
							// Contour - When you disable the lighting without shadows
							Color = Color.FromRGBASingle((float)((mat.AmbientColor.R + mat.DiffuseColor.R)), (float)((mat.AmbientColor.G + mat.DiffuseColor.G)), (float)((mat.AmbientColor.B + mat.DiffuseColor.B)), (float)((mat.AmbientColor.A + mat.DiffuseColor.A)));

							EnableLighting = false;
						}

						// 頂点インデックスを指定してポリゴン描画
						// Specifies the index vertex polygon drawing

						Begin(RenderMode.Triangles);
						foreach (ModelTriangle tri in mat.Triangles)
						{
							DrawTriangle(tri);
						}
						End();
					}
				}
			}
			else
			{
				EnableLighting = false;
				EnableTexturing = false;

				Color = Color.FromRGBADouble(1, 1, 1, 1);

				for (int i = 0; i < model.Surfaces[0].Vertices.Count; i += 3)
				{
					DrawTriangle(new ModelTriangle(model.Surfaces[0].Vertices[i], model.Surfaces[0].Vertices[i + 1], model.Surfaces[0].Vertices[i + 2]));
				}
			}
			if (RenderBones)
			{
				EnableCulling = false;
				// Internal.OpenGL.Methods.glFrontFace(Internal.OpenGL.Constants.GLFaceOrientation.Clockwise);
				// Internal.OpenGL.Methods.glCullFace(Internal.OpenGL.Constants.GL_BACK);

				EnableTexturing = false;

				foreach (ModelBone bone in model.Bones)
				{
					Matrix.Push();

					float[] ary = bone.Position.ToFloatArray();
					Matrix.Multiply(ary);

					Color = Color.FromRGBADouble(1.0f, 0.0f, 1.0f, 1.0f);
					//glutSolidCube( 0.3f );
					float fSize = 0.3f;
					Begin(RenderMode.Quads);
					DrawVertex(-fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(-fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, -fSize / 2.0f);
					DrawVertex(fSize / 2.0f, fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, fSize / 2.0f);
					DrawVertex(fSize / 2.0f, -fSize / 2.0f, -fSize / 2.0f);
					End();

					Matrix.Pop();
					Matrix.Push();

					Color = Color.FromRGBADouble(1.0f, 1.0f, 1.0f, 1.0f);

					if (bone.ParentBone != null)
					{
						Begin(RenderMode.Lines);
						DrawVertex(bone.ParentBone.Position.ToFloatArray());
						DrawVertex(bone.Position.ToFloatArray());
						End();
					}

					Matrix.Pop();
				}
			}
		}

		protected abstract void SetMaterialParameterInternal(FaceName face, MaterialParameterName parm, float[] value);
		public void SetMaterialParameter(FaceName face, MaterialParameterName parm, float[] value)
		{
			SetMaterialParameterInternal(face, parm, value);
		}
		protected abstract void SetMaterialParameterInternal(FaceName face, MaterialParameterName parm, float value);
		public void SetMaterialParameter(FaceName face, MaterialParameterName parm, float value)
		{
			SetMaterialParameterInternal(face, parm, value);
		}

		public void DrawTriangle(ModelTriangle triangle)
		{
			DrawVertex(triangle.Vertex1);
			DrawVertex(triangle.Vertex2);
			DrawVertex(triangle.Vertex3);
		}

		public void DrawVertex(ModelVertex vertex)
		{
			SetTextureCoordinates(vertex.Texture.U, vertex.Texture.V);
			SetNormalCoordinates(vertex.Normal.X, vertex.Normal.Y, vertex.Normal.Z);
			DrawVertex(vertex.Position.X, vertex.Position.Y, vertex.Position.Z);
		}

		protected abstract void SetNormalCoordinatesInternal(double x, double y, double z);
		public void SetNormalCoordinates(double x, double y, double z)
		{
			SetNormalCoordinatesInternal(x, y, z);
		}

		protected abstract void SetTextureCoordinatesInternal(double u, double v);
		public void SetTextureCoordinates(double u, double v)
		{
			SetTextureCoordinatesInternal(u, v);
		}
		protected abstract void SetTextureCoordinatesInternal(double s, double t, double r);
		public void SetTextureCoordinates(double s, double t, double r)
		{
			SetTextureCoordinatesInternal(s, t, r);
		}
		protected abstract void SetTextureCoordinatesInternal(double s, double t, double r, double q);
		public void SetTextureCoordinates(double s, double t, double r, double q)
		{
			SetTextureCoordinatesInternal(s, t, r, q);
		}

		protected abstract void DrawVertexInternal(double x, double y);
		public void DrawVertex(double x, double y)
		{
			DrawVertexInternal(x, y);
		}
		public void DrawVertex(float x, float y)
		{
			DrawVertexInternal(x, y);
		}
		public void DrawVertex(int x, int y)
		{
			DrawVertexInternal(x, y);
		}
		public void DrawVertex(short x, short y)
		{
			DrawVertexInternal(x, y);
		}
		protected abstract void DrawVertexInternal(double x, double y, double z);
		public void DrawVertex(double x, double y, double z)
		{
			DrawVertexInternal(x, y, z);
		}
		public void DrawVertex(float x, float y, float z)
		{
			DrawVertexInternal(x, y, z);
		}
		public void DrawVertex(float[] values)
		{
			if (values.Length > 3)
			{
				DrawVertexInternal(values[0], values[1], values[2], values[3]);
			}
			else if (values.Length > 2)
			{
				DrawVertexInternal(values[0], values[1], values[2]);
			}
			else if (values.Length > 1)
			{
				DrawVertexInternal(values[0], values[1]);
			}
			throw new NotImplementedException();
		}
		public void DrawVertex(int x, int y, int z)
		{
			DrawVertexInternal(x, y, z);
		}
		public void DrawVertex(short x, short y, short z)
		{
			DrawVertexInternal(x, y, z);
		}
		protected abstract void DrawVertexInternal(double x, double y, double z, double w);
		public void DrawVertex(double x, double y, double z, double w)
		{
			DrawVertexInternal(x, y, z, w);
		}
		public void DrawVertex(float x, float y, float z, float w)
		{
			DrawVertexInternal(x, y, z, w);
		}
		public void DrawVertex(int x, int y, int z, int w)
		{
			DrawVertexInternal(x, y, z, w);
		}
		public void DrawVertex(short x, short y, short z, short w)
		{
			DrawVertexInternal(x, y, z, w);
		}

		private int mvarOpsBegun = 0;
		protected abstract void BeginInternal(RenderMode mode);
		public void Begin(RenderMode mode)
		{
			BeginInternal(mode);
			mvarOpsBegun++;
		}
		protected abstract void EndInternal();
		public void End()
		{
			EndInternal();
			mvarOpsBegun--;
		}

		private Dictionary<PictureObjectModel, Texture> _texturesByPicture = new Dictionary<PictureObjectModel, Texture>();
		private Dictionary<string, Texture> _texturesByFileName = new Dictionary<string, Texture>();
		private Dictionary<uint, Texture> _texturesByID = new Dictionary<uint, Texture>();
		public Texture CreateTextureFromFile(string FileName)
		{
			return CreateTextureFromFile(FileName, TextureRotation.None, TextureFlip.None);
		}
		public Texture CreateTextureFromFile(string FileName, TextureRotation rotation, TextureFlip flip)
		{
			if (_texturesByFileName.ContainsKey(FileName))
			{
				Texture tex = _texturesByFileName[FileName];
				tex.Rotation = rotation;
				tex.Flip = flip;
				return tex;
			}

			uint[] textureIDs = Engine.GenerateTextureIDs(1);
			uint textureID = textureIDs[0];

			Texture texture = new Texture(textureID);
			texture.Target = TextureTarget.Texture2D;

			texture.FileName = FileName;

			texture.MinFilter = TextureFilter.Linear;
			texture.MagFilter = TextureFilter.Linear;
			// texture.TextureWrapR = TextureWrap.Repeat;
			texture.TextureWrapS = TextureWrap.Repeat;
			texture.TextureWrapT = TextureWrap.Repeat;

			texture.Rotation = rotation;
			texture.Flip = flip;

			if (!_texturesByID.ContainsKey(textureID))
			{
				_texturesByID.Add(textureID, texture);
			}
			if (!_texturesByFileName.ContainsKey(FileName))
			{
				_texturesByFileName.Add(FileName, texture);
			}
			return texture;
		}

	}
}
