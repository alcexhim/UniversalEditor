using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.FileSystem;
using UniversalEditor.ObjectModels.PropertyList;

using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.ObjectModels.Markup;

namespace UniversalEditor.UserInterface
{
	public abstract class Engine
	{
		private static Engine[] m_AvailableEngines = null;
		public static Engine[] GetAvailableEngines()
		{
			if (m_AvailableEngines == null)
			{
				string directory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string[] libraries = System.IO.Directory.GetFiles(directory, "*.dll");
				List<Engine> engines = new List<Engine>();
				foreach (string library in libraries)
				{
					try
					{
						Assembly assembly = Assembly.LoadFile(library);
						Type[] types = null;
						try
						{
							types = assembly.GetTypes();
						}
						catch (ReflectionTypeLoadException ex)
						{
							types = ex.Types;
						}
						if (types == null)
						{
							continue;
						}

						foreach (Type type in types)
						{
							if (type.IsSubclassOf(typeof(Engine)))
							{
								Engine engine = (Engine)type.Assembly.CreateInstance(type.FullName);
								engines.Add(engine);
							}
						}
					}
					catch
					{

					}
				}
				m_AvailableEngines = engines.ToArray();
			}
			return m_AvailableEngines;
		}

		public bool AttachCommandEventHandler(string commandID, EventHandler handler)
		{
			Command cmd = Commands[commandID];
			if (cmd != null)
			{
				cmd.Executed += handler;
				return true;
			}
			Console.WriteLine("attempted to attach handler for unknown command '" + commandID + "'");
			return false;
		}

		protected virtual void BeforeInitialization()
		{
		}
		protected virtual void AfterInitialization()
		{
		}

		private void AfterInitializationInternal()
		{
			// Initialize all the commands that are common to UniversalEditor
			#region File
			AttachCommandEventHandler("FileNewDocument", delegate(object sender, EventArgs e)
			{
				LastWindow.NewFile();
			});
			AttachCommandEventHandler("FileNewProject", delegate(object sender, EventArgs e)
			{
				LastWindow.NewProject();
			});
			AttachCommandEventHandler("FileOpenDocument", delegate(object sender, EventArgs e)
			{
				LastWindow.OpenFile();
			});
			AttachCommandEventHandler("FileOpenProject", delegate(object sender, EventArgs e)
			{
				LastWindow.OpenProject();
			});
			AttachCommandEventHandler("FileSaveDocument", delegate(object sender, EventArgs e)
			{
				LastWindow.SaveFile();
			});
			AttachCommandEventHandler("FileSaveDocumentAs", delegate(object sender, EventArgs e)
			{
				LastWindow.SaveFileAs();
			});
			AttachCommandEventHandler("FileSaveProject", delegate(object sender, EventArgs e)
			{
				LastWindow.SaveProject();
			});
			AttachCommandEventHandler("FileSaveProjectAs", delegate(object sender, EventArgs e)
			{
				LastWindow.SaveProjectAs();
			});
			AttachCommandEventHandler("FileSaveAll", delegate(object sender, EventArgs e)
			{
				LastWindow.SaveAll();
			});
			AttachCommandEventHandler("FileCloseDocument", delegate(object sender, EventArgs e)
			{
				LastWindow.CloseFile();
			});
			AttachCommandEventHandler("FileExit", delegate(object sender, EventArgs e)
			{
				ExitApplication();
			});
			#endregion
			#region Tools
			// ToolsOptions should actually be under the Edit menu as "Preferences" on Linux systems
			AttachCommandEventHandler("ToolsOptions", delegate(object sender, EventArgs e)
			{
				LastWindow.ShowOptionsDialog();
			});
			#endregion

			#region Help
			Command helpLanguage = mvarCommands["HelpLanguage"];
			if (helpLanguage != null)
			{
				foreach (Language lang in mvarLanguages)
				{
					Command cmdLanguage = new Command();
					cmdLanguage.ID = "HelpLanguage_" + lang.ID;
					cmdLanguage.Title = lang.Title;
					cmdLanguage.Executed += delegate(object sender, EventArgs e)
					{
						HostApplication.Messages.Add(HostApplicationMessageSeverity.Notice, "Clicked language " + lang.ID);
					};
					mvarCommands.Add(cmdLanguage);

					helpLanguage.Items.Add(new CommandReferenceCommandItem("HelpLanguage_" + lang.ID));
				}
			}
			#endregion
		}

