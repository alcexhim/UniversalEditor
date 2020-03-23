//
//  Engine.cs
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

namespace MBS.Framework.Rendering
{
	public abstract class Engine
	{
		protected abstract Canvas CreateCanvasInternal();
		public Canvas CreateCanvas()
		{
			return CreateCanvasInternal();
		}

		private static Engine[] _Engines = null;
		public static Engine[] Get()
		{
			if (_Engines == null)
			{
				List<Engine> list = new List<Engine>();
				Type[] types = Reflection.GetAvailableTypes(new Type[] { typeof(Engine) });
				for (int i = 0; i < types.Length; i++)
				{
					Engine engine = (Engine)types[i].Assembly.CreateInstance(types[i].FullName);
					list.Add(engine);
				}
				_Engines = list.ToArray();
			}
			return _Engines;
		}

		protected abstract void LoadShaderInternal(Shader shader, string code);
		internal void LoadShader(Shader shader, string code)
		{
			LoadShaderInternal(shader, code);
		}

		protected abstract void CompileShaderInternal(Shader shader);
		internal void CompileShader(Shader shader)
		{
			CompileShaderInternal(shader);
		}

		internal void SetProgramUniform(ShaderProgram program, string name, bool value)
		{
			SetProgramUniform(program, name, (value ? 1 : 0));
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, int value);
		internal void SetProgramUniform(ShaderProgram program, string name, int value)
		{
			SetProgramUniformInternal(program, name, value);
		}

		protected abstract uint GetProgramAttributeLocationInternal(ShaderProgram program, string name);
		internal uint GetProgramAttributeLocation(ShaderProgram program, string name)
		{
			return GetProgramAttributeLocationInternal(program, name);
		}

		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value);

		internal void SetProgramUniformMatrix(ShaderProgram program, string name, int count, bool transpose, float[] value)
		{
			SetProgramUniformMatrixInternal(program, name, count, transpose, value);
		}
		protected abstract void SetProgramUniformMatrixInternal(ShaderProgram program, string name, int count, bool transpose, float[] value);

		protected abstract void DeleteShaderInternal(Shader shader);
		internal void DeleteShader(Shader shader)
		{
			DeleteShaderInternal(shader);
		}

		internal void SetProgramUniform(ShaderProgram program, string name, float value)
		{
			SetProgramUniformInternal(program, name, value);
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2);

		protected abstract VertexArray[] CreateVertexArrayInternal(int count);
		public VertexArray[] CreateVertexArray(int count)
		{
			return CreateVertexArrayInternal(count);
		}
		protected abstract void DeleteVertexArrayInternal(VertexArray[] arrays);
		public void DeleteVertexArray(VertexArray[] arrays)
		{
			DeleteVertexArrayInternal(arrays);
		}

		internal void SetProgramUniform(ShaderProgram program, string name, float value1, float value2)
		{
			SetProgramUniformInternal(program, name, value1, value2);
		}
		protected abstract void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2, float value3);
		internal void SetProgramUniform(ShaderProgram program, string name, float value1, float value2, float value3)
		{
			SetProgramUniformInternal(program, name, value1, value2, value3);
		}

		protected abstract void LinkProgramInternal(ShaderProgram program);
		internal void LinkProgram(ShaderProgram program)
		{
			LinkProgramInternal(program);
		}

		protected abstract RenderBuffer[] CreateBuffersInternal(int count);
		public RenderBuffer[] CreateBuffers(int count)
		{
			return CreateBuffersInternal(count);
		}
		public RenderBuffer CreateBuffer()
		{
			RenderBuffer[] buffers = CreateBuffers(1);
			return buffers[0];
		}

		protected abstract void AttachShaderToProgramInternal(ShaderProgram program, Shader shader);
		internal void AttachShaderToProgram(ShaderProgram program, Shader shader)
		{
			AttachShaderToProgramInternal(program, shader);
		}

		protected abstract void BindTextureInternal(TextureTarget target, uint id);

		protected abstract void SetTextureParameterInternal(TextureParameterTarget target, TextureParameterName name, float value);
		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, float value)
		{
			SetTextureParameterInternal(target, name, value);
		}
		public void SetTextureParameter(TextureParameterTarget target, TextureParameterName name, int value)
		{
			SetTextureParameter(target, name, (float)value);
		}

		protected abstract void CreateShaderProgramInternal(ShaderProgram program);
		public ShaderProgram CreateShaderProgram()
		{
			ShaderProgram program = new ShaderProgram(this);
			CreateShaderProgramInternal(program);
			return program;
		}

		public void BindTexture(TextureTarget target, uint id)
		{
			BindTextureInternal(target, id);
		}

		public static Engine GetDefault()
		{
			Engine[] engines = Get();
			if (engines.Length == 0)
				return null;

			return engines[0];
		}

		protected abstract uint[] GenerateTextureIDsInternal(int count);
		public uint[] GenerateTextureIDs(int count)
		{
			return GenerateTextureIDsInternal(count);
		}

		protected abstract void UseProgramInternal(ShaderProgram program);
		internal void UseProgram(ShaderProgram program)
		{
			UseProgramInternal(program);
		}

		protected abstract void CreateShaderInternal(Shader shader);
		internal void CreateShader(Shader shader)
		{
			CreateShaderInternal(shader);
		}


		public Shader CreateShaderFromString(ShaderType type, string code)
		{
			Shader shader = new Shader(this, type);
			shader.LoadCode(code);
			return shader;
		}

		public Shader CreateShaderFromFile(ShaderType type, string fileName)
		{
			Shader shader = new Shader(this, type);
			shader.LoadFile(fileName);
			return shader;
		}

		protected abstract void FlushInternal();
		public void Flush()
		{
			FlushInternal();
		}


	}
}
