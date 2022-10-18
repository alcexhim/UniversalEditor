
using System;
using System.Collections.Generic;
using System.ComponentModel;
using MBS.Framework;
using MBS.Framework.Settings;
using MBS.Framework.UserInterface;
using MBS.Framework.UserInterface.Controls.Docking;
using MBS.Framework.UserInterface.Controls.ListView;
using MBS.Framework.UserInterface.Dialogs;
using MBS.Framework.UserInterface.Input.Keyboard;
using UniversalEditor.Accessors;
using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.ObjectModels.PropertyList;
using UniversalEditor.UserInterface.Dialogs;
using UniversalEditor.UserInterface.Panels;

namespace UniversalEditor.UserInterface
{
	/// <summary>
	/// The main Framework application class for Universal Editor. Can be
	/// overridden to customize the behavior of the Universal Editor platform in
	/// derived applications.
	/// </summary>
	public class EditorApplication : UIApplication, IHostApplication
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EditorApplication" />
		/// class. Most importantly, sets the application title, GUID ID, unique
		/// name (used for D-Bus requests), and short name (used for application
		/// configuration settings on Linux).
		/// </summary>
		public EditorApplication()
		{
			Title = Localization.StringTable.ApplicationName;

			CommandLine.Options.Add("command", '\0', null, CommandLineOptionValueType.Multiple);

			ID = new Guid("{b359fe9a-080a-43fc-ae38-00ba7ac1703e}");
			UniqueName = "net.alcetech.UniversalEditor";
			ShortName = "universal-editor";
		}

		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			InitializePanels();
			InitializeWindowSwitcher();
		}

		private void InitializeWindowSwitcher()
		{
			// FIXME: this should be implemented as a plugin or directly in the UI Framework codebase
			this.EventFilters.Add(new EventFilter<KeyEventArgs>(delegate (EventFilterType type, ref KeyEventArgs e)
			{
				if (e.Key == KeyboardKey.Tab && (e.ModifierKeys & KeyboardModifierKey.Control) == KeyboardModifierKey.Control)
				{
					EditorWindow mw = (CurrentWindow as EditorWindow);
					if (mw != null)
					{
						mw.SetWindowListVisible(true, false);
					}
					else
					{
						Console.WriteLine("got tab but no main window");
					}
					return true;
				}
				return false;
			}, EventFilterType.KeyDown));
			this.EventFilters.Add(new EventFilter<KeyEventArgs>(delegate (EventFilterType type, ref KeyEventArgs e)
			{
				if (e.Key == KeyboardKey.LControlKey || e.Key == KeyboardKey.RControlKey)
				{
					EditorWindow mw = (CurrentWindow as EditorWindow);
					if (mw != null)
					{
						mw.SetWindowListVisible(false, false);
					}
					Console.WriteLine("window switch");
					Console.WriteLine("current window: {0}", CurrentWindow?.ToString() ?? "(null)");
					return true;
				}
				return false;
			}, EventFilterType.KeyUp));
		}

		/// <summary>
		/// Initializes the panels. This can only be called after the application has been created. If this
		/// function is called before the application has been created (i.e. in a constructor), the application
		/// will crash.
		/// </summary>
		private void InitializePanels()
		{
			// FIXME: this should all be done in XML
			PanelReference prPropertyList = new PanelReference(PropertyListPanel.ID);
			prPropertyList.Title = "Property List";
			prPropertyList.Control = new PropertyListPanel();
			prPropertyList.Placement = DockingItemPlacement.Right;
			Panels.Add(prPropertyList);

			PanelReference prToolbox = new PanelReference(ToolboxPanel.ID);
			prToolbox.Title = "Toolbox";
			prToolbox.Control = new ToolboxPanel();
			prToolbox.Placement = DockingItemPlacement.Left;
			Panels.Add(prToolbox);

			PanelReference prSolutionExplorer = new PanelReference(SolutionExplorerPanel.ID);
			prSolutionExplorer.Title = "Solution Explorer";
			prSolutionExplorer.Control = new SolutionExplorerPanel();
			prSolutionExplorer.Placement = DockingItemPlacement.Left;
			Panels.Add(prSolutionExplorer);

			PanelReference prDocumentExplorer = new PanelReference(DocumentExplorerPanel.ID);
			prDocumentExplorer.Title = "Document Explorer";
			prDocumentExplorer.Control = new DocumentExplorerPanel();
			prDocumentExplorer.Placement = DockingItemPlacement.Left;
			Panels.Add(prDocumentExplorer);

			PanelReference prErrorList = new PanelReference(ErrorListPanel.ID);
			prErrorList.Title = "Error List";
			prErrorList.Control = new ErrorListPanel();
			prErrorList.Placement = DockingItemPlacement.Bottom;
			Panels.Add(prErrorList);
		}

		public ConfigurationManager ConfigurationManager { get; } = new ConfigurationManager();
		public RecentFileManager RecentFileManager { get; } = new RecentFileManager();
		public BookmarksManager BookmarksManager { get; } = new BookmarksManager();
		public SessionManager SessionManager { get; } = new SessionManager();

		public PanelReference.PanelReferenceCollection Panels { get; } = new PanelReference.PanelReferenceCollection();

		protected override void OnContextAdded(ContextChangedEventArgs e)
		{
			base.OnContextAdded(e);

			EditorContext ec = (e.Context as EditorContext);
			if (ec != null)
			{
				foreach (PanelReference panel in ec.Panels)
				{
					foreach (IHostApplicationWindow mw in Windows)
					{
						mw.AddPanel(panel);
					}
				}
			}
		}
		protected override void OnContextRemoved(ContextChangedEventArgs e)
		{
			base.OnContextRemoved(e);

			EditorContext ec = (e.Context as EditorContext);
			if (ec != null)
			{
				foreach (PanelReference panel in ec.Panels)
				{
					foreach (IHostApplicationWindow mw in Windows)
					{
						mw.RemovePanel(panel);
					}
				}
			}
		}

		protected override void OnStartup(EventArgs e)
		{
			base.OnStartup(e);

			string[] args1 = Environment.GetCommandLineArgs();
			string[] args = new string[args1.Length - 1];
			Array.Copy(args1, 1, args, 0, args.Length);

			System.Collections.ObjectModel.Collection<string> selectedFileNames = new System.Collections.ObjectModel.Collection<string>();
			if (selectedFileNames.Count == 0 || ConfigurationManager.GetValue<bool>(new string[] { "Application", "Startup", "ForceLoadStartupFileNames" }, false))
			{
				object[] oStartupFileNames = ConfigurationManager.GetValue<object[]>(new string[] { "Application", "Startup", "FileNames" }, new object[0]);
				for (int i = 0; i < oStartupFileNames.Length; i++)
				{
					string startupFileName = oStartupFileNames[i].ToString();
					selectedFileNames.Add(startupFileName);
				}
			}
			foreach (string commandLineArgument in args)
			{
				selectedFileNames.Add(commandLineArgument);
			}
			mvarSelectedFileNames = new System.Collections.ObjectModel.ReadOnlyCollection<string>(selectedFileNames);

			TemporaryFileManager.RegisterTemporaryDirectory();

			BeforeInitialization();

			// Initialize the branding for the selected application
			InitializeBranding();

			Initialize();
		}
		protected override void OnStopped(EventArgs e)
		{
			base.OnStopped(e);


			SessionManager.Save();
			BookmarksManager.Save();
			RecentFileManager.Save();
			ConfigurationManager.Save();

			TemporaryFileManager.UnregisterTemporaryDirectory();
		}

		/// <summary>
		/// Gets or sets the output window of the host application, where other plugins can read from and write to.
		/// </summary>
		public HostApplicationOutputWindow OutputWindow { get; set; } = new HostApplicationOutputWindow();
		/// <summary>
		/// A collection of messages to display in the Error List panel.
		/// </summary>
		public HostApplicationMessage.HostApplicationMessageCollection Messages { get; } = new HostApplicationMessage.HostApplicationMessageCollection();

		public event EventHandler<EditorChangingEventArgs> EditorChanging;
		protected internal virtual void OnEditorChanging(EditorChangingEventArgs e)
		{
			EditorChanging?.Invoke(this, e);
		}
		public event EventHandler<EditorChangedEventArgs> EditorChanged;
		protected internal virtual void OnEditorChanged(EditorChangedEventArgs e)
		{
			EditorChanged?.Invoke(this, e);
		}

		protected override Command FindCommandInternal(string commandID)
		{
			if (!Common.Reflection.Initialized)
			{
				// hack around an infinite loop we inadvertently created
				return base.FindCommandInternal(commandID);
			}

			EditorReference[] editors = Common.Reflection.GetAvailableEditors();
			foreach (EditorReference er in editors)
			{
				Command cmd = er.Commands[commandID];
				if (cmd != null) return cmd;
			}
			return base.FindCommandInternal(commandID);
		}
		protected override Context FindContextInternal(Guid contextID)
		{
			EditorReference[] editors = Common.Reflection.GetAvailableEditors();
			foreach (EditorReference er in editors)
			{
				Context ctx = er.Contexts[contextID];
				if (ctx != null) return ctx;
			}
			return base.FindContextInternal(contextID);
		}


		public bool ShowCustomOptionDialog(ref DataFormat df, CustomOptionDialogType type)
		{
			SettingsProvider coll = null;
			DataFormatReference dfr = df.MakeReference();

			if (type == CustomOptionDialogType.Export)
			{
				coll = dfr.ExportOptions;
			}
			else
			{
				coll = dfr.ImportOptions;
			}
			if (coll == null) return true;
			if (coll.Count == 0) return true;

			ApplyCustomOptions(coll, df);

			bool retval = ShowCustomOptionDialog(ref coll, dfr.Title + " Options", delegate (object sender, EventArgs e)
			{
				ShowAboutDialog(dfr);
			});

			if (retval)
			{
				ApplyCustomOptions(df, coll);
				return true;
			}
			return false;
		}
		public bool ShowCustomOptionDialog(ref Accessor df, CustomOptionDialogType type)
		{
			if (df == null) return true;

			SettingsProvider coll = null;
			AccessorReference dfr = df.MakeReference();

			if (type == CustomOptionDialogType.Export)
			{
				coll = dfr.ExportOptions;
			}
			else
			{
				coll = dfr.ImportOptions;
			}
			if (coll == null) return true;
			if (coll.Count == 0) return true;

			ApplyCustomOptions(coll, df);

			bool retval = ShowCustomOptionDialog(ref coll, dfr.Title + " Options");

			if (retval)
			{
				ApplyCustomOptions(df, coll);
				return true;
			}
			return false;
		}

		public void ApplyCustomOptions(object obj, SettingsProvider coll)
		{
			if (obj == null) return;

			foreach (SettingsGroup sg in coll.SettingsGroups)
			{
				foreach (Setting eo in sg.Settings)
				{
					System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(eo.Name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
					if (pi == null) continue;

					if (eo is RangeSetting)
					{
						RangeSetting itm = (eo as RangeSetting);
						pi.SetValue(obj, Convert.ChangeType(itm.GetValue(), pi.PropertyType), null);
					}
					else if (eo is BooleanSetting)
					{
						BooleanSetting itm = (eo as BooleanSetting);
						pi.SetValue(obj, Convert.ChangeType(itm.GetValue(), pi.PropertyType), null);
					}
					else if (eo is ChoiceSetting)
					{
						ChoiceSetting.ChoiceSettingValue choice = (ChoiceSetting.ChoiceSettingValue)(eo as ChoiceSetting).GetValue();
						if (choice != null)
						{
							Type[] interfaces = pi.PropertyType.GetInterfaces();
							bool convertible = false;
							foreach (Type t in interfaces)
							{
								if (t == typeof(IConvertible))
								{
									convertible = true;
									break;
								}
							}
							if (convertible)
							{
								pi.SetValue(obj, Convert.ChangeType(choice.Value, pi.PropertyType), null);
							}
							else
							{
								pi.SetValue(obj, choice, null);
							}
						}
					}
					else if (eo is TextSetting)
					{
						TextSetting itm = (eo as TextSetting);
						pi.SetValue(obj, Convert.ChangeType(itm.GetValue(), pi.PropertyType), null);
					}
					else if (eo is FileSetting)
					{
						FileSetting itm = (eo as FileSetting);
						pi.SetValue(obj, Convert.ChangeType(itm.GetValue(), pi.PropertyType), null);
					}
				}
			}
		}

		public void ApplyCustomOptions(SettingsProvider coll, object obj)
		{
			if (obj == null) return;

			foreach (SettingsGroup sg in coll.SettingsGroups)
			{
				foreach (Setting eo in sg.Settings)
				{
					System.Reflection.PropertyInfo pi = obj.GetType().GetProperty(eo.Name);
					if (pi == null) continue;

					if (eo is ChoiceSetting)
					{
						/*
						object choice = (eo as ChoiceSetting).GetValue();
						if (choice != null)
						{
							Type[] interfaces = pi.PropertyType.GetInterfaces();
							bool convertible = false;
							foreach (Type t in interfaces)
							{
								if (t == typeof(IConvertible))
								{
									convertible = true;
									break;
								}
							}
							if (convertible)
							{
								pi.SetValue(obj, Convert.ChangeType(choice, pi.PropertyType), null);
							}
							else
							{
								pi.SetValue(obj, choice, null);
							}
						}
						*/
					}
					else
					{
						eo.SetValue(pi.GetValue(obj, null));
					}
				}
			}
		}



		// ================ BEGIN: HERE BE DRAGONS

		#region implemented abstract members of Engine
		protected void BeforeInitialization()
		{
			// Application.EnableVisualStyles();
			// Application.SetCompatibleTextRenderingDefault(false);

			// TODO: figure out why this is being done on BeforeInitialization and whether we could move it to after
			//       the configuration is initialized, so we can specify the user's favorite theme in a configuration file

			// AwesomeControls.Theming.BuiltinThemes. theme = new AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme(AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme.ColorMode.Dark);
			// theme.UseAllCapsMenus = false;
			// theme.SetStatusBarState(AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme.StatusBarState.Initial);
			// AwesomeControls.Theming.BuiltinThemes.Office2003Theme theme = new AwesomeControls.Theming.BuiltinThemes.Office2003Theme();
			// AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme theme = new AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme();
			// AwesomeControls.Theming.BuiltinThemes.SlickTheme theme = new AwesomeControls.Theming.BuiltinThemes.SlickTheme();

			// Office 2000  =   {105843D0-2F26-4CB7-86AB-10A449815C19}
			// Office 2007	=	{4D86F538-E277-4E6F-9CAC-60F82D49A19D}
			// VS2012-Dark	=	{25134C94-B1EB-4C38-9B5B-A2E29FC57AE1}
			// VS2012-Light	=	{54CE64B1-2DE3-4147-B499-03F0934AFD37}
			// VS2012-Blue	=	{898A65FC-8D08-46F1-BB94-2BF666AC996E}

			// AwesomeControls.Theming.Theme theme = AwesomeControls.Theming.Theme.GetByID(new Guid("{54CE64B1-2DE3-4147-B499-03F0934AFD37}"));
			// AwesomeControls.Theming.Theme theme = AwesomeControls.Theming.Theme.GetByID(new Guid("{105843D0-2F26-4CB7-86AB-10A449815C19}"));
			// if (theme != null) AwesomeControls.Theming.Theme.CurrentTheme = theme;

			// AwesomeControls.Theming.Theme.CurrentTheme.Properties["UseAllCapsMenus"] = false;

			ConfigurationFileNameFilter = "*.uexml";

			Initialize();
		}

		protected override void OnAfterConfigurationLoaded(EventArgs e)
		{
			base.OnAfterConfigurationLoaded(e);

			#region Global Configuration
			{
				UpdateSplashScreenStatus("Loading global configuration");

				MarkupTagElement tagConfiguration = (((UIApplication)Application.Instance).RawMarkup.FindElement("UniversalEditor", "Configuration") as MarkupTagElement);
				if (tagConfiguration != null)
				{
					foreach (MarkupElement el in tagConfiguration.Elements)
					{
						MarkupTagElement tag = (el as MarkupTagElement);
						if (tag == null) continue;
						LoadConfiguration(tag);
					}
				}
			}
			#endregion
			#region Object Model Configuration
			{
				UpdateSplashScreenStatus("Loading object model configuration");

				MarkupTagElement tagObjectModels = (((UIApplication)Application.Instance).RawMarkup.FindElement("UniversalEditor", "ObjectModels") as MarkupTagElement);
				if (tagObjectModels != null)
				{
					MarkupTagElement tagDefault = (tagObjectModels.Elements["Default"] as MarkupTagElement);
					if (tagDefault != null)
					{
						ObjectModelReference[] omrs = UniversalEditor.Common.Reflection.GetAvailableObjectModels();
						MarkupAttribute attVisible = tagDefault.Attributes["Visible"];
						foreach (ObjectModelReference omr in omrs)
						{
							if (attVisible != null) omr.Visible = (attVisible.Value == "true");
						}
					}

					foreach (MarkupElement el in tagObjectModels.Elements)
					{
						MarkupTagElement tag = (el as MarkupTagElement);
						if (tag == null) continue;
						if (tag.FullName == "ObjectModel")
						{
							MarkupAttribute attTypeName = tag.Attributes["TypeName"];
							MarkupAttribute attID = tag.Attributes["ID"];
							MarkupAttribute attVisible = tag.Attributes["Visible"];
							MarkupAttribute attEmptyTemplatePrefix = tag.Attributes["EmptyTemplatePrefix"];

							ObjectModelReference omr = null;
							if (attTypeName != null)
							{
								omr = UniversalEditor.Common.Reflection.GetAvailableObjectModelByTypeName(attTypeName.Value);
							}
							else
							{
								omr = UniversalEditor.Common.Reflection.GetAvailableObjectModelByID(new Guid(attID.Value));
							}

							if (omr != null)
							{
								if (attVisible != null) omr.Visible = (attVisible.Value == "true");
								if (attEmptyTemplatePrefix != null) omr.EmptyTemplatePrefix = attEmptyTemplatePrefix.Value;
							}
						}
					}
				}
			}
			#endregion

			UpdateSplashScreenStatus("Loading object models...");
			UniversalEditor.Common.Reflection.GetAvailableObjectModels();

			UpdateSplashScreenStatus("Loading data formats...");
			UniversalEditor.Common.Reflection.GetAvailableDataFormats();

			// Initialize Recent File Manager
			RecentFileManager.DataFileName = ((UIApplication)Application.Instance).DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "RecentItems.xml";
			RecentFileManager.Load();
			RefreshRecentFilesList();

			// Initialize Bookmarks Manager
			BookmarksManager.DataFileName = ((UIApplication)Application.Instance).DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Bookmarks.xml";
			BookmarksManager.Load();

			// Initialize Session Manager
			SessionManager.DataFileName = ((UIApplication)Application.Instance).DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Sessions.xml";
			SessionManager.Load();

			// load editors into memory so we don't wait 10-15 seconds before opening a file
			Common.Reflection.GetAvailableEditors();

			AfterInitialization();
		}


		private void AddRecentMenuItem(string FileName)
		{
			Command mnuFileRecentFiles = ((UIApplication)Application.Instance).Commands["FileRecentFiles"];

			Command mnuFileRecentFile = new Command();
			mnuFileRecentFile.ID = "FileRecentFile_" + FileName;
			mnuFileRecentFile.Title = System.IO.Path.GetFileName(FileName);
			// mnuFileRecentFile.ToolTipText = FileName;
			((UIApplication)Application.Instance).Commands.Add(mnuFileRecentFile);

			CommandReferenceCommandItem tsmi = new CommandReferenceCommandItem("FileRecentFile_" + FileName);
			mnuFileRecentFiles.Items.Add(tsmi);
		}
		private void RefreshRecentFilesList()
		{
			Command mnuFileRecentFiles = ((UIApplication)Application.Instance).Commands["FileRecentFiles"];
			if (mnuFileRecentFiles != null)
			{
				mnuFileRecentFiles.Items.Clear();

				string[] filenames = RecentFileManager.GetFileNames();
				foreach (string filename in filenames)
				{
					AddRecentMenuItem(filename);
				}
				mnuFileRecentFiles.Visible = (mnuFileRecentFiles.Items.Count > 0);
			}

			Command mnuFileRecentProjects = ((UIApplication)Application.Instance).Commands["FileRecentProjects"];
			if (mnuFileRecentProjects != null)
			{
				mnuFileRecentProjects.Visible = (mnuFileRecentProjects.Items.Count > 0);
			}
			// mnuFileSep3.Visible = ((mnuFileRecentFiles.DropDownItems.Count > 0) || (mnuFileRecentProjects.DropDownItems.Count > 0));
		}

		/// <summary>
		/// Event handler for <see cref="Application.BeforeShutdown" /> event. Called when the
		/// <see cref="Application.Stop" /> method is called, before the application is stopped.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnBeforeShutdown(CancelEventArgs e)
		{
			base.OnBeforeShutdown(e);

			for (int i = 0; i < ((UIApplication)Application.Instance).Windows.Count; i++)
			{
				EditorWindow mw = (((UIApplication)Application.Instance).Windows[i] as EditorWindow);
				if (mw == null) continue;

				if (!mw.Close())
				{
					e.Cancel = true;
					break;
				}
			}
		}

		/// <summary>
		/// Creates an empty <see cref="EditorWindow" />. Usually this is just a
		/// call to <see cref="OpenWindow" />, but it can be customized to present
		/// a blank document editor for your application's default document format.
		/// </summary>
		protected virtual void CreateInitialWindow()
		{
			OpenWindow();
		}

		/// <summary>
		/// Event handler for <see cref="UIApplication.Activated" /> event.
		/// </summary>
		/// <param name="e">Event arguments.</param>
		protected override void OnActivated(ApplicationActivatedEventArgs e)
		{
			base.OnActivated(e);

			if (e.CommandLine.FileNames.Count > 0)
			{
				// file names were passed on the command line, so open them
				// we do not allow inherited applications to override this
				string[] filenames = e.CommandLine.FileNames.ToArray();
				if (LastWindow != null)
				{
					LastWindow.OpenFile(filenames);
				}
				else
				{
					OpenWindow(filenames);
				}
			}
			else
			{
				// no file names were passed, create an initial window
				// this may be overridden by an inherited application to e.g. display a blank document editor
				((EditorApplication)Application.Instance).CreateInitialWindow();
			}

			// if we are passed Framework commands on the command line (e.g. FileNewDocument), execute them
			List<string> commandsToExecute = (((UIApplication)Application.Instance).CommandLine.Options.GetValueOrDefault<List<string>>("command", null));
			if (commandsToExecute != null)
			{
				for (int i = 0; i < commandsToExecute.Count; i++)
				{
					((UIApplication)Application.Instance).ExecuteCommand(commandsToExecute[i]);
				}
			}
		}

		protected void MainLoop()
		{
			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					// Glue.ApplicationInformation.ApplicationDataPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { "universal-editor", "plugins" });
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					// Glue.ApplicationInformation.ApplicationDataPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { "Universal Editor", "Plugins" });
					break;
				}
			}

			// Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStart);
			// Glue.Common.Methods.SendApplicationEvent(e);
			// if (e.CancelApplication) return;

			/*
			PieMenuManager.Title = "Universal Editor pre-alpha build";

			PieMenuItemGroup row1 = new PieMenuItemGroup();
			row1.Title = "Tools";
			row1.Items.Add("atlMFCTraceTool", "ATL/MFC Trace Tool");
			row1.Items.Add("textEditor", "Text Editor");
			PieMenuManager.Groups.Add(row1);
			*/