		public virtual void ExitApplication()
		{
		}

		private IHostApplicationWindowCollection mvarWindows = new IHostApplicationWindowCollection();
		public IHostApplicationWindowCollection Windows { get { return mvarWindows; } }

		public void CloseAllWindows()
		{
			List<IHostApplicationWindow> windowsToClose = new List<IHostApplicationWindow>();
			foreach (IHostApplicationWindow window in mvarWindows)
			{
				windowsToClose.Add(window);
			}
			foreach (IHostApplicationWindow window in windowsToClose)
			{
				window.CloseWindow();
			}
		}

		public static bool Execute()
		{
			Engine[] engines = GetAvailableEngines();
			if (engines.Length < 1)
			{
				return false;
			}
			else if (engines.Length == 1)
			{
				engines[0].StartApplication();
			}
			else
			{
				engines[0].StartApplication();
			}
			return true;
		}

		protected abstract void MainLoop();

		private Command.CommandCollection mvarCommands = new Command.CommandCollection();
		/// <summary>
		/// The commands defined for this application.
		/// </summary>
		public Command.CommandCollection Commands { get { return mvarCommands; } }

		private Language mvarDefaultLanguage = null;
		/// <summary>
		/// The default <see cref="Language"/> used to display translatable text in this application.
		/// </summary>
		public Language DefaultLanguage { get { return mvarDefaultLanguage; } set { mvarDefaultLanguage = value; } }

		private Language.LanguageCollection mvarLanguages = new Language.LanguageCollection();
		/// <summary>
		/// The languages defined for this application. Translations can be added through XML files in the ~/Languages folder.
		/// </summary>
		public Language.LanguageCollection Languages { get { return mvarLanguages; } }
		
		private EngineMainMenu mvarMainMenu = new EngineMainMenu();
		/// <summary>
		/// The main menu of this application, which can hold multiple <see cref="CommandItem"/>s.
		/// </summary>
		public EngineMainMenu MainMenu { get { return mvarMainMenu; } }

		private CommandBar.CommandBarCollection mvarCommandBars = new CommandBar.CommandBarCollection();
		/// <summary>
		/// The command bars loaded in this application, which can each hold multiple <see cref="CommandItem"/>s.
		/// </summary>
		public CommandBar.CommandBarCollection CommandBars { get { return mvarCommandBars; } }
		
		/// <summary>
		/// The aggregated raw markup of all the various XML files loaded in the current search path.
		/// </summary>
		private MarkupObjectModel mvarRawMarkup = new MarkupObjectModel();
		
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

		public void OpenFile(params string[] FileNames)
		{
			if (LastWindow == null)
			{
				OpenWindow(FileNames);
				return;
			}
			LastWindow.OpenFile(FileNames);
		}

		/// <summary>
		/// Opens a new window, optionally loading the specified documents.
		/// </summary>
		/// <param name="FileNames">The file name(s) of the document(s) to load.</param>
		/// <returns>An <see cref="IHostApplicationWindow"/> representing the window that was created.</returns>
		protected abstract IHostApplicationWindow OpenWindowInternal(params string[] FileNames);

		/// <summary>
		/// Opens a new window, optionally loading the specified documents.
		/// </summary>
		/// <param name="FileNames">The file name(s) of the document(s) to load.</param>
		public void OpenWindow(params string[] FileNames)
		{
			IHostApplicationWindow window = OpenWindowInternal(FileNames);
			window.WindowClosed += delegate(object sender, EventArgs e)
			{
				mvarWindows.Remove(window);
			};
			mvarWindows.Add(window);
		}

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

