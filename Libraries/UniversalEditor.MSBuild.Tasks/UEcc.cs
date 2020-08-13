//
//  UEcc.cs - The Universal Editor Cross-Platform Extension Compiler
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
using System;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Tasks;
using Microsoft.Build.Utilities;

namespace UniversalEditor.MSBuild.Tasks
{
	public class UEcc : CompilerBase
	{
		/*
		public IBuildEngine BuildEngine { get; set; }
		public ITaskHost HostObject { get; set; }

		public bool Execute()
		{
			string exefile = System.Reflection.Assembly.GetExecutingAssembly().Location;
			string exepath = System.IO.Path.GetDirectoryName(exefile);
			string path = System.IO.Path.Combine(new string[]
			{
				exepath,
				"mcc.exe"
			});

			bool failed = false;

			BuildEngine.LogMessageEvent(new BuildMessageEventArgs(String.Format("launching mcc at {0}", path), string.Empty, string.Empty, MessageImportance.Normal));

			System.Diagnostics.Process p = System.Diagnostics.Process.Start(path, args.ToString());
			p.WaitForExit();

			return !failed;
		}
		*/

		protected override string ToolName => "uecc.exe";
	}
}
