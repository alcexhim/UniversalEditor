using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface
{
	public abstract class Engine
	{
		protected abstract void MainLoop();

		private System.Collections.ObjectModel.ReadOnlyCollection<string> mvarSelectedFileNames = null;
		public System.Collections.ObjectModel.ReadOnlyCollection<string> SelectedFileNames { get { return mvarSelectedFileNames; } }

		private string mvarBasePath = String.Empty;
		public string BasePath { get { return mvarBasePath; } }

		protected virtual void InitializeBranding()
		{

		}

		public void StartApplication()
		{
			string[] args1 = Environment.GetCommandLineArgs();
			string[] args = new string[args1.Length - 1];
			Array.Copy(args1, 1, args, 0, args.Length);

			System.Collections.ObjectModel.Collection<string> selectedFileNames = new System.Collections.ObjectModel.Collection<string>();
			foreach (string commandLineArgument in args)
			{
				selectedFileNames.Add(commandLineArgument);
			}
			mvarSelectedFileNames = new System.Collections.ObjectModel.ReadOnlyCollection<string>(selectedFileNames);

			// Set up the base path for the current application. Should this be able to be
			// overridden with a switch (/basepath:...) ?
			mvarBasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			
			// Initialize the branding for the selected application
			InitializeBranding();

			MainLoop();
		}
	}
}
