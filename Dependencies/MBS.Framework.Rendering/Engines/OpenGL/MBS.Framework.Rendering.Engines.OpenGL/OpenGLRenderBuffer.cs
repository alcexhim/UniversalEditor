//
//  OpenGLRenderBuffer.cs
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
using System.Runtime.InteropServices;

namespace MBS.Framework.Rendering.Engines.OpenGL
{
	public class OpenGLRenderBuffer : RenderBuffer
	{
		public uint Handle { get; private set; }

		private BufferTarget _target = BufferTarget.ArrayBuffer;

		protected override void BindInternal(BufferTarget target)
		{
			Internal.OpenGL.Methods.glBindBuffer(OpenGLEngine.BufferTargetToGLBufferTarget(target), Handle);
			Internal.OpenGL.Methods.glErrorToException();

			_target = target;
		}
		protected override void UnbindInternal()
		{
			Internal.OpenGL.Methods.glBindBuffer(OpenGLEngine.BufferTargetToGLBufferTarget(_target), 0);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void DeleteInternal()
		{
			Internal.OpenGL.Methods.glDeleteBuffers(1, new uint[] { Handle });
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetDataInternal<T>(T[] data, BufferDataUsage usage)
		{
			// ohhh yeahh
			int size = data.Length * Marshal.SizeOf(typeof(T));

			GCHandle ptr = GCHandle.Alloc(data, GCHandleType.Pinned);
			try
			{
				Internal.OpenGL.Methods.glBufferData(OpenGLEngine.BufferTargetToGLBufferTarget(_target), size, ptr.AddrOfPinnedObject(), OpenGLEngine.BufferDataUsageToGLBufferDataUsage(usage));
			}
			finally
			{
				ptr.Free();
			}
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetVertexAttributeInternal(uint index, int count, ElementType type, bool normalized, int stride, uint offset)
		{
			Internal.OpenGL.Methods.glEnableVertexAttribArray(index);
			Internal.OpenGL.Methods.glErrorToException();

			Internal.OpenGL.Methods.glVertexAttribPointer(index, count, OpenGLEngine.ElementTypeToGLElementType(type), normalized, stride, new IntPtr(offset));
			Internal.OpenGL.Methods.glErrorToException();
		}

		internal OpenGLRenderBuffer(uint handle)
		{
			Handle = handle;
		}
	}
}
