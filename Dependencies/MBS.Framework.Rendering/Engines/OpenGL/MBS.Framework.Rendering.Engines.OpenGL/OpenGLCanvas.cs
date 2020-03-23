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
using MBS.Framework.Drawing;
using MBS.Framework.Rendering.Engines.OpenGL.Internal.OpenGL;

namespace MBS.Framework.Rendering.Engines.OpenGL
{
	public class OpenGLCanvas : Canvas
	{
		public Engine Engine { get; private set; } = null;
		protected internal OpenGLCanvas(Engine engine)
			: base(engine)
		{
		}

		private Matrix _Matrix = null;
		protected override Matrix GetMatrixInternal()
		{
			if (_Matrix == null)
			{
				_Matrix = new OpenGLMatrix();
			}
			return _Matrix;
		}

		protected override void ClearInternal(Color black)
		{
			Internal.OpenGL.Methods.glClearColor(black.R, black.G, black.B, black.A);
			Internal.OpenGL.Methods.glErrorToException();

			Internal.OpenGL.Methods.glClear(Constants.GL_COLOR_BUFFER_BIT);
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override void SetTextureCoordinatesInternal(double u, double v)
		{
			Internal.OpenGL.Methods.glTexCoord(u, v);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetTextureCoordinatesInternal(double s, double t, double r)
		{
			Internal.OpenGL.Methods.glTexCoord(s, t, r);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetTextureCoordinatesInternal(double s, double t, double r, double q)
		{
			Internal.OpenGL.Methods.glTexCoord(s, t, r, q);
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override void SetNormalCoordinatesInternal(double x, double y, double z)
		{
			Internal.OpenGL.Methods.glNormal3dv(new double[] { x, y, z });
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override void DrawVertexInternal(double x, double y)
		{
			Internal.OpenGL.Methods.glVertex2d(x, y);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void DrawVertexInternal(double x, double y, double z)
		{
			Internal.OpenGL.Methods.glVertex3d(x, y, z);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void DrawVertexInternal(double x, double y, double z, double w)
		{
			Internal.OpenGL.Methods.glVertex4d(x, y, z, w);
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override void BeginInternal(RenderMode mode)
		{
			Internal.OpenGL.Methods.glBegin((int)mode);
		}
		protected override void EndInternal()
		{
			Internal.OpenGL.Methods.glEnd();
		}

		protected override void SetMaterialParameterInternal(FaceName face, MaterialParameterName parm, float[] value)
		{
			Internal.OpenGL.Methods.glMaterialfv(OpenGLEngine.FaceToGLFace(face), OpenGLEngine.MaterialParameterNameToGLConst(parm), value);
		}
		protected override void SetMaterialParameterInternal(FaceName face, MaterialParameterName parm, float value)
		{
			Internal.OpenGL.Methods.glMaterialf(OpenGLEngine.FaceToGLFace(face), OpenGLEngine.MaterialParameterNameToGLConst(parm), value);
		}

		protected override bool EnableTexturingInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.Texture2D); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.Texture2D);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.Texture2D);
				}
			}
		}
		protected override bool EnableBlendingInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.Blending); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.Blending);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.Blending);
				}
			}
		}

		protected override bool EnableTextureGenerationSInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.TextureGenS); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.TextureGenS);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.TextureGenS);
				}
			}
		}
		protected override bool EnableTextureGenerationTInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.TextureGenT); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.TextureGenT);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.TextureGenT);
				}
			}
		}
		protected override bool EnableTextureGenerationRInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.TextureGenR); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.TextureGenR);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.TextureGenR);
				}
			}
		}
		protected override bool EnableTextureGenerationQInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.TextureGenQ); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.TextureGenQ);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.TextureGenQ);
				}
			}
		}
		protected override bool EnableLightingInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.Lighting); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.Lighting);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.Lighting);
				}
			}
		}
		protected override bool EnableCullingInternal
		{
			get { return Internal.OpenGL.Methods.glIsEnabled(Constants.GLCapabilities.CullFace); }
			set
			{
				if (value)
				{
					Internal.OpenGL.Methods.glEnable(Constants.GLCapabilities.CullFace);
				}
				else
				{
					Internal.OpenGL.Methods.glDisable(Constants.GLCapabilities.CullFace);
				}
			}
		}

		private Color _Color = Color.Empty;
		protected override Color ColorInternal
		{
			get { return _Color; }
			set
			{
				_Color = value;

				Internal.OpenGL.Methods.glColor4d(value.R, value.G, value.B, value.A);
				Internal.OpenGL.Methods.glErrorToException();
			}
		}

		protected override void DrawArraysInternal(RenderMode mode, int start, int count)
		{
			Internal.OpenGL.Methods.glDrawArrays(OpenGLEngine.RenderModeToGLRenderMode(mode), start, count);
			Internal.OpenGL.Methods.glErrorToException();
		}
	}
}
