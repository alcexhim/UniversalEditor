//
//  ShaderProgram.cs
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
	public class ShaderProgram
	{
		public Engine Engine { get; private set; }
		internal ShaderProgram(Engine engine)
		{
			Engine = engine;
		}

		public Shader.ShaderCollection Shaders { get; } = new Shader.ShaderCollection();

		private bool _BeenLinked = false;
		public void Link()
		{
			for (int i = 0; i < Shaders.Count; i++)
			{
				Engine.AttachShaderToProgram(this, Shaders[i]);
			}
			Engine.LinkProgram(this);
			_BeenLinked = true;
		}

		public void Use()
		{
			if (!_BeenLinked)
				Link();
			Engine.UseProgram(this);
		}

		
		public void SetUniform(string name, bool value)
		{
			Engine.SetProgramUniform(this, name, value);
		}
		public void SetUniform(string name, int value)
		{
			Engine.SetProgramUniform(this, name, value);
		}
		public void SetUniform(string name, float v0)
		{
			Engine.SetProgramUniform(this, name, v0);
		}
		public void SetUniform(string name, float v0, float v1)
		{
			Engine.SetProgramUniform(this, name, v0, v1);
		}
		public void SetUniform(string name, float v0, float v1, float v2)
		{
			Engine.SetProgramUniform(this, name, v0, v1, v2);
		}

		public void SetUniformMatrix(string name, int count, bool transpose, float[] value)
		{
			Engine.SetProgramUniformMatrix(this, name, count, transpose, value);
		}

		public uint GetAttributeLocation(string name)
		{
			return Engine.GetProgramAttributeLocation(this, name);
		}
	}
}