		protected virtual void InitializeXMLConfiguration()
		{
			#region Load the XML files
			string[] xmlfiles = System.IO.Directory.GetFiles(mvarBasePath, "*.xml", System.IO.SearchOption.AllDirectories);

			UpdateSplashScreenStatus("Loading XML configuration files", 0, 0, xmlfiles.Length);
			
			XMLDataFormat xdf = new XMLDataFormat();
			foreach (string xmlfile in xmlfiles)
			{
				MarkupObjectModel markup = new MarkupObjectModel();
				Document doc = new Document(markup, xdf, new FileAccessor(xmlfile));
				doc.Accessor.DefaultEncoding = IO.Encoding.UTF8;

				doc.Accessor.Open ();
				doc.Load ();
				doc.Close ();

				markup.CopyTo(mvarRawMarkup);

				UpdateSplashScreenStatus("Loading XML configuration files", Array.IndexOf(xmlfiles, xmlfile) + 1);
			}

			UpdateSplashScreenStatus("Loading available commands");
			
			#endregion

			#region Initialize the configuration with the loaded data
			
			MarkupTagElement tagCommands = (mvarRawMarkup.FindElement ("UniversalEditor", "Application", "Commands") as MarkupTagElement);
			if (tagCommands != null)
			{
				foreach (MarkupElement elCommand in tagCommands.Elements)
				{
					MarkupTagElement tagCommand = (elCommand as MarkupTagElement);
					if (tagCommand == null) continue;
					if (tagCommand.FullName != "Command") continue;
					
					MarkupAttribute attID = tagCommand.Attributes["ID"];
					if (attID == null) continue;
					
					Command cmd = new Command();
					cmd.ID = attID.Value;

					MarkupAttribute attDefaultCommandID = tagCommand.Attributes["DefaultCommandID"];
					if (attDefaultCommandID != null)
					{
						cmd.DefaultCommandID = attDefaultCommandID.Value;
					}
					
					MarkupAttribute attTitle = tagCommand.Attributes["Title"];
					if (attTitle != null)
					{
						cmd.Title = attTitle.Value;
					}
					else
					{
						cmd.Title = cmd.ID;
					}
					
					MarkupTagElement tagItems = (tagCommand.Elements["Items"] as MarkupTagElement);
					if (tagItems != null)
					{
						foreach (MarkupElement el in tagItems.Elements)
						{
							MarkupTagElement tag = (el as MarkupTagElement);
							if (tag == null) continue;
							
							InitializeMainMenuItem(tag, cmd);
						}
					}
					
					mvarCommands.Add (cmd);
				}
			}

			UpdateSplashScreenStatus("Loading main menu items");
			
			MarkupTagElement tagMainMenuItems = (mvarRawMarkup.FindElement ("UniversalEditor", "Application", "MainMenu", "Items") as MarkupTagElement);
			foreach (MarkupElement elItem in tagMainMenuItems.Elements)
			{
				MarkupTagElement tagItem = (elItem as MarkupTagElement);
				if (tagItem == null) continue;
				InitializeMainMenuItem(tagItem, null);
			}

			UpdateSplashScreenStatus("Loading command bars");

			MarkupTagElement tagCommandBars = (mvarRawMarkup.FindElement("UniversalEditor", "Application", "CommandBars") as MarkupTagElement);
			if (tagCommandBars != null)
			{
				foreach (MarkupElement elCommandBar in tagCommandBars.Elements)
				{
					MarkupTagElement tagCommandBar = (elCommandBar as MarkupTagElement);
					if (tagCommandBar == null) continue;
					if (tagCommandBar.FullName != "CommandBar") continue;
					InitializeCommandBar(tagCommandBar);
				}
			}
			
			UpdateSplashScreenStatus("Loading languages and translations");

			MarkupTagElement tagLanguages = (mvarRawMarkup.FindElement("UniversalEditor", "Application", "Languages") as MarkupTagElement);
			foreach (MarkupElement elLanguage in tagLanguages.Elements)
			{
				MarkupTagElement tagLanguage = (elLanguage as MarkupTagElement);
				if (tagLanguage == null) continue;
				if (tagLanguage.FullName != "Language") continue;
				InitializeLanguage(tagLanguage);
			}

			MarkupAttribute attDefaultLanguageID = tagLanguages.Attributes["DefaultLanguageID"];
			if (attDefaultLanguageID != null)
			{
				mvarDefaultLanguage = mvarLanguages[attDefaultLanguageID.Value];
			}
			#endregion

			UpdateSplashScreenStatus("Setting language");

			if (mvarDefaultLanguage != null)
			{
				foreach (Command cmd in mvarCommands)
				{
					cmd.Title = mvarDefaultLanguage.GetCommandTitle(cmd.ID, cmd.ID);
				}
			}

			UpdateSplashScreenStatus("Finalizing configuration");
			ConfigurationManager.Load();
		}

