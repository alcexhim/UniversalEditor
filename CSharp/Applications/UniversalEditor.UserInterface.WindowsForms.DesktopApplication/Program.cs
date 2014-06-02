using AwesomeControls.PieMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.FileSystem.UXT;
using UniversalEditor.DataFormats.PropertyList.UniversalPropertyList;
using UniversalEditor.ObjectModels.FileSystem;

namespace UniversalEditor.UserInterface.WindowsForms
{
	// TODO: Finish implementing ObjectModel Converters... will need to have something
	// like GetAvailableConvertersFrom(Type objectModel) to get a list of available
	// converters that can convert from the specified object model.

	static class Program
	{
		private static readonly string INSTANCEID = "UniversalEditor.UserInterface.WindowsForms.DesktopApplication$2d429aa3371c421fb63b42525e51a50c$92751853175891031214292357218181357901238$" + System.Reflection.Assembly.GetEntryAssembly().Location;

		internal static MainWindow LastWindow = null;

		private static List<MainWindow> mvarWindows = new List<MainWindow>();
		public static List<MainWindow> Windows { get { return mvarWindows; } }

		private static SplashScreenWindow splasher = null;
		
		public static bool SessionLoading = false;

		// UniversalDataStorage.Editor.WindowsForms.Program
		private static void SingleInstanceManager_Callback(object sender, SingleInstanceManager.InstanceCallbackEventArgs e)
		{
			if (!e.IsFirstInstance)
			{
				if (Program.LastWindow != null)
				{
					Program.LastWindow.Invoke(new Action(delegate()
					{
						for (int i = 1; i < e.CommandLineArgs.Length; i++)
						{
							LastWindow.OpenFile(e.CommandLineArgs[i].ToString());
						}
						Program.LastWindow.Activate();
						Program.LastWindow.BringToFront();
					}));
				}
			}
		}


		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThreadAttribute()]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme theme = new AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme(AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme.ColorMode.Dark);
			theme.UseAllCapsMenus = false;
			// AwesomeControls.Theming.BuiltinThemes.Office2003Theme theme = new AwesomeControls.Theming.BuiltinThemes.Office2003Theme();
			// AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme theme = new AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme();
			// AwesomeControls.Theming.BuiltinThemes.SlickTheme theme = new AwesomeControls.Theming.BuiltinThemes.SlickTheme();
			AwesomeControls.Theming.Theme.CurrentTheme = theme;

			Branding_Initialize();

			if (!SingleInstanceManager.CreateSingleInstance(INSTANCEID, new EventHandler<SingleInstanceManager.InstanceCallbackEventArgs>(Program.SingleInstanceManager_Callback))) return;

