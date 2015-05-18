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
using UniversalEditor.UserInterface.WindowsForms.Dialogs;

namespace UniversalEditor.UserInterface.WindowsForms
{
	// TODO: Finish implementing ObjectModel Converters... will need to have something
	// like GetAvailableConvertersFrom(Type objectModel) to get a list of available
	// converters that can convert from the specified object model.

	internal class WindowsFormsEngine : Engine
	{
		private static SplashScreenWindow splasher = null;
		
		public static bool SessionLoading = false;

		protected override void AfterInitialization()
		{
			base.AfterInitialization();

			AttachCommandEventHandler("HelpLicensingAndActivation", delegate(object sender, EventArgs e)
			{
				MessageBox.Show("This product has already been activated.", "Licensing and Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);
			});
		}

		protected override void InitializeBranding()
		{
			base.InitializeBranding();

			// First, attempt to load the branding from Branding.uxt
			string BrandingFileName = BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding.uxt";
			if (System.IO.File.Exists(BrandingFileName))
			{
				FileSystemObjectModel fsom = new FileSystemObjectModel();
				FileAccessor fa = new FileAccessor(BrandingFileName);
				Document.Load(fsom, new UXTDataFormat(), fa, false);

				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenImage = fsom.Files["SplashScreen.png"];
				if (fileSplashScreenImage != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenImage.GetDataAsByteArray());
					LocalConfiguration.SplashScreen.Image = System.Drawing.Image.FromStream(ms);
				}

				UniversalEditor.ObjectModels.FileSystem.File fileSplashScreenSound = fsom.Files["SplashScreen.wav"];
				if (fileSplashScreenSound != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileSplashScreenSound.GetDataAsByteArray());
					LocalConfiguration.SplashScreen.Sound = ms;
				}

				UniversalEditor.ObjectModels.FileSystem.File fileMainIcon = fsom.Files["MainIcon.ico"];
				if (fileMainIcon != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileMainIcon.GetDataAsByteArray());
					LocalConfiguration.MainIcon = new System.Drawing.Icon(ms);
				}

				UniversalEditor.ObjectModels.FileSystem.File fileConfiguration = fsom.Files["Configuration.upl"];
				if (fileConfiguration != null)
				{
					System.IO.MemoryStream ms = new System.IO.MemoryStream(fileConfiguration.GetDataAsByteArray());

					UniversalEditor.ObjectModels.PropertyList.PropertyListObjectModel plomBranding = new ObjectModels.PropertyList.PropertyListObjectModel();
					Document.Load(plomBranding, new UniversalPropertyListDataFormat(), new StreamAccessor(ms), true);

					LocalConfiguration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, String.Empty);

					LocalConfiguration.ColorScheme.DarkColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					LocalConfiguration.ColorScheme.LightColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}

