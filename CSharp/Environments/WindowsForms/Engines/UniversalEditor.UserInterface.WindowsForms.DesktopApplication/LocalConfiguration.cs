using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;

namespace UniversalEditor.UserInterface.WindowsForms
{
	public class LocalConfiguration
	{
		private static string mvarApplicationName = "Universal Editor";
		public static string ApplicationName { get { return mvarApplicationName; } set { mvarApplicationName = value; } }

		private static string mvarApplicationShortName = "universal-editor"; // polymolive
		public static string ApplicationShortName { get { return mvarApplicationShortName; } set { mvarApplicationShortName = value; } }

		private static string mvarCompanyName = "Mike Becker's Software";
		public static string CompanyName { get { return mvarCompanyName; } set { mvarCompanyName = value; } }

		private static SplashScreenSettings mvarSplashScreen = new SplashScreenSettings();
		public static SplashScreenSettings SplashScreen { get { return mvarSplashScreen; } }

		private static StartPageSettings mvarStartPage = new StartPageSettings();
		public static StartPageSettings StartPage { get { return mvarStartPage; } }

		private static ColorSchemeSettings mvarColorScheme = new ColorSchemeSettings();
		public static ColorSchemeSettings ColorScheme { get { return mvarColorScheme; } }

		private static bool mvarConfirmExit = false;
		public static bool ConfirmExit { get { return mvarConfirmExit; } set { mvarConfirmExit = value; } }

		private static void LoadPreliminaryConfiguration(ref MarkupObjectModel mom)
		{
			string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			LoadConfigurationFiles(path, ref mom);

			MarkupTagElement tagTitle = (mom.FindElement("UniversalEditor", "Application", "Title") as MarkupTagElement);
			if (tagTitle != null)
			{
				mvarApplicationName = tagTitle.Value;
			}
			MarkupTagElement tagShortTitle = (mom.FindElement("UniversalEditor", "Application", "ShortTitle") as MarkupTagElement);
			if (tagShortTitle != null)
			{
				mvarApplicationShortName = tagShortTitle.Value;
			}
			MarkupTagElement tagCompanyName = (mom.FindElement("UniversalEditor", "Application", "CompanyName") as MarkupTagElement);
			if (tagCompanyName != null)
			{
				mvarCompanyName = tagCompanyName.Value;
			}
		}

		static LocalConfiguration()
		{
			MarkupObjectModel mom = new MarkupObjectModel();

			LoadPreliminaryConfiguration(ref mom);

			List<string> paths = new List<string>();
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				{
					break;
				}
				case PlatformID.Unix:
				{
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), mvarApplicationShortName }));
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mvarApplicationShortName }));
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), mvarApplicationShortName }));
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), mvarCompanyName, mvarApplicationName }));
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), mvarCompanyName, mvarApplicationName }));
					paths.Add(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[] { System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Mike Becker's Software", "PolyMo Live!" }));
					break;
				}
			}

			foreach (string path in paths)
			{
				if (!System.IO.Directory.Exists(path)) continue;
				LoadConfigurationFiles(path, ref mom);
			}
		}

		private static void LoadConfigurationFiles(string path, ref MarkupObjectModel mom)
		{
			string[] xmlfiles = null;
			try
			{
				xmlfiles = System.IO.Directory.GetFiles(path, "*.xml", System.IO.SearchOption.AllDirectories);
			}
			catch
			{
			}
			if (xmlfiles == null) return;

			foreach (string xmlfile in xmlfiles)
			{
				MarkupObjectModel local_mom = new MarkupObjectModel();

				Document doc = new Document(local_mom, new XMLDataFormat(), new FileAccessor(xmlfile));
				if (local_mom.FindElement("UniversalEditor") == null) continue;

				local_mom.CopyTo(mom);
			}
		}

		private static System.Drawing.Icon mvarMainIcon = null;
		public static System.Drawing.Icon MainIcon { get { return mvarMainIcon; } set { mvarMainIcon = value; } }
	}

	public class StartPageSettings
	{
		private bool mvarShowOnStartup = true;
		public bool ShowOnStartup { get { return mvarShowOnStartup; } set { mvarShowOnStartup = value; } }

		private string mvarHeaderImageFileName = @"Images/SplashScreen/header2.png"; // null;
		public string HeaderImageFileName { get { return mvarHeaderImageFileName; } set { mvarHeaderImageFileName = value; } }
	}

	public class ColorSchemeSettings
	{
		private Color mvarDarkColor = Color.FromArgb(30, 30, 30);
		public Color DarkColor { get { return mvarDarkColor; } set { mvarDarkColor = value; } }
		private Color mvarLightColor = Color.FromArgb(51, 51, 51);
		public Color LightColor { get { return mvarLightColor; } set { mvarLightColor = value; } }
		private Color mvarBackgroundColor = Color.FromArgb(77, 77, 77);
		public Color BackgroundColor { get { return mvarBackgroundColor; } set { mvarBackgroundColor = value; } }
	}
}
