//
//  RenderBuffer.cs
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
namespace MBS.Framework.Rendering
{
	public abstract class RenderBuffer : IDisposable
	{
		protected abstract void BindInternal(BufferTarget target);
		public void Bind(BufferTarget target)
		{
			BindInternal(target);
		}
		protected abstract void UnbindInternal();
		public void Unbind()
		{
			UnbindInternal();
		}
		protected abstract void DeleteInternal();
		public void Delete()
		{
			DeleteInternal();
		}

		protected abstract void SetDataInternal<T>(T[] data, BufferDataUsage usage);
		public void SetData<T>(T[] data, BufferDataUsage usage)
		{
			SetDataInternal<T>(data, usage);
		}

		protected abstract void SetVertexAttributeInternal(uint index, int count, ElementType type, bool normalized, int stride, uint offset);
		public void SetVertexAttribute(uint index, int count, ElementType type, bool normalized, int stride, uint offset)
		{
			SetVertexAttributeInternal(index, count, type, normalized, stride, offset);
		}

		public void Dispose()
		{
			Dispose(true);
		}
		protected void Dispose(bool disposing)
		{
			Delete();
		}
	}
}