			Glue.ApplicationInformation.ApplicationID = new Guid("{b359fe9a-080a-43fc-ae38-00ba7ac1703e}");
			switch (System.Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					Glue.ApplicationInformation.ApplicationDataPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { "universal-editor", "plugins" });
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				case PlatformID.Xbox:
				{
					Glue.ApplicationInformation.ApplicationDataPath = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { "Universal Editor", "Plugins" });
					break;
				}
			}

			RecentFileManager.DataFileName = Configuration.DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "RecentItems.xml";
			BookmarksManager.DataFileName = Configuration.DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Bookmarks.xml";
			SessionManager.DataFileName = Configuration.DataPath + System.IO.Path.DirectorySeparatorChar.ToString() + "Sessions.xml";

			RecentFileManager.Load();
			BookmarksManager.Load();
			SessionManager.Load();

			System.Threading.Thread threadLoader = new System.Threading.Thread(threadLoader_ThreadStart);
			threadLoader.Start();

			if (Configuration.SplashScreen.Enabled)
			{
				splasher = new SplashScreenWindow();
				splasher.ShowDialog();
			}

			Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStart);
			Glue.Common.Methods.SendApplicationEvent(e);
			if (e.CancelApplication) return;

			PieMenuManager.Title = "Universal Editor pre-alpha build";

			PieMenuItemGroup row1 = new PieMenuItemGroup();
			row1.Title = "Tools";
			row1.Items.Add("atlMFCTraceTool", "ATL/MFC Trace Tool");
			row1.Items.Add("textEditor", "Text Editor");
			PieMenuManager.Groups.Add(row1);

			OpenWindow(args);

			Application.Run();

			SessionManager.Save();
			BookmarksManager.Save();
			RecentFileManager.Save();

			Glue.Common.Methods.SendApplicationEvent(new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStop));
		}

		private static void Branding_Initialize()
		{
			string BasePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			if (System.IO.Directory.Exists(BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding"))
			{	
				string SplashScreenImageFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"SplashScreen.png"
				});
				if (System.IO.File.Exists(SplashScreenImageFileName)) Configuration.SplashScreen.ImageFileName = SplashScreenImageFileName;

				string SplashScreenSoundFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"SplashScreen.wav"
				});
				if (System.IO.File.Exists(SplashScreenSoundFileName)) Configuration.SplashScreen.SoundFileName = SplashScreenSoundFileName;

				string MainIconFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"MainIcon.ico"
				});
				if (System.IO.File.Exists(MainIconFileName)) Configuration.MainIcon = System.Drawing.Icon.ExtractAssociatedIcon(MainIconFileName);

				string ConfigurationFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"Configuration.upl"
				});
				if (System.IO.File.Exists(ConfigurationFileName))
				{
					UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel plomBranding = new ObjectModels.PropertyList.PropertyListObjectModel();
					Document.Load(plomBranding, new UniversalPropertyListDataFormat(), new FileAccessor(ConfigurationFileName), true);

					Configuration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, String.Empty);

					Configuration.ColorScheme.DarkColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					Configuration.ColorScheme.LightColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}
			}
			else if (System.IO.File.Exists(BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding.uxt"))
			{
				string BrandingFileName = BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding.uxt";
				FileSystemObjectModel fsom = new FileSystemObjectModel();
				FileAccessor fa = new FileAccessor(BrandingFileName);
				Document.Load(fsom, new UXTDataFormat(), fa, false);
				
				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenImage = fsom.Files["SplashScreen.png"];
				if (fileSplashScreenImage != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenImage.GetDataAsByteArray());
					Configuration.SplashScreen.Image = System.Drawing.Image.FromStream(ms);
				}

				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenSound = fsom.Files["SplashScreen.wav"];
				if (fileSplashScreenSound != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenSound.GetDataAsByteArray());
					Configuration.SplashScreen.Sound = ms;
				}

				UniversalEditor.ObjectModels.FileSystem.File fileMainIcon = fsom.Files["MainIcon.ico"];
				if (fileMainIcon != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileMainIcon.GetDataAsByteArray());
					Configuration.MainIcon = new System.Drawing.Icon(ms);
				}

				UniversalEditor.ObjectModels.FileSystem.File fileConfiguration = fsom.Files["Configuration.upl"];
				if (fileConfiguration != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileConfiguration.GetDataAsByteArray());
					
					UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel plomBranding = new ObjectModels.PropertyList.PropertyListObjectModel();
					Document.Load(plomBranding, new UniversalPropertyListDataFormat(), new StreamAccessor(ms), true);
					
					Configuration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, String.Empty);

					Configuration.ColorScheme.DarkColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					Configuration.ColorScheme.LightColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}

				fa.Close();
			}
		}

		private static System.Drawing.Color ParseColor(string htmlCode)
		{
			int iR = 0, iG = 0, iB = 0;
			if (htmlCode.StartsWith("#") && htmlCode.Length == 7)
			{
				string sR = htmlCode.Substring(1, 2);
				string sG = htmlCode.Substring(3, 2);
				string sB = htmlCode.Substring(5, 2);

				iR = Int32.Parse(sR, System.Globalization.NumberStyles.HexNumber);
				iG = Int32.Parse(sG, System.Globalization.NumberStyles.HexNumber);
				iB = Int32.Parse(sB, System.Globalization.NumberStyles.HexNumber);
			}
			else if (htmlCode.StartsWith("rgb(") && htmlCode.EndsWith(")") && htmlCode.Contains(","))
			{
				htmlCode = htmlCode.Substring(4, htmlCode.Length - 5);
				string[] s = htmlCode.Split(new char[] { ',' });
				if (s.Length != 3) return System.Drawing.Color.Empty;

				iR = Int32.Parse(s[0]);
				iG = Int32.Parse(s[1]);
				iB = Int32.Parse(s[2]);
			}
			return System.Drawing.Color.FromArgb(iR, iG, iB);
		}

