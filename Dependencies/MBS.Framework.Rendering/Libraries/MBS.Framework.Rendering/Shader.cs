//
//  Shader.cs
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
	public class Shader : IDisposable
	{
		public class ShaderCollection
			: System.Collections.ObjectModel.Collection<Shader>
		{
		}

		public uint Handle { get; private set; }
		public ShaderType ShaderType { get; private set; }
		public Engine Engine { get; private set; }

		internal Shader(Engine engine, ShaderType type)
		{
			Engine = engine;
			ShaderType = type;
		}

		public void LoadCode(string code)
		{
			Engine.LoadShader(this, code);
		}
		public void LoadFile(string fileName)
		{
			string text = System.IO.File.ReadAllText(fileName);
			LoadCode(text);
		}

		public void Compile()
		{
			Engine.CompileShader(this);
		}

		private bool disposed = false;

		public void Dispose()
		{
			// Dispose of unmanaged resources.
			Dispose(true);
			// Suppress finalization.
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (disposed)
				return;

			// free unmanaged resources regardless of 'disposing'

			// delete the shaders as they're linked into our program now and no longer necessery
			Engine.DeleteShader(this);

			// free managed resources only if 'disposing' == true
			if (disposing)
			{

			}

			disposed = true;
		}

		~Shader()
		{
			Dispose(false);
		}
	}
}