		private void InitializeLanguage(MarkupTagElement tag)
		{
			Language lang = new Language();
			
			MarkupAttribute attID = tag.Attributes["ID"];
			if (attID == null) return;
			lang.ID = attID.Value;

			MarkupAttribute attTitle = tag.Attributes["Title"];
			if (attTitle != null)
			{
				lang.Title = attTitle.Value;
			}
			else
			{
				lang.Title = lang.ID;
			}

			MarkupTagElement tagStringTable = (tag.Elements["StringTable"] as MarkupTagElement);
			if (tagStringTable != null)
			{
				foreach (MarkupElement elStringTableEntry in tagStringTable.Elements)
				{
					MarkupTagElement tagStringTableEntry = (elStringTableEntry as MarkupTagElement);
					if (tagStringTableEntry == null) continue;
					if (tagStringTableEntry.FullName != "StringTableEntry") continue;

					MarkupAttribute attStringTableEntryID = tagStringTableEntry.Attributes["ID"];
					if (attStringTableEntryID == null) continue;

					MarkupAttribute attStringTableEntryValue = tagStringTableEntry.Attributes["Value"];
					if (attStringTableEntryValue == null) continue;

					lang.SetStringTableEntry(attStringTableEntryID.Value, attStringTableEntryValue.Value);
				}
			}

			MarkupTagElement tagCommands = (tag.Elements["Commands"] as MarkupTagElement);
			if (tagCommands != null)
			{
				foreach (MarkupElement elCommand in tagCommands.Elements)
				{
					MarkupTagElement tagCommand = (elCommand as MarkupTagElement);
					if (tagCommand == null) continue;
					if (tagCommand.FullName != "Command") continue;

					MarkupAttribute attCommandID = tagCommand.Attributes["ID"];
					if (attCommandID == null) continue;

					MarkupAttribute attCommandTitle = tagCommand.Attributes["Title"];
					if (attCommandTitle == null) continue;

					lang.SetCommandTitle(attCommandID.Value, attCommandTitle.Value);
				}
			}

			mvarLanguages.Add(lang);
		}
		
		private void InitializeCommandBar(MarkupTagElement tag)
		{
			MarkupAttribute attID = tag.Attributes["ID"];
			if (attID == null) return;

			CommandBar cb = new CommandBar();
			cb.ID = attID.Value;

			MarkupAttribute attTitle = tag.Attributes["Title"];
			if (attTitle != null)
			{
				cb.Title = attTitle.Value;
			}
			else
			{
				cb.Title = cb.ID;
			}

			MarkupTagElement tagItems = tag.Elements["Items"] as MarkupTagElement;
			if (tagItems != null)
			{
				foreach (MarkupElement elItem in tagItems.Elements)
				{
					MarkupTagElement tagItem = (elItem as MarkupTagElement);
					if (tagItem == null) continue;
					switch (tagItem.FullName)
					{
						case "CommandReference":
						{
							MarkupAttribute attCommandID = tagItem.Attributes["CommandID"];
							if (attCommandID != null)
							{
								cb.Items.Add(new CommandReferenceCommandItem(attCommandID.Value));
							}
							break;
						}
						case "Separator":
						{
							cb.Items.Add(new SeparatorCommandItem());
							break;
						}
					}
				}
			}

			mvarCommandBars.Add(cb);
		}