				fa.Close();
			}

			// Now, determine if we should override any branding details with local copies
			if (System.IO.Directory.Exists(BasePath + System.IO.Path.DirectorySeparatorChar.ToString() + "Branding"))
			{
				string SplashScreenImageFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"SplashScreen.png"
				});
				if (System.IO.File.Exists(SplashScreenImageFileName)) LocalConfiguration.SplashScreen.ImageFileName = SplashScreenImageFileName;

				string SplashScreenSoundFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"SplashScreen.wav"
				});
				if (System.IO.File.Exists(SplashScreenSoundFileName)) LocalConfiguration.SplashScreen.SoundFileName = SplashScreenSoundFileName;

				string MainIconFileName = String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
				{
					BasePath,
					"Branding",
					"MainIcon.ico"
				});
				if (System.IO.File.Exists(MainIconFileName)) LocalConfiguration.MainIcon = System.Drawing.Icon.ExtractAssociatedIcon(MainIconFileName);

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

					LocalConfiguration.ApplicationName = plomBranding.GetValue<string>(new string[] { "Application", "Title" }, String.Empty);

					LocalConfiguration.ColorScheme.DarkColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "DarkColor" }, "#2A0068"));
					LocalConfiguration.ColorScheme.LightColor = ParseColor(plomBranding.GetValue<string>(new string[] { "ColorScheme", "LightColor" }, "#C0C0FF"));
				}
			}
		}

		protected override void ShowSplashScreen()
		{
			if (LocalConfiguration.SplashScreen.Enabled)
			{
				splasher = new SplashScreenWindow();
				splasher.ShowDialog();
			}
		}
		protected override void HideSplashScreen()
		{
			if (LocalConfiguration.SplashScreen.Enabled)
			{
				while (splasher == null)
				{
					System.Threading.Thread.Sleep(500);
				}
				splasher.InvokeClose();
			}
		}
		protected override void UpdateSplashScreenStatus(string message, int progressValue = -1, int progressMinimum = 0, int progressMaximum = 100)
		{
			if (LocalConfiguration.SplashScreen.Enabled)
			{
				int spins = 0, maxspins = 30;
				if (splasher == null) return;

				while (splasher == null)
				{
					System.Threading.Thread.Sleep(500);
					if (spins == maxspins) return;
					spins++;
				}
				splasher.InvokeUpdateStatus(message);
			}
		}

		protected override void BeforeInitialization()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			// TODO: figure out why this is being done on BeforeInitialization and whether we could move it to after
			//       the configuration is initialized, so we can specify the user's favorite theme in a configuration file

			AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme theme = new AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme(AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme.ColorMode.Dark);
			theme.UseAllCapsMenus = false;
			theme.SetStatusBarState(AwesomeControls.Theming.BuiltinThemes.VisualStudio2012Theme.StatusBarState.Initial);
			// AwesomeControls.Theming.BuiltinThemes.Office2003Theme theme = new AwesomeControls.Theming.BuiltinThemes.Office2003Theme();
			// AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme theme = new AwesomeControls.Theming.BuiltinThemes.OfficeXPTheme();
			// AwesomeControls.Theming.BuiltinThemes.SlickTheme theme = new AwesomeControls.Theming.BuiltinThemes.SlickTheme();
			AwesomeControls.Theming.Theme.CurrentTheme = theme;

			Glue.ApplicationInformation.ApplicationID = new Guid("{b359fe9a-080a-43fc-ae38-00ba7ac1703e}");

			base.BeforeInitialization();
		}

		[STAThreadAttribute()]
		protected override void MainLoop()
		{
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

			Glue.ApplicationEventEventArgs e = new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStart);
			Glue.Common.Methods.SendApplicationEvent(e);
			if (e.CancelApplication) return;

			PieMenuManager.Title = "Universal Editor pre-alpha build";

			PieMenuItemGroup row1 = new PieMenuItemGroup();
			row1.Title = "Tools";
			row1.Items.Add("atlMFCTraceTool", "ATL/MFC Trace Tool");
			row1.Items.Add("textEditor", "Text Editor");
			PieMenuManager.Groups.Add(row1);

#if !DEBUG
			Application.ThreadException += Application_ThreadException;
#endif

			Application.Run();

			Glue.Common.Methods.SendApplicationEvent(new Glue.ApplicationEventEventArgs(Glue.Common.Constants.EventNames.ApplicationStop));
		}

		private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			ShowCrashDialog(e.Exception);
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

		public override void ShowAboutDialog()
		{
			AboutDialog dlg = new AboutDialog();
			dlg.ShowDialog();
		}
		public override void ShowAboutDialog(DataFormatReference dfr)
		{
			DataFormatAboutDialog dlg = new DataFormatAboutDialog();
			dlg.DataFormatReference = dfr;
			dlg.ShowDialog();
		}

		protected override IHostApplicationWindow OpenWindowInternal(params Document[] documents)
		{
			MainWindow mw = new MainWindow();

			if (documents.Length > 0)
			{
				mw.OpenFile(documents);
			}
			mw.Show();
			return mw;
		}

		protected override void RestartApplicationInternal()
		{
			Application.Restart();
		}
		protected override void StopApplicationInternal()
		{
			Application.Exit();
		}

		public override bool ShowCustomOptionDialog(ref CustomOption.CustomOptionCollection customOptions, string title = null, EventHandler aboutButtonClicked = null)
		{
			if (CustomOptionDialog.ShowDialog(ref customOptions, title, aboutButtonClicked) == DialogResult.Cancel)
			{
				return false;
			}
			return true;
		}

		protected override void ShowCrashDialog(Exception ex)
		{
			Dialogs.CrashDialog dlg = new Dialogs.CrashDialog();
			dlg.Exception = ex;
			dlg.ShowDialog();
		}
	}
}
