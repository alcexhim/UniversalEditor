//
//  OpenGLEngine.cs
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
using System.Runtime.InteropServices;
using MBS.Framework.Rendering.Engines.OpenGL.Internal.OpenGL;

namespace MBS.Framework.Rendering.Engines.OpenGL
{
	public class OpenGLEngine : Engine
	{

		protected override Canvas CreateCanvasInternal()
		{
			return new OpenGLCanvas(this);
		}

		protected override void BindTextureInternal(TextureTarget target, uint id)
		{
			Internal.OpenGL.Methods.glBindTexture(TextureTargetToGLTextureTarget(target), id);
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override void SetTextureParameterInternal(TextureParameterTarget target, TextureParameterName name, float value)
		{
			Internal.OpenGL.Methods.glTexParameter(TextureParameterTargetToGLTextureParameterTarget(target), TextureParameterNameToGLTextureParameterName(name), (int)value);
			Internal.OpenGL.Methods.glErrorToException();
		}

		internal static Constants.TextureParameterName TextureParameterNameToGLTextureParameterName(TextureParameterName name)
		{
			switch (name)
			{
				case TextureParameterName.DepthTextureMode: return Constants.TextureParameterName.DepthTextureMode;
				case TextureParameterName.GenerateMipmap: return Constants.TextureParameterName.GenerateMipmap;
				case TextureParameterName.MaximumFilter: return Constants.TextureParameterName.MaximumFilter;
				case TextureParameterName.MaximumLOD: return Constants.TextureParameterName.MaximumLOD;
				case TextureParameterName.MinimumFilter: return Constants.TextureParameterName.MinimumFilter;
				case TextureParameterName.MinimumLOD: return Constants.TextureParameterName.MinimumLOD;
				case TextureParameterName.TextureBaseLevel: return Constants.TextureParameterName.TextureBaseLevel;
				case TextureParameterName.TextureCompareFunc: return Constants.TextureParameterName.TextureCompareFunc;
				case TextureParameterName.TextureCompareMode: return Constants.TextureParameterName.TextureCompareMode;
				case TextureParameterName.TextureMaxLevel: return Constants.TextureParameterName.TextureMaxLevel;
				case TextureParameterName.TexturePriority: return Constants.TextureParameterName.TexturePriority;
				case TextureParameterName.TextureWrapR: return Constants.TextureParameterName.TextureWrapR;
				case TextureParameterName.TextureWrapS: return Constants.TextureParameterName.TextureWrapS;
				case TextureParameterName.TextureWrapT: return Constants.TextureParameterName.TextureWrapT;
			}
			throw new InvalidEnumerationException();
		}

		protected override uint[] GenerateTextureIDsInternal(int count)
		{
			uint[] textureIDs = new uint[count];
			Internal.OpenGL.Methods.glGenTextures(count, textureIDs);

			Internal.OpenGL.Methods.glErrorToException();
			return textureIDs;
		}

		internal static Constants.TextureTarget TextureTargetToGLTextureTarget(TextureTarget target)
		{
			switch (target)
			{
				case TextureTarget.CubeMapNegativeX: return Constants.TextureTarget.CubeMapNegativeX;
				case TextureTarget.CubeMapNegativeY: return Constants.TextureTarget.CubeMapNegativeY;
				case TextureTarget.CubeMapNegativeZ: return Constants.TextureTarget.CubeMapNegativeZ;
				case TextureTarget.CubeMapPositiveX: return Constants.TextureTarget.CubeMapPositiveX;
				case TextureTarget.CubeMapPositiveY: return Constants.TextureTarget.CubeMapPositiveY;
				case TextureTarget.CubeMapPositiveZ: return Constants.TextureTarget.CubeMapPositiveZ;
				case TextureTarget.ProxyTexture2D: return Constants.TextureTarget.ProxyTexture2D;
				case TextureTarget.Texture2D: return Constants.TextureTarget.Texture2D;
			}
			throw new InvalidEnumerationException();
		}

		internal static Constants.TextureParameterTarget TextureParameterTargetToGLTextureParameterTarget(TextureParameterTarget target)
		{
			switch (target)
			{
				case TextureParameterTarget.Texture1D: return Constants.TextureParameterTarget.Texture1D;
				case TextureParameterTarget.Texture2D: return Constants.TextureParameterTarget.Texture2D;
				case TextureParameterTarget.Texture3D: return Constants.TextureParameterTarget.Texture3D;
				case TextureParameterTarget.TextureCubeMap: return Constants.TextureParameterTarget.TextureCubeMap;
			}
			throw new InvalidEnumerationException();
		}

		internal static Constants.MatrixMode MatrixModeToGLMatrixMode(MatrixMode mode)
		{
			switch (mode)
			{
				case MatrixMode.Color: return Constants.MatrixMode.Color;
				case MatrixMode.ModelView: return Constants.MatrixMode.ModelView;
				case MatrixMode.Projection: return Constants.MatrixMode.Projection;
				case MatrixMode.Texture: return Constants.MatrixMode.Texture;
			}
			throw new InvalidEnumerationException();
		}

		internal static int MaterialParameterNameToGLConst(MaterialParameterName parm)
		{
			switch (parm)
			{
				case MaterialParameterName.Ambient: return Constants.GL_AMBIENT;
				case MaterialParameterName.Diffuse: return Constants.GL_DIFFUSE;
				case MaterialParameterName.Shininess: return Constants.GL_SHININESS;
				case MaterialParameterName.Specular: return Constants.GL_SPECULAR;
			}
			throw new InvalidEnumerationException();
		}

		internal static Constants.GLFace FaceToGLFace(FaceName face)
		{
			switch (face)
			{
				case FaceName.Front: return Constants.GLFace.Front;
				case FaceName.Back: return Constants.GLFace.Back;
				case FaceName.Both: return Constants.GLFace.Both;
			}
			throw new InvalidEnumerationException();
		}


		public Dictionary<ShaderProgram, uint> _ProgramHandles { get; private set; } = new Dictionary<ShaderProgram, uint>();
		public Dictionary<uint, ShaderProgram> _HandlePrograms { get; private set; } = new Dictionary<uint, ShaderProgram>();

		internal uint CreateProgram(ShaderProgram sp)
		{
			uint id = Internal.OpenGL.Methods.glCreateProgram();
			Internal.OpenGL.Methods.glErrorToException();

			_ProgramHandles[sp] = id;
			_HandlePrograms[id] = sp;
			return id;
		}
		private bool IsProgramCreated(ShaderProgram sp)
		{
			return _ProgramHandles.ContainsKey(sp);
		}
		protected override void UseProgramInternal(ShaderProgram program)
		{
			if (program == null)
			{
				Internal.OpenGL.Methods.glUseProgram(0);
				return;
			}

			if (!IsProgramCreated(program))
			{
				CreateProgramFull(program);
			}

			Internal.OpenGL.Methods.glUseProgram(_ProgramHandles[program]);
			Internal.OpenGL.Methods.glErrorToException();
		}

		private void CreateProgramFull(ShaderProgram program)
		{
			CreateProgram(program);

			int status = 0;
			for (int i = 0; i < program.Shaders.Count; i++)
			{
				program.Shaders[i].Compile();
				Internal.OpenGL.Methods.glGetShaderiv(_ShaderHandles[program.Shaders[i]], Internal.OpenGL.Constants.GL_COMPILE_STATUS, ref status);

				AttachShaderToProgramInternal(program, program.Shaders[i]);
			}

			LinkProgramInternal(program);

			status = 0;
			Internal.OpenGL.Methods.glGetProgramiv(_ProgramHandles[program], Internal.OpenGL.Constants.GL_LINK_STATUS, ref status);
			bool success = (status == 1);
			if (!success)
			{
				throw new InvalidProgramException();
			}
		}

		internal static Constants.RenderMode RenderModeToGLRenderMode(RenderMode mode)
		{
			switch (mode)
			{
				case RenderMode.LineLoop: return Constants.RenderMode.LineLoop;
				case RenderMode.Lines: return Constants.RenderMode.Lines;
				case RenderMode.LineStrip: return Constants.RenderMode.LineStrip;
				case RenderMode.Points: return Constants.RenderMode.Points;
				case RenderMode.Polygon: return Constants.RenderMode.Polygon;
				case RenderMode.Quads: return Constants.RenderMode.Quads;
				case RenderMode.QuadStrip: return Constants.RenderMode.QuadStrip;
				case RenderMode.TriangleFan: return Constants.RenderMode.TriangleFan;
				case RenderMode.Triangles: return Constants.RenderMode.Triangles;
				case RenderMode.TriangleStrip: return Constants.RenderMode.TriangleStrip;
			}
			throw new InvalidEnumerationException();
		}

		public Dictionary<Shader, uint> _ShaderHandles { get; private set; } = new Dictionary<Shader, uint>();
		public Dictionary<uint, Shader> _HandleShaders { get; private set; } = new Dictionary<uint, Shader>();

		protected override void CreateShaderProgramInternal(ShaderProgram program)
		{
			CreateProgram(program);
		}

		protected override void LinkProgramInternal(ShaderProgram program)
		{
			Internal.OpenGL.Methods.glLinkProgram(_ProgramHandles[program]);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void AttachShaderToProgramInternal(ShaderProgram program, Shader shader)
		{
			Internal.OpenGL.Methods.glAttachShader(_ProgramHandles[program], _ShaderHandles[shader]);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetProgramUniformInternal(ShaderProgram program, string name, int value)
		{
			Internal.OpenGL.Methods.glUniform1i(Internal.OpenGL.Methods.glGetUniformLocation(_ProgramHandles[program], name), value);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetProgramUniformInternal(ShaderProgram program, string name, float value)
		{
			Internal.OpenGL.Methods.glUniform1f(Internal.OpenGL.Methods.glGetUniformLocation(_ProgramHandles[program], name), value);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2)
		{
			Internal.OpenGL.Methods.glUniform2f(Internal.OpenGL.Methods.glGetUniformLocation(_ProgramHandles[program], name), value1, value2);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetProgramUniformInternal(ShaderProgram program, string name, float value1, float value2, float value3)
		{
			Internal.OpenGL.Methods.glUniform3f(Internal.OpenGL.Methods.glGetUniformLocation(_ProgramHandles[program], name), value1, value2, value3);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void SetProgramUniformMatrixInternal(ShaderProgram program, string name, int count, bool transpose, float[] value)
		{
			int loc = Internal.OpenGL.Methods.glGetUniformLocation(_ProgramHandles[program], name);
			Internal.OpenGL.Methods.glUniformMatrix4fv(loc, count, transpose, value);
			Internal.OpenGL.Methods.glErrorToException();
		}

		protected override uint GetProgramAttributeLocationInternal(ShaderProgram program, string name)
		{
			if (!IsProgramCreated(program))
			{
				CreateProgramFull(program);
			}

			uint loc = Internal.OpenGL.Methods.glGetAttribLocation(_ProgramHandles[program], name);
			Internal.OpenGL.Methods.glErrorToException();

			return loc;
		}

		protected override void CreateShaderInternal(Shader shader)
		{
			Constants.GLShaderType underlyingType = Constants.GLShaderType.Vertex;
			switch (shader.ShaderType)
			{
				case ShaderType.Vertex:
				{
					underlyingType = Constants.GLShaderType.Vertex;
					break;
				}
				case ShaderType.Fragment:
				{
					underlyingType = Constants.GLShaderType.Fragment;
					break;
				}
				case ShaderType.Compute:
				{
					underlyingType = Constants.GLShaderType.Compute;
					break;
				}
			}

			uint id = Internal.OpenGL.Methods.glCreateShader(underlyingType);
			Internal.OpenGL.Methods.glErrorToException();

			_ShaderHandles[shader] = id;
			_HandleShaders[id] = shader;
		}
		protected bool IsShaderCreated(Shader shader)
		{
			return _ShaderHandles.ContainsKey(shader);
		}
		protected override void CompileShaderInternal(Shader shader)
		{
			if (!IsShaderCreated(shader))
				CreateShaderInternal(shader);

			Internal.OpenGL.Methods.glCompileShader(_ShaderHandles[shader]);
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void LoadShaderInternal(Shader shader, string code)
		{
			if (!IsShaderCreated(shader))
				CreateShaderInternal(shader);

			Internal.OpenGL.Methods.glShaderSource(_ShaderHandles[shader], 1, new string[] { code }, new int[] { code.Length });
			Internal.OpenGL.Methods.glErrorToException();
		}
		protected override void DeleteShaderInternal(Shader shader)
		{
			if (!IsShaderCreated(shader))
				return; // don't bother creating it if we're just going to delete it anyway

			Internal.OpenGL.Methods.glDeleteShader(_ShaderHandles[shader]);
			Internal.OpenGL.Methods.glErrorToException();

			_HandleShaders.Remove(_ShaderHandles[shader]);
			_ShaderHandles.Remove(shader);
		}

		protected override VertexArray[] CreateVertexArrayInternal(int count)
		{
			uint[] arrays = new uint[count];
			Internal.OpenGL.Methods.glGenVertexArrays(count, arrays);
			Internal.OpenGL.Methods.glErrorToException();

			List<VertexArray> list = new List<VertexArray>();
			for (int i = 0; i < count; i++)
			{
				list.Add(new OpenGLVertexArray(arrays[i]));
			}
			return list.ToArray();
		}
		protected override void DeleteVertexArrayInternal(VertexArray[] arrays)
		{
			uint[] handles = new uint[arrays.Length];
			for (int i = 0; i < arrays.Length; i++)
			{
				handles[i] = (arrays[i] as OpenGLVertexArray).Handle;
			}
			Internal.OpenGL.Methods.glDeleteVertexArrays(arrays.Length, handles);
		}

		protected override RenderBuffer[] CreateBuffersInternal(int count)
		{
			uint[] arrays = new uint[count];
			Internal.OpenGL.Methods.glGenBuffers(count, arrays);
			Internal.OpenGL.Methods.glErrorToException();

			List<RenderBuffer> list = new List<RenderBuffer>();
			for (int i = 0; i < count; i++)
			{
				list.Add(new OpenGLRenderBuffer(arrays[i]));
			}
			return list.ToArray();
		}

		internal static Constants.GLBindBufferTarget BufferTargetToGLBufferTarget(BufferTarget target)
		{
			switch (target)
			{
				case BufferTarget.ArrayBuffer: return Constants.GLBindBufferTarget.ArrayBuffer;
			}
			throw new InvalidEnumerationException();
		}

		internal static Constants.GLBufferDataUsage BufferDataUsageToGLBufferDataUsage(BufferDataUsage usage)
		{
			switch (usage)
			{
				case BufferDataUsage.DynamicCopy: return Constants.GLBufferDataUsage.DynamicCopy;
				case BufferDataUsage.DynamicDraw: return Constants.GLBufferDataUsage.DynamicDraw;
				case BufferDataUsage.DynamicRead: return Constants.GLBufferDataUsage.DynamicRead;
				case BufferDataUsage.StaticCopy: return Constants.GLBufferDataUsage.StaticCopy;
				case BufferDataUsage.StaticDraw: return Constants.GLBufferDataUsage.StaticDraw;
				case BufferDataUsage.StaticRead: return Constants.GLBufferDataUsage.StaticRead;
				case BufferDataUsage.StreamCopy: return Constants.GLBufferDataUsage.StreamCopy;
				case BufferDataUsage.StreamDraw: return Constants.GLBufferDataUsage.StreamDraw;
				case BufferDataUsage.StreamRead: return Constants.GLBufferDataUsage.StreamRead;
			}
			throw new InvalidEnumerationException();
		}

		internal static Constants.GLElementType ElementTypeToGLElementType(ElementType type)
		{
			switch (type)
			{
				case ElementType.Byte: return Constants.GLElementType.Byte;
				case ElementType.Double: return Constants.GLElementType.Double;
				case ElementType.Float: return Constants.GLElementType.Float;
				case ElementType.FourBytes: return Constants.GLElementType.FourBytes;
				case ElementType.Int: return Constants.GLElementType.Int;
				case ElementType.Short: return Constants.GLElementType.Short;
				case ElementType.ThreeBytes: return Constants.GLElementType.ThreeBytes;
				case ElementType.TwoBytes: return Constants.GLElementType.TwoBytes;
				case ElementType.UnsignedByte: return Constants.GLElementType.UnsignedByte;
				case ElementType.UnsignedInt: return Constants.GLElementType.UnsignedInt;
				case ElementType.UnsignedShort: return Constants.GLElementType.UnsignedShort;
			}
			throw new InvalidEnumerationException();
		}

		protected override void FlushInternal()
		{
			Internal.OpenGL.Methods.glFlush();
			Internal.OpenGL.Methods.glErrorToException();
		}
	}
}
