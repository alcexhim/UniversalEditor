//
//  BootstrapOperatingSystem.cs - provides operating system-specific information for a Microsoft ACME setup bootstrapper
//
//  Author:
//       Michael Becker <alcexhim@gmail.com>
//
//  Copyright (c) 2011-2020 Mike Becker's Software
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

namespace UniversalEditor.ObjectModels.Setup.Microsoft.ACME.BootstrapScript
{
	/// <summary>
	/// Provides operating system-specific information for a Microsoft ACME setup bootstrapper.
	/// </summary>
	public class BootstrapOperatingSystem : ICloneable
	{
		public class BootstrapOperatingSystemCollection
			: System.Collections.ObjectModel.Collection<BootstrapOperatingSystem>
		{
			public BootstrapOperatingSystem Add(string name)
			{
				BootstrapOperatingSystem item = new BootstrapOperatingSystem();
				item.Name = name;
				Add(item);
				return item;
			}

			public BootstrapOperatingSystem AddOrRetrieve(string name)
			{
				if (Contains(name)) return this[name];
				return Add(name);
			}

			public bool Contains(string name)
			{
				return (this[name] != null);
			}

			public BootstrapOperatingSystem this[string name]
			{
				get
				{
					foreach (BootstrapOperatingSystem item in this)
					{
						if (item.Name == name) return item;
					}
					return null;
				}
			}
		}

		private string mvarName = String.Empty;
		public string Name { get { return mvarName; } set { mvarName = value; } }

		private bool mvarEnabled = true;
		public bool Enabled { get { return mvarEnabled; } set { mvarEnabled = value; } }

		private string mvarWindowTitle = "Your Application Setup";
		/// <summary>
		/// The title of the setup initialization dialog.
		/// </summary>
		public string WindowTitle { get { return Parameters.ContainsKey("WndTitle") ? Parameters["WndTitle"]  : mvarWindowTitle; } set { mvarWindowTitle = value; Parameters["WndTitle"] = value; } }

		private string mvarWindowMessage = "Initializing Setup...";
		/// <summary>
		/// The message to display inside the setup initialization dialog.
		/// </summary>
		public string WindowMessage { get { return Parameters.ContainsKey("WndMess") ? Parameters["WndMess"] : mvarWindowMessage; } set { mvarWindowMessage = value; Parameters["WndMess"] = value; } }

		private int mvarTemporaryDirectorySize = 3200;
		/// <summary>
		/// The size of the directory in which to place the bootstrapped files.
		/// </summary>
		public int TemporaryDirectorySize { get { return Parameters.ContainsKey("TmpDirSize") ? Int32.Parse(Parameters["TmpDirSize"]) : mvarTemporaryDirectorySize; } set { mvarTemporaryDirectorySize = value; Parameters["TmpDirSize"] = value.ToString(); } }

		private string mvarTemporaryDirectoryName = "~msstfqf.t";
		/// <summary>
		/// The name of the directory in which to place the bootstrapped files.
		/// </summary>
		public string TemporaryDirectoryName { get { return Parameters.ContainsKey("TmpDirName") ? Parameters["TmpDirName"] : mvarTemporaryDirectoryName; } set { mvarTemporaryDirectoryName = value; Parameters["TmpDirName"] = value; } }

		private string mvarCommandLine = String.Empty;
		/// <summary>
		/// The command to execute after bootstrapping, including any arguments.
		/// </summary>
		public string CommandLine { get { return Parameters.ContainsKey("CmdLine") ? Parameters["CmdLine"] : mvarCommandLine; } set { mvarCommandLine = value; Parameters["CmdLine"] = value; } }

		private string mvarWindowClassName = "Stuff-Shell";
		/// <summary>
		/// The name of the window class to register for the setup initialization dialog.
		/// </summary>
		public string WindowClassName { get { return Parameters.ContainsKey("DrvWinClass") ? Parameters["DrvWinClass"] : mvarWindowClassName; } set { mvarWindowClassName = value; Parameters["DrvWinClass"] = value; } }

		private string mvarRequire31Message = "This application requires a newer version of Microsoft Windows.";
		/// <summary>
		/// The message to display when a newer version of Microsoft Windows is required.
		/// </summary>
		public string Require31Message { get { return Parameters.ContainsKey("Require31") ? Parameters["Require31"] : mvarRequire31Message; } set { mvarRequire31Message = value; Parameters["Require31"] = value; } }

		private bool mvarRequire31Enabled = false;
		/// <summary>
		/// Determines whether a newer version of Microsoft Windows is required.
		/// </summary>
		public bool Require31Enabled { get { return mvarRequire31Enabled; } set { mvarRequire31Enabled = value; } }

		private string mvarDriverModuleName = null;
		public string DriverModuleName { get { return Parameters.ContainsKey("DrvModName") ? Parameters["DrvModName"] : mvarDriverModuleName; } set { mvarDriverModuleName = value; Parameters["DrvModName"] = value; } }

		private BootstrapFile.BootstrapFileCollection mvarFiles = new BootstrapFile.BootstrapFileCollection();
		public BootstrapFile.BootstrapFileCollection Files { get { return mvarFiles; } }

		public object Clone()
		{
			BootstrapOperatingSystem clone = new BootstrapOperatingSystem();
			foreach (KeyValuePair<string, string> kvp in Parameters)
			{
				clone.Parameters.Add(kvp.Key, kvp.Value);
			}
			foreach (BootstrapFile file in mvarFiles)
			{
				clone.Files.Add(file.Clone() as BootstrapFile);
			}
			return clone;
		}

		private static BootstrapOperatingSystem mvarPlatformIndependent = new BootstrapOperatingSystem();
		/// <summary>
		/// Gets the platform-independent <see cref="BootstrapOperatingSystem" /> definition.
		/// </summary>
		public static BootstrapOperatingSystem PlatformIndependent { get { return mvarPlatformIndependent; } }

		public Dictionary<string, string> Parameters { get; } = new Dictionary<string, string>();

		public override string ToString()
		{
			return mvarName;
		}

		static BootstrapOperatingSystem()
		{
			mvarPlatformIndependent.Name = "(Platform-Independent)";
		}
	}
}
