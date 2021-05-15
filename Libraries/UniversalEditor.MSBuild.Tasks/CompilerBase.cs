//
//  Compiler.cs
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2020 Mike Becker's Software
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

using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace UniversalEditor.MSBuild.Tasks
{
	public abstract class CompilerBase : ToolTask
	{
		public string OutputAssembly { get; set; }
		public ITaskItem[] Sources { get; set; }

		protected override string GenerateFullPathToTool()
		{
			string exefile = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string exepath = System.IO.Path.GetDirectoryName(exefile);
			string path = System.IO.Path.Combine(new string[]
			{
				exepath,
				ToolName
			});
			return path;
		}

		protected override string GenerateCommandLineCommands()
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("/out:\"");
			sb.Append(OutputAssembly);
			sb.Append("\"");

			sb.Append(' ');

			for (int i = 0; i < Sources.Length; i++)
			{
				sb.Append('"');
				sb.Append(Sources[i].ItemSpec);
				sb.Append('"');
				if (i < Sources.Length - 1)
					sb.Append(' ');
			}

			return sb.ToString();
		}
	}
}
