//
//  ModelEditor.cs - provides a UWT-based Editor for a ModelObjectModel
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2019-2020 Mike Becker's Software
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
using MBS.Framework;
using MBS.Framework.Drawing;
using MBS.Framework.Rendering;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Layouts;

using UniversalEditor.ObjectModels.Multimedia3D.Model;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Plugins.Multimedia3D.UserInterface.Editors.Model
{
	/// <summary>
	/// Provides a UWT-based <see cref="Editor" /> for a <see cref="ModelObjectModel" />.
	/// </summary>
	public class ModelEditor : Editor
	{
		private static EditorReference _er = null;
		public override EditorReference MakeReference()
		{
			if (_er == null)
			{
				_er = base.MakeReference();
				_er.SupportedObjectModels.Add(typeof(ModelObjectModel));
			}
			return _er;
		}

		public override void UpdateSelections()
		{
			throw new NotImplementedException();
		}

		protected override Selection CreateSelectionInternal(object content)
		{
			throw new NotImplementedException();
		}

		private OpenGLCanvas gla = null;

		public ModelEditor()
		{
			this.Layout = new BoxLayout(Orientation.Vertical);

			gla = new OpenGLCanvas();
			gla.Realize += gla_Realize;
			gla.Render += gla_Render;
			this.Controls.Add(gla, new BoxLayout.Constraints(true, true));
		}

		private ShaderProgram p = null;

		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
		private struct VERTEX
		{
			public float PositionX;
			public float PositionY;
			public float PositionZ;

			public float NormalX;
			public float NormalY;
			public float NormalZ;

			public float ColorR;
			public float ColorG;
			public float ColorB;

			public float TextureU;
			public float TextureV;

			public VERTEX(float x, float y, float z, float nx, float ny, float nz, float r, float g, float b, float u, float v)
			{
				PositionX = x;
				PositionY = y;
				PositionZ = z;
				NormalX = nx;
				NormalY = ny;
				NormalZ = nz;
				ColorR = r;
				ColorG = g;
				ColorB = b;
				TextureU = u;
				TextureV = v;
			}
		}

		private static VERTEX[] vertex_data = new VERTEX[]
		{
			//          x   ,  y      , z       nx,		ny,	nz			   ,  r,  g ,  b ,           u,    v
			new VERTEX(0.0f,  0.500f, 0.0f,     0.0f, 0.0f, 0.0f,          1.0f, 0.0f, 0.0f,         0.0f, 0.0f),
			new VERTEX(0.5f, -0.366f, 0.0f,     0.0f, 0.0f, 0.0f,           0.0f, 1.0f, 0.0f,         0.0f, 0.0f),
			new VERTEX(-0.5f, -0.366f, 0.0f,    0.0f, 0.0f, 0.0f,           0.0f, 0.0f, 1.0f,         0.0f, 0.0f)
		};

		VertexArray[] vaos = null;

		/// <summary>
		/// Computes the modelview projection
		/// </summary>
		/// <param name="matrix">the matrix to update.</param>
		/// <param name="angleX">Angle X axis (phi).</param>
		/// <param name="angleY">Angle Y axis (theta).</param>
		/// <param name="angleZ">Angle Z axis (psi).</param>
		static void compute_mvp(ref float[] matrix, float angleX, float angleY, float angleZ)
		{
			// holy balls
			float x = (float)(angleX * (Math.PI / 180.0f));
			float y = (float)(angleY * (Math.PI / 180.0f));
			float z = (float)(angleZ * (Math.PI / 180.0f));
			float c1 = (float)Math.Cos(x), s1 = (float)Math.Sin(x);
			float c2 = (float)Math.Cos(y), s2 = (float)Math.Sin(y);
			float c3 = (float)Math.Cos(z), s3 = (float)Math.Sin(z);
			float c3c2 = c3 * c2;
			float s3c1 = s3 * c1;
			float c3s2s1 = c3 * s2 * s1;
			float s3s1 = s3 * s1;
			float c3s2c1 = c3 * s2 * c1;
			float s3c2 = s3 * c2;
			float c3c1 = c3 * c1;
			float s3s2s1 = s3 * s2 * s1;
			float c3s1 = c3 * s1;
			float s3s2c1 = s3 * s2 * c1;
			float c2s1 = c2 * s1;
			float c2c1 = c2 * c1;

			/* apply all three Euler angles rotations using the three matrices:
			*
			* ⎡  c3 s3 0 ⎤ ⎡ c2  0 -s2 ⎤ ⎡ 1   0  0 ⎤
			* ⎢ -s3 c3 0 ⎥ ⎢  0  1   0 ⎥ ⎢ 0  c1 s1 ⎥
			* ⎣   0  0 1 ⎦ ⎣ s2  0  c2 ⎦ ⎣ 0 -s1 c1 ⎦
			*/
			matrix[0] = c3c2; matrix[4] = s3c1 + c3s2s1; matrix[8] = s3s1 - c3s2c1; matrix[12] = 0.0f;
			matrix[1] = -s3c2; matrix[5] = c3c1 - s3s2s1; matrix[9] = c3s1 + s3s2c1; matrix[13] = 0.0f;
			matrix[2] = s2; matrix[6] = -c2s1; matrix[10] = c2c1; matrix[14] = 0.0f;
			matrix[3] = 0.0f; matrix[7] = 0.0f; matrix[11] = 0.0f; matrix[15] = 1.0f;
		}

		void gla_Realize(object sender, EventArgs e)
		{
			mvp = new float[16];
			mvp[0] = 1.0f; mvp[4] = 0.0f; mvp[8] = 0.0f; mvp[12] = 0.0f;
			mvp[1] = 0.0f; mvp[5] = 1.0f; mvp[9] = 0.0f; mvp[13] = 0.0f;
			mvp[2] = 0.0f; mvp[6] = 0.0f; mvp[10] = 1.0f; mvp[14] = 0.0f;
			mvp[3] = 0.0f; mvp[7] = 0.0f; mvp[11] = 0.0f; mvp[15] = 1.0f;

			compute_mvp(ref mvp, 0.0f, 0.0f, 0.0f);
		}

		float[] mvp = null;

		private bool fatalError = false; // stop repeating ourselves if we can't find the shader first time around

		void gla_Render(object sender, OpenGLCanvasRenderEventArgs e)
		{
			e.Canvas.Clear(Colors.Gray);

			if (p == null && !fatalError)
			{
				p = gla.Engine.CreateShaderProgram();

				string vtxFileName = ((UIApplication)Application.Instance).ExpandRelativePath("~/Editors/Multimedia3D/Model/Shaders/Default/default_vtx.glsl");
				if (!System.IO.File.Exists(vtxFileName))
				{
					MessageDialog.ShowDialog(String.Format("Vertex shader not found . The rendering will be unavailable.  Check to ensure the file exists and is readable .\n\n{0}", vtxFileName), "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					fatalError = true;
					p = null;
					return;
				}

				Shader vtx = gla.Engine.CreateShaderFromFile(ShaderType.Vertex, vtxFileName);
				vtx.Compile();

				string frgFileName = ((UIApplication)Application.Instance).ExpandRelativePath("~/Editors/Multimedia3D/Model/Shaders/Default/default_frg.glsl");
				if (!System.IO.File.Exists(vtxFileName))
				{
					MessageDialog.ShowDialog(String.Format("Fragment shader not found . The rendering will be unavailable.  Check to ensure the file exists and is readable .\n\n{0}", frgFileName), "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
					fatalError = true;
					p = null;
					return;
				}

				Shader frg = gla.Engine.CreateShaderFromFile(ShaderType.Fragment, frgFileName);
				frg.Compile();

				p.Shaders.Add(vtx);
				p.Shaders.Add(frg);
				p.Link();
			}

			if (p != null)
			{
				e.Canvas.Program = p;

				/* update the "mvp" matrix we use in the shader */
				p.SetUniformMatrix("mvp", 1, false, mvp);
			}

			/* use the buffers in the VAO */
			if (vaos != null)
			{
				try
				{
					vaos[0].Bind();

					/* draw the three vertices as a triangle */
					e.Canvas.DrawArrays(RenderMode.Triangles, 0, vertex_data.Length);
				}
				catch (InvalidOperationException ex)
				{
					// we might have to recreate the VAO
					changed = true;
				}
			}

			if (changed)
			{
				ModelObjectModel model = (ObjectModel as ModelObjectModel);
				if (model == null)
					return;

				if (model.Surfaces.Count > 0)
				{
					List<VERTEX> list = new List<VERTEX>();
					for (int i = 0; i < model.Materials.Count; i++)
					{
						for (int j = 0; j < model.Materials[i].Triangles.Count; j += 3)
						{
							VERTEX v1 = new VERTEX(), v2 = new VERTEX(), v3 = new VERTEX();
							v1.PositionX = (float)model.Materials[i].Triangles[j].Vertex1.Position.X;
							v1.PositionY = (float)model.Materials[i].Triangles[j].Vertex1.Position.Y;
							v1.PositionZ = (float)model.Materials[i].Triangles[j].Vertex1.Position.Z;

							v1.NormalX = (float)model.Materials[i].Triangles[j].Vertex1.Normal.X;
							v1.NormalY = (float)model.Materials[i].Triangles[j].Vertex1.Normal.Y;
							v1.NormalZ = (float)model.Materials[i].Triangles[j].Vertex1.Normal.Z;

							v1.TextureU = (float)model.Materials[i].Triangles[j].Vertex1.Texture.U;
							v1.TextureV = (float)model.Materials[i].Triangles[j].Vertex1.Texture.V;

							v2.PositionX = (float)model.Materials[i].Triangles[j].Vertex2.Position.X;
							v2.PositionY = (float)model.Materials[i].Triangles[j].Vertex2.Position.Y;
							v2.PositionZ = (float)model.Materials[i].Triangles[j].Vertex2.Position.Z;

							v2.NormalX = (float)model.Materials[i].Triangles[j].Vertex2.Normal.X;
							v2.NormalY = (float)model.Materials[i].Triangles[j].Vertex2.Normal.Y;
							v2.NormalZ = (float)model.Materials[i].Triangles[j].Vertex2.Normal.Z;

							v2.TextureU = (float)model.Materials[i].Triangles[j].Vertex2.Texture.U;
							v2.TextureV = (float)model.Materials[i].Triangles[j].Vertex2.Texture.V;

							v3.PositionX = (float)model.Materials[i].Triangles[j].Vertex3.Position.X;
							v3.PositionY = (float)model.Materials[i].Triangles[j].Vertex3.Position.Y;
							v3.PositionZ = (float)model.Materials[i].Triangles[j].Vertex3.Position.Z;

							v3.NormalX = (float)model.Materials[i].Triangles[j].Vertex3.Normal.X;
							v3.NormalY = (float)model.Materials[i].Triangles[j].Vertex3.Normal.Y;
							v3.NormalZ = (float)model.Materials[i].Triangles[j].Vertex3.Normal.Z;

							v3.TextureU = (float)model.Materials[i].Triangles[j].Vertex3.Texture.U;
							v3.TextureV = (float)model.Materials[i].Triangles[j].Vertex3.Texture.V;

							list.AddRange(new VERTEX[] { v1, v2, v3 });
						}
					}
					vertex_data = list.ToArray();
				}

				if (vaos != null)
				{
					gla.Engine.DeleteVertexArray(vaos);
				}

				// we need to create a VAO to store the other buffers
				vaos = gla.Engine.CreateVertexArray(1);

				// this is the VBO that holds the vertex data
				using (RenderBuffer buffer = gla.Engine.CreateBuffer())
				{
					vaos[0].Bind();

					buffer.Bind(BufferTarget.ArrayBuffer);
					buffer.SetData(vertex_data, BufferDataUsage.StaticDraw);

					if (p != null)
					{
						// enable and set the position attribute
						buffer.SetVertexAttribute(p.GetAttributeLocation("position"), 3, ElementType.Float, false, 11 * 4, 0);

						// enable and set the normal attribute
						// buffer.SetVertexAttribute(p.GetAttributeLocation("normal"), 3, ElementType.Float, false, 11 * 4, 3 * 4);

						// enable and set the color attribute
						buffer.SetVertexAttribute(p.GetAttributeLocation("color"), 3, ElementType.Float, false, 11 * 4, 6 * 4);
					}

					// reset the state; we will re-enable the VAO when needed
					buffer.Unbind();

					vaos[0].Unbind(); // must be called BEFORE the buffer gets disposed
				}
				changed = false;
			}

			// we finished using the buffers and program
			// e.Canvas.Engine.BindVertexArray(0);
			// e.Canvas.Program = null;
		}

		bool changed = false;
		protected override void OnObjectModelChanged(EventArgs e)
		{
			base.OnObjectModelChanged(e);

			changed = true;
			Refresh();
		}
	}
}
