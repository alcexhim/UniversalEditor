using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using UniversalEditor.ObjectModels.Markup;
using UniversalEditor.DataFormats.Markup.XML;
using UniversalEditor.Accessors;
using UniversalEditor.UserInterface;

namespace UniversalEditor.Engines.WindowsForms
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
