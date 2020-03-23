//
//  OpenGLMatrix.cs
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
using MBS.Framework.Rendering.Engines.OpenGL.Internal.OpenGL;

namespace MBS.Framework.Rendering.Engines.OpenGL
{
	public class OpenGLMatrix : Matrix
	{
		protected override void MultiplyInternal(double[] values)
		{
			Internal.OpenGL.Methods.glMultMatrixd(values);
		}

		protected override void MultiplyInternal(float[] values)
		{
			Internal.OpenGL.Methods.glMultMatrixf(values);
		}

		protected override void PopInternal()
		{
			Internal.OpenGL.Methods.glPopMatrix();
		}

		protected override void PushInternal()
		{
			Internal.OpenGL.Methods.glPushMatrix();
		}

		protected override void ResetInternal()
		{
			Internal.OpenGL.Methods.glLoadIdentity();
		}

		protected override void SetMatrixModeInternal(MatrixMode mode)
		{
			Internal.OpenGL.Methods.glMatrixMode(OpenGLEngine.MatrixModeToGLMatrixMode(mode));
		}
	}
}