#if INCLUDE_TESTS

		private static void Test_TextDocument()
		{
			UniversalEditor.ObjectModels.Text.TextObjectModel text = new UniversalEditor.ObjectModels.Text.TextObjectModel();
			UniversalEditor.DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(text.MakeReference());
			UniversalEditor.DataFormatReference dfr = dfrs[0];

			DataFormat df = dfr.Create();

			df.Open(@"W:\Libraries\UniversalEditor\bin\Debug\Tests\Text\Document.xps");
			df.Load<UniversalEditor.ObjectModels.Text.TextObjectModel>(ref text);
			df.Close();

			df.EnableWrite = true;
			df.ForceOverwrite = true;
			df.Open(@"W:\Libraries\UniversalEditor\bin\Debug\Tests\Text\Document2.xps");
			df.Save(text);
			df.Close();
		}
		private static void Test_InstallShieldScript()
		{
			string InputFileName = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Installers\InstallShield Legacy\SETUP.INS";
			UniversalEditor.ObjectModels.InstallShield.InstallShieldScriptObjectModel ins = UniversalEditor.Common.Reflection.GetAvailableObjectModel<UniversalEditor.ObjectModels.InstallShield.InstallShieldScriptObjectModel>(InputFileName);


		}
		private static void Test_Executable()
		{
			string InputFileName = @"version\syscor32.dll";
			string OutputFileName = @"version\syscoree.dll";
			// InputFileName = @"Tests\IMGSTART\imgstart.exe";
			// OutputFileName = @"Tests\IMGSTART\udnova.exe";

			UniversalEditor.ObjectModels.Executable.ExecutableObjectModel exe = new ObjectModels.Executable.ExecutableObjectModel();
			UniversalEditor.DataFormats.Executable.Microsoft.MicrosoftExecutableDataFormat exedf = new DataFormats.Executable.Microsoft.MicrosoftExecutableDataFormat();

			/*
			exedf.Open(InputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();
			exe.Sections[".rsrc"].Save(@"version\syscor32.ver");
			
			exe = new UniversalEditor.ObjectModels.Executable.ExecutableObjectModel();
			exedf.Open(OutputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();
			exe.Sections[".rsrc"].Save(@"version\syscoree.ver");
			exe = new UniversalEditor.ObjectModels.Executable.ExecutableObjectModel();
			*/

			exedf.Open(InputFileName);
			exedf.Load<UniversalEditor.ObjectModels.Executable.ExecutableObjectModel>(ref exe);
			exedf.Close();

			exedf.EnableWrite = true;
			exedf.ForceOverwrite = true;

			UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock version = (exe.Resources[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock);
			UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceStringFileInfoBlock sfi = (version.ChildBlocks[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceStringFileInfoBlock);
			sfi.Entries[0].StringTableEntries[1].Value = "Hello";

			exedf.Open(OutputFileName);
			// (exe.Resources[0] as UniversalEditor.ObjectModels.Executable.ResourceBlocks.VersionResourceBlock).ChildBlocks[0].Add("Website", "http://www.google.com/");
			exedf.Save(exe);
			exedf.Close();
		}
		private static void Test_RenPyArchive()
		{
			/*
			object nidl = (new UniversalEditor.Serializers.PickleSerializer()).Deserialize("cos\nsystem\n(S'ls ~'\ntR.");

			UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel fsom = new ObjectModels.FileSystem.FileSystemObjectModel();
			UniversalEditor.DataFormats.RenPy.Archive.RenPyArchiveDataFormat rpa = new DataFormats.RenPy.Archive.RenPyArchiveDataFormat();

			ObjectModel om = fsom;
			rpa.Open(@"C:\Program Files (x86)\Katawa Shoujo\game\data.rpa");
			rpa.Load(ref om);
			rpa.Close();
			*/
			return;
		}
		private static void Test_ARJ()
		{
			string FileName = @"C:\ARJ\test1.arj";
			string FileName2 = @"C:\ARJ\test1_aaaa.arj";

			UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel fsom = new ObjectModels.FileSystem.FileSystemObjectModel();
			UniversalEditor.DataFormats.FileSystem.ARJ.ARJDataFormat arj = new DataFormats.FileSystem.ARJ.ARJDataFormat();
			arj.Open(FileName);
			arj.Load<UniversalEditor.ObjectModels.FileSystem.FileSystemObjectModel>(ref fsom);
			arj.Close();

			arj.EnableWrite = true;

			arj.Open(FileName2);
			arj.Save(fsom);
			arj.Close();
		}
		private static void Test_Outsim()
		{
			ObjectModelReference omrHOM = UniversalEditor.Common.Reflection.GetObjectModelByTypeName("UniversalEditor.ObjectModels.Outsim.SynthMaker.Module.ModuleObjectModel");
			ObjectModel omHOM = omrHOM.Create();

			DataFormatReference[] dfrs = UniversalEditor.Common.Reflection.GetAvailableDataFormats(omrHOM);
			DataFormat dfHOM = dfrs[0].Create();

			dfHOM.Open(@"C:\Program Files (x86)\Outsim\SynthMaker\modules\System\ADSR.hom");
			dfHOM.Load(ref omHOM);
			dfHOM.Close();
		}
		private static void Test_WinHelpBookCollection()
		{
			string FileName = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Book Collection\WinHelp\ACCESS.HLP";
			string FileName2 = @"W:\Libraries\UniversalEditor\bin\Debug\Tests\Book Collection\WinHelp\ACCESS_aaaa.HLP";

			BookCollectionObjectModel books = new BookCollectionObjectModel();
			UniversalEditor.DataFormats.BookCollection.WinHelp.WinHelpDataFormat winhlp = new DataFormats.BookCollection.WinHelp.WinHelpDataFormat();
			winhlp.Open(FileName);
			winhlp.Load<BookCollectionObjectModel>(ref books);
			winhlp.Close();

			winhlp.EnableWrite = true;
			winhlp.Open(FileName2);
			winhlp.Save(books);
			winhlp.Close();
		}
#endif

		private static void threadLoader_ThreadStart()
		{
			/*
			if (Configuration.SplashScreen.Enabled)
			{
				while (splasher == null) System.Threading.Thread.Sleep(500);
			}
			*/

			// if (Configuration.SplashScreen.Enabled) splasher.InvokeUpdateStatus("Loading object models...");
			UniversalEditor.Common.Reflection.GetAvailableObjectModels();

			// if (Configuration.SplashScreen.Enabled) splasher.InvokeUpdateStatus("Loading data formats...");
			UniversalEditor.Common.Reflection.GetAvailableDataFormats();

			if (Configuration.SplashScreen.Enabled)
			{
				while (splasher == null)
				{
					System.Threading.Thread.Sleep(500);
				}
				splasher.InvokeClose();
			}
		}

		public static void OpenFile(params string[] FileNames)
		{
			LastWindow.OpenFile(FileNames);
		}
		public static void OpenWindow(params string[] FileNames)
		{
			MainWindow mw = new MainWindow();

			if (FileNames.Length > 0)
			{
				mw.OpenFile(FileNames);
			}
			mw.Show();

			mvarWindows.Add(mw);
		}

		public static void ExitApplication()
		{
			if (Configuration.ConfirmExit)
			{
				if (MessageBox.Show("Are you sure you wish to quit " + Configuration.ApplicationName + "?", "Quit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
				{
					return;
				}
			}
			Application.Exit();
		}

		public static void CloseAllWindows()
		{
			List<MainWindow> windowsToClose = new List<MainWindow>();
			foreach (MainWindow window in mvarWindows)
			{
				windowsToClose.Add(window);
			}
			foreach (MainWindow window in windowsToClose)
			{
				window.Close();
			}
		}
	}
}