#if !DEBUG
			// Application.ThreadException += Application_ThreadException;
#endif
			Application.Instance.Start();

			// Glue.Common.Methods.SendApplicationEvent(new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStop));
		}

		public void ShowAboutDialog(DataFormatReference dfr)
		{
			if (dfr == null)
			{
				string dlgfilename = ExpandRelativePath("~/Dialogs/AboutDialog.glade");
				if (dlgfilename != null)
				{
					Dialogs.AboutDialog dlg = new Dialogs.AboutDialog();
					dlg.ShowDialog();
				}
				else
				{
					MBS.Framework.UserInterface.Dialogs.AboutDialog dlg = new MBS.Framework.UserInterface.Dialogs.AboutDialog();
					dlg.ProgramName = Title;
					dlg.Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version;
					dlg.Copyright = String.Format("(c) 1997-{0} {1}", DateTime.Now.Year, "Michael Becker");
					dlg.Comments = "A modular, extensible document editor";
					dlg.LicenseType = LicenseType.GPL30;
					dlg.ShowDialog();
				}
			}
			else
			{
				DataFormatAboutDialog dlg = new DataFormatAboutDialog();
				dlg.DataFormatReference = dfr;
				dlg.ShowDialog();
			}
		}
		public bool ShowCustomOptionDialog(ref SettingsProvider customOptions, string title = null, EventHandler aboutButtonClicked = null)
		{
			SettingsDialog dlg = new SettingsDialog();
			dlg.Text = title ?? "Options";
			dlg.SettingsProfiles.Clear();

			dlg.SettingsProviders.Clear();
			dlg.SettingsProviders.Add(customOptions);

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				return true;
			}
			return false;
		}
		#endregion

		private void Bookmarks_Bookmark_Click(object sender, EventArgs e)
		{
			Command cmd = (Command)sender;
			int i = Int32.Parse(cmd.ID.Substring("Bookmarks_Bookmark".Length));
			LastWindow.OpenFile(BookmarksManager.FileNames[i]);
		}

		private void AfterInitialization()
		{
			if (BookmarksManager.FileNames.Count > 0)
			{
				Command mnuBookmarks = Application.Instance.Commands["Bookmarks"];
				if (mnuBookmarks != null)
				{
					mnuBookmarks.Items.Add(new SeparatorCommandItem());
					for (int i = 0; i < BookmarksManager.FileNames.Count; i++)
					{
						Application.Instance.Commands.Add(new Command(String.Format("Bookmarks_Bookmark{0}", i.ToString()), System.IO.Path.GetFileName(((EditorApplication)Application.Instance).BookmarksManager.FileNames[i]).Replace("_", "__")));
						mnuBookmarks.Items.Add(new CommandReferenceCommandItem(String.Format("Bookmarks_Bookmark{0}", i.ToString())));

						Application.Instance.AttachCommandEventHandler(String.Format("Bookmarks_Bookmark{0}", i.ToString()), Bookmarks_Bookmark_Click);
					}
				}
			}

			// Initialize all the commands that are common to UniversalEditor
			#region File
			Application.Instance.AttachCommandEventHandler("FileNewDocument", delegate (object sender, EventArgs e)
			{
				LastWindow.NewFile();
			});
			Application.Instance.AttachCommandEventHandler("FileNewProject", delegate (object sender, EventArgs e)
			{
				LastWindow.NewProject();
			});
			Application.Instance.AttachCommandEventHandler("FileOpenDocument", delegate (object sender, EventArgs e)
			{
				LastWindow.OpenFile();
			});
			Application.Instance.AttachCommandEventHandler("FileOpenProject", delegate (object sender, EventArgs e)
			{
				LastWindow.OpenProject();
			});
			Application.Instance.AttachCommandEventHandler("FileSaveDocument", delegate (object sender, EventArgs e)
			{
				LastWindow.SaveFile();
			});
			Application.Instance.AttachCommandEventHandler("FileSaveDocumentAs", delegate (object sender, EventArgs e)
			{
				LastWindow.SaveFileAs();
			});
			Application.Instance.AttachCommandEventHandler("FileSaveProject", delegate (object sender, EventArgs e)
			{
				LastWindow.SaveProject();
			});
			Application.Instance.AttachCommandEventHandler("FileSaveProjectAs", delegate (object sender, EventArgs e)
			{
				LastWindow.SaveProjectAs();
			});
			Application.Instance.AttachCommandEventHandler("FileSaveAll", delegate (object sender, EventArgs e)
			{
				LastWindow.SaveAll();
			});
			Application.Instance.AttachCommandEventHandler("FileCloseDocument", delegate (object sender, EventArgs e)
			{
				LastWindow.CloseFile();
			});
			Application.Instance.AttachCommandEventHandler("FileCloseProject", delegate (object sender, EventArgs e)
			{
				LastWindow.CloseProject();
			});
			Application.Instance.AttachCommandEventHandler("FilePrint", delegate (object sender, EventArgs e)
			{
				LastWindow.PrintDocument();
			});
			Application.Instance.AttachCommandEventHandler("FileProperties", delegate (object sender, EventArgs e)
			{
				LastWindow.ShowDocumentPropertiesDialog();
			});
			Application.Instance.AttachCommandEventHandler("FileRestart", delegate (object sender, EventArgs e)
			{
				Restart();
			});
			Application.Instance.AttachCommandEventHandler("FileExit", delegate (object sender, EventArgs e)
			{
				Stop();
			});
			#endregion
			#region Edit
			Application.Instance.AttachCommandEventHandler("EditCut", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor == null) return;
				editor.Cut();
			});
			Application.Instance.AttachCommandEventHandler("EditCopy", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor == null) return;
				editor.Copy();
			});
			Application.Instance.AttachCommandEventHandler("EditPaste", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor == null) return;
				editor.Paste();
			});
			Application.Instance.AttachCommandEventHandler("EditDelete", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor != null)
				{
					editor.Delete();
				}
				else
				{
					Control ctl = LastWindow.ActiveControl;
					if (ctl is ListViewControl && ctl.Parent is SolutionExplorerPanel)
					{
						(ctl.Parent as SolutionExplorerPanel).Delete();
					}
				}
			});
			Application.Instance.AttachCommandEventHandler("EditBatchFindReplace", delegate (object sender, EventArgs e)
			{
				if (LastWindow == null) return;

				Editor ed = LastWindow.GetCurrentEditor();
				if (ed == null) return;

				BatchFindReplaceWindow dlg = new BatchFindReplaceWindow();
				dlg.Editor = ed;
				dlg.Show();
			});
			Application.Instance.AttachCommandEventHandler("EditUndo", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor == null) return;
				editor.Undo();
			});
			Application.Instance.AttachCommandEventHandler("EditRedo", delegate (object sender, EventArgs e)
			{
				Editor editor = LastWindow.GetCurrentEditor();
				if (editor == null) return;
				editor.Redo();
			});
			#endregion
			#region View
			Application.Instance.AttachCommandEventHandler("ViewFullScreen", delegate (object sender, EventArgs e)
			{
				Command cmd = (e as CommandEventArgs).Command;
				LastWindow.FullScreen = !LastWindow.FullScreen;
				cmd.Checked = LastWindow.FullScreen;
			});
			#region Perspective
			Application.Instance.AttachCommandEventHandler("ViewPerspective1", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(1);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective2", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(2);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective3", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(3);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective4", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(4);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective5", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(5);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective6", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(6);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective7", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(7);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective8", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(8);
			});
			Application.Instance.AttachCommandEventHandler("ViewPerspective9", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.SwitchPerspective(9);
			});
			#endregion

			Application.Instance.AttachCommandEventHandler("ViewStartPage", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow)?.ShowStartPage();
			});
			Application.Instance.AttachCommandEventHandler("ViewStatusBar", delegate (object sender, EventArgs e)
			{
				((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).StatusBar.Visible = !((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).StatusBar.Visible;
				Application.Instance.Commands["ViewStatusBar"].Checked = ((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).StatusBar.Visible;
			});

			#endregion
			#region Bookmarks
			Application.Instance.AttachCommandEventHandler("BookmarksAdd", delegate (object sender, EventArgs e)
			{
				Editor ed = LastWindow.GetCurrentEditor();
				if (ed == null) return;

				AddBookmark(ed.ObjectModel?.Accessor ?? (ed.Parent as Pages.EditorPage)?.Document?.Accessor);
				ShowBookmarksManagerDialog();
			});
			Application.Instance.AttachCommandEventHandler("BookmarksAddAll", delegate (object sender, EventArgs e)
			{
				Page[] pages = LastWindow.GetPages();

				for (int i = 0; i < pages.Length; i++)
				{
					if (pages[i] is Pages.EditorPage)
					{
						Pages.EditorPage ep = (pages[i] as Pages.EditorPage);
						Editor ed = (ep.Controls[0] as Editor);

						AddBookmark(ed.ObjectModel?.Accessor ?? (ed.Parent as Pages.EditorPage)?.Document?.Accessor);
					}
				}

				ShowBookmarksManagerDialog();
			});
			Application.Instance.AttachCommandEventHandler("BookmarksManage", delegate (object sender, EventArgs e)
			{
				ShowBookmarksManagerDialog();
			});
			#endregion
			#region Tools
			// ToolsOptions should actually be under the Edit menu as "Preferences" on Linux systems
			Application.Instance.AttachCommandEventHandler("ToolsOptions", delegate (object sender, EventArgs e)
			{
				LastWindow.ShowOptionsDialog();
			});
			Application.Instance.AttachCommandEventHandler("ToolsCustomize", delegate (object sender, EventArgs e)
			{
				((UIApplication)Application.Instance).ShowSettingsDialog(new string[] { "Application", "Command Bars" });
			});
			#endregion
			#region Window
			Application.Instance.AttachCommandEventHandler("WindowNewWindow", delegate (object sender, EventArgs e)
			{
				OpenWindow();
			});
			Application.Instance.AttachCommandEventHandler("WindowWindows", delegate (object sender, EventArgs e)
			{
				LastWindow.SetWindowListVisible(true, true);
			});
			#endregion
			#region Help
			Application.Instance.AttachCommandEventHandler("HelpViewHelp", delegate (object sender, EventArgs e)
			{
				((UIApplication)Application.Instance).ShowHelp();
			});
			Application.Instance.AttachCommandEventHandler("HelpCustomerFeedbackOptions", delegate (object sender, EventArgs e)
			{
				// MessageDialog.ShowDialog("This product has already been activated.", "Licensing and Activation", MessageDialogButtons.OK, MessageDialogIcon.Information);
				TaskDialog td = new TaskDialog();
				td.ButtonStyle = TaskDialogButtonStyle.Commands;

				td.Prompt = "Please open a trouble ticket on GitHub if you need support.";
				td.Text = "Customer Experience Improvement Program";
				td.Content = String.Format("You are using the GNU GPLv3 licensed version of {0}. This program comes with ABSOLUTELY NO WARRANTY.\r\n\r\nSupport contracts may be available for purchase; please contact your software distributor.", Application.Instance.Title);
				td.Footer = "<a href=\"GPLLicense\">View the license terms</a>";
				td.EnableHyperlinks = true;
				td.HyperlinkClicked += Td_HyperlinkClicked;

				td.Icon = TaskDialogIcon.SecurityOK;
				td.Parent = (Window)LastWindow;
				td.ShowDialog();
			});
			Application.Instance.AttachCommandEventHandler("HelpLicensingAndActivation", delegate (object sender, EventArgs e)
			{
				// MessageDialog.ShowDialog("This product has already been activated.", "Licensing and Activation", MessageDialogButtons.OK, MessageDialogIcon.Information);
				TaskDialog td = new TaskDialog();
				td.ButtonStyle = TaskDialogButtonStyle.Commands;

				td.Prompt = "This product has already been activated.";
				td.Text = "Licensing and Activation";
				td.Content = String.Format("You are using the GNU GPLv3 licensed version of {0}. No activation is necessary.", Application.Instance.Title);
				td.Footer = "<a href=\"GPLLicense\">View the license terms</a>";
				td.EnableHyperlinks = true;
				td.HyperlinkClicked += Td_HyperlinkClicked;

				td.Icon = TaskDialogIcon.SecurityOK;
				td.Parent = (Window)LastWindow;
				td.ShowDialog();
			});
			Application.Instance.AttachCommandEventHandler("HelpSoftwareManager", delegate (object sender, EventArgs e)
			{
				// FIXME: launch the software manager to add or remove plugins for Universal Editor
				MessageDialog.ShowDialog("FIXME: launch the software manager to add or remove plugins for Universal Editor", "Not Implemented", MessageDialogButtons.OK, MessageDialogIcon.Error);
			});
			Application.Instance.AttachCommandEventHandler("HelpAboutPlatform", delegate (object sender, EventArgs e)
			{
				ShowAboutDialog();
			});
			#endregion


			Application.Instance.AttachCommandEventHandler("DockingContainerContextMenu_Close", delegate (object sender, EventArgs e)
			{
				CommandEventArgs ce = (e as CommandEventArgs);
				if (ce != null)
				{
					DockingWindow dw = ce.GetNamedParameter<DockingWindow>("Item");
					LastWindow?.CloseFile(dw);
				}
			});
			Application.Instance.AttachCommandEventHandler("DockingContainerContextMenu_CloseAll", delegate (object sender, EventArgs e)
			{
				LastWindow?.CloseWindow();
			});
			Application.Instance.AttachCommandEventHandler("DockingContainerContextMenu_CloseAllButThis", delegate (object sender, EventArgs e)
			{
				MessageDialog.ShowDialog("Not implemented ... yet", "Error", MessageDialogButtons.OK, MessageDialogIcon.Error);
			});


			#region Dynamic Commands
			#region View
			#region Panels
			for (int i = ((UIApplication)Application.Instance).CommandBars.Count - 1; i >= 0; i--)
			{
				Command cmdViewToolbarsToolbar = new Command();
				cmdViewToolbarsToolbar.ID = "ViewToolbars" + i.ToString();
				cmdViewToolbarsToolbar.Title = ((UIApplication)Application.Instance).CommandBars[i].Title;
				cmdViewToolbarsToolbar.Executed += cmdViewToolbarsToolbar_Executed;
				Application.Instance.Commands.Add(cmdViewToolbarsToolbar);
				Application.Instance.Commands["ViewToolbars"].Items.Insert(0, new CommandReferenceCommandItem(cmdViewToolbarsToolbar.ID));
			}
			#endregion
			#region Panels
			if (Application.Instance.Commands["ViewPanels"] != null)
			{
				Command cmdViewPanels1 = new Command();
				cmdViewPanels1.ID = "ViewPanels1";
				Application.Instance.Commands.Add(cmdViewPanels1);
				((UIApplication)Application.Instance).Commands["ViewPanels"].Items.Add(new CommandReferenceCommandItem("ViewPanels1"));
			}
			#endregion
			#endregion
			#endregion

			#region Language Strings
			#region Help
			Command helpAboutPlatform = Application.Instance.Commands["HelpAboutPlatform"];
			if (helpAboutPlatform != null)
			{
				helpAboutPlatform.Title = String.Format(helpAboutPlatform.Title, Localization.StringTable.ApplicationName);
			}

			Command helpLanguage = Application.Instance.Commands["HelpLanguage"];
			if (helpLanguage != null)
			{
				foreach (Language lang in ((UIApplication)Application.Instance).Languages)
				{
					Command cmdLanguage = new Command();
					cmdLanguage.ID = String.Format("HelpLanguage_{0}", lang.ID);
					cmdLanguage.Title = lang.Title;
					cmdLanguage.Executed += delegate (object sender, EventArgs e)
					{
						(Application.Instance as IHostApplication).Messages.Add(HostApplicationMessageSeverity.Notice, "Clicked language " + lang.ID);
					};
					Application.Instance.Commands.Add(cmdLanguage);

					helpLanguage.Items.Add(new CommandReferenceCommandItem(cmdLanguage.ID));
				}
			}
			#endregion
			#endregion
		}

		/// <summary>
		/// Adds a bookmark referencing the specified <see cref="Accessor" /> to the application's bookmarks menu.
		/// </summary>
		/// <returns><c>true</c>, if bookmark was added, <c>false</c> otherwise.</returns>
		/// <param name="accessor">The <see cref="Accessor" /> referencing the bookmark to add.</param>
		public bool AddBookmark(Accessor accessor)
		{
			// we cannot yet bookmark a file that does not yet exist. (this would be akin to creating a shortcut to a template I guess...?)
			if (accessor == null) return false;

			string filename = accessor.GetFileName();
			BookmarksManager.FileNames.Add(filename);

			Command cmdBookmarks = Application.Instance.Commands["Bookmarks"];
			if (cmdBookmarks.Items.Count == 4)
			{
				cmdBookmarks.Items.Add(new SeparatorCommandItem());
			}

			((UIApplication)Application.Instance).Commands.Add(new Command(String.Format("{0}", (((EditorApplication)Application.Instance).BookmarksManager.FileNames.Count - 1).ToString()), System.IO.Path.GetFileName(((EditorApplication)Application.Instance).BookmarksManager.FileNames[(BookmarksManager.FileNames.Count - 1)])));
			((UIApplication)Application.Instance).Commands["Bookmarks"].Items.Add(new CommandReferenceCommandItem(String.Format("Bookmarks_Bookmark{0}", (BookmarksManager.FileNames.Count - 1).ToString())));

			Application.Instance.AttachCommandEventHandler(String.Format("Bookmarks_Bookmark{0}", (((EditorApplication)Application.Instance).BookmarksManager.FileNames.Count - 1).ToString()), Bookmarks_Bookmark_Click);
			return true;
		}

		void Td_HyperlinkClicked(object sender, TaskDialogHyperlinkClickedEventArgs e)
		{
			System.Diagnostics.Process.Start("https://www.gnu.org/licenses/gpl-3.0.en.html");
		}


		void cmdViewToolbarsToolbar_Executed(object sender, EventArgs e)
		{
			Command cmd = (e as CommandEventArgs).Command;

		}

		private void ShowBookmarksManagerDialog()
		{
			ManageBookmarksDialog dlg = new ManageBookmarksDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				// saving the BookmarksManager state is handled by the ManageBookmarksDialog
			}
		}

		public IHostApplicationWindowCollection Windows { get; } = new IHostApplicationWindowCollection();

		public void CloseAllWindows()
		{
			List<IHostApplicationWindow> windowsToClose = new List<IHostApplicationWindow>();
			foreach (IHostApplicationWindow window in Windows)
			{
				windowsToClose.Add(window);
			}
			foreach (IHostApplicationWindow window in windowsToClose)
			{
				window.CloseWindow();
			}
		}

		private System.Collections.ObjectModel.ReadOnlyCollection<string> mvarSelectedFileNames = null;
		public System.Collections.ObjectModel.ReadOnlyCollection<string> SelectedFileNames { get { return mvarSelectedFileNames; } }

		public IHostApplicationWindow LastWindow { get; set; }

		public void OpenFile(params string[] fileNames)
		{
			Document[] documents = new Document[fileNames.Length];
			for (int i = 0; i < fileNames.Length; i++)
			{
				documents[i] = new Document(null, null, new FileAccessor(fileNames[i]));
			}
			OpenFile(documents);
		}
		public void OpenFile(params Document[] documents)
		{
			if (LastWindow == null)
			{
				OpenWindow(documents);
				return;
			}
			LastWindow.OpenFile(documents);
		}

		public void ShowAboutDialog()
		{
			this.ShowAboutDialog(null);
		}

		/// <summary>
		/// Opens a new window, with no documents loaded.
		/// </summary>
		public void OpenWindow()
		{
			EditorWindow mw = new EditorWindow();
			mw.Show();
		}
		/// <summary>
		/// Opens a new window, optionally loading the specified documents.
		/// </summary>
		/// <param name="fileNames">The file name(s) of the document(s) to load.</param>
		public void OpenWindow(params string[] fileNames)
		{
			EditorWindow mw = new EditorWindow();
			mw.Show();

			mw.OpenFile(fileNames);
		}
		/// <summary>
		/// Opens a new window, optionally loading the specified documents.
		/// </summary>
		/// <param name="documents">The document model(s) of the document(s) to load.</param>
		public void OpenWindow(params Document[] documents)
		{
			EditorWindow mw = new EditorWindow();
			mw.Show();

			mw.OpenFile(documents);
		}

		private void LoadConfiguration(MarkupTagElement tag, Group group = null)
		{
			if (tag.FullName == "Group")
			{
				Group group1 = new Group();
				group1.Name = tag.Attributes["ID"].Value;
				foreach (MarkupElement el in tag.Elements)
				{
					MarkupTagElement tg = (el as MarkupTagElement);
					if (tg == null) continue;
					LoadConfiguration(tg, group1);
				}

				if (group == null)
				{
					ConfigurationManager.AddGroup(group1, ConfigurationManagerPropertyScope.Global);
				}
				else
				{
					group.Items.Add(group1);
				}
			}
			else if (tag.FullName == "Property")
			{
				Property property = new Property();
				property.Name = tag.Attributes["ID"].Value;
				MarkupAttribute att = tag.Attributes["Value"];
				if (att != null)
				{
					property.Value = att.Value;
				}

				if (group == null)
				{
					ConfigurationManager.AddProperty(property, ConfigurationManagerPropertyScope.Global);
				}
				else
				{
					group.Items.Add(property);
				}
			}
		}

		protected virtual void InitializeBranding()
		{
			// FIXME: Possible race condition bug, if there is a MessageDialog popped while another thread is fucking around,
			// it
			NotificationPopup nip = new NotificationPopup();
			nip.Actions.Add(new ActionCommandItem("id1", "Ignore", delegate (object sender, EventArgs e)
			{
			}));
			nip.Actions.Add(new ActionCommandItem("id2", "Show Message Dialog", delegate (object sender, EventArgs e)
			{
				MessageDialog.ShowDialog("This message dialog being displayed will crash the program. Wait for it...\r\n\r\n Closing the message dialog before the end will avoid this issue.");
			}));
			nip.Content = "please fix this race condition as soon as possible. if there is a MessageDialog present while another thread is running calling Application.DoEvents may crash the program";
			nip.Summary = "race warning : click button to demonstrate";
			nip.Show();

			// I don't know why this ever was WindowsFormsEngine-specific...

			// First, attempt to load the branding from Branding.uxt
			/*
			string BrandingFileName = Application.BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding.uxt";
			if (System.IO.File.Exists(BrandingFileName))
			{
				FileSystemObjectModel fsom = new FileSystemObjectModel();
				FileAccessor fa = new FileAccessor(BrandingFileName);
				Document.Load(fsom, new UXTDataFormat(), fa, false);

				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenImage = fsom.Files["SplashScreen.png"];
				if (fileSplashScreenImage != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenImage.GetData());
					// oh, yeah, maybe because of this?
					// LocalConfiguration.SplashScreen.Image = System.Drawing.Image.FromStream(ms);

					// when I did this back in ... who knows when, Universal Widget Toolkit didn't
					// exist, let alone have a MBS.Framework.UserInterface.Drawing.Image class...
				}

				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenSound = fsom.Files["SplashScreen.wav"];
				if (fileSplashScreenSound != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenSound.GetData());
					// LocalConfiguration.SplashScreen.Sound = ms;
				}

				UniversalEditor.ObjectModels.FileSystem.File fileMainIcon = fsom.Files["MainIcon.ico"];
				if (fileMainIcon != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileMainIcon.GetData());
					// LocalConfiguration.MainIcon = new System.Drawing.Icon(ms);
				}

				UniversalEditor.ObjectModels.FileSystem.File fileConfiguration = fsom.Files["Configuration.upl"];
				if (fileConfiguration != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileConfiguration.GetData());

					UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel plomBranding = new ObjectModels.PropertyList.PropertyListObjectModel();
					Document.Load(plomBranding, new UniversalPropertyListDataFormat(), new StreamAccessor(ms), true);

					LocalConfiguration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, String.Empty);

					LocalConfiguration.ColorScheme.DarkColor = Color.FromString(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					LocalConfiguration.ColorScheme.LightColor = Color.FromString(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}

				fa.Close();
			}

			// Now, determine if we should override any branding details with local copies
			if (System.IO.Directory.Exists(Application.BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding"))
			{
				string SplashScreenImageFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Application.BasePath,
					"Branding",
					"SplashScreen.png"
				});
				if (System.IO.File.Exists(SplashScreenImageFileName)) LocalConfiguration.SplashScreen.ImageFileName = SplashScreenImageFileName;

				string SplashScreenSoundFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Application.BasePath,
					"Branding",
					"SplashScreen.wav"
				});
				if (System.IO.File.Exists(SplashScreenSoundFileName)) LocalConfiguration.SplashScreen.SoundFileName = SplashScreenSoundFileName;

				string MainIconFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Application.BasePath,
					"Branding",
					"MainIcon.ico"
				});
				// if (System.IO.File.Exists(MainIconFileName)) LocalConfiguration.MainIcon = System.Drawing.Icon.ExtractAssociatedIcon(MainIconFileName);

				string ConfigurationFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					Application.BasePath,
					"Branding",
					"Configuration.upl"
				});

				if (System.IO.File.Exists(ConfigurationFileName))
				{
					UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel plomBranding = new ObjectModels.PropertyList.PropertyListObjectModel();
					Document.Load(plomBranding, new UniversalPropertyListDataFormat(), new FileAccessor(ConfigurationFileName), true);

					LocalConfiguration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, "Universal Editor");
					LocalConfiguration.ApplicationShortName = plomBranding.GetValue<string>(new string[] { "Application", "ShortTitle" }, "universal-editor");
					LocalConfiguration.CompanyName = plomBranding.GetValue<string>(new string[] { "Application", "CompanyName" }, "Mike Becker's Software");

					LocalConfiguration.ColorScheme.DarkColor = Color.FromString(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					LocalConfiguration.ColorScheme.LightColor = Color.FromString(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}
			}
			*/
		}

		private Perspective.PerspectiveCollection mvarPerspectives = new Perspective.PerspectiveCollection();
		public Perspective.PerspectiveCollection Perspectives { get { return mvarPerspectives; } }

		public void ShowProjectSettings(UniversalEditor.ObjectModels.Project.ProjectObjectModel project)
		{
			List<SettingsProvider> list = new List<SettingsProvider>();
			foreach (ProjectType projType in project.ProjectTypes)
			{
				if (projType.SettingsProvider != null)
				{
					for (int i = 0; i < projType.SettingsProvider.SettingsGroups.Count; i++)
					{
						for (int j = 0; j < projType.SettingsProvider.SettingsGroups[i].Settings.Count; j++)
						{
							projType.SettingsProvider.SettingsGroups[i].Settings[j].SetValue(project.GetProjectSetting(projType.SettingsProvider.SettingsGroups[i].Settings[j].ID, projType.SettingsProvider.SettingsGroups[i].Settings[j].DefaultValue));
						}
					}
					list.Add(projType.SettingsProvider);
				}
			}
			if (list.Count > 0)
			{
				SettingsDialog dlg = new SettingsDialog();
				dlg.SettingsProviders.Clear();
				foreach (SettingsProvider sp in list)
				{
					dlg.SettingsProviders.Add(sp);
				}

				dlg.Text = String.Format("{0} Properties", project.Title);
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					// TODO: apply properties to project
					foreach (ProjectType projType in project.ProjectTypes)
					{
						if (projType.SettingsProvider != null)
						{
							for (int i = 0; i < projType.SettingsProvider.SettingsGroups.Count; i++)
							{
								for (int j = 0; j < projType.SettingsProvider.SettingsGroups[i].Settings.Count; j++)
								{
									project.SetProjectSetting(projType.SettingsProvider.SettingsGroups[i].Settings[j].ID, projType.SettingsProvider.SettingsGroups[i].Settings[j].GetValue());
								}
							}
						}
					}
				}
				return;
			}

			Accessors.MemoryAccessor ma = new Accessors.MemoryAccessor(new byte[0], String.Format("{0} Properties", project.Title));
			Document d = new Document(project, null, ma);
			d.Title = String.Format("{0} Properties", project.Title);
			((Application.Instance as UIApplication).CurrentWindow as IHostApplicationWindow).OpenFile(d);
		}
	}
}
