using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

namespace UniversalEditor.UserInterface
{
	public abstract class Engine
	{
		protected abstract void MainLoop();

		private PropertyListObjectModel mvarConfiguration = new PropertyListObjectModel();
		public PropertyListObjectModel Configuration { get { return mvarConfiguration; } }

		private System.Collections.ObjectModel.ReadOnlyCollection<string> mvarSelectedFileNames = null;
		public System.Collections.ObjectModel.ReadOnlyCollection<string> SelectedFileNames { get { return mvarSelectedFileNames; } }

		private string mvarBasePath = String.Empty;
		public string BasePath { get { return mvarBasePath; } }

		private static string mvarDataPath = null;
		public static string DataPath
		{
			get
			{
				if (mvarDataPath == null)
				{
					mvarDataPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
					{
						Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
						"Mike Becker's Software",
						"Universal Editor"
					});
				}
				return mvarDataPath;
			}
		}

		private IHostApplicationWindow mvarLastWindow = null;
		public IHostApplicationWindow LastWindow { get { return mvarLastWindow; } set { mvarLastWindow = value; } }

		// UniversalDataStorage.Editor.WindowsForms.Program
		private void SingleInstanceManager_Callback(object sender, SingleInstanceManager.InstanceCallbackEventArgs e)
		{
			if (!e.IsFirstInstance)
			{
				if (LastWindow != null)
				{
					string[] FileNames = new string[e.CommandLineArgs.Length - 1];
					for (int i = 1; i < e.CommandLineArgs.Length; i++)
					{
						FileNames[i - 1] = e.CommandLineArgs[i];
					}
					LastWindow.OpenFile(FileNames);
					LastWindow.ActivateWindow();
				}
			}
		}

		protected virtual void InitializeBranding()
		{

		}

		private RecentFileManager mvarRecentFileManager = new RecentFileManager();
		public RecentFileManager RecentFileManager { get { return mvarRecentFileManager; } }

		private BookmarksManager mvarBookmarksManager = new BookmarksManager();
		public BookmarksManager BookmarksManager { get { return mvarBookmarksManager; } }

		private SessionManager mvarSessionManager = new SessionManager();
		public SessionManager SessionManager { get { return mvarSessionManager; } set { mvarSessionManager = value; } }

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

			// Initialize Recent File Manager
			mvarRecentFileManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "RecentItems.xml";
			mvarRecentFileManager.Load();

			// Initialize Bookmarks Manager
			mvarBookmarksManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Bookmarks.xml";
			mvarBookmarksManager.Load();

			// Initialize Session Manager
			mvarSessionManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Sessions.xml";
			mvarSessionManager.Load();

			string INSTANCEID = GetType().FullName + "$2d429aa3371c421fb63b42525e51a50c$92751853175891031214292357218181357901238$";
			if (Configuration.GetValue<bool>("SingleInstanceUniquePerDirectory", true))
			{
				// The single instance should be unique per directory
				INSTANCEID += System.Reflection.Assembly.GetEntryAssembly().Location;
			}
			if (!SingleInstanceManager.CreateSingleInstance(INSTANCEID, new EventHandler<SingleInstanceManager.InstanceCallbackEventArgs>(SingleInstanceManager_Callback))) return;

			MainLoop();
		}
	}
}