		private void InitializeCommandBarItem(MarkupTagElement tag, CommandBar parent)
		{
			CommandItem item = null;
			switch (tag.FullName)
			{
				case "CommandReference":
				{
					MarkupAttribute attCommandID = tag.Attributes["CommandID"];
					if (attCommandID != null)
					{
						item = new CommandReferenceCommandItem(attCommandID.Value);
					}
					break;
				}
				case "Separator":
				{
					item = new SeparatorCommandItem();
					break;
				}
			}

			if (item != null)
			{
				parent.Items.Add(item);
			}
		}
		private void InitializeMainMenuItem(MarkupTagElement tag, Command parent)
		{
			CommandItem item = null;
			switch (tag.FullName)
			{
				case "CommandReference":
				{
					MarkupAttribute attCommandID = tag.Attributes["CommandID"];
					if (attCommandID != null)
					{
						item = new CommandReferenceCommandItem(attCommandID.Value);
					}
					break;
				}
				case "Separator":
				{
					item = new SeparatorCommandItem();
					break;
				}
			}
			
			if (item != null)
			{
				if (parent == null)
				{
					mvarMainMenu.Items.Add(item);
				}
				else
				{
					parent.Items.Add(item);
				}
			}
		}
		
		protected virtual void InitializeBranding()
		{

		}

		private ConfigurationManager mvarConfigurationManager = new ConfigurationManager();
		public ConfigurationManager ConfigurationManager { get { return mvarConfigurationManager; } }

		private RecentFileManager mvarRecentFileManager = new RecentFileManager();
		public RecentFileManager RecentFileManager { get { return mvarRecentFileManager; } }

		private BookmarksManager mvarBookmarksManager = new BookmarksManager();
		public BookmarksManager BookmarksManager { get { return mvarBookmarksManager; } }

		private SessionManager mvarSessionManager = new SessionManager();
		public SessionManager SessionManager { get { return mvarSessionManager; } set { mvarSessionManager = value; } }

		private static Engine mvarCurrentEngine = null;
		public static Engine CurrentEngine { get { return mvarCurrentEngine; } }

		protected virtual void ShowSplashScreen()
		{
		}
		protected virtual void HideSplashScreen()
		{
		}
		protected virtual void UpdateSplashScreenStatus(string message, int progressValue = -1, int progressMinimum = 0, int progressMaximum = 100)
		{
		}

		private void Initialize()
		{
			System.Threading.Thread threadLoader = new System.Threading.Thread(threadLoader_ThreadStart);
			threadLoader.Name = "Initialization Thread";
			threadLoader.Start();

			ShowSplashScreen();
		}
		protected virtual void InitializeInternal()
		{
			UpdateSplashScreenStatus("Loading object models...");
			UniversalEditor.Common.Reflection.GetAvailableObjectModels();

			UpdateSplashScreenStatus("Loading data formats...");
			UniversalEditor.Common.Reflection.GetAvailableDataFormats();

			// Initialize the XML files
			InitializeXMLConfiguration();

			// Initialize Recent File Manager
			mvarRecentFileManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "RecentItems.xml";
			mvarRecentFileManager.Load();

			// Initialize Bookmarks Manager
			mvarBookmarksManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Bookmarks.xml";
			mvarBookmarksManager.Load();

			// Initialize Session Manager
			mvarSessionManager.DataFileName = DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Sessions.xml";
			mvarSessionManager.Load();
		}
		private void threadLoader_ThreadStart()
		{
			/*
			if (Configuration.SplashScreen.Enabled)
			{
				while (splasher == null) System.Threading.Thread.Sleep(500);
			}
			*/

			InitializeInternal();
			HideSplashScreen();
		}

		public void StartApplication()
		{
			Engine.mvarCurrentEngine = this;

			string INSTANCEID = GetType().FullName + "$2d429aa3371c421fb63b42525e51a50c$92751853175891031214292357218181357901238$";
			if (ConfigurationManager.GetValue<bool>("SingleInstanceUniquePerDirectory", true))
			{
				// The single instance should be unique per directory
				INSTANCEID += System.Reflection.Assembly.GetEntryAssembly().Location;
			}
			if (!SingleInstanceManager.CreateSingleInstance(INSTANCEID, new EventHandler<SingleInstanceManager.InstanceCallbackEventArgs>(SingleInstanceManager_Callback))) return;

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

			BeforeInitialization();

			// Initialize the branding for the selected application
			InitializeBranding();
			
			Initialize();

			AfterInitializationInternal();
			AfterInitialization();

			OpenWindow(SelectedFileNames.ToArray<string>());

			MainLoop();

			SessionManager.Save();
			BookmarksManager.Save();
			RecentFileManager.Save();
			ConfigurationManager.Save();
		}
	}
}
